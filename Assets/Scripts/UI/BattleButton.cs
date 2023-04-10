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
            
            EventSystem.AddListener(MenuEvent.HeroSelected, UpdateInteractability);
            EventSystem.AddListener(MenuEvent.HeroDeselected, UpdateInteractability);
        }

        private void OnEnable()
        {
            UpdateInteractability(false);
        }

        private void OnDisable()
        {
            EventSystem.RemoveListener(MenuEvent.HeroSelected, UpdateInteractability);
            EventSystem.RemoveListener(MenuEvent.HeroDeselected, UpdateInteractability);
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
            EventSystem.Invoke(CoreEvent.BattleLoaded);
        }
    }
}
