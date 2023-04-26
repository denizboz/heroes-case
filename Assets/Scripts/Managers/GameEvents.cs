using System;
using System.Collections.Generic;
using Utilities;

namespace Managers
{
    public enum CoreEvent { MenuLoaded, BattleLoaded, BattleStarted, BattleWon, BattleLost }
    public enum MenuEvent { HeroSelected, HeroDeselected }
    public enum BattleEvent { HeroIsShot, EnemyIsShot }
    
    public static class GameEvents
    {
        private static readonly Dictionary<CoreEvent, Action> coreEvents = new Dictionary<CoreEvent, Action>();
        private static readonly Dictionary<BattleEvent, Action> battleEvents = new Dictionary<BattleEvent, Action>();
        private static readonly Dictionary<MenuEvent, Action<HeroData>> menuEvents = new Dictionary<MenuEvent, Action<HeroData>>();

        
        public static void Invoke(CoreEvent coreEvent)
        {
            coreEvents[coreEvent]?.Invoke();
        }

        public static void Invoke(BattleEvent battleEvent)
        {
            battleEvents[battleEvent]?.Invoke();
        }
        
        public static void Invoke(MenuEvent menuEvent, HeroData heroData)
        {
            menuEvents[menuEvent]?.Invoke(heroData);
        }
        
        public static void AddListener(CoreEvent coreEvent, Action action)
        {
            if (!coreEvents.TryAdd(coreEvent, action))
                coreEvents[coreEvent] += action;
        }
        
        public static void AddListener(BattleEvent battleEvent, Action action)
        {
            if (!battleEvents.TryAdd(battleEvent, action))
                battleEvents[battleEvent] += action;
        }

        public static void AddListener(MenuEvent menuEvent, Action<HeroData> action)
        {
            if (!menuEvents.TryAdd(menuEvent, action))
                menuEvents[menuEvent] += action;
        }
        
        public static void RemoveListener(CoreEvent coreEvent, Action action)
        {
            if (!coreEvents.ContainsKey(coreEvent))
                return;
            
            coreEvents[coreEvent] -= action;
        }
        
        public static void RemoveListener(BattleEvent battleEvent, Action action)
        {
            if (!battleEvents.ContainsKey(battleEvent))
                return;
            
            battleEvents[battleEvent] -= action;
        }
        
        public static void RemoveListener(MenuEvent menuEvent, Action<HeroData> action)
        {
            if (!menuEvents.ContainsKey(menuEvent))
                return;
            
            menuEvents[menuEvent] -= action;
        }
    }
}
