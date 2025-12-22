using UnityEngine;

public class PlayerForwardMovement : MonoBehaviour
{
    [SerializeField] private GameTuning tuning;

    private float currentSpeed;

    private void Start()
    {
        currentSpeed = tuning.cruiseSpeed;
    }

    private void Update()
    {
        // Prototype logic:
        // Always move forward at current speed
        transform.position += Vector3.forward * currentSpeed * Time.deltaTime;
    }

    public float GetSpeed()
    {
        return currentSpeed;
    }
}