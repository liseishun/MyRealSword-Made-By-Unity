using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportOnKeyPress : MonoBehaviour
{
    public string nextScene;
    public string tagToCheck;
    public Canvas pressECanvas;

    private bool showCanvas = false;

    private void Start()
    {
        pressECanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (showCanvas && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagToCheck))
        {
            pressECanvas.gameObject.SetActive(true);
            showCanvas = true;
        }
    }
}