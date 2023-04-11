using TankBattle.Tank.EnemyTank;
using TankBattle.Tank.PlayerTank;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TankBattle
{
    public class GameManager : GenericMonoSingleton<GameManager>
    {
        [SerializeField] private Tank.PlayerTank.InputSystem.InputReader _input;
        [SerializeField] private GameObject pauseMenu;

        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private GameObject gameWonMenu;

        private Button restartbtn;
        private Button addEnemies;

        protected override void Awake()
        {
            base.Awake();
            restartbtn = gameOverMenu.GetComponentInChildren<Button>();
            addEnemies = gameWonMenu.GetComponentInChildren<Button>();
        }

        private void Start()
        {
            _input.PauseEvent += HandlePause;
            _input.ResumeEvent += HandleResume;
            PlayerService.Instance.OnPlayerDeath += LoadGameOver;
            EnemyService.Instance.EnemiesFinished += LoadGameWon;
        }

        private void OnDestroy()
        {
            _input.PauseEvent -= HandlePause;
            _input.ResumeEvent -= HandleResume;
            PlayerService.Instance.OnPlayerDeath -= LoadGameOver;
            EnemyService.Instance.EnemiesFinished -= LoadGameWon;
        }

        public void LoadGameOver()
        {
            gameOverMenu.SetActive(true);
            restartbtn.onClick.AddListener(RestartLevel);
        }

        public void LoadGameWon()
        {
            gameWonMenu.SetActive(true);
            PlayerService.Instance.StopPlayerMoveController();
            addEnemies.onClick.AddListener(AddMoreEnemies);
        }

        private void AddMoreEnemies()
        {
            EnemyService.Instance.CreateEnemies();
            PlayerService.Instance.StartPlayerMoveController();
            gameWonMenu.SetActive(false);
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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