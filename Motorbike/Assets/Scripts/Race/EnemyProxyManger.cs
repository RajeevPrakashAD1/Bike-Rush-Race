using UnityEngine;
using System.Collections.Generic;

public class EnemyProxyManager : MonoBehaviour
{
    [SerializeField] private EnemyProxyController proxyPrefab;

    [SerializeField] private float visibleAhead = 80f;
    [SerializeField] private float visibleBehind = 15f;

    private Dictionary<RacerData, EnemyProxyController> active = new();

    private void Update()
    {
        foreach (var enemy in RaceManager.Instance.enemies)
        {
            float delta = RaceManager.Instance.GetRelativeDistance(enemy);

            bool visible = delta < visibleAhead && delta > -visibleBehind;

            if (visible && !active.ContainsKey(enemy))
            {
                var proxy = Instantiate(proxyPrefab, transform);
                proxy.Bind(enemy);
                active.Add(enemy, proxy);
            }
            else if (!visible && active.ContainsKey(enemy))
            {
                Destroy(active[enemy].gameObject);
                active.Remove(enemy);
            }
        }
    }
}