using System;
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
        }

        private void OnEnable()
        {
            UpdateInteractability(false);
        }

        public static void UpdateInteractability(bool val)
        {
            button.interactable = val;
        }

        public static void StartBattle()
        {
            // start battle
        }
    }
}
