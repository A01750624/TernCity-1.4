using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationReceiver : MonoBehaviour
{
    public string state = "";
    public string country = "";

    public void setState(string input) {
        state = input;
        Debug.Log("City: " + state);
    }

    public void setCountry(string input) {
        country = input;
        Debug.Log("Country: " + country);
    }
}

