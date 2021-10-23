using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionSelector : MonoBehaviour {
    public bool isUnlocked = false;
    public CityManager city;
    public GameObject lockedButtonMission;
    public GameObject unlockedButtonMission;
    public Image[] starsImages;


    private void Update() {
        UpdateMissionButton();
        UnlockMission();
    }

    private void UpdateMissionButton() {
        if (isUnlocked) {
            unlockedButtonMission.gameObject.SetActive(true);
            lockedButtonMission.gameObject.SetActive(false);

            // MISION 1-30
            // PEDIDO 1001 -1009
            //  CAMBIAR EL PREFAB POR BASE DE DATOS
            // HACER PREFABS PARA MISION
            for (int i = 0; i < PlayerPrefs.GetInt("Mission" + gameObject.name); i++) {
                starsImages[i].GetComponent<Image>().color = new Color32(255,255,255,255);
            }

        } else {
            unlockedButtonMission.gameObject.SetActive(false);
            lockedButtonMission.gameObject.SetActive(true);
        }
    }

    private void UnlockMission() {
        int previousMissionIndex = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Mission" + previousMissionIndex) > 0) {
            isUnlocked = true;
        }
    }

    public void FromCityToQuiz(int _missionIndex) {
        PlayerPrefs.SetInt("CurrentMission", _missionIndex);
        PlayerPrefs.SetInt("playerSteel", city.steel);
        SceneManager.LoadScene("Quiz");
    }
    
}
