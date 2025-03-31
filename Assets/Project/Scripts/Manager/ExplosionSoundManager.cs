using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundManager : MonoBehaviour
{
    AudioSource audioSource;

   void Awake() {
    audioSource = GetComponent<AudioSource>();
   }

   void OnEnable() {
        if (audioSource != null && audioSource.clip != null) 
        {
            audioSource.Play();
        }
   }
}
