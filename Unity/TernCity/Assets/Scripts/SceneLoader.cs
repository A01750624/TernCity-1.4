using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    SceneManager sceneManager;

    public void FromStartToCharacter () {
     
        if (PlayerPrefs.GetInt("SelectedCharacter") == 10)
        {
            SceneManager.LoadScene("CharacterSelection");
        }
        else
        {
            SceneManager.LoadScene("PruebaCity");
        }
    }

    public void FromStartToCity () {
        SceneManager.LoadScene("PruebaCity");
    }

    public void FromCityToSpecialMission()
    {
        SceneManager.LoadScene("City");
    }

    public void FromCityToQuiz () {
        SceneManager.LoadScene("Quiz");
    }

    public void FromQuizToCity () {
        SceneManager.LoadScene("PruebaCity");
    }

    public void FromCityToStart () {
        SceneManager.LoadScene("StartMenu");
    }
}