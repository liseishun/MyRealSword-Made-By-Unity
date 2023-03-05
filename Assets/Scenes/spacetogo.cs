using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spacetogo : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private AudioSource soundSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            soundSoundEffect.Play();
            SceneManager.LoadScene(sceneName);
        }
    }
}
