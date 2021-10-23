using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    public GameObject[] buttonAccept;
    public GameObject[] buttonBlock;
    public GameObject[] buttonDeliver;
    public GameObject[] buttonDone;
    public GameObject barTime;
    public Countdown countdown;
    public CityManager cityManager;
    private int newButtonDone;
    public GameObject[] orders;
    public int[] levelsOrders;

    public void Start()
    {
        orders = GameObject.FindGameObjectsWithTag("Order"); // Get all orders
        buttonAccept = GameObject.FindGameObjectsWithTag("BAceptar"); // Button Accept
        buttonBlock = GameObject.FindGameObjectsWithTag("BBloqueado"); // Button Block
        buttonDeliver = GameObject.FindGameObjectsWithTag("BEntregar"); // Button Deliver
        buttonDone = GameObject.FindGameObjectsWithTag("BEntregado"); // Button Delivered
        
        for (int i = 0; i < orders.Length; i++)
        {
            buttonBlock[i].SetActive(false);
            buttonDeliver[i].SetActive(false);
            buttonDone[i].SetActive(false);
        }
    }

    public void ButtonPress(Object button) // Function to call when Accept Button pressed
    {
        barTime.SetActive(true);

        for (int i = 0; i < orders.Length; i++) // For loop on the lenght of Accept buttons
        {
            if (button != buttonAccept[i] && !buttonDone[i].activeSelf) // If the button pressed isnt "this" one and its also not a Done Button
            {
                buttonAccept[i].SetActive(false); // SetActive(false) this accept button
                buttonBlock[i].SetActive(true); // SetActive(true) this block button

            }
            else
            {
                buttonBlock[i].SetActive(false); // SetActive(false) all buttons that werent the pressed one
            }
        }
        //if (button != buttonAccept[i])
    }

    public void ButtonDeliver(Object button) // Function to call when Deliver Button pressed
    {
        barTime.SetActive(false);

        for (int i = 0; i < orders.Length; i++) // For loop on the lenght of Deliver buttons
        {
            if (button != buttonDeliver[i] && !buttonDone[i].activeSelf) // If the button pressed isnt "this" one and its also not a Done Button
            {                
                buttonAccept[i].SetActive(true); // SetActive(true) this Accept button
                buttonBlock[i].SetActive(false); // SetActive(false) this Block button
            }
            else
            {
                buttonAccept[i].SetActive(false); // SetActive(false) all buttons that werent
            }
        }
    }

    public void TimeForDeliver()
    {
        if (countdown.timesUp == true)
        {
            Debug.Log("TURN DELIVER TO DONE");
            for (int i = 0; i < orders.Length; i++)
            {
                if (buttonDeliver[i].activeSelf) // este if
                {
                    newButtonDone = i;
                }
                buttonDeliver[i].SetActive(false);
                if (!buttonDone[i].activeSelf)
                {
                    buttonAccept[i].SetActive(true); // SetActive(true) this Accept button
                    buttonBlock[i].SetActive(false); // SetActive(false) this Block button
                }
                else
                {
                    buttonAccept[i].SetActive(false);
                }
            }
            buttonDone[newButtonDone].SetActive(true); // esta línea
            buttonAccept[newButtonDone].SetActive(false);
        }
    }

    public void UnlockOrderLevel() // LLAMADO POR EL UPDATE EN CUALQUIER CODIGO FUERA DEL MENU UI
    {
        for (int i = 0; i < orders.Length; i++)
        {
            if (cityManager.level < levelsOrders[i])
            {
                if (!buttonDone[i].activeSelf)
                {
                    buttonBlock[i].SetActive(true);
                    buttonAccept[i].SetActive(false);
                }
                else
                {
                    buttonBlock[i].SetActive(false);
                    buttonAccept[i].SetActive(true);
                }

            }
            else // if (cityManager.level >= levelsOrders[i])
            {
                if (!buttonDone[i].activeSelf) { buttonAccept[i].SetActive(true); }
                else { buttonAccept[i].SetActive(false); }
            }
        }
    }
}
