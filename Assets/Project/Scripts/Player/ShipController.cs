using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float launchForce = 200000f;

    //Movement
    [SerializeField] float thrustForce = 15000f;
    //[SerializeField][Range(1000f, 10000f)] float verticalForce = 1000f;
    //[SerializeField][Range(1000f, 10000f)] float horizontalForce = 1000f;
    [SerializeField][Range(1000f, 10000f)] float pitchForce = 3000f;
    [SerializeField][Range(1000f, 10000f)] float yawForce = 2000f;
    [SerializeField][Range(1000f, 10000f)] float rollForce = 1000f;
    
    [SerializeField][Range(-1f, 1f)] public float thrustDirection, pitchDirection, rollDirection, yawDirection, verticalDirection, horizontalDirection = 0;

    //Direction
    [SerializeField] float thresholdRadius = 0.1f;
    Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    
    Rigidbody rb;
    Quaternion initialRotation;
    bool rotationFixed = true;

    //Audio
    [SerializeField] ShipAudioManager shipAudioManager;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        //shipAudioManager = GetComponentInChildren<ShipAudioManager>();
    }

    private void Start() {
        initialRotation = transform.rotation;
        rb.AddRelativeForce(Vector3.forward * launchForce * Time.fixedDeltaTime);
        StartCoroutine(EnableRotation(1));
    }

    IEnumerator EnableRotation(float delay) {
        yield return new WaitForSeconds(delay);
        rotationFixed = false;
    }

    private void Update()
    {
        if(rotationFixed) {
            transform.rotation = initialRotation;
        }
        ThrustControl();
        //VerticalControl();
        //HorizontalControl();
        PitchControl();
        YawControl();
        RollControl();
    }

    public void ThrustControl() {
        float nextThrust = Input.GetAxis("Vertical");

        thrustDirection = Mathf.Lerp(thrustDirection, nextThrust, Time.deltaTime * 5f);

         if (shipAudioManager != null)
        {
            shipAudioManager.UpdateEngineAudio(thrustDirection);
        }
    }

    /*public void VerticalControl() {
        if(Input.GetKey(KeyCode.Space)) {
            verticalDirection = Mathf.Lerp(verticalDirection, 1f, Time.deltaTime * 5f);
        }
        if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
            verticalDirection = Mathf.Lerp(verticalDirection, -1f, Time.deltaTime * 5f);
        }
    }*/

    /*public void HorizontalControl() {
        float nextHorizontal = Input.GetAxis("Horizontal");

        horizontalDirection = Mathf.Lerp(horizontalDirection, nextHorizontal, Time.deltaTime * 5f);
    }*/

    public void PitchControl()
    {
        Vector3 mousePosition = Input.mousePosition;

        float distance = (screenCenter.y - mousePosition.y) / screenCenter.y;

        pitchDirection = Mathf.Abs(distance) > thresholdRadius ? distance : 0f;
    }

    public void YawControl()
    {
        Vector3 mousePosition = Input.mousePosition;

        float distance = (mousePosition.x - screenCenter.x) / screenCenter.x;

        yawDirection = Mathf.Abs(distance) > thresholdRadius ? distance : 0f;
    }

    public void RollControl()
    {
        float nextRoll;
        if(Input.GetKey(KeyCode.E)) {
            nextRoll = 1f;
        } else {
            nextRoll = Input.GetKey(KeyCode.Q) ? -1f : 0f;
        }
        //interpolation between new value and old value of roll
        rollDirection = Mathf.Lerp(rollDirection, nextRoll, Time.deltaTime * 5f);
    }

    private void FixedUpdate()
    {
        ProcessForce();
    }

    //TakeDamage: make the ship vibrate for a while

    private void ProcessForce()
    {
        if (!Mathf.Approximately(thrustDirection, 0f))
        {
            rb.AddRelativeForce(Vector3.forward * thrustForce * thrustDirection * Time.fixedDeltaTime);
        }
        if (!Mathf.Approximately(pitchDirection, 0f))
        {
            rb.AddRelativeTorque(Vector3.right * pitchForce * pitchDirection * Time.fixedDeltaTime);
            //force applied based on global coordinate around local axis.
            //rb.AddTorque(transform.right * (pitchForce * pitch * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(rollDirection, 0f))
        {
            rb.AddRelativeTorque(Vector3.back * rollForce * rollDirection * Time.fixedDeltaTime);
            //rb.AddTorque(transform.forward * (rollForce * roll * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(yawDirection, 0f))
        {
            rb.AddRelativeTorque(Vector3.up * yawForce * yawDirection * Time.fixedDeltaTime);
            //rb.AddTorque(transform.up * (yawForce * yaw * Time.fixedDeltaTime));
        }
    }
}
