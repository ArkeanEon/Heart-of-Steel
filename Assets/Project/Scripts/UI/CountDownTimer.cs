using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] TMP_Text countdownText;
    float timeRemaining = 10f;

    private void OnEnable() {
        timeRemaining = 10f;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                countdownText.text = timeRemaining.ToString("F2"); // Display with 2 decimal places
            }

    }
}
