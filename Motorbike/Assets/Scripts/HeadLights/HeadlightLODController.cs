using UnityEngine;

public class HeadlightLODSmooth : MonoBehaviour
{
    [Header("Distance Settings")]
    public float farStart = 140f;   // FAR fully visible beyond this
    public float farEnd   = 100f;   // FAR completely gone by this

    public float midStart = 120f;   // MID starts appearing
    public float midEnd   = 50f;    // MID gone by this

    public float nearStart = 60f;   // NEAR starts appearing
    public float nearEnd   =-10f;   // NEAR fully visible

    [Header("References")]
    public Renderer farGlow;
    public Renderer[] midGlows;
    public Light[] nearLights;

    Transform player;

    float nearMaxIntensity = 500f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Ensure lights start off
        foreach (var l in nearLights)
            l.intensity = 0f;
    }

    void Update()
    {
        if (!player) return;

        float d = Vector3.Distance(player.position, transform.position);

        // ---------- FAR ----------
        float farAlpha = Mathf.InverseLerp(farEnd, farStart, d);
        farAlpha = Mathf.SmoothStep(0, 1, farAlpha);
        SetAlpha(farGlow, farAlpha);

        // ---------- MID ----------
        float midAlpha = Mathf.InverseLerp(midEnd, midStart, d);
        midAlpha = Mathf.SmoothStep(0, 1, midAlpha);
        foreach (var r in midGlows)
            SetAlpha(r, midAlpha);

        // ---------- NEAR ----------
        float nearT = Mathf.InverseLerp(nearEnd, nearStart, d);
        nearT = Mathf.SmoothStep(0, 1, nearT);
        foreach (var l in nearLights)
            l.intensity = nearT * nearMaxIntensity;
    }

    void SetAlpha(Renderer r, float a)
    {
        if (!r) return;
        Color c = r.material.color;
        c.a = a;
        r.material.color = c;
    }
}