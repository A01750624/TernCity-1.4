using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;

public class SpecialMissionManager : MonoBehaviour
{
    public GameObject[] coins;
    public GameObject player;
    public GameObject cam;
    public AudioSource audioS; 
    public GameObject[] UIs;
    public SpecialMissionTimer timer;
    public int coinCounter = 0;
    public bool gameFinished = false; 
    public TextMeshProUGUI coinCounterText;

    // * Data base connection 
    PutRequests PR = new PutRequests(); 
    string userName;

    // Start is called before the first frame update
    void Start()
    {
        userName = PlayerPrefs.GetString("userName");
        UIs[0].SetActive(true); // Intro
        UIs[1].SetActive(false); // MainUI (Timer y monedas)
        UIs[2].SetActive(false); // Resultado final
        UIs[3].SetActive(false); // Minimap
        audioS.volume = 1f;
        player.GetComponent<ThirdPersonMovement>().enabled = false;
        cam.GetComponent<CinemachineFreeLook>().enabled = false;
        timer.timerIsRunning = false;
        int i = 0;
        while (i < 5)
        {
            int indexCoins= Random.Range(0, coins.Length-1);
            if (!coins[indexCoins].activeSelf)
            {
                coins[indexCoins].SetActive(true);
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinCounterText.GetComponent<TextMeshProUGUI>().text = coinCounter + " / 5";
        if (coinCounter == 5)
        {
            gameFinished = true; 

        }
        gameOver();
    }

    void gameOver()
    {
        if (gameFinished == true)
        {
            player.GetComponent<ThirdPersonMovement>().enabled = false;
            cam.GetComponent<CinemachineFreeLook>().enabled = false;
            timer.timerIsRunning = false;
            FindObjectOfType<AudioManager>().Play("Complete");
            audioS.volume = 0.3f;
            //Debug.Log("Fin de juego");
            UIs[0].SetActive(false);
            UIs[1].SetActive(false);
            UIs[2].SetActive(true);
            UIs[3].SetActive(false);
            UIs[2].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = coinCounter + "/5";
            UIs[2].transform.GetChild(1).GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "x " + ((float)(1 + (coinCounter * 0.2))).ToString();
        }
    }

    public void startGame()
    {
        player.GetComponent<ThirdPersonMovement>().enabled = true;
        cam.GetComponent<CinemachineFreeLook>().enabled = true;
        timer.timerIsRunning = true;
        UIs[0].SetActive(false);
        UIs[1].SetActive(true);
        UIs[2].SetActive(false);
        UIs[3].SetActive(true);
    }

    public void returnGame()
    {
        PlayerPrefs.SetInt("steelPercentaje", (coinCounter));

        // * Data base connection
        StartCoroutine(PR.UpdateSpecialMissionCallback(userName, coinCounter));
        StartCoroutine(PR.UpdateSpecialStructureCallback(userName, 1));

        Debug.Log("Coin" + coinCounter);
        Debug.Log("Status 1" );
        PlayerPrefs.SetString("SpecialMissionStatus", "true");
        SceneManager.LoadScene("PruebaCity");
    }
}
