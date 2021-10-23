using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float timeValue;
    public TextMeshProUGUI timeText;
    public bool timesUp = false;
    public bool buttonPressed = false;
    public GameObject barTimer;

    private void Start()
    {
        //bc = GetComponent<CityManager>();
    }

    public void stringToTime(TextMeshProUGUI timer)
    {
        string timeStr = timer.GetComponent<TextMeshProUGUI>().text;
        string minutesStr = timeStr.Substring(0, 2);
        string secondsStr = timeStr.Substring(3, 2);
        int.TryParse(minutesStr, out int minutesInt);
        int.TryParse(secondsStr, out int secondsInt);
        minutesInt *= 60;      
        timeValue = minutesInt + secondsInt;
        buttonPressed = true;
        

    } // convertir string a segundos para timeValue

    private void Update() // Solo funciona timer dentro de pedidos ****
    {
        //Debug.Log(timeValue);

        if (buttonPressed)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;
                timesUp = true;
                buttonPressed = false;
                barTimer.SetActive(false);
            }

            DisplayTime(timeValue);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EndTime()
    {
        timeValue = 0;
        timesUp = false;
        buttonPressed = false;
    }
}
