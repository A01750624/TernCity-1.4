using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json;
using System;



// Escena start menu
// Todos los atributos del jugador

public class PlayerSettings : MonoBehaviour
{
    string userName = "";
    public UserGetter userGetter;
    CityManager CT;
    public GameObject button;

    
    // * Bought structures
    public static List<DBResponse.BoughtStructure> BSPlayer =  new List<DBResponse.BoughtStructure>();

    // * Theory missions
    public static List<DBResponse.Missions> TheoryMPlayer =  new List<DBResponse.Missions>();
    
    // * Order missions
    public static List<DBResponse.Missions> OrderMPlayer =  new List<DBResponse.Missions>();

    // * Verifies variable initialization
    public static bool TheoryMissionInit = false;
    public static bool OrderMissionInit = false;

    // * Special mission
    //public static bool specialStructure;

    // public static int coins;

     void Start() {
        button.GetComponent<Button>().interactable = false;
        Debug.Log(userName);
        //setPrefs(userName);
    }
    
    public void setPrefs(string userName) {


        PlayerPrefs.SetString("userName", userName);
        button.GetComponent<Button>().interactable = true;
        Debug.Log("Unity username: " + userName);
        GetRequests GR = new GetRequests();
        StartCoroutine(GR.GetPlayerInformationCallback(userName, (token) =>
        {
            PlayerPrefs.SetInt("playerLevel", token.level);
            PlayerPrefs.SetInt("SelectedCharacter", token.avatar);
            PlayerPrefs.SetInt("playerSteel", token.steel);
            PlayerPrefs.SetInt("playerSecretWord", token.secretWord);
            PlayerPrefs.SetInt("playerSecretWordProgress", token.secretWordProgress);
            PlayerPrefs.SetInt("playerStars", token.stars);

            Debug.Log("Personaje" + PlayerPrefs.GetInt("SelectedCharacter"));


        }
        ));  
        
        StartCoroutine(GR.GetAvatarIndexCallback(userName, (token) =>
        {
            PlayerPrefs.SetInt("avatarIndex", token.avatarIndex);
        }
        )); 

        StartCoroutine(GR.GetBoughtStructureCallback(userName, (token) =>
        {

            for(int i = 0; i < token.Count; i++)
            {
                DBResponse.BoughtStructure OneBoughtStructure = new DBResponse.BoughtStructure();
                OneBoughtStructure.name = token[i].name;
                OneBoughtStructure.status = token[i].status;

                BSPlayer.Add(OneBoughtStructure);

            }

        }
        )); 

        // * Theory missions
        StartCoroutine(GR.GetInfoMissionsCallback(userName, 0, (token) =>
        {
             for(int i = 0; i < token.Count; i++)
            {
                DBResponse.Missions OneTMission = new DBResponse.Missions();
                OneTMission.key = token[i].key;
                OneTMission.name = token[i].name;
                OneTMission.stars = token[i].stars;


                TheoryMPlayer.Add(OneTMission);
            }


        }
        )); 

        // * Order missions
        StartCoroutine(GR.GetInfoMissionsCallback(userName, 1, (token) =>
        {
             for(int i = 0; i < token.Count; i++)
            {
                DBResponse.Missions OneOMission = new DBResponse.Missions();
                OneOMission.key = token[i].key;
                OneOMission.name = token[i].name;
                OneOMission.steel = token[i].steel;
                OneOMission.percentage = token[i].percentage;

                OrderMPlayer.Add(OneOMission);
            }

        }
        )); 

        // * Special Mission 

            // * Structure
        StartCoroutine(GR.GetSpecialStructureCallback(userName, (token) =>
        {
            if(token.status)
            {
                PlayerPrefs.SetString("SpecialMissionStatus", "true");
            } else 
            {
                PlayerPrefs.SetString("SpecialMissionStatus", "false");
            }
        }
        )); 

            // * Coins
        StartCoroutine(GR.GetSpecialMissionCallback(userName, (token) =>
        {
            PlayerPrefs.SetInt("steelPercentaje", token.coins);
             
        }
        )); 



        

    }
    private void Update() {
                
    }
}
