using Mechanics;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Events;
using Events.Implementations.Battle;
using Events.Implementations.Core;

namespace Managers
{
    public enum BattleState { PlayerTurn, EnemyTurn, Over }
    
    public class BattleManager : Manager
    {
        [SerializeField] private Hero[] m_heroes;
        [SerializeField] private Enemy m_enemy;

        private static readonly List<Battler> liveHeroesList = new List<Battler>(3);

        private static Camera mainCam;
        public static BattleState battleState { get; private set; }

        protected override void Awake()
        {
            m_dependencyContainer.Bind<BattleManager>(this);
            
            mainCam = Camera.main;
        }

        private void OnEnable()
        {
            LoadHeroes();
            battleState = BattleState.PlayerTurn;
            
            GameEvents.AddListener<HeroIsShotEvent>(AllowHeroToAttack);
            GameEvents.AddListener<EnemyIsShotEvent>(MakeEnemyAttack);
            
            GameEvents.Invoke<BattleStartedEvent>();
        }

        private void OnDisable()
        {
            GameEvents.RemoveListener<HeroIsShotEvent>(AllowHeroToAttack);
            GameEvents.RemoveListener<EnemyIsShotEvent>(MakeEnemyAttack);
        }

        private void Update()
        {
            if (battleState != BattleState.PlayerTurn)
                return;
            
            if (Input.GetMouseButtonDown(0))
            {
                var ray = mainCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.TryGetComponent(out Hero hero))
                    {
                        hero.Attack(m_enemy);
                        ChangeTurns(BattleState.EnemyTurn);
                    }
                }
            }
        }

        private void LoadHeroes()
        {
            liveHeroesList.Clear();
            
            var selectedDataArray = MenuManager.SelectedDataArray;

            for (var i = 0; i < 3; i++)
            {
                var hero = m_heroes[i];
                var data = selectedDataArray[i];
                
                hero.SetReady(data.Name, data.Color, data.MaxHealth, data.AttackPower);

                liveHeroesList.Add(hero);
            }
            
            m_enemy.SetReady("Enemy", Color.red, 10f, 3f);
        }

        private static void ChangeTurns(BattleState state)
        {
            battleState = state;
        }

        private static void AllowHeroToAttack(object obj)
        {
            ChangeTurns(BattleState.PlayerTurn);
        }
        
        private void MakeEnemyAttack(object obj)
        {
            if (battleState == BattleState.Over)
                return;
            
            int rand = Random.Range(0, liveHeroesList.Count);
            var targetedHero = liveHeroesList[rand];
            
            DOTween.Sequence().AppendInterval(0.5f).OnComplete(() =>
            {
                if (battleState != BattleState.Over)
                    m_enemy.Attack(targetedHero);
            });
        }
        
        public static void BattlerDown(Battler battler)
        {
            if (battler is Enemy)
                EndBattle(playerWon: true);
            else
            {
                liveHeroesList.Remove(battler);
                
                if (liveHeroesList.Count < 1)
                    EndBattle(playerWon: false);
            }
        }

        private static void EndBattle(bool playerWon)
        {
            battleState = BattleState.Over;
            
            if (playerWon)
                GameEvents.Invoke<BattleWonEvent>();
            else
                GameEvents.Invoke<BattleLostEvent>();
        }

        public static string[] GetAliveHeroNames()
        {
            return liveHeroesList.Select(hero => hero.Name).ToArray();
        }
    }
}
