using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    public GameObject levelSelectionPanel;
    public GameObject[] levelObjects;

    public int stars = 0;
    public LevelSelector[] LevelSelectors;
    public TextMeshProUGUI[] requiredStarsTexts;
    public TextMeshProUGUI[] lockedStarsTexts;
    public TextMeshProUGUI[] unlockedStarsTexts;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy(gameObject);
            }
        }
        /// DontDestroyOnLoad(gameObject);
        /// PlayerPrefs.DeleteAll();
        if(!PlayerSettings.TheoryMissionInit)
        {
            SetTheoryMissions(PlayerSettings.TheoryMPlayer);
            PlayerSettings.TheoryMissionInit = true;
        }

        UpdateStarUI();
    }

    private void Update() {
        // * Data base
        UpdateLockedStarUI();
        UpdateUnlockedStarUI();
    }

    private void UpdateLockedStarUI() {
        for (int i = 0; i < LevelSelectors.Length; i++) {
            requiredStarsTexts[i].text = LevelSelectors[i].requiredStars.ToString();
        }
    }

    private void UpdateUnlockedStarUI() {
        for (int i = 0; i < LevelSelectors.Length; i++) {
            /// unlockedStarsTexts[i].text = stars.ToString() + "/" + LevelSelectors[i].endMission * 3;

            switch (i) {
                case 0:    
                    unlockedStarsTexts[i].text = 
                        (PlayerPrefs.GetInt("Mission" + 1)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break;
                case 1:
                    unlockedStarsTexts[i].text = 
                        (PlayerPrefs.GetInt("Mission" + 2) +
                        PlayerPrefs.GetInt("Mission" + 3)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break; 
                case 2:
                    unlockedStarsTexts[i].text =
                        (PlayerPrefs.GetInt("Mission" + 4) +
                        PlayerPrefs.GetInt("Mission" + 5)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break;
                case 3:
                    unlockedStarsTexts[i].text = 
                        (PlayerPrefs.GetInt("Mission" + 6) +
                        PlayerPrefs.GetInt("Mission" + 7)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break;
                case 4:
                    unlockedStarsTexts[i].text =
                        (PlayerPrefs.GetInt("Mission" + 8) +
                        PlayerPrefs.GetInt("Mission" + 9)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break;
                case 5:
                    unlockedStarsTexts[i].text = 
                        (PlayerPrefs.GetInt("Mission" + 10) +
                        PlayerPrefs.GetInt("Mission" + 11) +
                        PlayerPrefs.GetInt("Mission" + 12)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break;
                case 6:
                    unlockedStarsTexts[i].text = 
                        (PlayerPrefs.GetInt("Mission" + 13) +
                        PlayerPrefs.GetInt("Mission" + 14) +
                        PlayerPrefs.GetInt("Mission" + 15)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break;
                case 7:
                    unlockedStarsTexts[i].text = 
                        (PlayerPrefs.GetInt("Mission" + 16) +
                        PlayerPrefs.GetInt("Mission" + 17) +
                        PlayerPrefs.GetInt("Mission" + 18)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break;
                case 8:
                    unlockedStarsTexts[i].text = 
                        (PlayerPrefs.GetInt("Mission" + 19) +
                        PlayerPrefs.GetInt("Mission" + 20) +
                        PlayerPrefs.GetInt("Mission" + 21) +
                        PlayerPrefs.GetInt("Mission" + 22)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break;
                case 9:
                    unlockedStarsTexts[i].text = 
                        (PlayerPrefs.GetInt("Mission" + 23) +
                        PlayerPrefs.GetInt("Mission" + 24) +
                        PlayerPrefs.GetInt("Mission" + 25) +
                        PlayerPrefs.GetInt("Mission" + 26)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break;
                case 10:
                    unlockedStarsTexts[i].text = 
                        (PlayerPrefs.GetInt("Mission" + 27) +
                        PlayerPrefs.GetInt("Mission" + 28) +
                        PlayerPrefs.GetInt("Mission" + 29) +
                        PlayerPrefs.GetInt("Mission" + 30)) +
                        "/" + (LevelSelectors[i].endMission - LevelSelectors[i].startMission + 1) * 3;
                    break;
            }
        }
    }

    private void UpdateStarUI() {
        for (int i = 1; i <= 30; i++) {
            stars += PlayerPrefs.GetInt("Mission" + i);
        }
    }

    // ** Data base
    private void SetTheoryMissions(List<DBResponse.Missions> TMission)
    {
        for(int iC = 1; iC <= TMission.Count-1; iC++)
        {
            //Debug.Log("Estreellas  "+ TMission[iC-1].stars);
            PlayerPrefs.SetInt("Mission" + iC, TMission[iC - 1].stars);
        }
    }


}
