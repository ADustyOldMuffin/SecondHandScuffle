using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField] AudioSource myAudioPlayer;
    // Start is called before the first frame update

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        bool newSong = false;
        if (GameObject.FindGameObjectsWithTag("GameMusic").Length > 1)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("GameMusic");
            foreach (GameObject musicBox in gameObjects)
            {
                if (musicBox.GetComponent<AudioSource>().clip != GetComponent<AudioSource>().clip)
                {
                    Destroy(musicBox);
                    newSong = true;
                }
            }

            if (!newSong)
            {
                //Debug.Log("destroying");
                gameObject.SetActive(false);
                Destroy(gameObject);
            }

        }
        else
        {

            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        myAudioPlayer.Play();
    }
}
