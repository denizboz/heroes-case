using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class HeroSelectionField : MonoBehaviour
    {
        [SerializeField] private Image m_image;
        
        public bool IsSelected;

        private HeroData m_heroData;
        private Outline m_outline;
        
        private bool m_isBeingSelected;
        [SerializeField] private float m_timer;

        private const float tapAndHoldThreshold = 3f;

        private void Awake()
        {
            m_outline = GetComponent<Outline>();
        }

        private void OnEnable()
        {
            DeselectHero();
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
                InfoPopup.DisplayDataAt();
            }
        }

        public void SetHero(HeroData heroData)
        {
            m_image.enabled = heroData.IsUnlocked;

            if (!heroData.IsUnlocked)
                return;

            m_image.color = heroData.Color;
            m_heroData = heroData;
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
            
            if (!IsSelected)
                SelectHero();
            else
                DeselectHero();
        }
        
        private void SelectHero()
        {
            IsSelected = true;
            m_outline.enabled = true;
        }

        private void DeselectHero()
        {
            IsSelected = false;
            m_outline.enabled = false;
        }
    }
}
