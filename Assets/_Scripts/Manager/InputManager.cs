using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private InputSystem_Actions playerControls;

    private void Awake()
    {
        if (instance == null) instance = this;

        playerControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public bool GetSpaceKeyPress()
    {
        return playerControls.Player.Jump.IsPressed();
    }

    public bool GetSacrificeKeyPress()
    {
        return playerControls.Player.Interact.WasPressedThisFrame();
    }

    public bool GetSwitchKeyPress()
    {
        return playerControls.Player.Switch.WasPressedThisFrame();
    }

    public bool GetPauseKeyPress()
    {
        return playerControls.Player.Pause.WasPressedThisFrame();
    }    
}