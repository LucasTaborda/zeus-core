using System.Collections;
using UnityEngine;

namespace ZeusCore
{
    public class AppInit : MonoBehaviour
    {

        private void Awake()
        {
            new Locator();
        }


        private void Start()
        {
            StartCoroutine(StartApplication());
        }


        private IEnumerator StartApplication() //TODO: for de ServiceComponent abstractos
        {
            yield return new WaitUntil(() => Locator.ServiceExists<Jano.SceneService>());

            yield return new WaitUntil(() => Locator.ServiceExists<Cronos.UpdateService>());

            yield return new WaitUntil(() => Locator.ServiceExists<Euterpe.SoundService>());

            yield return new WaitUntil(() => Locator.ServiceExists<GameService>());


            var gameService = Locator.GetService<GameService>();

            yield return new WaitUntil(()=> gameService.IsReady);

            gameService.GoToMainMenu();
        }


    }
}
