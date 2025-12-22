using System.Collections;
using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    [SerializeField]
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Timeout());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed *  Time.deltaTime);
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }
}
