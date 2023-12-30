using UnityEngine;

namespace ZeusCore
{
    public class GameServiceComponent : MonoBehaviour
    {
        [SerializeField] private int _mainMenuId;

        private void Awake()
        {
            Locator.AddService(new GameService(_mainMenuId, this));
        }

    }
}
