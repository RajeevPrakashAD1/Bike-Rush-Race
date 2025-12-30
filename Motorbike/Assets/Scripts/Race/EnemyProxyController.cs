using UnityEngine;

public class EnemyProxyController : MonoBehaviour
{
    [Header("Lane Positions")]
    public float[] laneX = { -3f, 0f, 3f };

    [Header("Movement")]
    public float laneChangeSpeed = 6f;

    private RacerData enemy;
    private float currentX;

    public void Bind(RacerData data)
    {
        enemy = data;
        currentX = laneX[data.lane];
    }

    private void Update()
    {
        if (enemy == null) return;

        float delta = RaceManager.Instance.GetRelativeDistance(enemy);

        // Z = relative distance
        Vector3 pos = transform.localPosition;
        pos.z = delta;

        // X = lane + smoothing
        float targetX = laneX[enemy.lane];
        currentX = Mathf.Lerp(currentX, targetX, laneChangeSpeed * Time.deltaTime);
        pos.x = currentX;

        transform.localPosition = pos;
    }
}