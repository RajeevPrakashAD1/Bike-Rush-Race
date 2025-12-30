using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealth health;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI text;
    [Header("Colors")]
    [SerializeField] private Color healthyColor = Color.green;
    [SerializeField] private Color warningColor = new Color(1f, 0.65f, 0f);
    [SerializeField] private Color criticalColor = Color.red;

    [Header("Behavior")]
    [SerializeField] private float smoothSpeed = 8f;
    [SerializeField] private float criticalPulseSpeed = 4f;

    private float displayedHealth = 1f;

    private void Update()
    {
        text.text = "health: " + health.CurrentHealth;

    }

    private void UpdateColor(float health01)
    {
        if (health01 > 0.6f)
        {
            fillImage.color = healthyColor;
        }
        else if (health01 > 0.3f)
        {
            fillImage.color = Color.Lerp(
                warningColor,
                healthyColor,
                (health01 - 0.3f) / 0.3f
            );
        }
        else
        {
            // Critical pulse
            float pulse = Mathf.Abs(Mathf.Sin(Time.time * criticalPulseSpeed));
            fillImage.color = Color.Lerp(
                criticalColor,
                warningColor,
                pulse
            );
        }
    }
}