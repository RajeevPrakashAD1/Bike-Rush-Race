using UnityEngine;

public class PlayerInputRouter : MonoBehaviour
{
    [SerializeField] private GameplayInputHandler input;
    [SerializeField] private PlayerLaneController laneController;

    [Header("Steering Settings")]
    [Tooltip("How far the stick/tilt must move to trigger a lane change")]
    [SerializeField] private float laneChangeThreshold = 0.5f;

    private float lastSteering;

    private void Update()
    {
        float steering = input.SteeringInput;
        Debug.Log("steering = "+steering);
        // Detect LEFT lane change
        if (steering < -laneChangeThreshold && lastSteering >= -laneChangeThreshold)
        {
            laneController.RequestLaneChange(-1);
        }

        // Detect RIGHT lane change
        if (steering > laneChangeThreshold && lastSteering <= laneChangeThreshold)
        {
            laneController.RequestLaneChange(+1);
        }

        lastSteering = steering;
    }
}