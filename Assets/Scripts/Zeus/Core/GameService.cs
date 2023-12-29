using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeusCore.Jano;
using ZeusCore.Cronos;

namespace ZeusCore
{
    public class GameService
    {
        private SceneService _sceneService;
        private UpdateService _updateService;
        private int _mainMenuId;
        public enum State { Starting, MainMenu, Paused, Playing, Loading, VideoCinematic, InGameCinematic }
        public State state;
        public bool IsReady { get; private set; }

        public GameService(int mainMenuId, MonoBehaviour coroutiner)
        {
            _mainMenuId = mainMenuId;
            state = State.Starting;

            coroutiner.StartCoroutine(GetServices());
        }

        private IEnumerator GetServices()
        {
            yield return new WaitUntil(() => Locator.ServiceExists<SceneService>());
            _sceneService = Locator.GetService<SceneService>();
            _sceneService.OnSceneLoaded += CheckIfSceneIsMainMenu;

            yield return new WaitUntil(() => Locator.ServiceExists<UpdateService>());
            _updateService = Locator.GetService<UpdateService>();
            IsReady = true;
        }


        public void GoToMainMenu()
        {
            _sceneService.LoadScene(_mainMenuId);
        }


        public void StartGameApp()
        {
            GoToMainMenu(); //TODO: Esto no. Datos de escenas? Cargar escena inicial (los splash)
        }


        public void PauseGame()
        {
            state = State.Paused;

            Time.timeScale = 0;
            _updateService.active = false;
        }

        public void ResumeGame()
        {
            state = State.Playing;

            Time.timeScale = 1;
            _updateService.active = true;
        }


        private void CheckIfSceneIsMainMenu(int scene)
        {
            if (scene == _mainMenuId)
                state = State.MainMenu;
        }


        public void FinishLevel()
        {

        }


        public void StartGame(int scene)
        {
            _sceneService.LoadScene(scene);
        }


        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
