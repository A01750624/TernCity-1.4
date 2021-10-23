using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class DeliveryMissionSelector : MonoBehaviour  {
    public bool isUnlocked = false;
    public GameObject lockedButtonMission;
    public CityManager city; 
    public Button unlockedButtonMission;
    public TextMeshProUGUI textSteelQuantity;
    public TextMeshProUGUI textPercent;
    public int steelCostQuantity;
    public int previousMission; 


    void Start()
    {
        
        if(!PlayerSettings.OrderMissionInit)
        {
            SetOrderMissions(PlayerSettings.OrderMPlayer);
            PlayerSettings.OrderMissionInit = true;
        }
    }

    private void Update() {
        UpdateMissionButton();
        UnlockMission();
    }

    private void UpdateMissionButton() {
        textSteelQuantity.text = steelCostQuantity.ToString();
        
        if (!isUnlocked) {
            unlockedButtonMission.gameObject.SetActive(false);
            lockedButtonMission.gameObject.SetActive(true);
            textPercent.text = "0%";
        } else {
            unlockedButtonMission.gameObject.SetActive(true);
            lockedButtonMission.gameObject.SetActive(false);
            // GUARDAS DEL 0 - 100
            // MPEDIODO 1001-1009
            // LAS MISSION DELIVARY
            int steelPercent = (PlayerPrefs.GetInt("MissionDeliveryPer" + int.Parse(gameObject.name)));
            
            Debug.Log( int.Parse(gameObject.name) + " Steel Percent: "+ steelPercent);
            textPercent.text = steelPercent.ToString() + "%";
            unlockedButtonMission.interactable = true;
            if (steelPercent >= 85 || city.steel < steelCostQuantity) {
                unlockedButtonMission.interactable = false; 
            }
        }
    }

    private void UnlockMission() {
        if (PlayerPrefs.GetInt("Mission" + previousMission) > 0) {
            isUnlocked = true;
        }
    }

    public void FromCityToQuiz() {
        PlayerPrefs.SetInt("CurrentMission", int.Parse(gameObject.name));
        PlayerPrefs.SetInt("MissionSteel", steelCostQuantity);
        PlayerPrefs.SetInt("playerSteel", city.steel);
        SceneManager.LoadScene("Quiz");
    }


    // ** Data base
    private void SetOrderMissions(List<DBResponse.Missions> OMission)
    {
        for(int iC = 1001; iC <= 1010; iC++)
        {
            PlayerPrefs.SetInt("MissionDeliveryPer" + iC, OMission[iC-1001].percentage);
        }
    }


}
