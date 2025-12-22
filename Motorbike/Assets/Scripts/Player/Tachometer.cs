using UnityEngine;

public class Tachometer : MonoBehaviour
{
    [SerializeField]
    private Transform needle;
    [SerializeField]
    private MotorcycleController motorcycle;
    [SerializeField]
    private float maxRange;

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, maxRange, motorcycle.EngineSpeed / motorcycle.Redline));
    }
}
