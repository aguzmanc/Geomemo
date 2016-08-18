using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    #region PUBLIC PROPERTIES
    public float Radius;
    public GameObject Target;
    #endregion

    #region CACHED PROPERTIES
    public float Distance { get { return _distance; } }
    public ThirdPersonControl TargetsControl { get { return _targetsControl; } }
    public Vector3 ForwardXforward { get { return _forwardXforward; } }
    #endregion

    #region PRIVATE PROPERTIES
    private float _distance;
    private ThirdPersonControl _targetsControl;
    private Vector3 _forwardXforward;
    #endregion

    void Start () {
        if (Target == null) {
            Target = GameObject.FindGameObjectWithTag(Tag.Player);
        }

        _targetsControl = Target.GetComponent<ThirdPersonControl>();
    }

    void Update () {
        _forwardXforward = Vector3.Cross(transform.forward, Target.transform.forward);
        _distance = Vector3.Distance(transform.position, Target.transform.position);
    }



    #region PUBLIC METHODS
    public bool IsTargetInsideCircle ()
    {
        return Vector3.Distance(transform.position, Target.transform.position) < Radius;
    }



    public bool IsTargetLookingBehind ()
    {
        return Mathf.Abs(_forwardXforward.y) < 0.05f;
    }
    #endregion
}
