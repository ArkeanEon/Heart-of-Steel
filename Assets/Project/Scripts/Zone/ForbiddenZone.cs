using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForbiddenZone : MonoBehaviour
{
    [SerializeField] float countDownTime = 5f;

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            Debug.Log("bye bye");
            ShipSelfDestructor playerShip = other.GetComponent<ShipSelfDestructor>();
            if(playerShip != null) {
                playerShip.StartCountDown(countDownTime);
            }
        }
    }
}
