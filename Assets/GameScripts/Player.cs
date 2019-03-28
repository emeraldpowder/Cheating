using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text GemsText;
    
    private Camera mainCamera;
    private int gems;

    private void Start()
    {
        mainCamera = Camera.main;
        
        GemsText.text = gems.ToString();
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            gems++;
            GemsText.text = gems.ToString();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.layer == 9)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}