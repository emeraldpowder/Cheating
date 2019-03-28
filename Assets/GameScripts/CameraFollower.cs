using UnityEngine;
using UnityEngine.UI;

public class CameraFollower : MonoBehaviour
{
    public Text DistanceText;

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 5);

        DistanceText.text = string.Format("{0:#.0} m", transform.position.y);
    }
}