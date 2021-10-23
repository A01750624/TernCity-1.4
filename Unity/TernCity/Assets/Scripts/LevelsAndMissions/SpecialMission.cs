using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SpecialMission : MonoBehaviour
{
    public LocationReceiver actualLocation;
    public GameObject UISpecialMission;
    public bool missionStatus = false;
    public bool missionComplete; 

    //PlayerPrefs.SetString("SpecialMissionStatus", "true");
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("SpecialMissionStatus") == "true")
        {
            missionComplete = true;
        }
        else
        {
            missionComplete = false; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        setStatus();
    }

    void setStatus()
    {
        if (actualLocation.state == "Nuevo Leon" && actualLocation.country == "Mexico")
        {
            missionStatus = true;
        }
        else
        {
            // ! Cambia a false
            missionStatus = true; 
        }
        if (missionComplete == true)
        {
            UISpecialMission.transform.GetChild(3).gameObject.SetActive(false);
            UISpecialMission.transform.GetChild(4).gameObject.SetActive(true);
            UISpecialMission.transform.GetChild(5).gameObject.SetActive(false);
        }
        else
        {
            if (missionStatus == true)
            {
                UISpecialMission.transform.GetChild(3).gameObject.SetActive(true);
                UISpecialMission.transform.GetChild(4).gameObject.SetActive(false);
                UISpecialMission.transform.GetChild(5).gameObject.SetActive(false);
            }
            else
            {
                UISpecialMission.transform.GetChild(3).gameObject.SetActive(false);
                UISpecialMission.transform.GetChild(4).gameObject.SetActive(false);
                UISpecialMission.transform.GetChild(5).gameObject.SetActive(true);
            }
        }
    }

    public void goSpecialMission()
    {
        SceneManager.LoadScene("City");
    }
}
