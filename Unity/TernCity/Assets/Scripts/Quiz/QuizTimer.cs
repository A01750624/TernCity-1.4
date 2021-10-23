using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizTimer : MonoBehaviour
{
    [HideInInspector]
    public float timeRemaining = 15;
    [HideInInspector]
    public bool timerIsRunning = true;
    private int timeR;
    public TextMeshProUGUI timerText;
    public QuizManager quizManager;


    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timeR = Mathf.RoundToInt(timeRemaining);
                timerText.text = timeR.ToString();
            }
            else
            {
                Debug.Log("Time has run out, Incorrect answer");
                timeRemaining = 0;
                timerIsRunning = false;
                quizManager.thirdStar = false;
                for (int i = 0; i < quizManager.QA[quizManager.currentQuestion].Answers.Length; i++)
                {
                    quizManager.options[i].GetComponent<Button>().interactable = false;
                }
                FindObjectOfType<AudioManager>().Play("Wrong");
                quizManager.wrong();
            }
        }
    }
}
