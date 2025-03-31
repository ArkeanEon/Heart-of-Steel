using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
   private void OnEnable() {
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RestartGame() {
        Debug.Log("Game restart.");
    }

    public void QuitToMain() {
        Debug.Log("Quit to menu.");
        Application.Quit();
    }
}
