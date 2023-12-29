using UnityEngine;
using System.Collections.Generic;

namespace ZeusCore.Euterpe
{
    public class SoundModel : MonoBehaviour
    {
        [SerializeField] private SoundData[] _registries;
        private Dictionary<string, SoundData> _registriesDictionary = new();


        private void Awake()
        {
            foreach (var registry in _registries)
            {
                _registriesDictionary[registry.name] = registry;
            }   
        }


        public SoundData GetSound(string soundId)
        {
            return _registriesDictionary[soundId];
        }
    }
}
