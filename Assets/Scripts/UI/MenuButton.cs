using Managers;
using UnityEngine;

namespace UI
{
    public class MenuButton : MonoBehaviour
    {
        public static void GoBackToMenu()
        {
            GameEvents.Invoke(CoreEvent.MenuLoaded);
        }
    }
}