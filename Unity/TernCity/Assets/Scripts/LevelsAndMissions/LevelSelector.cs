using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {
    public bool isUnlocked = false;
    public GameObject lockedLevel;
    public GameObject unlockedLevel;
    public int requiredStars;
    public int startMission, endMission;
    public UIManager manager;

    private void Update() {
        UnlockLevel();
        UpdateLevelStatus();
    }

    private void UpdateLevelStatus() {
        if (isUnlocked) {
            unlockedLevel.gameObject.SetActive(true);
            lockedLevel.gameObject.SetActive(false);
        } else {
            unlockedLevel.gameObject.SetActive(false);
            lockedLevel.gameObject.SetActive(true);
        }
    }

    private void UnlockLevel() {
        if (manager.stars >= requiredStars) {  ///Stars
            isUnlocked = true;
        } else {
            isUnlocked = false;
        }
    }

}
