using System;
using UnityEngine;
using Utilities;

namespace Managers
{
    [DefaultExecutionOrder(-50)]
    public class GameManager : Manager
    {
        public static GameManager Instance;

        private DataManager m_dataManager;
        private MenuManager m_menuManager;
        private BattleManager m_battleManager;
        
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
        
            LoadPrefs();
        }

        private void Start()
        {
            LoadDependencies();
            LoadMenu();
        }

        public void LoadPrefs()
        {
        
        }
        
        public void LoadDependencies()
        {
            m_dataManager = m_dependencyContainer.Resolve<DataManager>();
            m_menuManager = m_dependencyContainer.Resolve<MenuManager>();
            m_battleManager = m_dependencyContainer.Resolve<BattleManager>();
        }

        public void LoadMenu()
        {
        
        }

        public void LoadBattle()
        {
        
        }
    }
}
