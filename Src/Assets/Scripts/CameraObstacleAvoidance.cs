// TODO: suspension system;
using UnityEngine;
using System.Collections;

public class CameraObstacleAvoidance : MonoBehaviour {
    #region CONSTANTS
    private const int UNBOUNDED = 0;
    private const int FIXED = 1;
    private const int RETURNING = 2;
    #endregion

    #region PUBLIC PROPERTIES
    public float ReturningTime = 1;
    public float ToleranceToSurface = 1;
    #endregion

    #region PRIVATE PROPERTIES
    private float _timeOnState = 0;
    private int _currentState = 0;
    private GameObject _camera;
    private CameraController _controller;
    #endregion

    #region CACHED PROPERTIES
    // cached once at start    
    private Vector3 _originalLocalPosition;
    // cached on state switch
    private Vector3 _oldPosition;
    // cached smartly by the current state behaviour
    private Vector3 _desiredPosition;
    #endregion


    #region UNITY METHODS
    public void Start () {
        _camera = GameObject.FindGameObjectWithTag(Tag.MainCamera);
        _controller = GetComponent<CameraController>();

        _originalLocalPosition = transform.InverseTransformPoint(_camera.transform.position);
    }
    
    public void Update () {
        _timeOnState += Time.deltaTime;

        switch (_currentState) {
            case UNBOUNDED:
                _UnboundedBehaviour();
                break;
            case FIXED:
                _FixedBehaviour();
                break;
            case RETURNING:
                _ReturningBehaviour();
                break;
        }
    }
    #endregion


    #region PRIVATE METHODS
    public void _UnboundedBehaviour () {
        // exit conditions...
        RaycastHit hit;
        if (IsCurrentlyBlocked(out hit)) {
            _desiredPosition = _GetSafeHitPoint(hit);
            _SwitchState(FIXED);
        }
    }

    public void _FixedBehaviour () {
        // behaviour...
        SetCameraPosition(_desiredPosition);

        // exit conditions...
        RaycastHit hit;
        if (false == IsOriginalBlocked(out hit)) {
            _SwitchState(RETURNING);
        } else if (IsCurrentlyBlocked(out hit)) {
            _desiredPosition = _GetSafeHitPoint(hit);
            _SwitchState(FIXED);
        }
    }

    public void _ReturningBehaviour () {
        // behaviour...
        float factor = _timeOnState/ReturningTime;
        SetCameraPosition(Vector3.Lerp(_oldPosition, transform.TransformPoint(_originalLocalPosition), Mathf.SmoothStep(0,1, factor)));

        // exit conditions;
        if (factor >=1) {
            _SwitchState(UNBOUNDED);
        }
    }

    private void _SwitchState (int state) {
        // caching
        _oldPosition = _camera.transform.position;

        // reseting
        _timeOnState = 0;
        _currentState = state;
    }

    private Vector3 _GetSafeHitPoint (RaycastHit hit) {
        return hit.point + hit.normal * ToleranceToSurface;
    }
    #endregion


    #region PUBLIC METHODS
    public bool IsPointBlocked (Vector3 point, out RaycastHit hit) {
        Vector3 difference = point - _controller.Target.transform.position;

        return Physics.Raycast(_controller.Target.transform.position + new Vector3(0, 0.5f, 0), difference, out hit, difference.magnitude);
    }

    public bool IsOriginalBlocked (out RaycastHit hit) {
        return IsPointBlocked(transform.TransformPoint(_originalLocalPosition), out hit);
    }

    public bool IsCurrentlyBlocked (out RaycastHit hit) {
        return IsPointBlocked(_camera.transform.position, out hit);
    }

    public void SetCameraPosition (Vector3 point) {
        _camera.transform.position = point;
        GetComponent<CameraFocus>().LookAtTarget();
    }
    #endregion
}
