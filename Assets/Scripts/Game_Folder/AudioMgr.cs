using System;
using Blocks_Folder;
using UnityEngine;

namespace Game_Folder
{
    public class AudioMgr : MonoBehaviour
    {
        [SerializeField] private AudioClip emptyAudio;
        [SerializeField] private AudioClip juiceAudio;
        [SerializeField] private AudioClip bigJuiceAudio;
        [SerializeField] private AudioClip hardSoulAudio;
        [SerializeField] private AudioClip levelPassAudio;
        [SerializeField] private AudioClip levelPassAudio2;

        private AudioSource _audio;

        private static AudioMgr _instance;
        

        private void Start()
        {
            _instance = this;
            _audio = GetComponent<AudioSource>();
        }

        public static AudioMgr GetInstance()
        {
            return _instance;
        }

        public void PlayeBlockAudio(BlockBase block)
        {
            switch (block.info.type)
            {
                case 0:
                    Play(emptyAudio);
                    break;
                case 1:
                    break;
                case 2:
                    switch (block.info.stepAward)
                    {
                        case 6:
                            Play(juiceAudio);
                            break;
                        case 12:
                            Play(bigJuiceAudio);
                            break;
                        case -1:
                            Play(hardSoulAudio);
                            break;
                    }
                    break;
            }
        }

        public void PlayLevelPassAudio(int curLevel)
        {
            switch (curLevel)
            {
                case 0:
                    Play(levelPassAudio);
                    break;
                case 1:
                    Play(levelPassAudio);
                    break;
                case 2:
                    Play(levelPassAudio2);
                    break;
            }
        }

        private void Play(AudioClip newClip)
        {
            _audio.clip = newClip;
            _audio.Play();
        }
    }
}