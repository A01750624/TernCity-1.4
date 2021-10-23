using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private const int rotate = 90;
    public SpecialMissionManager GPSMission; 
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Coin");
        Destroy(this.gameObject);
        GPSMission.coinCounter++; 
    }

    private void Update()
    {
        float degreesToRotate = rotate * Time.deltaTime;
        transform.Rotate(degreesToRotate, 0, 0);
    }
}
