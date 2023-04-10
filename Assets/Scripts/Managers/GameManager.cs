using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{
    [DefaultExecutionOrder(-50)]
    public class GameManager : Manager
    {
        public static GameManager Instance;

        protected override void Awake()
        {
            #region SINGLETON
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            #endregion

            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
        }

        private void Start()
        {
            LoadMenu(forTheFirstTime: true);
        }

        public void LoadMenu(bool forTheFirstTime = false)
        {
            if (!forTheFirstTime)
                SceneManager.UnloadSceneAsync(2);
            
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        public void LoadBattle()
        {
            SceneManager.UnloadSceneAsync(1);
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}
