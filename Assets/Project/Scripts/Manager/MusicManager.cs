using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float loopDuration = 2.2f;

   void Awake() {
    audioSource = GetComponent<AudioSource>();
   }

   void OnEnable() {
        if (audioSource != null && audioSource.clip != null) 
        {
            audioSource.Play();
        }
   }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayLoopSection());
    }

    private IEnumerator PlayLoopSection()
    {
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(loopDuration);
            audioSource.Stop();
            audioSource.time = 0; // Reset the playback position to the start
        }
    }
}
