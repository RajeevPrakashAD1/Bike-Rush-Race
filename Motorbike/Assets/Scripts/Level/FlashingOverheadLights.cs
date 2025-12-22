using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FlashingOverheadLights : MonoBehaviour
{
    public List<Light> lights;
    public bool flashing;
    public float flashingSpeed;

    private int currentLight = 0;
    private float flashTimer;

    private void Awake()
    {
        flashTimer = flashingSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if(flashing)
        {
            flashTimer -= Time.deltaTime;
            if(flashTimer < 0)
            {
                currentLight++;
                if(currentLight > lights.Count - 1)
                {
                    currentLight = 0;
                }
                flashTimer = flashingSpeed;
            }

            for (int i = 0; i < lights.Count; i++)
            {
                if (i == currentLight) lights[i].enabled = true;
                else lights[i].enabled = false;
            }

        }
    }
}
