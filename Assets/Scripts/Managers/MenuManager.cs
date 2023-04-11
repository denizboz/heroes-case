using Utilities;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Managers
{
    [DefaultExecutionOrder(-10)]
    public class MenuManager : Manager
    {
        [SerializeField] private HeroSelectionField[] m_selectionFields;
        
        private static readonly List<HeroData> selectedHeroesList = new List<HeroData>();

        public static HeroData[] SelectedDataArray => selectedHeroesList.ToArray();
        public static bool IsSelectionAllowed => selectedHeroesList.Count < 3;

        
        protected override void Awake()
        {
            m_dependencyContainer.Bind<MenuManager>(this);
            
            GameEvents.AddListener(MenuEvent.HeroSelected, RegisterSelectedHero);
            GameEvents.AddListener(MenuEvent.HeroDeselected, RemoveSelectedHero);
            
            GameEvents.AddListener(CoreEvent.BattleStarted, ClearSelection);
        }

        private void OnEnable()
        {
            SetupHeroFields();
        }

        private void OnDisable()
        {
            GameEvents.RemoveListener(MenuEvent.HeroSelected, RegisterSelectedHero);
            GameEvents.RemoveListener(MenuEvent.HeroDeselected, RemoveSelectedHero);
        }

        private static void RegisterSelectedHero(HeroData heroData)
        {
            selectedHeroesList.Add(heroData);
        }

        private static void RemoveSelectedHero(HeroData heroData)
        {
            selectedHeroesList.Remove(heroData);
        }

        private void SetupHeroFields()
        {
            var dataManager = m_dependencyContainer.Resolve<DataManager>();
            var dataArray = dataManager.LoadDataFromJson();
            
            for (var i = 0; i < dataArray.Length; i++)
            {
                m_selectionFields[i].SetHero(dataArray[i]);
            }
        }

        private static void ClearSelection()
        {
            selectedHeroesList.Clear();
            
            GameEvents.RemoveListener(CoreEvent.BattleStarted, ClearSelection);
        }
    }
}
