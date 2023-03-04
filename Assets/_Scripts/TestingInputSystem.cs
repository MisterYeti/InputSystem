using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    Rigidbody rb;
    PlayerInput playerInput;
    PlayerInputActions playerInputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.UI.Submit.performed += Submit;

        #region Rebinding
        //playerInputActions.Player.Disable();
        //playerInputActions.Player.Jump.PerformInteractiveRebinding()
        //    .OnComplete(callback =>
        //    {
        //        Debug.Log(callback);
        //        callback.Dispose();
        //        playerInputActions.Player.Enable();
        //    })
        //    .Start(); 
        #endregion

    }

    private void Update()
    {
        #region SwitchMap
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            playerInput.SwitchCurrentActionMap("UI");
            playerInputActions.Player.Disable();
            playerInputActions.UI.Enable();
        }
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            playerInput.SwitchCurrentActionMap("Player");
            playerInputActions.UI.Disable();
            playerInputActions.Player.Enable();
        } 
        #endregion
    }

    private void FixedUpdate()
    {
        float speed = 1f;

        #region AxisMovement
        //float axisMovementHorizontal = playerInputActions.Player.AxisMovementHorizontal.ReadValue<float>();
        //rb.AddForce(new Vector3(axisMovementHorizontal, 0f, 0f) * speed,
        //     ForceMode.Force);
        #endregion

        #region Vector2Movement
        Vector2 movement = playerInputActions.Player.Movement.ReadValue<Vector2>();
        rb.AddForce(new Vector3(movement.x, 0f, movement.y) * speed,
             ForceMode.Force); 
        #endregion
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump " + context);
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    public void Submit(InputAction.CallbackContext context)
    {
        Debug.Log("Submit " + context);
    }
}
