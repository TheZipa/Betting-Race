using System.Collections.Generic;
using BettingRace.Code.Data.Sound;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BettingRace.Code.Services.Sound
{
    public class SoundService : MonoBehaviour, IService
    {
        [SerializeField] private AudioSource _mainSource;
        [SerializeField] private AudioSource _backgroundMusicSource;

        private readonly Stack<AudioSource> _sourceGroup = new Stack<AudioSource>(4);
        private IStaticDataService _staticData;

        private void Awake() => DontDestroyOnLoad(this);

        public void Construct(IStaticDataService staticData) => _staticData = staticData;

        public void CreateAudioGroup(SoundType sound, int length)
        {
            AudioClip groupSound = _staticData.GetSound(sound);
            
            for (int i = 0; i < length; i++)
            {
                AudioSource soundGroupElement = gameObject.AddComponent<AudioSource>();
                soundGroupElement.playOnAwake = false;
                soundGroupElement.loop = true;
                soundGroupElement.pitch = Random.Range(0.9f, 1.15f);
                soundGroupElement.clip = groupSound;
                _sourceGroup.Push(soundGroupElement);
            }
        }

        public void PlaySound(SoundType sound)
        {
            AudioClip audioClip = _staticData.GetSound(sound);
            _mainSource.PlayOneShot(audioClip);
        }
        
        public void PlaySoundGroup()
        {
            foreach (AudioSource source in _sourceGroup)
                source.Play();
        }

        public void SwitchSoundMute(bool enabled)
        {
            foreach (AudioSource source in _sourceGroup)
                source.mute = !enabled;

            _mainSource.mute = !enabled;
            _backgroundMusicSource.mute = !enabled;
        }

        public void StopSoundGroupElement() =>
            _sourceGroup.Pop().Stop();

        public void MuteBackgroundMusic() =>
            _backgroundMusicSource.volume = 0f;

        public void UnmuteBackgroundMusic() =>
            _backgroundMusicSource.volume = 1f;
    }
}