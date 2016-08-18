using UnityEngine;
using System.Collections;

public class CameraStreet : MonoBehaviour {

    #region STATE CONSTANTS
    // private const int 
    #endregion

    #region PRIVATE PROPERTIES
    private CameraController _controller;
    #endregion

    void Start ()
    {
        _controller = GetComponent<CameraController>();
    }




    void Update ()
    {

    }
}
