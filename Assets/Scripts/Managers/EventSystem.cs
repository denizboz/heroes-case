using System;
using Utilities;

namespace Managers
{
    public enum CoreEvent { MenuLoaded, BattleStarted, BattleWon, BattleLost }
    public enum MenuEvent { HeroSelected, HeroDeselected }
    
    public static class EventSystem
    {
        public static event Action MenuLoadedEvent, BattleStartedEvent, BattleWonEvent, BattleLostEvent;
        
        public static event Action<HeroData> HeroSelectedEvent, HeroDeselectedEvent;

        
        private static readonly Action[] CoreEvents = new Action[]
        {
            MenuLoadedEvent, BattleStartedEvent, BattleWonEvent, BattleLostEvent
        };

        private static readonly Action<HeroData>[] MenuEvents = new Action<HeroData>[]
        {
            HeroSelectedEvent, HeroDeselectedEvent
        };

        
        public static void Invoke(CoreEvent coreEvent)
        {
            CoreEvents[(int)coreEvent]?.Invoke();
        }

        public static void Invoke(MenuEvent menuEvent, HeroData heroData)
        {
            MenuEvents[(int)menuEvent]?.Invoke(heroData);
        }
        
        public static void AddListener(CoreEvent coreEvent, Action action)
        {
            CoreEvents[(int)coreEvent] += action;
        }

        public static void AddListener(MenuEvent menuEvent, Action<HeroData> action)
        {
            MenuEvents[(int)menuEvent] += action;
        }
        
        public static void RemoveListener(CoreEvent coreEvent, Action action)
        {
            CoreEvents[(int)coreEvent] -= action;
        }
        
        public static void RemoveListener(MenuEvent menuEvent, Action<HeroData> action)
        {
            MenuEvents[(int)menuEvent] -= action;
        }
    }
}
