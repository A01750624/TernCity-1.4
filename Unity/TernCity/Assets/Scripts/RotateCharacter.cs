using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour {
    public float speed = 20f;
    void Update() {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
