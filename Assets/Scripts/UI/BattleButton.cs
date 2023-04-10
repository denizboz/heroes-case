using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class BattleButton : MonoBehaviour
    {
        private static Button button;
        
        private void Awake()
        {
            button = GetComponent<Button>();
            
            EventSystem.AddListener(MenuEvent.HeroSelected, UpdateInteractability);
            EventSystem.AddListener(MenuEvent.HeroDeselected, UpdateInteractability);
        }

        private void OnEnable()
        {
            UpdateInteractability(false);
        }

        private static void UpdateInteractability(bool val)
        {
            button.interactable = val;
        }

        private static void UpdateInteractability(HeroData data)
        {
            button.interactable = !MenuManager.IsNewSelectionAllowed;
        }
        
        public static void StartBattle()
        {
            GameManager.Instance.LoadBattle();
        }
    }
}
