using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            Debug.Log("welcome back");
            ShipSelfDestructor playerShip = other.GetComponent<ShipSelfDestructor>();
            if(playerShip != null) {
                playerShip.CancelCountDown();
            }
        }
    }
}
