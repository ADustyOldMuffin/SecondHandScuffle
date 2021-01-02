using UnityEngine;

namespace Managers
{
    public class InputManager : SingletonBehavior<InputManager>
    {
        public MasterInput InputMaster { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            InputMaster = new MasterInput();
        }
    }
}