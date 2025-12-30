using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    [Header("Player")]
    public float playerDistance;

    [Header("Enemies")]
    public List<RacerData> enemies = new List<RacerData>();


    private void Awake()
    {
        Instance = this;
    }

    public void UpdateRace(float playerSpeed, float dt)
    {
        // Player
        playerDistance += playerSpeed * dt;

        // Enemies
        foreach (var enemy in enemies)
        {
            enemy.distance += enemy.speed * dt;
        }
    }

    public float GetRelativeDistance(RacerData enemy)
    {
        return enemy.distance - playerDistance;
    }
}