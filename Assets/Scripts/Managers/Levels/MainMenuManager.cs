using UnityEngine;

namespace Managers.Levels
{
    public class MainMenuManager : MonoBehaviour
    {
        public void LoadSceneMenu(int menu)
        {
            LevelManager.Instance.LoadLevel(menu);
        }
    }
}