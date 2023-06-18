using Events;
using Events.Implementations.Core;
using Events.Implementations.Menu;
using Managers;
using Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BattleButton : MonoBehaviour
    {
        private static Button button;
        
        private void Awake()
        {
            button = GetComponent<Button>();
            
            GameEvents.AddListener<HeroSelectedEvent>(UpdateInteractability);
            GameEvents.AddListener<HeroDeselectedEvent>(UpdateInteractability);
        }

        private void OnEnable()
        {
            button.interactable = false;
        }

        private void OnDisable()
        {
            GameEvents.RemoveListener<HeroSelectedEvent>(UpdateInteractability);
            GameEvents.RemoveListener<HeroDeselectedEvent>(UpdateInteractability);
        }

        private static void UpdateInteractability(object data)
        {
            button.interactable = !MenuManager.IsSelectionAllowed;
        }
        
        public static void StartBattle()
        {
            GameEvents.Invoke<BattleLoadedEvent>();
        }
    }
}
