using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json;

public class PutRequests
{
    string page = "http://localhost:4000";


    public IEnumerator UpdateEconomyCallback(string userName, int steel)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        string url = page + "/api/game/updateEconomy/" + userName + "/" + steel.ToString();
        using UnityWebRequest web = UnityWebRequest.Put
        (url, json);
        web.method = UnityWebRequest.kHttpVerbPUT;
        web.SetRequestHeader("Content-type", "application/json");
        web.SetRequestHeader("Accept", "application/json");
        yield return web.SendWebRequest();

        if (web.isNetworkError || web.isHttpError)
        {
            Debug.Log(url);
            Debug.Log(web.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public IEnumerator UpdateLevelCallback(string userName, int level)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        
        string url = page + "/api/game/updateLevel/" + userName + "/" + level.ToString();
        using UnityWebRequest www = UnityWebRequest.Put(url, json);
        www.method = UnityWebRequest.kHttpVerbPUT;
        www.SetRequestHeader("Content-type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public IEnumerator UpdateAvatarCallback(string userName, int avatarIndex)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        
        string url = page + "/api/game/updateAvatar/" + userName + "/" + avatarIndex;
        using UnityWebRequest www = UnityWebRequest.Put(url, json);
        www.method = UnityWebRequest.kHttpVerbPUT;
        www.SetRequestHeader("Content-type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public IEnumerator UpdateAvatarIndexCallback(string userName, int avatarIndex)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        
        string url = page + "/api/game/updateAvatarIndex/" + userName + "/" + avatarIndex.ToString();
        using UnityWebRequest www = UnityWebRequest.Put(url, json);
        www.method = UnityWebRequest.kHttpVerbPUT;
        www.SetRequestHeader("Content-type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public IEnumerator UpdateSecretCodeCallback(string userName, int index)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        
        string url = page + "/api/game/updateSecretCode/" + userName + "/" + index.ToString();
        using UnityWebRequest www = UnityWebRequest.Put(url, json);
        www.method = UnityWebRequest.kHttpVerbPUT;
        www.SetRequestHeader("Content-type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public IEnumerator UpdateSecretCodeProgressCallback(string userName, int progress)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        
        string url = page + "/api/game/updateSecretCodeProgress/" + userName + "/" + progress.ToString();
        using UnityWebRequest www = UnityWebRequest.Put(url, json);
        www.method = UnityWebRequest.kHttpVerbPUT;
        www.SetRequestHeader("Content-type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public IEnumerator UpdateBoughtStructureCallback(string userName, string key)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        
        string url = page + "/api/game/updateBoughtStructure/" + userName + "/" + key;
        using UnityWebRequest www = UnityWebRequest.Put(url, json);
        www.method = UnityWebRequest.kHttpVerbPUT;
        www.SetRequestHeader("Content-type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public IEnumerator UpdateStarsCallback(string userName, int key, int stars)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        
        string url = page + "/api/game/updateStars/" + userName + "/" + key.ToString() + "/" + stars.ToString();
        using UnityWebRequest www = UnityWebRequest.Put(url, json);
        www.method = UnityWebRequest.kHttpVerbPUT;
        www.SetRequestHeader("Content-type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public IEnumerator UpdateOrderMissionCallback(string userName, int key, int percentage)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        
        string url = page + "/api/game/updateOrderMission/" + userName + "/" + key.ToString() + "/" + percentage.ToString();
        using UnityWebRequest www = UnityWebRequest.Put(url, json);
        www.method = UnityWebRequest.kHttpVerbPUT;
        www.SetRequestHeader("Content-type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    // * Special missions
    public IEnumerator UpdateSpecialStructureCallback(string userName, int status)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        
        string url = page + "/api/game/updateSpecialStructure/" + userName + "/" + status.ToString();
        using UnityWebRequest www = UnityWebRequest.Put(url, json);
        www.method = UnityWebRequest.kHttpVerbPUT;
        www.SetRequestHeader("Content-type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public IEnumerator UpdateSpecialMissionCallback(string userName, int coins)
    {
        Player p = new Player(); 
        string json = JsonConvert.SerializeObject(p);
        
        string url = page + "/api/game/updateSpecialMission/" + userName + "/" + coins.ToString();
        using UnityWebRequest www = UnityWebRequest.Put(url, json);
        www.method = UnityWebRequest.kHttpVerbPUT;
        www.SetRequestHeader("Content-type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }


}
