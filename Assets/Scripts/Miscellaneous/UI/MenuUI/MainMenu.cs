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

        private void Start()
        {
            playButton.onClick.AddListener(playFunction);
            quitButton.onClick.AddListener(QuitGame);

            playAudio(bgMusicClip);
        }

        private void playAudio(AudioClip clip2Play)
        {
            menuAudio.clip = clip2Play;
            menuAudio.Play();
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(playFunction);
            quitButton.onClick.RemoveListener(QuitGame);
        }

        private void playFunction()
        {
            Debug.Log("Click click!");
            playAudio(null);
            GameManager.Instance.StartGame();
            this.gameObject.SetActive(false);
        }

        private void QuitGame()
        {
            GameManager.Instance.QuitGame();
        }
    }
}
