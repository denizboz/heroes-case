using UnityEngine;
using Managers;
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
            GameEvents.AddListener(CoreEvent.BattleWon, ShowWonUI);
            GameEvents.AddListener(CoreEvent.BattleLost, ShowLostUI);
        }

        private void OnDisable()
        {
            GameEvents.RemoveListener(CoreEvent.BattleWon, ShowWonUI);
            GameEvents.RemoveListener(CoreEvent.BattleLost, ShowLostUI);
        }

        private static void ShowWonUI()
        {
            ShowCanvas(battleWon: true);
        }

        private static void ShowLostUI()
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
