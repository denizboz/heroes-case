using System;

namespace Managers
{
    public enum GameEvent { MenuLoaded, BattleStarted, BattleWon, BattleLost }
    
    public static class GameEvents
    {
        public static event Action MenuLoadedEvent;
        public static event Action BattleStartedEvent;
        public static event Action BattleWonEvent;
        public static event Action BattleLostEvent;

        private static readonly Action[] Events = new Action[]
        {
            MenuLoadedEvent, BattleStartedEvent, BattleWonEvent, BattleLostEvent
        };

        public static void Invoke(GameEvent gameEvent)
        {
            Events[(int)gameEvent]?.Invoke();
        }

        public static void AddListener(GameEvent gameEvent, Action action)
        {
            Events[(int)gameEvent] += action;
        }

        public static void RemoveListener(GameEvent gameEvent, Action action)
        {
            Events[(int)gameEvent] -= action;
        }
    }
}
