using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Levels
{
    public class SceneTransitionLoader : MonoBehaviour
    {
        public Animator transition;
        [SerializeField] float transitionTime = 1f;
        public void TransitionToNewScene(int level)
        {
            StartCoroutine(LoadLevel(level));
        }

        IEnumerator LoadLevel(int level)
        {
            //play animation
            transition.SetTrigger("Start");
            //wait until done
            yield return new WaitForSecondsRealtime(transitionTime);
            //load new scene
            EventBus.Instance?.ChangeLevel(level);
        }
    }
}
