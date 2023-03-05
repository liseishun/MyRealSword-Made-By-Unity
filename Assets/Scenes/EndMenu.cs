using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private AudioSource clickSoundEffect;
    public void Quit()
    {
        clickSoundEffect.Play();
        Application.Quit();
    }
}