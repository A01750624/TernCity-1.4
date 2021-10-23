using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityTimer : MonoBehaviour
{
    [HideInInspector]
    public float timeRemaining = 15;
    [HideInInspector]
    public bool timerIsRunning = true;
    private int timeR;
    public CityManager city;

    string userName;
    void Start()
    {
        timerIsRunning = true;
        userName = PlayerPrefs.GetString("userName");
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timeR = Mathf.RoundToInt(timeRemaining);
            }
            else
            {
                Debug.Log("Recolección de dinero");
                timeRemaining = 0;
                timerIsRunning = false;
                city.steel += city.steelGen;

                PutRequests PR = new PutRequests();
                StartCoroutine(PR.UpdateEconomyCallback(userName, city.steel));
                // UPDATE ECONOMY 
                // CAMBIAR USERNAME Y STEEL

            }
        }
    }
}
