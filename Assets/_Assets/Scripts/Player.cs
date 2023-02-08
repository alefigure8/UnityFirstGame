using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isWalking;
    [SerializeField] private float speed = 7f;

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        // Normalize the input vector so that the player moves at the same speed diagonally as they do horizontally or vertically
        inputVector = inputVector.normalized;

        // Move the player
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * Time.deltaTime * speed;

        // Set the walking state
        isWalking = moveDir != Vector3.zero;

        // Rotate the player to face the direction they are moving
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
