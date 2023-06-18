using Events;
using Events.Implementations.Core;
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
            GameEvents.AddListener<MenuLoadedEvent>(LoadMenu);
            GameEvents.AddListener<BattleLoadedEvent>(LoadBattle);
            
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        private static void LoadMenu(object obj)
        {
            SceneManager.UnloadSceneAsync(2);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        private static void LoadBattle(object obj)
        {
            SceneManager.UnloadSceneAsync(1);
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}
