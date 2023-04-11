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
            
            GameEvents.AddListener(MenuEvent.HeroSelected, UpdateInteractability);
            GameEvents.AddListener(MenuEvent.HeroDeselected, UpdateInteractability);
        }

        private void OnEnable()
        {
            UpdateInteractability(false);
        }

        private void OnDisable()
        {
            GameEvents.RemoveListener(MenuEvent.HeroSelected, UpdateInteractability);
            GameEvents.RemoveListener(MenuEvent.HeroDeselected, UpdateInteractability);
        }

        private static void UpdateInteractability(bool val)
        {
            button.interactable = val;
        }

        private static void UpdateInteractability(HeroData data)
        {
            button.interactable = !MenuManager.IsSelectionAllowed;
        }
        
        public static void StartBattle()
        {
            GameEvents.Invoke(CoreEvent.BattleLoaded);
        }
    }
}
