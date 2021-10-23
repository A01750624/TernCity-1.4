using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterResult : MonoBehaviour
{
    public SpecialMissionManager specialMission; 
    public GameObject[] character;
    private Animator animator, animatorF;
    int last;

    // Start is called before the first frame update
    void Start()
    {
        last = PlayerPrefs.GetInt("SelectedCharacter");
        animator = character[last].transform.GetChild(0).GetComponent<Animator>();
        animatorF = character[last].GetComponent<Animator>();
        animator.SetInteger("coins", specialMission.coinCounter);
        animatorF.SetInteger("coins", specialMission.coinCounter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
