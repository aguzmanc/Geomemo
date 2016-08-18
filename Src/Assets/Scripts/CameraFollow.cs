using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    #region CONSTANTS
    public const int NO_MOVEMENT = 0;
    public const int FOLLOWING = 1;
    public const int CHASING = 2;
    #endregion

    #region PUBLIC PROPERTIES
    public float ChasingTime = 2;
    public float IdleSwitchTime = 0.5f;
    #endregion

    #region PRIVATE PROPERTIES
    private int _currentState;
    private float _timeOnState;
    #endregion

    #region CACHED PROPERTIES
    // caching frequency depends on the state dynamics
    private Vector3 _myPosition;

    // cached once at start
    private CameraController _controller;
    #endregion

    void Start ()
    {
        _controller = GetComponent<CameraController>();
    }

    void Update ()
    {
        _timeOnState += Time.deltaTime;

        switch (_currentState) {
            case NO_MOVEMENT:
                _NoMovementBehavior();
                break;
            case CHASING:
                _ChaseBehavior();
                break;
            case FOLLOWING:
                _FollowBehavior();
                break;
        }
    }

    #region PUBLIC METHODS
    public bool IsFollowing () {
        return _currentState == FOLLOWING;
    }



    public bool IsChasing () {
        return _currentState == CHASING;
    }



    public bool IsMoving () {
        return _currentState != NO_MOVEMENT;
    } 
    #endregion

    #region PRIVATE METHODS
    // BEHAVIOR and BEHAVIOUR are both well spelled.
    private void _ChaseBehavior ()
    {
        float factor = _timeOnState/ChasingTime;
        _SetPosition(Vector3.Lerp(_myPosition,
                                  _controller.Target.transform.position,
                                  Mathf.SmoothStep(0,1, factor)));
        if (factor >= 1) {
            _SwitchState(FOLLOWING);
        }
    }



    private void  _NoMovementBehavior ()
    {
        if (false == _controller.IsTargetInsideCircle() ||
            (_controller.TargetsControl.MainStreetWalking &&
             _controller.Distance > _controller.Radius * 0.5f)) {
            _SwitchState(CHASING);
        }
    }



    private void _FollowBehavior ()
    {
        _SetPosition(_controller.Target.transform.position);
        if (_controller.TargetsControl.IdleTime > IdleSwitchTime) {
            _SwitchState(NO_MOVEMENT);
        }
    }



    private void _SwitchState (int newState)
    {
        _timeOnState = 0;
        _myPosition = transform.position;
        _currentState = newState;
    }

    private void _SetPosition (Vector3 newPosition) {
        transform.position = newPosition;
    }
    #endregion

    #region PUBLIC METHODS
    public void Chase () {
        _SwitchState(CHASING);
    }
    #endregion
}
