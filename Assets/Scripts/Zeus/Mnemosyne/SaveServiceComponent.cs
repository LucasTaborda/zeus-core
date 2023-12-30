using UnityEngine;

namespace ZeusCore.Mnemosyne
{
    public class SaveServiceComponent : MonoBehaviour
    {
        [SerializeField] private bool _useEncryption;
        [SerializeField] private string _encryptionKey;

        private void Awake()
        { 
            Locator.AddService(new SaveService(_useEncryption, _encryptionKey));
        }
    }
}
