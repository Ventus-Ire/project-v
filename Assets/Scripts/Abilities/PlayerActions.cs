using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] InputActionReference shootAction;

    //PlayerShooting
    [SerializeField] BulletSpawner spawner;

    private void Awake()
    {
        if(shootAction != null )
        {
            shootAction.action.Enable();
            shootAction.action.performed += Shoot;
            //shootAction.action.canceled += Shoot;
            InputSystem.onDeviceChange += OnDeviceChange;
        }
    }
    private void OnDestory()
    {
        shootAction.action.Disable();

        shootAction.action.performed -= Shoot;
        //shootAction.action.canceled -= Shoot;

        //Handler for multiple devices
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("I ACTUALLY SHOT SOMETHING");
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch(change)
        {
            case InputDeviceChange.Disconnected:
                shootAction.action.Disable();
                break;
            case InputDeviceChange.Reconnected:
                shootAction.action.Enable();
                break;
        }
    }
}
