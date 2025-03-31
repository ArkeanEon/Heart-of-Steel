using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileLauncherTransform;
    [SerializeField] [Range(0f, 5f)] float coolDownTime = 0.25f;
    float coolDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        if(coolDown <= 0f && Input.GetMouseButton(0) && !PauseController.isPaused) {
            Fire();
        }
    }

    void Fire() {
        coolDown = coolDownTime;
        Instantiate(projectilePrefab, projectileLauncherTransform.position, transform.rotation);
    }
}
