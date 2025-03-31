using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    void Start()
    {
        Invoke("HideStartScreen", 3f);
    }

    void HideStartScreen()
    {
        gameObject.SetActive(false);
    }
}
