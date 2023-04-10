using Managers;
using UnityEngine;

namespace UI
{
    public class MenuButton : MonoBehaviour
    {
        public static void GoBackToMenu()
        {
            EventSystem.Invoke(CoreEvent.MenuLoaded);
        }
    }
}