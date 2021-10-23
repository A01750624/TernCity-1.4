using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderControl : MonoBehaviour
{
    //public TextMeshProUGUI textAmount; // Amount of steel ordered in text

    public CityManager cityManager;
    public Countdown countdown;

    public int steelAmount; // Convert steel amount to int
    private string amountStr; // Convert steel amount text to string


    public void stringToSteel(TextMeshProUGUI textAmount)
    {
        amountStr = textAmount.GetComponent<TextMeshProUGUI>().text;
        int.TryParse(amountStr, out steelAmount);
    }

    public void PressDeliver()
    {
        Debug.Log("BUTTON PRESS steelAmount Order: " + steelAmount);
        cityManager.DeliverSteel(steelAmount);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(countdown.timesUp);
        if (countdown.timesUp == true)
        {
            Debug.Log(countdown.timesUp);
            Debug.Log("TIMES UP");
            cityManager.DeliverSteel(steelAmount);
            countdown.timesUp = false;
        }

    }

}
