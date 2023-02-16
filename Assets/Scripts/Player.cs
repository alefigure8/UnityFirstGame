using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;
    
    private bool isWalking;
    private Vector3 lastInteractDir;
    BaseCounter selectedCounter;
    private kitchenObject kitchenObject;
    

    public static Player Instance { get; private set; }

    public event EventHandler<onSelectedCounterChangedEventArgs> onSelectedCounterChanged;
    public class onSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    private void Awake()
    {
        if (Instance != null)
            Debug.Log("More than one instance of Player found!");
        
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);  
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

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        bool canInteract = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit rayCastHit, interactDistance, countersLayerMask);

        if (canInteract)
        {
            if (rayCastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Has a clear counter
                if (baseCounter != selectedCounter)
                {
                    setSelectedCounter(baseCounter);
                }
            }
            else
            {
                // ir there is not a clear counter
                setSelectedCounter(null);
            }
        }
        else
        {
            // If there is not interaction
            setSelectedCounter(null);
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
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Move only on the X axis
                moveDir = moveDirX;
            }
            else
            {
                // Move only on the Z axis
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDirZ, moveDistance);

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

    private void setSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        onSelectedCounterChanged?.Invoke(this, new onSelectedCounterChangedEventArgs { 
            selectedCounter = this.selectedCounter
        });
    }

    public Transform getKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }

    public void SetKitchenObject(kitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public kitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
