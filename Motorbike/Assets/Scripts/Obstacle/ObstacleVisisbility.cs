using UnityEngine;

public class ObstacleSignalController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform signalRoot;

    [Header("Distances")]
    [SerializeField] private float farDistance = 45f;
    [SerializeField] private float nearDistance = 8f;

    [Header("Signal Width")]
    [SerializeField] private float farWidth = 7f;
    [SerializeField] private float nearWidth = 0.5f;

    [Header("Instability")]
    [SerializeField] private float farJitter = 1.2f;
    [SerializeField] private float nearJitter = 0f;

    [Header("Intensity")]
    [SerializeField] private float farIntensity = 0.4f;
    [SerializeField] private float nearIntensity = 1f;

    private Vector3 baseLocalPos;
    private Renderer[] renderers;

    private void Awake()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        baseLocalPos = signalRoot.localPosition;
        renderers = signalRoot.GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 0 = FAR (ambiguous), 1 = NEAR (clear)
        float clarity = Mathf.InverseLerp(farDistance, nearDistance, distance);

        // Width tightens as clarity increases
        float width = Mathf.Lerp(farWidth, nearWidth, clarity);
        signalRoot.localScale = new Vector3(width, 1f, 1f);

        // Instability decreases as clarity increases
        float jitterAmount = Mathf.Lerp(farJitter, nearJitter, clarity);
        float jitter = Mathf.Sin(Time.time * 8f) * jitterAmount;
        signalRoot.localPosition = baseLocalPos + Vector3.right * jitter;

        // Intensity increases with clarity
        float intensity = Mathf.Lerp(farIntensity, nearIntensity, clarity);

        foreach (var r in renderers)
        {
            Color c = r.material.color;
            c.a = intensity;
            r.material.color = c;
        }
    }
}
