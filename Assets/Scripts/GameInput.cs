using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        // InputActions
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // Subscribe to the Interact action
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += interactAlternate_performed;
    }
    
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void interactAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Normalize the input vector so that the player moves at the same speed diagonally as they do horizontally or vertically
        inputVector = inputVector.normalized;

        return inputVector;
    }
}