using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneScript : MonoBehaviour
{
    public string nextScene;

    [SerializeField] private AudioSource clickSoundEffect;
    public void onClicknextScene()
    {
        clickSoundEffect.Play();
        SceneManager.LoadScene(nextScene);
    }
}
