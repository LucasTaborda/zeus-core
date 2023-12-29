using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ZeusCore.Jano
{
    public class SceneService
    {
        private readonly MonoBehaviour _coroutiner;
        public float? LoadingProgress { get; private set; } = 0;
        public delegate void FinishLoadEvent(int scene);
        public delegate void StartLoadEvent();
        public event StartLoadEvent OnStartLoading;
        public event FinishLoadEvent OnSceneLoaded;
        public event FinishLoadEvent OnSceneUnloaded;


        public SceneService(MonoBehaviour coroutiner)
        {
            _coroutiner = coroutiner;
        }


        public void LoadScene(int index)
        {
            _coroutiner.StartCoroutine(LoadSceneAsync(index, LoadSceneMode.Single));
        }


        public void UnloadScene(int index)
        {
            _coroutiner.StartCoroutine(UnloadSceneAsync(index));
        }


        private IEnumerator LoadSceneAsync(int index, LoadSceneMode mode)
        {
            OnStartLoading?.Invoke();
            var asyncLoad = UnitySceneManager.LoadSceneAsync(index, mode);

            while (!asyncLoad.isDone)
            {
                LoadingProgress = asyncLoad.progress * 100f;
                yield return null;
            }
            LoadingProgress = null;
            OnSceneLoaded?.Invoke(index);
        }


        private IEnumerator UnloadSceneAsync(int index)
        {
            var asyncUnload = UnitySceneManager.UnloadSceneAsync(index);

            while (!asyncUnload.isDone) yield return null;

            OnSceneUnloaded?.Invoke(index);
        }


    }
}
