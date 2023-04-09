using TankBattle.Tank.EnemyTank;
using TankBattle.Tank.PlayerTank;
using UnityEngine;

namespace TankBattle
{
    public class GameManager : GenericMonoSingleton<GameManager>
    {
        [SerializeField] private Tank.PlayerTank.InputSystem.InputReader _input;
        [SerializeField] private GameObject pauseMenu;


        private void Start()
        {
            _input.PauseEvent += HandlePause;
            _input.ResumeEvent += HandleResume;
        }

        private void OnDisable()
        {
            _input.PauseEvent -= HandlePause;
            _input.ResumeEvent -= HandleResume;
        }

        public void StartGame()
        {
            PlayerService.Instance.CreatePlayer();
            EnemyService.Instance.CreateEnemies();
        }

        public void QuitGame()
        {
#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
            Application.Quit();
#elif (UNITY_WEBGL)
            Application.OpenURL("about:blank");
#endif
        }

        private void HandlePause()
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        private void HandleResume()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }

    }
}