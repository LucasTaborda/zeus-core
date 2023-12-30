using UnityEngine;

namespace ZeusCore.Euterpe
{
    public class SoundServiceComponent : MonoBehaviour
    {
        [SerializeField] private SoundModel _soundModel;
        public int channelCount;


        private void Awake()
        {
            var channels = CreateChannels(channelCount);
            var soundService = new SoundService(channels, _soundModel);

            Locator.AddService(soundService);
        }


        private AudioSource[] CreateChannels(int count)
        {
            var channels = new AudioSource[count];
            for (int i = 0; i < count; i++)
            {
                channels[i] = gameObject.AddComponent<AudioSource>();
            }

            return channels;
        }


    }
}
