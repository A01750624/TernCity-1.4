using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinIconPos : MonoBehaviour
{
    public Transform coin;

    // Update is called once per frame
    void Update()
    {
        Vector3 posInMap = coin.position;
        posInMap.y = 50f;
        transform.position = posInMap;
    }
}
