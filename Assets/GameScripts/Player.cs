using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text GemsText;

    private Camera mainCamera;
    private int gems;
    private bool lost;

    private void Start()
    {
        mainCamera = Camera.main;

        gems = PlayerPrefsSafe.GetInt("gems", 0);
        GemsText.text = gems.ToString();

        transform.Translate(Vector3.up * PlayerPrefsSafe.GetFloat("distance", 0));
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 world = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            world.z = 0;
            transform.position = world;
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * 5);
        }

        if (!lost) PlayerPrefsSafe.SetFloat("distance", transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            gems++;
            PlayerPrefsSafe.SetInt("gems", gems);
            GemsText.text = gems.ToString();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.layer == 9)
        {
            PlayerPrefsSafe.DeleteKey("gems");
            PlayerPrefsSafe.DeleteKey("distance");
            lost = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}