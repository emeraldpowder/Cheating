using UnityEngine;

public class Rock : MonoBehaviour
{
private float rotationSpeed;

    private void Start()
    {
        rotationSpeed = Random.Range(-100f, 100f);
    }

    private void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
    }
}
