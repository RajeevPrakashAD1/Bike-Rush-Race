using UnityEngine;

public class PlayerSpeedController : MonoBehaviour
{
    [SerializeField] private GameTuning tuning;
    [SerializeField] private GameplayInputHandler input;

    [Header("Runtime (Read Only)")]
    [SerializeField] private float currentSpeed;

    public float CurrentSpeed => currentSpeed;

    private void Start()
    {
        currentSpeed = tuning.cruiseSpeed;
    }

    private void Update()
    {
        UpdateSpeed();
        MoveForward();
    }

    private void UpdateSpeed()
    {
        if (input.ThrottleInput > 0.1f)
        {
            // Push speed toward max
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                tuning.maxSpeed,
                tuning.throttleAcceleration * Time.deltaTime
            );
        }
        else
        {
            // Return to cruise speed naturally
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                tuning.cruiseSpeed,
                tuning.speedReturnRate * Time.deltaTime
            );
        }
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * currentSpeed * Time.deltaTime;
    }
    
    public float Speed01
    {
        get
        {
            return Mathf.InverseLerp(
                tuning.cruiseSpeed,
                tuning.maxSpeed,
                currentSpeed
            );
        }
    }

}