using Events;
using Events.Implementations.Core;
using UnityEngine;
using TMPro;

namespace UI
{
    public class BattleEndCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas m_canvas;
        [SerializeField] private TextMeshProUGUI m_resultText;

        private static Canvas canvas;
        private static TextMeshProUGUI resultUI;
        
        private void Awake()
        {
            canvas = m_canvas;
            resultUI = m_resultText;

            canvas.enabled = false;
        }

        private void OnEnable()
        {
            GameEvents.AddListener<BattleWonEvent>(ShowWonUI);
            GameEvents.AddListener<BattleLostEvent>(ShowLostUI);
        }

        private void OnDisable()
        {
            GameEvents.RemoveListener<BattleWonEvent>(ShowWonUI);
            GameEvents.RemoveListener<BattleLostEvent>(ShowLostUI);
        }

        private static void ShowWonUI(object obj)
        {
            ShowCanvas(battleWon: true);
        }

        private static void ShowLostUI(object obj)
        {
            ShowCanvas(battleWon: false);
        }
        
        private static void ShowCanvas(bool battleWon)
        {
            canvas.enabled = true;
            resultUI.text = battleWon ? "BATTLE WON!" : "BATTLE LOST :(";
        }
    }
}
