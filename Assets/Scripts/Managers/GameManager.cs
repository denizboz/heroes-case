using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    [DefaultExecutionOrder(-50)]
    public class GameManager : Manager
    {
        protected override void Awake()
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
        }

        private void OnEnable()
        {
            GameEvents.AddListener(CoreEvent.MenuLoaded, LoadMenu);
            GameEvents.AddListener(CoreEvent.BattleLoaded, LoadBattle);
            
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        private static void LoadMenu()
        {
            SceneManager.UnloadSceneAsync(2);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        private static void LoadBattle()
        {
            SceneManager.UnloadSceneAsync(1);
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}
