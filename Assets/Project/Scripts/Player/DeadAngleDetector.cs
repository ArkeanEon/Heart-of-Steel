using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeadAngleDetector : MonoBehaviour
{
    [SerializeField] float detectionRange = 50f;

    //Light
    [SerializeField] Light alarmLight;
    [SerializeField] float flashInterval = 0.75f;

    Coroutine coroutine;
    bool isFlashing = false;

    // Start is called before the first frame update
    void Start()
    {
        alarmLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        DeadAngleDetection();
    }

    void DeadAngleDetection() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("HighThreat");

        foreach(GameObject enemy in enemies) {
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToEnemy);
            float distance = directionToEnemy.magnitude;

            if(angle > 90f && distance <= detectionRange) {
                //Debug.Log("At behind");
                if(isFlashing == false) {
                    isFlashing = true;
                    coroutine = StartCoroutine(FlashAlarmLight());
                }
            } else {
                if(isFlashing == true) {
                    isFlashing = false;
                    if(coroutine != null) {
                        StopCoroutine(FlashAlarmLight());
                        coroutine = null;
                    }
                    alarmLight.enabled = false;
                }
            }
        }
    }

    IEnumerator FlashAlarmLight() {
        while(isFlashing) {
            alarmLight.enabled = !alarmLight.enabled;
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
