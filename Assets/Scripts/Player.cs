using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isWalking;
    [SerializeField] private float speed = 7f;
    [SerializeField] private GameInput gameInput;

    private void Update()
    {
        // Get the input vector from the GameInput script
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

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
