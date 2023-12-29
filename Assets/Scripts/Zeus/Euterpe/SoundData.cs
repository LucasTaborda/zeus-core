using UnityEngine;

namespace ZeusCore.Euterpe
{
    [CreateAssetMenu(fileName = "New Sound Data", menuName = "Zeus/SoundData")]
    public class SoundData : ScriptableObject
    {
        public string soundId;
        public AudioClip audioClip;
        public float audioScale;
    }
}
