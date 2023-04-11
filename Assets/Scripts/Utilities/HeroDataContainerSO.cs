using System;
using UnityEngine;

namespace Utilities
{
    [CreateAssetMenu(fileName = "HeroDataContainer", menuName = "Data Container/Hero Data Container")]
    public class HeroDataContainerSO : ScriptableObject
    {
        /// <summary>
        /// Lower limit for maximum health range.
        /// </summary>
        [Tooltip("Lower Limit for Max Health")]
        public int MinHealthLimit = 3;
        
        /// <summary>
        /// Upper limit for maximum health range.
        /// </summary>
        [Tooltip("Upper Limit for Max Health")]
        public int MaxHealthLimit = 10;

        public int MinAttackPower = 1;
        public int MaxAttackPower = 5;
        
        public HeroIdentity[] IdentityArray;
    }
    
    [Serializable]
    public struct HeroIdentity
    {
        public string Name;
        public Color Color;
    }

    public struct HeroData
    {
        public string Name;
        public Color Color;
        
        public float MaxHealth;
        public float AttackPower;
        public int Experience;
        public int Level;

        public bool IsUnlocked;
    }
}
