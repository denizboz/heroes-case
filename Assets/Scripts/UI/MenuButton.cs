using Events;
using Events.Implementations.Core;
using UnityEngine;

namespace UI
{
    public class MenuButton : MonoBehaviour
    {
        public static void GoBackToMenu()
        {
            GameEvents.Invoke<MenuLoadedEvent>();
        }
    }
}