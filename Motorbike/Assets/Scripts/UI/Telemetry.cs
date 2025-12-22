using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Telemetry : MonoBehaviour
{
    [SerializeField]
    private TMP_Text speedValue;
    [SerializeField]
    private TMP_Text distanceValue;
    [SerializeField]
    private TMP_Text currentGearValue;
    [SerializeField]
    private Image accelerator;
    [SerializeField]
    private Image brake;
    [SerializeField]
    private MotorcycleController motorcycle;
    [SerializeField]
    private GameplayInputHandler inputHandler;

    private void Start()
    {
        currentGearValue.text = motorcycle.CurrentGear.ToString();
    }

    void Update()
    {
        speedValue.text = Mathf.Ceil(motorcycle.Speed).ToString();
        distanceValue.text = Mathf.Ceil(motorcycle.TripDistance).ToString() + "m";

        accelerator.fillAmount = inputHandler.ThrottleInput;
        brake.fillAmount = inputHandler.BrakeInput;
    }

    public void ChangeCurrentGear()
    {
        currentGearValue.text = motorcycle.CurrentGear.ToString();
    }
}
