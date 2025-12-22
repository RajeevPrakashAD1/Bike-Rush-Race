using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInputHandler : MonoBehaviour
{
    public float SteeringInput;
    public float ThrottleInput;
    public float BrakeInput;

    public void OnSteering(InputValue value)
    {
        SteeringInput = value.Get<float>();
        Debug.Log("Steering: " + SteeringInput);
    }

    public void OnThrottle(InputValue value)
    {
        ThrottleInput = value.Get<float>();
    }

    public void OnBrake(InputValue value)
    {
        BrakeInput = value.Get<float>();
    }
}