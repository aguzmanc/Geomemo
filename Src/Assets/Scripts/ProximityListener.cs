using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProximityListener : MonoBehaviour
{
    #region PUBLIC PROPERTIES
    public List<ProximitySensor> Sensors {
        get {
            if (null == _sensors)
                _sensors = new List<ProximitySensor>();
            return _sensors;
        }
    }

    // this will only select one sensor on.
    public ProximitySensor SensorOn;
    #endregion

    #region PRIVATE PROPERTIES
    private List<ProximitySensor> _sensors;
    #endregion

    #region UNITY METHODS
    void Start () {
    }

    void Update () {
        SensorOn = null;

        // this will select the target that the listener is looking at.
        // breaking matches only by the angle.
        foreach (ProximitySensor sensor in Sensors) {
            if (sensor.IsOn) {
                if (null == SensorOn) {
                    SensorOn = sensor;
                } else if (SensorOn.Angle > sensor.Angle) {
                    SensorOn = sensor;
                }
            }
        }
    }
    #endregion

    #region PUBLIC METHODS
    public int AddSensor (ProximitySensor sensor) {
        Sensors.Add(sensor);
        return Sensors.Count -1;
    }
    #endregion
}
