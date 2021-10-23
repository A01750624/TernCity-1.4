using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorManager : MonoBehaviour {
    public GameObject[] characterList;
    private int index = 0;
    public int numberOfCharacters;

    // ****************************************************AQUI SE ACTUALIZA EL NUMERO DE PERSONAJES QUE TIENE EL JUGADOR
    // lOS PERSONAJES TIENEN INDICE DE 1 - 8

    
    void Start()
    {
        numberOfCharacters = PlayerPrefs.GetInt("avatarIndex");
        Debug.Log(numberOfCharacters.ToString());
    }
    
    


    public void NextCharacter() {
        characterList[index].SetActive(false);
        index++;
        if (index > numberOfCharacters - 1) index = 0;
        characterList[index].SetActive(true);
    }

    public void PreviousCharacter() {
        characterList[index].SetActive(false);
        index--;
        if (index < 0) index = numberOfCharacters - 1;
        characterList[index].SetActive(true);
    }

    public void ConfirmCharacter() {
        SceneManager.LoadScene("PruebaCity");
        PlayerPrefs.SetInt("SelectedCharacter", index);
        
        string userName = PlayerPrefs.GetString("userName");

        PutRequests PR = new PutRequests();
        StartCoroutine(PR.UpdateAvatarCallback(userName, index));

        
    }

    public void ConfirmCharacterInCity() {
        PlayerPrefs.SetInt("SelectedCharacter", index);

        string userName = PlayerPrefs.GetString("userName");

        PutRequests PR = new PutRequests();
        StartCoroutine(PR.UpdateAvatarCallback(userName, index));
    }
}
