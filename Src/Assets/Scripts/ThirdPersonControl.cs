using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThirdPersonControl : MonoBehaviour {
    #region CONSTANTS
    public const int WALKING = 0;
    public const int DRIVING = 1;
    #endregion

    #region PUBLIC EDITOR PROPERTIES
    public float Speed = 10;
    public float IdleTime;
    #endregion

    #region PUBLIC PROPERTIES
    [HideInInspector]
    public List<GameObject> StreetsWalking;
    [HideInInspector]
    public GameObject MainStreetWalking;
    #endregion

    #region PRIVATE PROPERTIES
    private Camera _mainCamera;
    private Rigidbody _body;
    #endregion

    #region UNITY METHODS
    void Start ()
    {
        _mainCamera = GameObject.FindGameObjectWithTag(Tag.MainCamera)
            .GetComponent<Camera>();
        _body = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        TemporalMovementControl();
    }
    
    void OnTriggerEnter (Collider c)
    {
        if (c.gameObject.CompareTag(Tag.Street) && !c.gameObject.GetComponent<Street>().BeingWalked) {
            // the flag prevents the algorithm from happening several times
            // before the OnTriggerExit is triggered
            c.gameObject.GetComponent<Street>().BeingWalked = true;
            StreetsWalking.Add(c.gameObject);
        }

        GameObject lastStreet = MainStreetWalking;
        UpdateMainStreet();
        if (lastStreet == null  && MainStreetWalking != null) {
            // TODO: broadcast that you are now walking a street
            _mainCamera.transform.parent.gameObject
                .GetComponent<CameraFocus>().Refocus();
        }
    }

    void OnTriggerExit (Collider c)
    {
        if (c.gameObject.CompareTag(Tag.Street)) {
            int i;
            for (i=0; i<StreetsWalking.Count; i++) {
                if (StreetsWalking[i] == c.gameObject) {
                    break;
                }
            }

            if (i < StreetsWalking.Count) { // if gameobject found on list
                StreetsWalking[i].GetComponent<Street>().BeingWalked = false;
                StreetsWalking.RemoveAt(i);
            }
        }

        UpdateMainStreet();
        if (MainStreetWalking == null) {
            // TODO: broadcast that you have just left all the streets.
            _mainCamera.transform.parent.GetComponent<CameraFocus>().Refocus();
            _mainCamera.transform.parent.GetComponent<CameraFollow>().Chase();
        }
    }
    #endregion


    #region UNCOMPLETE METHODS
    // TODO: make this work correctly...
    public void TemporalMovementControl ()
    {
        Vector3 velocity = _mainCamera.transform.forward * Input.GetAxis("Vertical")
            + _mainCamera.transform.right * Input.GetAxis("Horizontal");
        velocity = new Vector3(velocity.x, 0, velocity.z).normalized * Speed;
        _body.velocity = velocity + new Vector3(0, _body.velocity.y, 0);

        if (velocity.magnitude > 0.1f) {
            transform.forward = velocity;
            IdleTime = 0;
        } else {
            IdleTime += Time.deltaTime;
        }
    }

    // TODO: set material's alpha!!
    public void Fade () {
        // Debug.Log("imma fading");
    }

    public void Unfade () {
        // Debug.Log("imma unfading");
    }
    #endregion

    #region PUBLIC METHODS
    /* Makes the street on StreetsWalking with the closest forward or backward angle 
       between the pj's forward, the MainStreet. */
    public void UpdateMainStreet ()
    {
        float foundMinAngle = -1;

        if (StreetsWalking.Count > 0) {
            MainStreetWalking = StreetsWalking[0];
            foundMinAngle = Vector3.Angle(StreetsWalking[0].transform.forward,
                                          transform.forward);

            foreach (GameObject street in StreetsWalking) {
                float angle = Vector3.Angle(street.transform.forward, transform.forward);

                if (angle < foundMinAngle) {
                    foundMinAngle = angle;
                    MainStreetWalking = street;
                }
            }

        } else {
            MainStreetWalking = null;
        }
    }
    #endregion
}
