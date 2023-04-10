using Mechanics;
using UnityEngine;

namespace Managers
{
    public enum BattleState { PlayerTurn, EnemyTurn, Idle }
    
    public class BattleManager : Manager
    {
        [SerializeField] private Hero[] m_heroes;
        [SerializeField] private Enemy m_enemy;
        [SerializeField] private LightBeam m_lightBeam;

        private static BattleState m_battleState;
        
        private static Camera mainCam;

        protected override void Awake()
        {
            m_dependencyContainer.Bind<BattleManager>(this);
            mainCam = Camera.main;
        }

        private void OnEnable()
        {
            LoadHeroes();
            m_battleState = BattleState.PlayerTurn;
            
            EventSystem.Invoke(CoreEvent.BattleStarted);
        }

        private void Update()
        {
            if (m_battleState != BattleState.PlayerTurn)
                return;
            
            if (Input.GetMouseButtonDown(0))
            {
                var ray = mainCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.TryGetComponent(out Hero hero))
                    {
                        m_lightBeam.Shoot(from: hero.transform.position, to: m_enemy.transform.position);
                    }
                }
            }
        }

        private void LoadHeroes()
        {
            var selectedDataArray = MenuManager.SelectedDataArray;

            for (var i = 0; i < 3; i++)
            {
                var hero = m_heroes[i];
                var data = selectedDataArray[i];
                
                hero.SetReady(data.Color, data.MaxHealth, data.AttackPower);
            }
            
            m_enemy.SetReady(Color.red, 10f, 3f);
        }

        public static void ChangeTurns(BattleState battleState)
        {
            m_battleState = battleState;
        }
    }
}
