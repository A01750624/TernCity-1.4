using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json;
using System;


public class GetRequests
{
    string page = "http://localhost:4000";
    // CityManager CT = gameObject.GetComponent<CityManager>();
    
    //List<DBResponse.BoughtStructure> playerDataBS;


  public IEnumerator GetPlayerInformationCallback(string userName, Action<DBResponse.Player> token)
  {
      
    string url = page + "/api/game/getPlayerInformation/" + userName;
    // Load Player data in JSON format
    UnityWebRequest web = UnityWebRequest.Get(url);
    web.useHttpContinue = false;

    Debug.Log("Webrequest");
    yield return web.SendWebRequest();

    if (web.isNetworkError || web.isHttpError)
    {
        Debug.Log("Error API: " + web.error);
    }
    else
    {
      string reJson =  web.downloadHandler.text;
      DBResponse.Player playerData = JsonConvert.DeserializeObject<DBResponse.Player>(reJson.Substring(1, reJson.Length-2));
            
      Debug.Log(playerData.playerName);

      if (playerData.playerName != null)
              token(playerData);
      
      }
  }  

  public IEnumerator GetAvatarIndexCallback(string userName, Action<DBResponse.TotalAvatar> token)
  {
      
    string url = page + "/api/game/getAvatarIndex/" + userName;
    // Load Player data in JSON format
    UnityWebRequest web = UnityWebRequest.Get(url);
    web.useHttpContinue = false;

    Debug.Log("Webrequest");
    yield return web.SendWebRequest();

    if (web.isNetworkError || web.isHttpError)
    {
        Debug.Log("Error API: " + web.error);
    }
    else
    {
      string reJson =  web.downloadHandler.text;
      DBResponse.TotalAvatar playerData = JsonConvert.DeserializeObject<DBResponse.TotalAvatar>(reJson.Substring(1, reJson.Length-2));
            
      //Debug.Log("Dentro de corutina " + playerData.avatarIndex.ToString());

      //if (playerData.avatarIndex != null)
      token(playerData);
      
      }
  }  
  
  public IEnumerator GetBoughtStructureCallback(string userName, Action<List<DBResponse.BoughtStructure>> token)
  {
      
    string url = page + "/api/game/getBoughtStructure/" + userName;
    // Load Player data in JSON format
    UnityWebRequest web = UnityWebRequest.Get(url);
    web.useHttpContinue = false;

    Debug.Log("Webrequest");
    yield return web.SendWebRequest();

    if (web.isNetworkError || web.isHttpError)
    {
        Debug.Log("Error API: " + web.error);
    }
    else
    {
      string reJson =  web.downloadHandler.text;
      List<DBResponse.BoughtStructure> playerDataBS = JsonConvert.DeserializeObject<List<DBResponse.BoughtStructure>>(reJson);
      
      
      
      if (playerDataBS[0].name != null)
      {
              token(playerDataBS);
      }
    }

  }  

  public IEnumerator GetInfoMissionsCallback(string userName, int type, Action<List<DBResponse.Missions>> token)
  {
      
    string url = page + "/api/game/getInfoMissions/" + userName + "/" + type.ToString();
    // Load Player data in JSON format
    UnityWebRequest web = UnityWebRequest.Get(url);
    web.useHttpContinue = false;

    Debug.Log("Webrequest");
    yield return web.SendWebRequest();

    if (web.isNetworkError || web.isHttpError)
    {
        Debug.Log("Error API: " + web.error);
    }
    else
    {
      string reJson =  web.downloadHandler.text;
      List<DBResponse.Missions> playerData = JsonConvert.DeserializeObject<List<DBResponse.Missions>>(reJson);
      
      
      if (playerData[0].name != null)
      {
              token(playerData);
      }
    }
  }  

  // * Special mission
  public IEnumerator GetSpecialStructureCallback(string userName, Action<DBResponse.SpecialStructure> token)
  {
      
    string url = page + "/api/game/getSpecialStructure/" + userName;
    // Load Player data in JSON format
    UnityWebRequest web = UnityWebRequest.Get(url);
    web.useHttpContinue = false;

    Debug.Log("Webrequest");
    yield return web.SendWebRequest();

    if (web.isNetworkError || web.isHttpError)
    {
        Debug.Log("Error API: " + web.error);
    }
    else
    {
      string reJson =  web.downloadHandler.text;
      DBResponse.SpecialStructure playerData = JsonConvert.DeserializeObject<DBResponse.SpecialStructure>(reJson.Substring(1, reJson.Length-2));
            

      token(playerData);
      
      }
  }  

  public IEnumerator GetSpecialMissionCallback(string userName, Action<DBResponse.SpecialMission> token)
  {
      
    string url = page + "/api/game/getSpecialMission/" + userName;
    // Load Player data in JSON format
    UnityWebRequest web = UnityWebRequest.Get(url);
    web.useHttpContinue = false;

    Debug.Log("Webrequest");
    yield return web.SendWebRequest();

    if (web.isNetworkError || web.isHttpError)
    {
        Debug.Log("Error API: " + web.error);
    }
    else
    {
      string reJson =  web.downloadHandler.text;
      DBResponse.SpecialMission playerData = JsonConvert.DeserializeObject<DBResponse.SpecialMission>(reJson.Substring(1, reJson.Length-2));
            
      token(playerData);
      
      }
  }  
  

}
