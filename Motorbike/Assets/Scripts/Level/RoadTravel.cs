using UnityEngine;

public class RoadTravel : MonoBehaviour
{
    public Vector3 TravelSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(TravelSpeed * Time.deltaTime);
    }
}
