using System;
using UnityEngine;

public class PlayerLaneController : MonoBehaviour
{
    [SerializeField] private GameTuning tuning;
    [SerializeField] private PlayerSpeedController speedController;


    private int currentLane = 0; // 0 = left, 1 = right
    private int targetLane;
    private bool isSwitching;
    private float currentX;
    private float speed01;

    private void Awake()
    {
        speed01 = speedController.Speed01;
        
    }

    private void Start()
    {
        currentX = GetLaneX(currentLane);
        SetX(currentX);
        

    }
    public bool IsSwitching => isSwitching;
    public int CurrentLane => currentLane;
    public int TargetLane => targetLane;

    
    public void RequestLaneChange(int direction)
    {
        if (isSwitching) return;

        int newLane = Mathf.Clamp(currentLane + direction, 0, 1);
        if (newLane == currentLane) return;

        targetLane = newLane;
        isSwitching = true;
    }

    private void Update()
    {
        if (!isSwitching) return;

        float speed01 = Mathf.Clamp01(this.speed01);
        float switchSpeed = Mathf.Lerp(
            tuning.laneChangeSpeedLow,
            tuning.laneChangeSpeedHigh,
            speed01
        );

        float targetX = GetLaneX(targetLane);

        currentX = Mathf.MoveTowards(
            currentX,
            targetX,
            switchSpeed * Time.deltaTime
        );

        SetX(currentX);

        if (Mathf.Abs(currentX - targetX) < 0.01f)
        {
            currentLane = targetLane;
            isSwitching = false;
        }
    }

    private float GetLaneX(int lane)
    {
        return lane == 0
            ? -tuning.laneWidth * 0.5f
            :  tuning.laneWidth * 0.5f;
    }

    private void SetX(float x)
    {
        Vector3 pos = transform.position;
        pos.x = x;
        transform.position = pos;
    }

    
}