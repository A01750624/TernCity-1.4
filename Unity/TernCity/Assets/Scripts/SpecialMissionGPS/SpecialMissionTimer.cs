using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialMissionTimer : MonoBehaviour
{
    [HideInInspector]
    public float timeRemaining = 240;
    [HideInInspector]
    public bool timerIsRunning = true;
    private int timeR;
    public TextMeshProUGUI timerText;
    public SpecialMissionManager specialMission; 


    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timeR = Mathf.RoundToInt(timeRemaining);
                timerText.GetComponent<TextMeshProUGUI>().text = timeR.ToString() + " seg";
            }
            else
            {
                specialMission.gameFinished = true;
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}
