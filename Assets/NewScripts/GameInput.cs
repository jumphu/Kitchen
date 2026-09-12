using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractionAlternativeAction;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractionAlternative.performed += InteractionAlternative_performed;


        {
            Debug.Log("Interact Pressed");
        }
        ;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAction != null)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

    }

    private void InteractionAlternative_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractionAlternativeAction != null)
        {
            OnInteractionAlternativeAction?.Invoke(this, EventArgs.Empty);
        }
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        playerInputActions.Player.Move.ReadValue<Vector2>();

        // Debug.Log(inputVector);

        return inputVector.normalized;
    }
}
