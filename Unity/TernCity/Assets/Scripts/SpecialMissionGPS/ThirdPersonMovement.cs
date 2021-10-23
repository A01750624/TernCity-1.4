using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public GameObject[] character;
    public SpecialMissionTimer timer;
    private Animator animator;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public int last;
    float turnSmoothVelocity;

    private void Start()
    {
        last = PlayerPrefs.GetInt("SelectedCharacter");
        animator = character[last].GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        var charSpeed = 0.0f;
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0f) * Vector3.forward; 
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            charSpeed = Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z);
        }
        animator.SetFloat("speed", charSpeed);
        Vector3 gravity = Quaternion.Euler(0f, 9.8f, 0f) * Vector3.down;
        controller.Move(gravity.normalized * speed * Time.deltaTime);
        if (timer.timerIsRunning == false)
        {
            animator.SetFloat("speed", 0f);
        }
    }
}
