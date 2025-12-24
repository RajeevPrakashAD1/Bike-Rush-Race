using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Telemetry : MonoBehaviour
{
    [SerializeField]
    private TMP_Text speedValue;

    [SerializeField]private PlayerSpeedController playerSpeedController;



    void Update()
    {
        speedValue.text = Mathf.Ceil(playerSpeedController.CurrentSpeed).ToString();

    }

   
}
