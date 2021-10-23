using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGetter : MonoBehaviour
{
    // Start is called before the first frame update
    public  string username = "";
    //public  bool Init = false;

    public void setUserName(string input_name) {
        username = input_name;
        PlayerPrefs.SetString("userName", username);
        Debug.Log("Username (unity): " + username);
    }
}
