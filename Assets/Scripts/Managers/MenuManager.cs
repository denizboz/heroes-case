using Utilities;
using System.Collections.Generic;
using Events;
using Events.Implementations.Core;
using Events.Implementations.Menu;
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
            
            GameEvents.AddListener<HeroSelectedEvent>(RegisterSelectedHero);
            GameEvents.AddListener<HeroDeselectedEvent>(RemoveSelectedHero);
            
            GameEvents.AddListener<BattleStartedEvent>(ClearSelection);
        }

        private void OnEnable()
        {
            SetupHeroFields();
        }

        private void OnDisable()
        {
            GameEvents.RemoveListener<HeroSelectedEvent>(RegisterSelectedHero);
            GameEvents.RemoveListener<HeroDeselectedEvent>(RemoveSelectedHero);
        }

        private static void RegisterSelectedHero(object heroData)
        {
            selectedHeroesList.Add((HeroData)heroData);
        }

        private static void RemoveSelectedHero(object heroData)
        {
            selectedHeroesList.Remove((HeroData)heroData);
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

        private static void ClearSelection(object obj)
        {
            selectedHeroesList.Clear();
            
            GameEvents.RemoveListener<BattleStartedEvent>(ClearSelection);
        }
    }
}
