using System.Collections;
using UnityEngine;
using ZeusCore.Jano;
using ZeusCore.Cronos;
using ZeusCore.Mnemosyne;
using System.Collections.Generic;

namespace ZeusCore
{
    public class GameService
    {
        private SceneService _sceneService;
        private UpdateService _updateService;
        private SaveService _saveService;
        private int _mainMenuId;
        private MonoBehaviour _coroutiner;
        public enum State { Starting, MainMenu, Paused, Playing, Loading, VideoCinematic, InGameCinematic }
        public State state;
        public bool IsReady { get; private set; }

        public delegate void LoadGameEvent(Dictionary<string, string> dataCollection);
        public delegate void SaveGameEvent();

        public event LoadGameEvent OnGameLoaded;
        public event SaveGameEvent OnGameSaved;


        public GameService(int mainMenuId, MonoBehaviour coroutiner)
        {
            _mainMenuId = mainMenuId;
            _coroutiner = coroutiner;
            state = State.Starting;

            _coroutiner.StartCoroutine(GetServices());
        }


        private IEnumerator GetServices()
        {
            yield return new WaitUntil(() => Locator.ServiceExists<SceneService>());
            _sceneService = Locator.GetService<SceneService>();
            _sceneService.OnSceneLoaded += CheckIfSceneIsMainMenu;

            yield return new WaitUntil(() => Locator.ServiceExists<UpdateService>());
            _updateService = Locator.GetService<UpdateService>();

            yield return new WaitUntil(() => Locator.ServiceExists<SaveService>());
            _saveService = Locator.GetService<SaveService>();

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


        public void StartNewGame()
        {

        }


        public void LoadGame(string[] keys)
        {
            _coroutiner.StartCoroutine(LoadGameCoroutine(keys));
        }


        private IEnumerator LoadGameCoroutine(string[] keys)
        {
            yield return null;

            Dictionary<string, string> dataLoaded = new();

            foreach (var key in keys)
            {
                dataLoaded[key] = _saveService.LoadData(key);
            }

            OnGameLoaded?.Invoke(dataLoaded);
        }


        public void SaveGame(Dictionary<string, string> dataCollection)
        {
            _coroutiner.StartCoroutine(SaveGameCoroutine(dataCollection));
        }


        private IEnumerator SaveGameCoroutine(Dictionary<string, string> dataCollection)
        {
            yield return null;

            foreach (var data in dataCollection)
            {
                _saveService.SaveData(data.Key, data.Value);
            }
            OnGameSaved?.Invoke();
        }


        public void QuitGameApp()
        {
            Application.Quit();
        }
    }
}
