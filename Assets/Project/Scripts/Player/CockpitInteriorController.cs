using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitInteriorController : MonoBehaviour
{
    [SerializeField] Transform joystick;
    [SerializeField] Vector3 joystickRange = new Vector3(20f, 20f, 20f);
    [SerializeField] List<Transform> throttleComponents;
    [SerializeField] float throttleRange = 30f;
    [SerializeField] ShipController ship;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ship.PitchControl();
        ship.YawControl();
        ship.RollControl();
        ship.ThrustControl();

        joystick.localRotation = Quaternion.Euler(ship.pitchDirection * joystickRange.x, ship.yawDirection * joystickRange.y, -ship.rollDirection * joystickRange.z);

        Vector3 throttleRotation = throttleComponents[0].localRotation.eulerAngles;

        throttleRotation.x = ship.thrustDirection * throttleRange;
        
        foreach(Transform throttleComponent in throttleComponents) {
            throttleComponent.localRotation = Quaternion.Euler(throttleRotation);
        }
    }
}
