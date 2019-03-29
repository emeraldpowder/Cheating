using UnityEngine;
using UnityEngine.UI;

public class CameraFollower : MonoBehaviour
{
    public Text DistanceText;

    SafeFloat prevY;

    private void Start()
    {
        transform.Translate(Vector3.up * PlayerPrefsSafe.GetFloat("distance", 0));

        prevY = transform.position.y;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 5);

        if (transform.position.y - prevY > 10)
        {
            Debug.Log("Cheating detected!");
        }
        prevY = transform.position.y;

        DistanceText.text = string.Format("{0:#.0} m", transform.position.y);
    }
}