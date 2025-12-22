using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class MotorcycleController : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private List<float> gearRatios;
    [SerializeField]
    private float finalDriveRatio;
    [SerializeField]
    private float accelerationRate;
    [SerializeField]
    private float torque;
    [SerializeField]
    private AnimationCurve torqueCurve;
    [SerializeField]
    private float engineIdleSpeed;
    [SerializeField]
    private float redline;
    [SerializeField]
    private float engineBrakingFactor;
    [SerializeField]
    private float inertialDecelerationRate;
    [SerializeField]
    private float brakingRate;
    [SerializeField]
    private GameplayInputHandler input;
    [SerializeField]
    private float leanRate;
    [SerializeField]
    private float turnSpeedFactor;
    [SerializeField]
    private float turnRate;
    [SerializeField]
    private float speedSampleRate;
    [SerializeField]
    private GameObject groundPlane;
    [SerializeField]
    private Transform playerCam;
    [SerializeField]
    private GameObject tailLights;

    [SerializeField]
    private AudioMixer audioMixer;

    private float speed;
    private float engineSpeed;
    private int currentGear;
    private float targetAngle;
    private Vector3 previousPosition;
    private float tripDistance;
    private float speedSampleTimer;
    private float currentWheelSpeed;

    public float Speed
    {
        get
        {
            return speed;
        }
    }
    public int CurrentGear
    {
        get
        {
            return currentGear;
        }
    }
    public float TripDistance
    {
        get
        {
            return tripDistance;
        }
    }
    public float EngineSpeed
    {
        get
        {
            return engineSpeed;
        }
    }
    public float Redline
    {
        get
        {
            return redline;
        }
    }

    private void Awake()
    {
        input = gameObject.GetComponent<GameplayInputHandler>();
        previousPosition = transform.position;
        engineSpeed = engineIdleSpeed;
        currentGear = 1;
        speedSampleTimer = speedSampleRate;
    }

    private void Start()
    {
        StartCoroutine(SpeedReckoner());
    }

    // Update is called once per frame
    void Update()
    {
        tripDistance += Vector3.Distance(previousPosition, gameObject.transform.position);

        //if(speedSampleTimer <= 0)
        //{
        //    // speed = (Vector3.Distance(previousPosition, gameObject.transform.position)/Time.deltaTime) * 2.237f;
        //    speed = (Vector3.Distance(previousPosition, gameObject.transform.position) / Time.deltaTime);
        //    speedSampleTimer = speedSampleRate;
        //}
        //speedSampleTimer -= Time.deltaTime;

        previousPosition = transform.position;

        // Lean the bike.
        transform.Rotate(0f, 0f, (input.SteeringInput * leanRate * Time.deltaTime) * -1f);
        // Counter-lean for the player head/camera.
        playerCam.Rotate(0f, 0f, (input.SteeringInput * leanRate * Time.deltaTime) * 0.5f);
        // Turn the bike based on the lean angle and speed.
        transform.Rotate(Vector3.up * CalcAngle(transform.up) * turnRate * (speed * turnSpeedFactor) * Time.deltaTime, Space.World);

        groundPlane.transform.forward = transform.forward;

        if (input.ThrottleInput > 0f)
        {
            if(engineSpeed < redline)
            {
                // Increase the engine speed based on the torque curve.
                engineSpeed += torqueCurve.Evaluate(engineSpeed / redline) * torque * Time.deltaTime;
            }
        }
        else
        {
            if (engineSpeed > 0)
            {
                // If not accelerating, apply engine braking.
                engineSpeed -= inertialDecelerationRate * engineBrakingFactor * Time.deltaTime;
            }
        }

        // Calculate the linear movement based on engine speed, gear ratios and wheel size.
        Vector3 moveVector = Vector3.forward * CalcSpeed(engineSpeed, gearRatios[currentGear], finalDriveRatio, 0.3f) * Time.deltaTime;

        if (input.BrakeInput > 0f)
        {
            if (speed > 0)
            {
                // Reduce the movement amount based on braking force.
                moveVector -= Vector3.forward * brakingRate * input.BrakeInput * Time.deltaTime;
            }

            tailLights.SetActive(true);
        }
        else tailLights.SetActive(false);

        transform.Translate(moveVector);

        // Maintain minimum engine idle speed.  Refactor later to allow for stalling.
        if (engineSpeed < engineIdleSpeed) engineSpeed = engineIdleSpeed;

        if(speed > 0)
        {
            // Adjust the pitch of the engine SFX based on RPMs.
            audioMixer.SetFloat("EnginePitch", Mathf.Lerp(1, 6, engineSpeed / redline));
        }
    }

    private void LateUpdate()
    {
        float rpm = Mathf.Lerp(CalcEngineSpeed(0.3f), engineSpeed, 0.5f);
        engineSpeed = rpm;
    }

    private float CalcSpeed(float rpm, float gearRatio, float finalDrive, float wheelDiameter)
    {
        float linearSpeed = 0f;

        float engineSpeedRads = rpm * 3.14f / 30f;
        float wheelSpeed = engineSpeedRads / (gearRatio * finalDrive);
        currentWheelSpeed = wheelSpeed;
        linearSpeed = wheelSpeed * wheelDiameter;

        return linearSpeed;
    }
    private float CalcEngineSpeed(float wheelDiameter)
    {
        float rpm = 0f;

        float wheelSpeed = Vector3.Distance(previousPosition, gameObject.transform.position) / Time.deltaTime / wheelDiameter;
        //Debug.Log(wheelSpeed = speed / wheelDiameter);

        float engineSpeedRads = wheelSpeed * (gearRatios[currentGear] * finalDriveRatio);
        //Debug.Log(engineSpeedRads = wheelSpeed * (gearRatios[currentGear] * finalDriveRatio));

        rpm = engineSpeedRads / 3.14f * 30f;
        //Debug.Log(rpm = engineSpeedRads / 3.14f * 30f);

        //Debug.Log(rpm);
        return rpm;
    }

    public void GearUp()
    {
        if (currentGear < gearRatios.Count - 1)
        {
            currentGear++;
            engineSpeed = CalcEngineSpeed(0.3f);
        }
    }
    public void GearDown()
    {
        if(currentGear > 1)
        {
            currentGear--;
            engineSpeed = CalcEngineSpeed(0.3f);
            // engineSpeed += 600 * currentGear * (engineSpeed + 500) / redline;
        }
    }

    private float CalcAngle(Vector3 newDirection)
    {
        // the vector that we want to measure an angle from
        Vector3 referenceForward = groundPlane.transform.up;/* some vector that is not Vector3.up */

        // the vector perpendicular to referenceForward (90 degrees clockwise)
        // (used to determine if angle is positive or negative)
        Vector3 referenceRight = groundPlane.transform.right;

        // Get the angle in degrees between 0 and 180
        float angle = Vector3.Angle(newDirection, referenceForward);

        // Determine if the degree value should be negative. Here, a positive value
        // from the dot product means that our vector is on the right of the reference vector
        // whereas a negative value means we're on the left.
        float sign = Mathf.Sign(Vector3.Dot(newDirection, referenceRight));

        return sign * angle;
    }

    private IEnumerator SpeedReckoner()
    {

        YieldInstruction timedWait = new WaitForSeconds(speedSampleRate);
        Vector3 lastPosition = transform.position;
        float lastTimestamp = Time.time;

        while (enabled)
        {
            yield return timedWait;

            var deltaPosition = (transform.position - lastPosition).magnitude;
            var deltaTime = Time.time - lastTimestamp;

            if (Mathf.Approximately(deltaPosition, 0f)) // Clean up "near-zero" displacement
                deltaPosition = 0f;

            speed = deltaPosition / deltaTime;


            lastPosition = transform.position;
            lastTimestamp = Time.time;
        }
    }
}
