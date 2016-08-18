using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour {
    #region CONSTANTS
    public const int NO_ROTATION = 0;
    public const int REFOCUS = 1;
    public const int FOLLOWING = 2;
    #endregion

    #region PUBLIC PROPERTIES
    public float RefocusSpeed = 35;
    // higher values might cause the camera to glitch while following behind
    public float FollowingRotationSpeed = 20;
    public float IdleSwitchTime = 2;
    #endregion

    #region CACHED PROPERTIES
    private Vector3 _myForward;
    private Vector3 _targetsForward;
    // cached once at start
    private CameraController _controller;
    private GameObject _camera;
    #endregion CACHED PROPERTIES

    #region PRIVATE PROPERTIES
    private float _timeOnState;
    private int _currentState;
    #endregion

    void Start ()
    {
        _controller = GetComponent<CameraController>();
        _camera = GameObject.FindGameObjectWithTag(Tag.MainCamera);
    }

    void Update ()
    {
        _timeOnState += Time.deltaTime;

        switch (_currentState) {
            case REFOCUS:
                _RefocusBehavior();
                break;
            case NO_ROTATION:
                _NoRotationBehavior();
                break;
            case FOLLOWING:
                _FollowBehavior();
                break;
        }

        LookAtTarget();
    }



    #region PRIVATE METHODS
    private void _RefocusBehavior ()
    {
        // time = distance / velocity
        // hacking slerp to work with velocity instead of time.
        float time = (Mathf.PI * Vector3.Angle(_myForward, _targetsForward)/(Mathf.PI * 2)) / _GetRefocusSpeed();
        float factor = _timeOnState / time;

        if (factor >= 1 || _controller.TargetsControl.IdleTime < 0.1f) {
            _SwitchState(NO_ROTATION);
        } else {
            _SetForward(Vector3.Slerp(_myForward, _targetsForward,
                                      Mathf.SmoothStep(0,1, factor)));
        }

    }



    /*
     * it is dangerous to use slerp here! we need the camera to rotate at a given speed
     * slerp will not let us control that speed; slerp will only let us control the time.
     * using slerp here, might cause the camera to glitch.
     */
    private void _FollowBehavior ()
    {
        if (_controller.TargetsControl.MainStreetWalking != null) {
            _SwitchState(REFOCUS);
            return;
        } 

        float deltaRotation = FollowingRotationSpeed * Time.deltaTime;

        // won't rotate if the target is inside a "tolerance angle" range.
        // won't rotate if the target is looking behind.
        if (Vector3.Angle(transform.forward, _controller.Target.transform.forward) > FollowingRotationSpeed
            && false == _controller.IsTargetLookingBehind()) {
            // where should the speed of the camera point to? left or right?
            if (_controller.ForwardXforward.y < 0) {
                deltaRotation *= -1;
            }
            // rotate!
            _SetForward(Quaternion.Euler(0, deltaRotation, 0) * transform.forward);
        }

        if (_controller.TargetsControl.IdleTime > 0 ||
            !GetComponent<CameraFollow>().IsMoving()) {
            _SwitchState(REFOCUS);
        }
    }



    private float _GetRefocusSpeed () {
        if (_controller.TargetsControl.MainStreetWalking) {
            return RefocusSpeed * 3;
        } else {
            return RefocusSpeed;
        }
    }




    private void _NoRotationBehavior ()
    {
        if (_controller.TargetsControl.IdleTime > IdleSwitchTime ||
            _controller.TargetsControl.MainStreetWalking != null) {
            _SwitchState(REFOCUS);
        }

        if (GetComponent<CameraFollow>().IsMoving() &&
            _controller.TargetsControl.MainStreetWalking == null) {
            _SwitchState(FOLLOWING);
        }
    }



    private void _SwitchState (int newState)
    {
        _timeOnState = 0;
        _myForward = transform.forward;
        if (_controller.TargetsControl.MainStreetWalking != null) {
            _targetsForward = _controller.TargetsControl.MainStreetWalking.transform.forward;
            float dot = Vector3.Dot(_controller.Target.transform.forward, _targetsForward);
            if (Mathf.Abs(dot) > 0.5f) {
                _targetsForward = Mathf.Sign(dot) * _targetsForward;
            }
        } else {
            _targetsForward = _controller.Target.transform.forward;
        }
        _currentState = newState;
    }



    private void _SetForward (Vector3 r) {
        // OK, that's the new forward...
        transform.forward = new Vector3(r.x, transform.forward.y, r.z);
        // but do not touch the pitch!
        Vector3 e = transform.rotation.eulerAngles;
        transform.rotation =
            Quaternion.Euler(0, e.y, 0);
    }
    #endregion

    #region PUBLIC METHODS
    public void Refocus () {
        _SwitchState(REFOCUS);
    }

    public void LookAtTarget () {
        _camera.transform.forward =
            (_controller.Target.transform.position - _camera.transform.position);
    }
    #endregion
}
