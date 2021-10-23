using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverTimeOutside : MonoBehaviour
{
    public ButtonControl buttonControl;

    // Update is called once per frame
    void Update()
    {
        buttonControl.TimeForDeliver();
        buttonControl.UnlockOrderLevel();
    }
}
