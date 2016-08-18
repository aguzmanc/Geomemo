using UnityEngine;
using System.Collections;

public class ProximitySensor : MonoBehaviour {
    public GameObject Target;
    public float DistanceRequired = 2;
    public float AngleRequired = 45;
    public bool IsOn;
    public int IndexOnListener;
    public float Distance;
    public float Angle;
    public GameObject OnIndicator;

    private ProximityListener _listener;

    void Start () {
        // every sensor's target must have a ProximityListener attached
        if (null == Target) {
            Target = GameObject.FindGameObjectWithTag(Tag.Player);
        }
        if (!Target.GetComponent<ProximityListener>()) {
            Target.AddComponent<ProximityListener>();
        }
        _listener = Target.GetComponent<ProximityListener>();
        IndexOnListener = _listener.AddSensor(this);
    }
    
    void Update () {
        IsOn = false;

        Distance = Vector3.Distance(Target.transform.position, this.transform.position);
        Angle = Vector3.Angle(Target.transform.forward, (this.transform.position - Target.transform.position) / Distance);

        IsOn = Distance < DistanceRequired && // at the right distance
            Angle < AngleRequired; // and at the right angle

        OnIndicator.SetActive(_listener.SensorOn == this);
    }

    public bool IsActive () {
        return OnIndicator.activeSelf;
    }
}
