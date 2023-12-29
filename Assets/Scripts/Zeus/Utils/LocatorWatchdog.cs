using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZeusCore.Utils
{
    public class LocatorWatchdog : MonoBehaviour
    {
        public int loadScene;

        private void Awake()
        {
            if (!Locator.Initialized) SceneManager.LoadScene(loadScene);
        }
    }

}
