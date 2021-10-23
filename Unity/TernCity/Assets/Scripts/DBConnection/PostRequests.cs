using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json;

public class PostRequests
{
    string page = "http://localhost:4000";
    

    public IEnumerator AddErrorLogCallback(string userName, int questionID)
    {
        DBResponse.Player p = new DBResponse.Player(); 
        string json = JsonConvert.SerializeObject(p);

        string url = page + "/api/game/addErrorLog/" + userName + "/" + questionID.ToString();


        using UnityWebRequest web = UnityWebRequest.Post(url, json);
        web.method = UnityWebRequest.kHttpVerbPOST;

        yield return web.SendWebRequest();

        if (web.isNetworkError || web.isHttpError)
        {
            Debug.Log(web.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }
    
}