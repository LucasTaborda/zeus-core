using System.Collections;
using UnityEngine;

namespace ZeusCore.Jano
{
    public class SceneServiceComponent : MonoBehaviour
    {
        public LoadingDisplay loadingView;
        public SceneService sceneService;
        private bool _isLoading;


        private void Awake()
        {
            sceneService = new SceneService(this);
            sceneService.OnStartLoading += StartLoader;
            sceneService.OnSceneLoaded += StopLoader;

            Locator.AddService(sceneService);
        }


        private void StartLoader()
        {
            _isLoading = true;
            loadingView.Show();
            StartCoroutine(RefreshLoaderView());
        }


        private void StopLoader(int scene)
        {
            _isLoading = false;
            loadingView.Hide();
        }


        private IEnumerator RefreshLoaderView()
        {
            while(_isLoading)
            {
                loadingView.Refresh((int)sceneService.LoadingProgress);

                yield return new WaitForEndOfFrame();
            }
        }


#if UNITY_EDITOR
        public void TestLoader()
        {
            StartCoroutine(TestLoaderEnumerator());
        }


        private IEnumerator TestLoaderEnumerator()
        {
            int i = 0;
            while (i < 100)
            {
                yield return new WaitForSeconds(0.5f);
                i++;
                loadingView.Refresh(i);
            }
        }
#endif


    }
}
