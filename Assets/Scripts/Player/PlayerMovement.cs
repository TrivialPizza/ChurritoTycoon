using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed, runSpeed, rotationSpeed;
    public bool canMove;
    public Transform cameraAim;

    private Vector3 movementVector;
    private float speed;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        movementVector = Vector3.zero;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Walk();
            Run();
        }

        Gravity();
    }

    void Walk() 
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");

        movementVector = movementVector.normalized;

        movementVector = cameraAim.TransformDirection(movementVector);

        characterController.Move(movementVector*speed*Time.deltaTime);
    }

    void Run()
    {
        if (Input.GetAxis("Run") > 0f)
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }

    void AlignPlayer()
    {
        if (characterController.velocity.magnitude > 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementVector), rotationSpeed * Time.deltaTime);
        }
    }

    void Gravity()
    {
        characterController.Move(new Vector3(0F, -4f * Time.deltaTime, 0f));
    }
}
