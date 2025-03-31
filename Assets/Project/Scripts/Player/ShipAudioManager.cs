using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource engineLow;
    [SerializeField] AudioSource engineHigh;

    public void UpdateEngineAudio(float thrustDirection) {
        float absThrust = Mathf.Abs(thrustDirection);

        // Smaller sound
        if(!Mathf.Approximately(absThrust, 0f) && absThrust <= 0.5f) {
             engineLow.volume = absThrust / 0.5f; // Scale volume up to 0.5
            if (!engineLow.isPlaying) {
                engineLow.Play();
            } 
        }
        else {
            if(engineLow.isPlaying) {
                engineLow.Stop();
            }
        }

        // Larger sound
        if(!Mathf.Approximately(absThrust, 0.5f) && absThrust <= 1f){
            engineHigh.volume = (absThrust - 0.5f) / 0.5f; // Scale volume 0.5 to 1
            if (!engineHigh.isPlaying) {
                engineHigh.Play();
            } 
        }
        else {
            if(engineHigh.isPlaying) {
                    engineHigh.Stop();
            }
        }
    }
}
