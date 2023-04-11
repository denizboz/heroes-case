using System;
using Utilities;

namespace Managers
{
    public enum CoreEvent { MenuLoaded, BattleLoaded, BattleStarted, BattleWon, BattleLost }
    public enum MenuEvent { HeroSelected, HeroDeselected }
    
    public enum BattleEvent { HeroIsShot, EnemyIsShot }
    
    public static class GameEvents
    {
        public static event Action MenuLoadedEvent, BattleLoadedEvent, BattleStartedEvent, BattleWonEvent, BattleLostEvent;
        public static event Action HeroIsShotEvent, EnemyIsShotEvent; 
        public static event Action<HeroData> HeroSelectedEvent, HeroDeselectedEvent;

        
        private static readonly Action[] CoreEvents = new Action[]
        {
            MenuLoadedEvent, BattleLoadedEvent, BattleStartedEvent, BattleWonEvent, BattleLostEvent
        };

        private static readonly Action[] BattleEvents = new Action[]
        {
            HeroIsShotEvent, EnemyIsShotEvent
        };
        
        private static readonly Action<HeroData>[] MenuEvents = new Action<HeroData>[]
        {
            HeroSelectedEvent, HeroDeselectedEvent
        };

        
        public static void Invoke(CoreEvent coreEvent)
        {
            CoreEvents[(int)coreEvent]?.Invoke();
        }

        public static void Invoke(BattleEvent battleEvent)
        {
            BattleEvents[(int)battleEvent]?.Invoke();
        }
        
        public static void Invoke(MenuEvent menuEvent, HeroData heroData)
        {
            MenuEvents[(int)menuEvent]?.Invoke(heroData);
        }
        
        public static void AddListener(CoreEvent coreEvent, Action action)
        {
            CoreEvents[(int)coreEvent] += action;
        }
        
        public static void AddListener(BattleEvent battleEvent, Action action)
        {
            BattleEvents[(int)battleEvent] += action;
        }

        public static void AddListener(MenuEvent menuEvent, Action<HeroData> action)
        {
            MenuEvents[(int)menuEvent] += action;
        }
        
        public static void RemoveListener(CoreEvent coreEvent, Action action)
        {
            CoreEvents[(int)coreEvent] -= action;
        }
        
        public static void RemoveListener(BattleEvent battleEvent, Action action)
        {
            BattleEvents[(int)battleEvent] -= action;
        }
        
        public static void RemoveListener(MenuEvent menuEvent, Action<HeroData> action)
        {
            MenuEvents[(int)menuEvent] -= action;
        }
    }
}
