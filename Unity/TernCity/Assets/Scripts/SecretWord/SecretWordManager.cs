using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SecretWordManager : MonoBehaviour
{
    public GameObject[] textWords;
    public GameObject[] collectables; 
    public TextMeshProUGUI contLettersView; 
    public SecretWord[] secretWords;  // * 0 es la palabra
    public CityManager city;
    public SelectorManager character; 

    public int word;
    public int[] letterWordIndex = new int[5];  
    public int letter;
    public int letterCounter;
    public int letterIndex = 0;
    public int wordIndex = 0; 
    public int stars = 0;

    // * Data base connection
    public string userName;
    PutRequests PR = new PutRequests();

    // Start is called before the first frame updatefpl
    void Start()
    {
        // * Data base connection
        wordIndex = PlayerPrefs.GetInt("playerSecretWord");
        letterIndex = PlayerPrefs.GetInt("playerSecretWordProgress");
        userName = PlayerPrefs.GetString("userName");

        SetCollectibles(wordIndex);

        getInfoWords();
        for (int i = 0; i < textWords.Length; i++)
        {
            textWords[i].transform.GetChild(0).gameObject.SetActive(false);
            textWords[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        stars = city.stars;
        letterCounter = Mathf.FloorToInt(stars / 3);
        if (wordIndex == 0)
        {
            contLettersView.GetComponent<TextMeshProUGUI>().text = letterIndex + "/" + (letterWordIndex[wordIndex]);
        }
        else
        {
            contLettersView.GetComponent<TextMeshProUGUI>().text = letterIndex + "/" + (letterWordIndex[wordIndex] - letterWordIndex[wordIndex - 1]).ToString();
        }
        setIndex();
        showWord();
        showCollectable();
    }

    public void showWord()
    {
        for (int i = 0; i < secretWords[wordIndex].words.Length; i++)
        {
            textWords[i].SetActive(true);
        }
        for (int i = secretWords[wordIndex].words.Length; i < textWords.Length; i++)
        {
            textWords[i].SetActive(false);
        }
        for (int i = 0; i < letterIndex; i++)
        {
            textWords[i].transform.GetChild(0).gameObject.SetActive(true);
            textWords[i].transform.GetChild(1).gameObject.SetActive(false);
            textWords[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = secretWords[wordIndex].words[i].ToString().ToUpper();
        }
        for (int i = letterIndex; i < textWords.Length; i++)
        {
            textWords[i].transform.GetChild(0).gameObject.SetActive(false);
            textWords[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void getInfoWords()
    {
        for (int i = 0; i < secretWords.Length; i++)
        {
            for (int m = 0; m <= i; m++)
            {
                letterWordIndex[i] += secretWords[m].words.Length;
            }
        }
    }

    public void setIndex()
    {
        if (wordIndex == 0)
        {
            word = letterWordIndex[0];
            character.numberOfCharacters = 3;
        }
        if (wordIndex == 1)
        {
            word = letterWordIndex[1];
            character.numberOfCharacters = 4;
        }
        if (wordIndex == 2)
        {
            word = letterWordIndex[2];
            character.numberOfCharacters = 5;
        }
        if (wordIndex == 3)
        {
            word = letterWordIndex[3];
            character.numberOfCharacters = 6;
        }
        if (wordIndex == 4)
        {
            word = letterWordIndex[4];
            character.numberOfCharacters = 7;
        }

        if (letterCounter > word)
        {
            secretWords[wordIndex].isDiscovered = true;
            letterIndex = 0;
            wordIndex += 1;

            // * Data base
            Debug.Log("Word index " + wordIndex);

            Debug.Log("AvatarIndex " + character.numberOfCharacters);

            StartCoroutine(PR.UpdateSecretCodeProgressCallback(userName, letterIndex));

            StartCoroutine(PR.UpdateSecretCodeCallback(userName, wordIndex));

            StartCoroutine(PR.UpdateAvatarIndexCallback(userName, 3 + wordIndex));

            PlayerPrefs.SetInt("avatarIndex", 3 + wordIndex);

        }
        if (letterCounter == 30 && word == 30)
        {
            secretWords[wordIndex].isDiscovered = true;  // ** Mostrar los personajes en los logros
            character.numberOfCharacters = 8;
        }
        if (letterCounter <= word)
        {
            if (wordIndex == 0)
            {
                // * Data base
                if (letterIndex != letterCounter)
                {
                    StartCoroutine(PR.UpdateSecretCodeProgressCallback(userName, letterCounter));
                }

                letterIndex = letterCounter;
            }
            else
            {
                // * Data base
                if (letterIndex != letterCounter - letterWordIndex[wordIndex - 1])
                {   // * leter Counter: total de letras desbloqueadas con las estrellas
                    // * Letter word index 0 logitud de la 1, 1 la longitud de la 1 2
                    Debug.Log("letterWordIndex " + wordIndex);
                    Debug.Log("letterCounter " + letterCounter);

                    // * WordIndex 3
                    // * 

                    StartCoroutine(PR.UpdateSecretCodeProgressCallback(userName, letterCounter - letterWordIndex[wordIndex - 1]));
                }

                letterIndex = letterCounter - letterWordIndex[wordIndex - 1];


                if (letterIndex < 0)
                {
                    letterIndex = 0;
                }
            }
        }
    }

    public void showCollectable()
    {
        for (int i = 0; i < secretWords.Length; i++)
        {
            if (secretWords[i].isDiscovered == true)
            {
                collectables[i].transform.GetChild(1).gameObject.SetActive(false);
                collectables[i].transform.GetChild(2).gameObject.SetActive(true);
                collectables[i].transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = secretWords[i].completeWord.ToUpper();
            }
            else
            {
                collectables[i].transform.GetChild(1).gameObject.SetActive(true);
                collectables[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    // * Data base connection
    public void SetCollectibles(int TotalWords)
    {
        for (int iC = 0; iC < TotalWords; iC++)
        {
            Debug.Log("Personaje Desbloqueados");
            secretWords[iC].isDiscovered = true;
        }

    }
}
