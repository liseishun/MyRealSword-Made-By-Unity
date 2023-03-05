using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace TESTING
{
    public class Testing_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;
        AudioSource audioSource;
        bool isPlaying;
        
        public string sceneName;


        IEnumerator PlayDialogue()
        {
            audioSource.Play();
            architect.Build(lines[currentLineIndex]);
            while (audioSource.isPlaying)
            {
                yield return null;
            }
            currentLineIndex++;
            isPlaying = false;

        }

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        public string[] lines = new string[]
        {
            "This is a random line of dialogue.",
            "I want to say something, come over here.",
            "The world is a crazy place sometimes.",
            "Don't lose hope, things will get better!",
            "It's a bird? It's a plane? No! - It's Super Sheltie!"
        };
        int currentLineIndex = 0;


        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.typewriter;
            architect.speed = 0.5f;
            audioSource = GetComponent<AudioSource>();



        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (bm != architect.buildMethod)
                {
                    architect.buildMethod = bm;
                    architect.Stop();
                }
                else if (currentLineIndex >= lines.Length)
                {
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                    StartCoroutine(LoadScene());

                    return;
                }

                if (!isPlaying && audioSource != null)
                {
                    StartCoroutine(PlayDialogue());
                }
                IEnumerator LoadScene()
                {
                    
                    yield return new WaitForSeconds(0.5f);
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
    }
}