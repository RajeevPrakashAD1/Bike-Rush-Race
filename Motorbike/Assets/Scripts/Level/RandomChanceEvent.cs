using UnityEngine;
using UnityEngine.Events;

public class RandomChanceEvent : MonoBehaviour
{
    public UnityEvent EventTrigger;

    public float PercentChance;

    private void Start()
    {
        if (Random.Range(0, 100) < PercentChance)
        {
            EventTrigger.Invoke();
        }
    }
}
