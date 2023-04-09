using UnityEngine;
using Utilities;

namespace Managers
{
    public class MenuManager : Manager
    {
        protected override void Awake()
        {
            m_dependencyContainer.Bind<MenuManager>(this);
        }
        
        
    }
}
