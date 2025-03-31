using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject controlUI;
    public static bool isPaused = false;

    // Update is called once per frame
    void Start() {
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void ResumeGame() {
        AudioListener.volume = 1f;
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        isPaused = false;
    }

    void PauseGame() {
        AudioListener.volume = 0f;
        pauseUI.SetActive(true);
        controlUI.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void checkControl() {
        controlUI.SetActive(true);
        pauseUI.SetActive(false);
        isPaused = false;
    }

    // Todo
    public void QuitToMain() {
        Debug.Log("Quit to menu.");
        Application.Quit();
    }
}
