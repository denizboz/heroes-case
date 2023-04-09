using UnityEngine;
using Utilities;

namespace Managers
{
    public abstract class Manager : MonoBehaviour
    {
        [SerializeField] protected DependencyContainerSO m_dependencyContainer;

        protected abstract void Awake();
    }
}
