using UnityEngine;
using UnityEngine.SceneManagement;

public class DevPreload : MonoBehaviour
{
    private void Awake()
    {
        if (GameObject.Find("__app"))
            return;

        SceneManager.LoadScene("_preload");
    }
}