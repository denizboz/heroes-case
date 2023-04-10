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
        
        private int m_numberOfSelectedHeroes;

        private static readonly List<HeroData> selectedHeroesList = new List<HeroData>();

        public static bool IsNewSelectionAllowed => selectedHeroesList.Count < 3;
        
        protected override void Awake()
        {
            m_dependencyContainer.Bind<MenuManager>(this);
            
            EventSystem.AddListener(MenuEvent.HeroSelected, RegisterSelectedHero);
            EventSystem.AddListener(MenuEvent.HeroDeselected, RemoveSelectedHero);
        }

        private void OnEnable()
        {
            SetupHeroFields();
        }

        private static void RegisterSelectedHero(HeroData heroData)
        {
            selectedHeroesList.Add(heroData);
        }

        private static void RemoveSelectedHero(HeroData heroData)
        {
            selectedHeroesList.Remove(heroData);
        }

        public void SetupHeroFields()
        {
            var dataArray = DataManager.ActiveDataArray;
            
            for (var i = 0; i < dataArray.Length; i++)
            {
                m_selectionFields[i].SetHero(dataArray[i]);
            }
        }
    }
}
