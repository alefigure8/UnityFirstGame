using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isWalking;
    private Vector3 lastInteractDir;
    [SerializeField] private float speed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float interactDistance = 2f;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        bool canInteract = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit rayCastHit, interactDistance, countersLayerMask);

        if (canInteract)
        {
            if (rayCastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //Has a clear counter
                clearCounter.Interact();
            }
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float interactDistance = 2f;

        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        bool canInteract = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit rayCastHit, interactDistance, countersLayerMask);
        
        if(canInteract)
        {
            if (rayCastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //Has a clear counter
                //clearCounter.Interact();
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // Collider
        float moveDistance = Time.deltaTime * speed;
        float playerRadius = .7f;
        float playerheight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Move only on the X axis
                moveDir = moveDirX;
            }
            else
            {
                // Move only on the Z axis
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //Can't move in any direction
                }
            }
        }

        // Move the player
        if (canMove)
            transform.position += moveDir * moveDistance;

        // Set the walking state
        isWalking = moveDir != Vector3.zero;

        // Rotate the player to face the direction they are moving
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
}
