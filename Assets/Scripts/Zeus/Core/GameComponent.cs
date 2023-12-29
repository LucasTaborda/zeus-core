using UnityEngine;

namespace ZeusCore
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int _mainMenuId;

        private void Awake()
        {
            Locator.AddService(new GameService(_mainMenuId, this));
        }

    }
}
