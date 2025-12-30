using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("Damage Scaling")]
    [Tooltip("Extra damage at high speed")]
    [SerializeField] private AnimationCurve damageBySpeed =
        AnimationCurve.EaseInOut(0, 0.5f, 1, 1.5f);

    public float Health01 => currentHealth / maxHealth;
    public float CurrentHealth => currentHealth;
    public bool IsDead => currentHealth <= 0f;

    private PlayerSpeedController speedController;

    private void Awake()
    {
        speedController = GetComponent<PlayerSpeedController>();
        currentHealth = maxHealth;
    }

    // =========================
    // MAIN DAMAGE ENTRY POINT
    // =========================
    public void TakeDamage(float baseDamage)
    {
        float speed01 = speedController.Speed01;
        float speedMultiplier = damageBySpeed.Evaluate(speed01);

        float finalDamage = baseDamage * speedMultiplier;

        currentHealth -= finalDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // TEMP DEBUG
        Debug.Log($"Health: {currentHealth:F1}");

        if (IsDead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Debug.Log("💀 Player crashed (health depleted)");
        // Notify race manager / game manager
        // Disable controls
        // Play crash FX
    }
}