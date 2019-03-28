using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Objects;
    public float Frequency = 1;

    private void Update()
    {
        if (Random.value < Time.deltaTime * Frequency)
        {
            Vector3 position = new Vector3(Random.Range(-2.5f, 2.5f), transform.position.y);
            Instantiate(Objects[Random.Range(0, Objects.Length)], position, Quaternion.identity);
        }
    }
}