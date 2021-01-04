using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class AudioManager : SingletonBehavior<AudioManager>
    {
        [SerializeField] private AudioSource musicPlayer;
        [SerializeField] private AudioClip cinematicMusic;
        [SerializeField] private AudioClip mainMenuMusic;
        [SerializeField] private AudioClip battleIntroMusic;
        [SerializeField] private AudioClip battleMusic;
        
        protected override void Awake()
        {
            base.Awake();
        }

        public void PlayCinematicMusic()
        {
            musicPlayer.clip = cinematicMusic;
            musicPlayer.loop = true;
            musicPlayer.Play();
        }

        public void PlayMainMenuMusic()
        {
            musicPlayer.clip = mainMenuMusic;
            musicPlayer.loop = true;
            musicPlayer.Play();
        }

        public IEnumerator PlayBattleTheme()
        {
            musicPlayer.clip = battleIntroMusic;
            musicPlayer.loop = false;
            musicPlayer.Play();

            while (musicPlayer.isPlaying)
                yield return null;

            musicPlayer.clip = battleMusic;
            musicPlayer.loop = true;
            musicPlayer.Play();
        }
    }
}