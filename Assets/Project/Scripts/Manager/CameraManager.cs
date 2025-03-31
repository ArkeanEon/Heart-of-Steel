using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam1;
    [SerializeField] CinemachineVirtualCamera cam2;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        LoadCamera();
    }

    private void LoadCamera() {
        if(Input.GetKeyDown(KeyCode.C)) {
            if (cam1.Priority > cam2.Priority)
            {
                cam1.Priority = 5;
                cam2.Priority = 10;
            }
            else
            {
                cam1.Priority = 10;
                cam2.Priority = 5;
            }
        }
    }

}
