using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string nextScene;
    public string tagToCheck;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagToCheck))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
