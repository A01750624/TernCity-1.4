using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharacter : MonoBehaviour {
    public GameObject[] characterList;
    int last;

    private void Start()
    {
        last = PlayerPrefs.GetInt("SelectedCharacter");
        characterList[last].SetActive(true);
    }
    private void Update () {
        if (last != PlayerPrefs.GetInt("SelectedCharacter"))
        {
            characterList[last].SetActive(false);
            characterList[PlayerPrefs.GetInt("SelectedCharacter")].SetActive(true);
            last = PlayerPrefs.GetInt("SelectedCharacter"); 

            // ******************************************************************* MENU DEL UI PARA QUE SE VEA EN EN EL JUGADOR
        }
    }
}