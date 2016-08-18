using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraOnEditor : MonoBehaviour {
    private CameraController _thirdPersonCamera ;
    private GameObject _radiusIndicator;

    public float DistanceToGround = 0.1f;
    public float PointsInCircle = 20;

    void Update () {
        _thirdPersonCamera = GetComponent<CameraController>();
        _radiusIndicator = transform.FindChild("Radius").gameObject;

        _thirdPersonCamera.Radius =
            Vector3.Distance(_radiusIndicator.transform.position,
                             transform.position);
    }

    void OnDrawGizmos () {
        DrawCircle();
    }

    public void DrawCircle () {
        try {
            Vector3 circlePoint = new Vector3(_thirdPersonCamera.Radius, DistanceToGround, 0) + transform.position,
                lastCirclePoint;

            for (int i = 1; i<=PointsInCircle; i++) {
                // angle in radians
                float angle = 2 * Mathf.PI * i / (float) (PointsInCircle);

                lastCirclePoint = circlePoint;
                circlePoint = new Vector3(_thirdPersonCamera.Radius * Mathf.Cos(angle),
                                          DistanceToGround,
                                          _thirdPersonCamera.Radius * Mathf.Sin(angle)) + transform.position;
                Gizmos.DrawLine(circlePoint, lastCirclePoint);

            }
        } catch {}

    }
}
