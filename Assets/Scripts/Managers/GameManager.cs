using UnityEngine;

namespace Managers
{
    public class GameManager : SingletonBehavior<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();

            gameObject.SetActive(true);
        }
    }
}
