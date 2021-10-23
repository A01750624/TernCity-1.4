using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answers : MonoBehaviour {
    public bool isCorrect = false;
    public QuizManager quizManager;
    public QuizTimer timer;

    //public void Start() {
    //    sprite = GetComponent<Image>().sprite;
    //}

    public void Answer()
    {
        for (int i = 0; i < quizManager.options.Length; i++)
        {
            quizManager.options[i].GetComponent<Button>().interactable = false;
        }
        timer.timerIsRunning = false;
        if (isCorrect)
        {
            GetComponent<Image>().sprite = quizManager.spriteOptions[1]; /// Assets/UI/Simple UI/Simple UI/PNG/UI_Button_Standard_Green.png
            FindObjectOfType<AudioManager>().Play("Correct");
            Debug.Log("Correct Answer");
            quizManager.correct();
        }
        else
        {
            GetComponent<Image>().sprite = quizManager.spriteOptions[2];
            FindObjectOfType<AudioManager>().Play("Wrong");
            Debug.Log("Incorrect Answer");
            quizManager.wrong();
        }
    }
}
