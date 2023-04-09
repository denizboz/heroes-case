using UnityEngine;
using Utilities;

namespace Managers
{
    public class BattleManager : Manager
    {
        protected override void Awake()
        {
            m_dependencyContainer.Bind<BattleManager>(this);
        }
    }
}
