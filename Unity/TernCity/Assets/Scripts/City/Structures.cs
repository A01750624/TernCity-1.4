using UnityEngine;

[System.Serializable]
public class Structures 
{
    public GameObject structure;
    public GameObject beforeStructure;
    public string nameStructure;
    public string description; 
    public int cantSteelGen;
    public int cost;
    public int levelToUnlock;
    public bool unlocked = false;
    public bool update = true;
}
