using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// * Data base connection
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;

public class QuizManager : MonoBehaviour
{
    public int indexMission; 
    public List<QuestionsAnswers> QA;
    public GameObject[] options;
    public Sprite[] spriteOptions;
    public AudioSource audioS; 

    public QuizTimer timer;

    public GameObject QuizPanel;
    public GameObject GameFinalPanel;

    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI resourceSteelText;

    public Image firstS;
    public Image secondS;
    public Image thirdS;
    public Image steelR;

    int TotalQuestions = 0;
    int score;
    int steel;
    public int stars;
    public int steelRes;

    [HideInInspector]
    public int currentQuestion;
    [HideInInspector]
    public bool firstStar;
    [HideInInspector]
    public bool secondStar;
    [HideInInspector]
    public bool thirdStar;

    // * Data base connection
    string userName;
    PutRequests PR = new PutRequests();


    public void Start()
    {
        // * Data base connection
        userName = PlayerPrefs.GetString("userName");

        audioS.volume = 1f;
        indexMission = PlayerPrefs.GetInt("CurrentMission");
        if (indexMission >= 1000)
        {
            steelRes = PlayerPrefs.GetInt("MissionSteel");
            steel = PlayerPrefs.GetInt("playerSteel");
            GameFinalPanel.transform.GetChild(2).GetComponent<Button>().interactable = false;

            // * Data base connection
            StartCoroutine(JoinQuestionAnswer(1));  
        }
        
        if (indexMission < 1000)
        {
            GameFinalPanel.transform.GetChild(2).GetComponent<Button>().interactable = true;
            stars = 0;
            firstStar = false;
            firstS.GetComponent<Image>().color = new Color32(0, 0, 0, 50);
            secondStar = false;
            secondS.GetComponent<Image>().color = new Color32(0, 0, 0, 50);
            thirdStar = true;
            thirdS.GetComponent<Image>().color = new Color32(0, 0, 0, 50);
            steelR.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            resourceSteelText.GetComponent<TextMeshProUGUI>().text = "";

            // * Data base connection
            StartCoroutine(JoinQuestionAnswer(0));
        }
        else
        {
            firstS.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            secondS.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            thirdS.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            steelR.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void returnGame()
    {
        SceneManager.LoadScene("PruebaCity");
        if (stars > PlayerPrefs.GetInt("Mission" + indexMission) && indexMission < 1000)
        {
            PlayerPrefs.SetInt("Mission" + indexMission, stars);

            // * Data base
            StartCoroutine(PR.UpdateStarsCallback(userName, indexMission, stars));
        }
        if (indexMission >= 1000)
        {
            PlayerPrefs.SetInt("Mission" + indexMission, steelRes);
            PlayerPrefs.SetInt("MissionDeliveryPer" + indexMission, (score * 100 / TotalQuestions));
            steel = steel + steelRes;
            PlayerPrefs.SetInt("playerSteel", steel);

            // * Data base
            int DBpercentage =  (score * 100 / TotalQuestions);
            StartCoroutine(PR.UpdateOrderMissionCallback(userName, indexMission, DBpercentage));
        }
    }

    void GameOver()
    {
        QuizPanel.SetActive(false);
        GameFinalPanel.SetActive(true);
        timer.timerIsRunning = false;
        audioS.volume = 0.3f;
        FindObjectOfType<AudioManager>().Play("Complete");
        if (indexMission < 1000)
        {
            ScoreText.text = "Resultado: " + score + " / " + TotalQuestions;
            if ((float)score >= ((float)TotalQuestions / 2))
            {
                firstStar = true;
                stars++;
                Debug.Log("First Star");
            }

            if (score == TotalQuestions)
            {
                secondStar = true;
                stars++;
                Debug.Log("Second Star");
            }

            if (thirdStar == true)
            {
                stars++;
                Debug.Log("Third Star");
            }

            if (stars == 1)
            {
                firstS.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            if (stars == 2)
            {
                firstS.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                secondS.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            if (stars == 3)
            {
                firstS.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                secondS.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                thirdS.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }

        }
        else
        {
            ScoreText.text = "Resultado: " + ((score * 100 / TotalQuestions)) + "%";
            if (score == TotalQuestions)
            {
                steelRes = steelRes * 2;
            }
            if ((float)score >= ((float)TotalQuestions * 0.85) && (float)score <= ((float)TotalQuestions * 0.99))
            {
                steelRes = (int)(steelRes * 1.5);
            }
            if ((float)score >= ((float)TotalQuestions * 0.70) && (float)score <= ((float)TotalQuestions * 0.84))
            {
                
            }
            if ((float)score >= ((float)TotalQuestions * 0.60) && (float)score <= ((float)TotalQuestions * 0.69))
            {
                steelRes = (int)(steelRes * -1.5);
            }
            if ((float)score < ((float)TotalQuestions * 0.60) && (float)score > ((float)TotalQuestions * 0.30))
            {
                steelRes = steelRes * -2;
            }
            if ((float)score <= ((float)TotalQuestions * 0.30))
            {
                steelRes = steelRes * -3;
            }
            resourceSteelText.GetComponent<TextMeshProUGUI>().text = steelRes.ToString();

        }
    }

    public void correct()
    {
        score += 1;
        QA.RemoveAt(currentQuestion);
        StartCoroutine(waitForNext());
    }

    public void wrong()
    {
        PostRequests PR = new PostRequests();
        // Mandar error a la base de datos ##################################################################################################################################################################
        options[QA[currentQuestion].CorrectAnswer - 1].GetComponent<Image>().sprite = spriteOptions[3];
        
        StartCoroutine(PR.AddErrorLogCallback(userName, QA[currentQuestion].id));

        QA.RemoveAt(currentQuestion);
        StartCoroutine(waitForNext());
    }

    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(2);
        generateQuestion();
    }

    void setAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].SetActive(false);
        }
        for (int i = 0; i < QA[currentQuestion].Answers.Length; i++)
        {
            options[i].SetActive(true);
            options[i].GetComponent<Button>().interactable = true;
            options[i].GetComponent<Image>().sprite = spriteOptions[0];
            options[i].GetComponent<Answers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QA[currentQuestion].Answers[i];

            if (QA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<Answers>().isCorrect = true;
            }
        }
        timer.timerIsRunning = true;
        timer.timeRemaining = 15;
    }

    void generateQuestion()
    {
        if (QA.Count > 0)
        {
            currentQuestion = Random.Range(0, QA.Count);
            QuestionText.text = QA[currentQuestion].Question;
            setAnswers();
        }
        else
        {
            Debug.Log("Out of questions");
            GameOver();
            // MANDAR LA ECONOMIA DE LA CIUDAD
        }
    }

    // * Data base 
    IEnumerator JoinQuestionAnswer(int Type)
    {
        string urlQ = "http://localhost:4000/api/game/getQuestion/" + userName + "/" + Type.ToString();
        // Questions
        UnityWebRequest webQ = UnityWebRequest.Get(urlQ);
        webQ.useHttpContinue = false;

        Debug.Log("Webrequest");
        yield return webQ.SendWebRequest();

        if (webQ.isNetworkError || webQ.isHttpError)
        {
            Debug.Log(urlQ);
            Debug.Log("Error API: " + webQ.error);
        }
        else
        {
        
            // It is not necesarry to take out the [] because I need a type List
            string reJsonQ =  webQ.downloadHandler.text;

            List<DBResponse.Question> questions  = JsonConvert.DeserializeObject<List<DBResponse.Question>>(reJsonQ);
            /* Debug.Log(questions[0].question);
            Debug.Log(questions[1].question);
            Debug.Log(questions[2].question); */

            Debug.Log("Questions" + questions.Count);

            // Get Answers
            for(int iQC = 0; iQC < questions.Count; iQC++)
            {
                string urlA = "http://localhost:4000/api/game/getAnswers/" + questions[iQC].questionID.ToString();

                UnityWebRequest webA = UnityWebRequest.Get(urlA);
                webA.useHttpContinue = false;

                //Debug.Log(Path.Combine(Api, userName));
                Debug.Log("Webrequest");
                yield return webA.SendWebRequest();

                if (webA.isNetworkError || webA.isHttpError)
                {
                    Debug.Log("Error API: " + webA.error);
                }
                else
                {

                    // It is not necesarry to take out the [] because I need a type List
                    string reJsonA =  webA.downloadHandler.text;
                    List<DBResponse.Answer> answers = JsonConvert.DeserializeObject<List<DBResponse.Answer>>(reJsonA);

                    QuestionsAnswers qaObject = new QuestionsAnswers {Question = questions[iQC].question, id = questions[iQC].questionID};

                    // Initialiezes the array for Answers

                    // Total of answers
                    int Decremenet = 0;
                    for(int iC = 0; iC < answers.Count; iC++)
                    {
                        if(answers[iC].answer == "")
                        {
                            Decremenet++;
                        }
                    }

                    qaObject.Answers = new string[answers.Count - Decremenet];

                    
                    // Join everything
                    for(int iAC = 0; iAC < qaObject.Answers.Length; iAC++)
                    {

                        if(answers[iAC].answer != "")
                        {
                            qaObject.Answers[iAC] = answers[iAC].answer;

                            if(answers[iAC].status == true)
                            {
                                qaObject.CorrectAnswer = iAC + 1;
                            }
                        }
                        
                    }

                    // Adds the question and answer to the main objetc
                    
                    QA.Add(qaObject);
                }
            }

            TotalQuestions = QA.Count;
            QuizPanel.SetActive(true);
            GameFinalPanel.SetActive(false);
            generateQuestion();
        }
        
    }
    


}
