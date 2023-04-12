using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private AudioSource menuAudio;
        [SerializeField] private AudioClip bgMusicClip;

        public static Action PlayStartGame;
        public static Action QuitGameEvent;

        private void Start()
        {
            playButton.onClick.AddListener(PlayFunction);
            quitButton.onClick.AddListener(QuitGame);

            PlayAudio(bgMusicClip);
        }

        private void PlayAudio(AudioClip clip2Play)
        {
            menuAudio.clip = clip2Play;
            menuAudio.Play();
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(PlayFunction);
            quitButton.onClick.RemoveListener(QuitGame);
        }

        private void PlayFunction()
        {
            PlayAudio(null);
            PlayStartGame?.Invoke();
            this.gameObject.SetActive(false);
        }

        private void QuitGame()
        {
            QuitGameEvent?.Invoke();
        }
    }
}
