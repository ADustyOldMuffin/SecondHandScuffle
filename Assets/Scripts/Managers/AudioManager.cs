using UnityEngine;

namespace Managers
{
    public class AudioManager : SingletonBehavior<AudioManager>
    {
        [SerializeField] private AudioSource audioPlayer;
        
        protected override void Awake()
        {
            base.Awake();
            
            
        }
    }
}