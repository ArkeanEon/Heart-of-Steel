using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelfDestructor : MonoBehaviour
{
    [SerializeField] GameObject warningUI;
    public Coroutine selfDestructCoroutine;

    [SerializeField] Light alarmLight;
    float lightInterval = 0.75f;
    Coroutine turnOnAlarmCoroutine;

    [SerializeField]AudioSource audioSource;
    
    void Start() {
    }

    void Update() {
        if(selfDestructCoroutine != null) {
            if(!audioSource.isPlaying) {
            audioSource.Play();
        }
        } else {
            if(audioSource.isPlaying) {
            audioSource.Stop();
        }
        }
    }

    IEnumerator CountDown(float time) {
        Debug.Log("countdown starts");
        warningUI.SetActive(true);
        yield return new WaitForSeconds(time);
        Debug.Log("detonate");
        Destroy(gameObject);
    }

    public void StartCountDown(float time)
    {
        if(selfDestructCoroutine == null) {
            selfDestructCoroutine = StartCoroutine(CountDown(time));
        }
        if(turnOnAlarmCoroutine == null) {
            turnOnAlarmCoroutine = StartCoroutine(TurnOnAlarm());
        }
    }

    public void CancelCountDown() {
        if (selfDestructCoroutine != null) {
        warningUI.SetActive(false);
        StopCoroutine(selfDestructCoroutine);
        selfDestructCoroutine = null;
    }
    if (turnOnAlarmCoroutine != null) {
        StopCoroutine(turnOnAlarmCoroutine);
        turnOnAlarmCoroutine = null;
        alarmLight.enabled = false;
    }
    }

    IEnumerator TurnOnAlarm() {
        while(true) {
            alarmLight.enabled = !alarmLight.enabled;
            yield return new WaitForSeconds(lightInterval);
        }
    }
}
