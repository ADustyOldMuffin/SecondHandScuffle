using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class AudioManager : SingletonBehavior<AudioManager>
    {
        [SerializeField] private AudioSource musicPlayer;
        [SerializeField] private AudioClip[] songs;
        
        protected override void Awake()
        {
            base.Awake();

            StartCoroutine(PlayGameBackgroundMusic());
        }

        private IEnumerator PlayGameBackgroundMusic()
        {
            yield return null;

            foreach (var t in songs)
            {
                musicPlayer.clip = t;
                musicPlayer.Play();

                while (musicPlayer.isPlaying)
                    yield return null;
            }
        }
    }
}