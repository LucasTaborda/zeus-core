using UnityEngine;

namespace ZeusCore.Euterpe
{
    public class SoundService
    {
        private AudioSource[] _channels;
        private SoundModel _model;


        public SoundService(AudioSource[] channels, SoundModel model)
        {
            _channels = channels;
            _model = model;
        }


        public void Play(string soundId)
        {
            var channel = GetChannel();
            var sound = _model.GetSound(soundId);
            channel.PlayOneShot(sound.audioClip, sound.audioScale);
        }


        private AudioSource GetChannel()
        {
            foreach (var channel in _channels)
            {
                if (channel.isPlaying) continue;

                return channel;
            }

            throw new System.IndexOutOfRangeException("Sound Manager: All channels are in use!");
        }


    }
}
