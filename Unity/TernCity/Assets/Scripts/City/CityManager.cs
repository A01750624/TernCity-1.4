using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class CityManager : MonoBehaviour
{
    public Structures[] structures;
    public Streets[] street;
    public GameObject[] upgradeButtons;
    public GameObject[] levels;
    public TextMeshProUGUI steelText;
    public TextMeshProUGUI starText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI steelGenText;
    public CityTimer timer;
    public UIManager UIQuizManager; 
    public int steel;
    public int stars;
    public int level;
    public int steelGen;
    int contStructure = 0;
    bool specialMissionStatus = false;
    //public float startTime;
    //public float barTime;

    // * Data base connection
    PutRequests PR = new PutRequests();
    string userName;
    int levelAuxiliar = -1;

    List<DBResponse.BoughtStructure> BSPlayer;

    void Start()
    {
        // * Data base connection
        userName = PlayerPrefs.GetString("userName");
        // ? showInfo(PlayerSettings.BSPlayer);
       // SetStructure();
        setStructures(PlayerSettings.BSPlayer);


        steel = PlayerPrefs.GetInt("playerSteel");
        stars = PlayerPrefs.GetInt("playerStars");
        Debug.Log("Stars" + stars.ToString());
        level = PlayerPrefs.GetInt("playerLevel");
        setSteelGen();
        setInfoButtons();

    }

    // Update is called once per frame
    void Update()
    {
        //startTime = Time.time;

        if (steel < 0)
        {
            steelText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            steelText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        steelText.GetComponent<TextMeshProUGUI>().text = steel.ToString();
        starText.GetComponent<TextMeshProUGUI>().text = stars.ToString();
        levelText.GetComponent<TextMeshProUGUI>().text = level.ToString();
        steelGenText.GetComponent<TextMeshProUGUI>().text = steelGen.ToString() + " / 15s";
        if (timer.timerIsRunning == false)
        {
            timer.timerIsRunning = true;
            timer.timeRemaining = 15; 
        }
        setUpdate();
        unlockButtons();
        stars = UIQuizManager.stars;
        setLevel();
        setStreet();
        specialMissionStructure();
    }

    void setLevel()
    {
        if (stars >= levels[0].GetComponent<LevelSelector>().requiredStars && stars < levels[1].GetComponent<LevelSelector>().requiredStars)
        {
            level = 0;
        }
        if (stars >= levels[1].GetComponent<LevelSelector>().requiredStars && stars < levels[2].GetComponent<LevelSelector>().requiredStars)
        {
            level = 1;
        }
        if (stars >= levels[2].GetComponent<LevelSelector>().requiredStars && stars < levels[3].GetComponent<LevelSelector>().requiredStars)
        {
            level = 2;
        }
        if (stars >= levels[3].GetComponent<LevelSelector>().requiredStars && stars < levels[4].GetComponent<LevelSelector>().requiredStars)
        {
            level = 3;
        }
        if (stars >= levels[4].GetComponent<LevelSelector>().requiredStars && stars < levels[5].GetComponent<LevelSelector>().requiredStars)
        {
            level = 4;
        }
        if (stars >= levels[5].GetComponent<LevelSelector>().requiredStars && stars < levels[6].GetComponent<LevelSelector>().requiredStars)
        {
            level = 5;
        }
        if (stars >= levels[6].GetComponent<LevelSelector>().requiredStars && stars < levels[7].GetComponent<LevelSelector>().requiredStars)
        {
            level = 6;
        }
        if (stars >= levels[7].GetComponent<LevelSelector>().requiredStars && stars < levels[8].GetComponent<LevelSelector>().requiredStars)
        {
            level = 7;
        }
        if (stars >= levels[8].GetComponent<LevelSelector>().requiredStars && stars < levels[9].GetComponent<LevelSelector>().requiredStars)
        {
            level = 8;
        }
        if (stars >= levels[9].GetComponent<LevelSelector>().requiredStars && stars < levels[10].GetComponent<LevelSelector>().requiredStars)
        {
            level = 9;
        }
        if (stars >= levels[10].GetComponent<LevelSelector>().requiredStars)
        {
            level = 10; 
        }

        // * Data base connection
        if(levelAuxiliar != level)
        {
            StartCoroutine(PR.UpdateLevelCallback(userName, level));
            levelAuxiliar = level;
        }
    }

    void setStreet()
    {
        for (int i = 0; i < level; i++)
        {
            street[i].streetStructure.SetActive(true);
            street[i].beforeStreetStructure.SetActive(false);
        }
    }

    void specialMissionStructure()
    { 
        if (PlayerPrefs.GetString("SpecialMissionStatus") == "true")
        {
            specialMissionStatus = true;
            structures[30].cantSteelGen = PlayerPrefs.GetInt("steelPercentaje");
        }
        if ((specialMissionStatus == true) && (!structures[30].structure.activeSelf))
        {
            structures[30].structure.SetActive(true);
            structures[30].beforeStructure.SetActive(false);
            structures[30].unlocked = true;
            steelGen = (int)(steelGen * ((1+(structures[30].cantSteelGen*0.2))));
        }
        if((structures[30].unlocked == true) && (contStructure == 0))
        {
            for (int i = 0; i < structures.Length-1; i++)
            {
                structures[i].cantSteelGen = (int)(structures[i].cantSteelGen * (1 + (structures[30].cantSteelGen * 0.2)));
            }
            contStructure++; 
        }
    }

    void setSteelGen()
    {
        for (int i = 0; i < structures.Length; i++)
        {
            if(structures[i].unlocked == true)
            {
                steelGen += (structures[i].cantSteelGen/4);
                structures[i].beforeStructure.SetActive(false);
                structures[i].structure.SetActive(true);
            }
        }
        if (structures[11].unlocked == true)
        {
            structures[28].structure.SetActive(false);
        }
        if (structures[15].unlocked == true)
        {
            structures[5].structure.SetActive(false);
        }
    }

    void setInfoButtons()
    {
        for (int i = 0; i < structures.Length-3; i++)
        {
            upgradeButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = structures[i].nameStructure;
            upgradeButtons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = structures[i].description + ". (" + structures[i].cantSteelGen/4 + ")";
            upgradeButtons[i].transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level: " + structures[i].levelToUnlock.ToString();
            upgradeButtons[i].transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text =  structures[i].cost.ToString();
        }
    }

    void unlockButtons()
    {
        for (int i = 0; i < structures.Length-3; i++)
        {
            if (level >= structures[i].levelToUnlock)
            {
                if (steel >= structures[i].cost)
                {
                    if(structures[i].update == true)
                    {
                        upgradeButtons[i].transform.GetChild(6).gameObject.SetActive(false);
                        if (structures[i].unlocked == true)
                        {
                            upgradeButtons[i].transform.GetChild(4).gameObject.SetActive(false);
                            upgradeButtons[i].transform.GetChild(5).gameObject.SetActive(true);

                        }
                        else
                        {
                            upgradeButtons[i].transform.GetChild(4).gameObject.SetActive(true);
                            upgradeButtons[i].transform.GetChild(4).GetComponent<Button>().interactable = true; 
                            upgradeButtons[i].transform.GetChild(5).gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        upgradeButtons[i].transform.GetChild(4).gameObject.SetActive(false);
                        upgradeButtons[i].transform.GetChild(5).gameObject.SetActive(false);
                        upgradeButtons[i].transform.GetChild(6).gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (structures[i].update == true)
                    {
                        if (structures[i].unlocked == false)
                        {
                            upgradeButtons[i].transform.GetChild(4).gameObject.SetActive(true);
                            upgradeButtons[i].transform.GetChild(4).GetComponent<Button>().interactable = false;
                            upgradeButtons[i].transform.GetChild(5).gameObject.SetActive(false);
                            upgradeButtons[i].transform.GetChild(6).gameObject.SetActive(false);
                        }
                        else
                        {
                            upgradeButtons[i].transform.GetChild(4).gameObject.SetActive(false);
                            upgradeButtons[i].transform.GetChild(5).gameObject.SetActive(true);
                            upgradeButtons[i].transform.GetChild(6).gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        upgradeButtons[i].transform.GetChild(4).gameObject.SetActive(false);
                        upgradeButtons[i].transform.GetChild(5).gameObject.SetActive(false);
                        upgradeButtons[i].transform.GetChild(6).gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                upgradeButtons[i].transform.GetChild(4).gameObject.SetActive(false);
                upgradeButtons[i].transform.GetChild(5).gameObject.SetActive(false);
                upgradeButtons[i].transform.GetChild(6).gameObject.SetActive(true);
            }
        }
    }

    void setUpdate()
    {
        if (structures[1].unlocked == true)
        {
            structures[12].update = true;
        }
        else
        {
            structures[12].update = false;
        }

        if (structures[5].unlocked == true)
        {
            structures[15].update = true;
        }
        else
        {
            structures[15].update = false;
        }

        if (structures[14].unlocked == true)
        {
            structures[20].update = true;
        }
        else
        {
            structures[20].update = false;
        }

        if (structures[2].unlocked == true)
        {
            structures[7].update = true;
            if (structures[7].unlocked == true)
            {
                structures[10].update = true;
                if (structures[10].unlocked == true)
                {
                    structures[22].update = true;
                    if (structures[22].unlocked == true)
                    {
                        structures[25].update = true;
                        if (structures[25].unlocked == true)
                        {
                            structures[27].update = true;
                        }
                        else
                        {
                            structures[27].update = false;
                        }
                    }
                    else
                    {
                        structures[25].update = false;
                        structures[27].update = false;
                    }
                }
                else
                {
                    structures[22].update = false;
                    structures[25].update = false;
                    structures[27].update = false;
                }
            }
            else
            {
                structures[10].update = false;
                structures[22].update = false;
                structures[25].update = false;
                structures[27].update = false;
            }
        }
        else
        {
            structures[7].update = false;
            structures[10].update = false;
            structures[22].update = false;
            structures[25].update = false;
            structures[27].update = false;
        }
    }

    public void Hospital()
    {
        structures[0].structure.SetActive(true);
        structures[0].beforeStructure.SetActive(false);
        structures[0].unlocked = true;
        steel -= structures[0].cost;
        steelGen += (structures[0].cantSteelGen/4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[0].nameStructure));
        PlayerSettings.BSPlayer[0].status = true;
    }

    public void Neighbourhood1()
    {
        structures[1].structure.SetActive(true);
        structures[1].beforeStructure.SetActive(false);
        structures[1].unlocked = true;
        steel -= structures[1].cost;
        steelGen += (structures[1].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[1].nameStructure));
        PlayerSettings.BSPlayer[1].status = true;
    }

    public void FirstOffice()
    {
        structures[2].structure.SetActive(true);
        structures[2].beforeStructure.SetActive(false);
        structures[2].unlocked = true;
        steel -= structures[2].cost;
        steelGen += (structures[2].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[2].nameStructure));
        PlayerSettings.BSPlayer[2].status = true;
    }

    public void Mayor()
    {
        structures[3].structure.SetActive(true);
        structures[3].beforeStructure.SetActive(false);
        structures[3].unlocked = true;
        steel -= structures[3].cost;
        steelGen += (structures[3].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[3].nameStructure));
        PlayerSettings.BSPlayer[3].status = true;
    }

    public void SquareNeighbour()
    {
        structures[4].structure.SetActive(true);
        structures[4].beforeStructure.SetActive(false);
        structures[4].unlocked = true;
        steel -= structures[4].cost;
        steelGen += (structures[4].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[4].nameStructure));
        PlayerSettings.BSPlayer[4].status = true;
    }

    public void SmallDistributionFactory()
    {
        structures[5].structure.SetActive(true);
        structures[5].unlocked = true;
        steel -= structures[5].cost;
        steelGen += (structures[5].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[5].nameStructure));
        PlayerSettings.BSPlayer[5].status = true;
    }

    public void School()
    {
        structures[6].structure.SetActive(true);
        structures[6].beforeStructure.SetActive(false);
        structures[6].unlocked = true;
        steel -= structures[6].cost;
        steelGen += (structures[6].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[6].nameStructure));
        PlayerSettings.BSPlayer[6].status = true;
    }

    public void Office1()
    {
        structures[7].structure.SetActive(true);
        structures[7].beforeStructure.SetActive(false);
        structures[7].unlocked = true;
        steel -= structures[7].cost;
        steelGen += (structures[7].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[7].nameStructure));
        PlayerSettings.BSPlayer[7].status = true;
    }

    public void StorageFactory()
    {
        structures[8].structure.SetActive(true);
        structures[8].unlocked = true;
        steel -= structures[8].cost;
        steelGen += (structures[8].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[8].nameStructure));
        PlayerSettings.BSPlayer[8].status = true;
    }

    public void Park()
    {
        structures[9].structure.SetActive(true);
        structures[9].beforeStructure.SetActive(false);
        structures[9].unlocked = true;
        steel -= structures[9].cost;
        steelGen += (structures[9].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[9].nameStructure));
        PlayerSettings.BSPlayer[9].status = true;
    }

    public void Office2()
    {
        structures[10].structure.SetActive(true);
        structures[10].beforeStructure.SetActive(false);
        structures[10].unlocked = true;
        steel -= structures[10].cost;
        steelGen += (structures[10].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[10].nameStructure));
        PlayerSettings.BSPlayer[10].status = true;
    }

    public void BigFactoriesFactory()
    {
        structures[11].structure.SetActive(true);
        structures[11].beforeStructure.SetActive(false);
        structures[11].unlocked = true;
        steel -= structures[11].cost;
        steelGen += (structures[11].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[11].nameStructure));
        PlayerSettings.BSPlayer[11].status = true;
    }

    public void Neighbourhood2()
    {
        structures[12].structure.SetActive(true);
        structures[12].unlocked = true;
        steel -= structures[12].cost;
        steelGen += (structures[12].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[12].nameStructure));
        PlayerSettings.BSPlayer[12].status = true;
    }

    public void Police()
    {
        structures[13].structure.SetActive(true);
        structures[13].beforeStructure.SetActive(false);
        structures[13].unlocked = true;
        steel -= structures[13].cost;
        steelGen += (structures[13].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[13].nameStructure));
        PlayerSettings.BSPlayer[13].status = true;
    }

    public void Apartments1()
    {
        structures[14].structure.SetActive(true);
        structures[14].beforeStructure.SetActive(false);
        structures[14].unlocked = true;
        steel -= structures[14].cost;
        steelGen += (structures[14].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[14].nameStructure));
        PlayerSettings.BSPlayer[14].status = true;
    }

    public void BigDistributionFactory()
    {
        structures[15].structure.SetActive(true);
        structures[15].beforeStructure.SetActive(false);
        structures[15].unlocked = true;
        steel -= structures[15].cost;
        steelGen += (structures[15].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[15].nameStructure));
        PlayerSettings.BSPlayer[15].status = true;
    }

    public void VehiclesDistribution()
    {
        structures[16].structure.SetActive(true);
        structures[16].unlocked = true;
        steel -= structures[16].cost;
        steelGen += (structures[16].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[16].nameStructure));
        PlayerSettings.BSPlayer[16].status = true;
    }

    public void Square2()
    {
        structures[17].structure.SetActive(true);
        structures[17].beforeStructure.SetActive(false);
        structures[17].unlocked = true;
        steel -= structures[17].cost;
        steelGen += (structures[17].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[17].nameStructure));
        PlayerSettings.BSPlayer[17].status = true;
    }

    public void Fire()
    {
        structures[18].structure.SetActive(true);
        structures[18].beforeStructure.SetActive(false);
        structures[18].unlocked = true;
        steel -= structures[18].cost;
        steelGen += (structures[18].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[18].nameStructure));
        PlayerSettings.BSPlayer[18].status = true;
    }

    public void SolarPanelsFactory()
    {
        structures[19].structure.SetActive(true);
        structures[19].beforeStructure.SetActive(false);
        structures[19].unlocked = true;
        steel -= structures[19].cost;
        steelGen += (structures[19].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[19].nameStructure));
        PlayerSettings.BSPlayer[19].status = true;
    }

    public void Apartments2()
    {
        structures[20].structure.SetActive(true);
        structures[20].unlocked = true;
        steel -= structures[20].cost;
        steelGen += (structures[20].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[20].nameStructure));
        PlayerSettings.BSPlayer[20].status = true;
    }

    public void WindEnergy()
    {
        structures[21].structure.SetActive(true);
        structures[21].beforeStructure.SetActive(false);
        structures[21].unlocked = true;
        steel -= structures[21].cost;
        steelGen += (structures[21].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[21].nameStructure));
        PlayerSettings.BSPlayer[21].status = true;
    }

    public void Office3()
    {
        structures[22].structure.SetActive(true);
        structures[22].beforeStructure.SetActive(false);
        structures[22].unlocked = true;
        steel -= structures[22].cost;
        steelGen += (structures[22].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[22].nameStructure));
        PlayerSettings.BSPlayer[22].status = true;
    }

    public void Stadium()
    {
        structures[23].structure.SetActive(true);
        structures[23].beforeStructure.SetActive(false);
        structures[23].unlocked = true;
        steel -= structures[23].cost;
        steelGen += (structures[23].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[23].nameStructure));
        PlayerSettings.BSPlayer[23].status = true;
    }

    public void Hotel()
    {
        structures[24].structure.SetActive(true);
        structures[24].beforeStructure.SetActive(false);
        structures[24].unlocked = true;
        steel -= structures[24].cost;
        steelGen += (structures[24].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[24].nameStructure));
        PlayerSettings.BSPlayer[24].status = true;
    }

    public void Office4()
    {
        structures[25].structure.SetActive(true);
        structures[25].beforeStructure.SetActive(false);
        structures[25].unlocked = true;
        steel -= structures[25].cost;
        steelGen += (structures[25].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[25].nameStructure));
        PlayerSettings.BSPlayer[25].status = true;
    }

    public void NuclearEnergy()
    {
        structures[26].structure.SetActive(true);
        structures[26].beforeStructure.SetActive(false);
        structures[26].unlocked = true;
        steel -= structures[26].cost;
        steelGen += (structures[26].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[26].nameStructure));
        PlayerSettings.BSPlayer[26].status = true;
    }

    public void Office5()
    {
        structures[27].structure.SetActive(true);
        structures[27].beforeStructure.SetActive(false);
        structures[27].unlocked = true;
        steel -= structures[27].cost;
        steelGen += (structures[27].cantSteelGen / 4);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
        StartCoroutine(PR.UpdateBoughtStructureCallback(userName, structures[27].nameStructure));
        PlayerSettings.BSPlayer[27].status = true;
    }

    // ** Data base
    private bool setStructure(int index)
    {
        switch (index)
        {
            case 0: // Hospital
            case 1: // Neighbourhood1
            case 2: // FirstOffice
            case 3: // Mayor
            case 4: // SquareNeighbour
            case 6: // School
            case 7: // Office1
            case 9: // Park
            case 10: // Office2
            case 11: // BigFactoriesFactory
            case 13: // Police
            case 14: // Apartments1
            case 15: // BigDistributionFactory
            case 17: // Square2
            case 18: // Fire
            case 19: // SolarPanelsFactory
            case 21: // WindEnergy
            case 22: // Office3
            case 23: // Stadium
            case 24: // Hotel
            case 25: // Office4
            case 26: // NuclearEnergy
            case 27: // Office5
                structures[index].structure.SetActive(true);
                structures[index].beforeStructure.SetActive(false);
                structures[index].unlocked = true;
                return true;
                // ? break;
            case 5: // * SmallDistributionFactory
            case 8: // * StorageFactory
            case 12: // * Neighbourhood2
            case 16: // * VehiclesDistribution
            case 20: // * Apartments2
                structures[index].structure.SetActive(true);
                structures[index].unlocked = true;
                return true;
                // ? break;
            default:
                return false;
                // ? break;
        }
    }

    void setStructures(List<DBResponse.BoughtStructure> Structures)
    {
        Debug.Log(Structures.Count);
        for (int i = 0; i < Structures.Count; i++)
        {
            
            if (Structures[i].status)
            {
                if (!setStructure(i))
                {
                    Debug.Log("Error al cargar estructuras: Indice de estructura '" + i + ": " + Structures[i] + "' no reconocido");
                }
            }
        }
    }

    // void SetStructure()
    // {
    //     for(int iC = 0; iC <= 27; iC++)
    //     {
    //         structures[iC].structure.SetActive(false);
    //         structures[iC].beforeStructure.SetActive(true);
    //         structures[iC].unlocked = false;
            
    //     }
    //     structures[28].structure.SetActive(true);
    //     structures[28].beforeStructure.SetActive(false);
    //     structures[28].unlocked = true;

    //     structures[29].structure.SetActive(true);
    //     structures[29].beforeStructure.SetActive(true);
    //     structures[29].unlocked = true;
    // }

    void ShowtStructure()
    {
        for(int iC = 0; iC <= 27; iC++)
        {
            Debug.Log(structures[iC].nameStructure);
            
            
        }
        Debug.Log(structures[28].nameStructure);
        

        Debug.Log(structures[29].nameStructure);
    }

    public void DeliverSteel(int steelAmount) // Called by OrderControl
    {
        Debug.Log(steelAmount);
        if (steel >= steelAmount)
        {
            steel -= steelAmount;
            steelGen += 3;
        }
        else
        {
            steel = 0;
            steelGen -= 2;
        }
        Debug.Log("REMAINING: " + steel);

        // * Data base
        StartCoroutine(PR.UpdateEconomyCallback(userName, steel));
    }

    public void OrderMultiplier()
    {
        steelGen += 3;
    }
}