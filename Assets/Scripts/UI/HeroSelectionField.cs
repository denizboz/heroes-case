using Events;
using Events.Implementations.Menu;
using Managers;
using Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HeroSelectionField : MonoBehaviour
    {
        [SerializeField] private Image m_image;
        
        private bool m_isSelected;

        private HeroData m_heroData;
        private Outline m_outline;

        private Vector2 m_screenPos;
        
        private bool m_isBeingSelected;
        private float m_timer;

        private const float tapAndHoldThreshold = 2f;

        private void Awake()
        {
            m_screenPos = GetComponent<RectTransform>().anchoredPosition;
            m_outline = GetComponent<Outline>();
        }

        private void Update()
        {
            if (!m_isBeingSelected)
                return;
            
            if (m_timer < tapAndHoldThreshold)
                m_timer += Time.deltaTime;
            else
            {
                m_isBeingSelected = false;
                InfoPopup.DisplayDataAt(m_heroData, m_screenPos);
            }
        }

        public void OnPointerDown()
        {
            m_isBeingSelected = true;
        }

        public void OnPointerUp()
        {
            m_timer = 0f;

            if (!m_isBeingSelected)
            {
                InfoPopup.Close();
                return;
            }
            
            m_isBeingSelected = false;
            
            if (!m_isSelected)
                SelectHero();
            else
                DeselectHero();
        }
        
        private void SelectHero()
        {
            if (!MenuManager.IsSelectionAllowed)
                return;
            
            m_isSelected = true;
            m_outline.enabled = true;
            
            GameEvents.Invoke<HeroSelectedEvent>(m_heroData);
        }

        private void DeselectHero()
        {
            m_isSelected = false;
            m_outline.enabled = false;
            
            GameEvents.Invoke<HeroDeselectedEvent>(m_heroData);
        }
        
        public void SetHero(HeroData heroData)
        {
            m_image.enabled = heroData.IsUnlocked;

            if (!heroData.IsUnlocked) 
                return;

            m_image.color = heroData.Color;
            m_heroData = heroData;
        }
    }
}
