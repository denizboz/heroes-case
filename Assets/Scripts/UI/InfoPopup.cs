using UnityEngine;
using Utilities;
using TMPro;

namespace UI
{
    public class InfoPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_nameText;
        [SerializeField] private TextMeshProUGUI m_levelText;
        [SerializeField] private TextMeshProUGUI m_powerText;
        [SerializeField] private TextMeshProUGUI m_healthText;
        [SerializeField] private TextMeshProUGUI m_experienceText;

        private static TextMeshProUGUI nameUI, levelUI, expUI, healthUI, powerUI;
        
        private static GameObject popupObject;
        private static RectTransform rectTr;

        private void Awake()
        {
            SetStaticReferences();
            popupObject.SetActive(false);
        }

        public static void DisplayDataAt(HeroData heroData, Vector2 screenPos)
        {
            nameUI.text = $"Name: {heroData.Name}";
            levelUI.text = $"Level: {heroData.Level.ToString()}";
            powerUI.text = $"Attack Power: {heroData.AttackPower:F1}";
            healthUI.text = $"Health: {heroData.MaxHealth:F1}";
            expUI.text = $"Experience: {heroData.Experience.ToString()}";
            
            SetPositionWithOffset(screenPos);
            popupObject.SetActive(true);
        }
        
        public static void Close()
        {
            popupObject.SetActive(false);
        }

        private void SetStaticReferences()
        {
            popupObject = gameObject;
            rectTr = GetComponent<RectTransform>();
            
            nameUI = m_nameText;
            levelUI = m_levelText;
            expUI = m_experienceText;
            healthUI = m_healthText;
            powerUI = m_powerText;
        }

        /// <summary>
        /// Sets position and changes the pivot position to create an offset depending on the screen position.
        /// </summary>
        private static void SetPositionWithOffset(Vector2 screenPos)
        {
            if (screenPos.x < 0f)
                rectTr.pivot = screenPos.y < 0f ? Vector2.zero : Vector2.up;
            else
                rectTr.pivot = screenPos.y < 0f ? Vector2.right : Vector2.one;
            
            rectTr.anchoredPosition = screenPos;
        }
    }
}
