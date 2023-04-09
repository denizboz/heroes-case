using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities
{
    [CreateAssetMenu(fileName = "HeroDataContainer", menuName = "Data Container/Hero Fixed Data Container")]
    public class HeroDataContainerSO : ScriptableObject
    {
        [Tooltip("Lower Limit For Max Health")]
        public int MinHealth = 3;
        [Tooltip("Upper Limit For Max Health")]
        public int MaxHealth = 10;


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

        public int Unlocked; // 1 for true, 0 for false
    }
}
