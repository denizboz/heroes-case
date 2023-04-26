using System;
using System.IO;
using System.Linq;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Managers
{
    [DefaultExecutionOrder(-30)]
    public class DataManager : Manager
    {
        [SerializeField] private HeroDataContainerSO m_dataContainer;

        private static int battleCount;

        private const int stepForProgress = 5;
        
        private const string jsonFileName = "HeroAttributeData.json";
        private const string prefsKeyForJson = "is_json_created";
        private const string prefsKeyForBattleCount = "battle_count";
        
        protected override void Awake()
        {
            m_dependencyContainer.Bind<DataManager>(this);

            if (PlayerPrefs.HasKey(prefsKeyForJson))
                return;
            
            GenerateAndSaveAttributes();
        }

        private void OnEnable()
        {
            battleCount = PlayerPrefs.GetInt(prefsKeyForBattleCount);
            
            GameEvents.AddListener(CoreEvent.BattleWon, UpdatePersistentData);
            GameEvents.AddListener(CoreEvent.BattleLost, UpdatePersistentData);
        }

        private void GenerateAndSaveAttributes()
        {
            PlayerPrefs.SetInt(prefsKeyForBattleCount, 0);
            
            var identityArray = m_dataContainer.IdentityArray;

            var totalHeroCount = identityArray.Length;
            var dataArray = new HeroData[totalHeroCount];
            
            for (var i = 0; i < totalHeroCount; i++)
            {
                var maxHealth = Random.Range(m_dataContainer.MinHealthLimit, m_dataContainer.MaxHealthLimit + 1);
                var attackPower = Random.Range(m_dataContainer.MinAttackPower, m_dataContainer.MaxAttackPower + 1);

                dataArray[i] = new HeroData
                {
                    Name = identityArray[i].Name,
                    Color = identityArray[i].Color,
                    
                    MaxHealth = maxHealth,
                    AttackPower = attackPower,
                    Experience = 0,
                    Level = 1,
                    
                    IsUnlocked = i < 3
                };
            }
            
            SaveDataToJson(dataArray);
        }

        private static void SaveDataToJson(HeroData[] array)
        {
            var path = Path.Combine(Application.persistentDataPath, jsonFileName);

            var fileMode = File.Exists(path) ? FileMode.Open : FileMode.Create;
            var fileStream = new FileStream(path, fileMode);
            
            var writer = new StreamWriter(fileStream);
            
            for (var i = 0; i < array.Length; i++)
            {
                var line = JsonUtility.ToJson(array[i]);
                writer.WriteLine(line);
            }
            
            writer.Close();
            fileStream.Close();
            
            PlayerPrefs.SetInt(prefsKeyForJson, 1);
        }
        
        public HeroData[] LoadDataFromJson()
        {
            var path = Path.Combine(Application.persistentDataPath, jsonFileName);
            var lines = File.ReadAllLines(path);

            var dataArray = new HeroData[lines.Length];

            for (var i = 0; i < dataArray.Length; i++)
                dataArray[i] = JsonUtility.FromJson<HeroData>(lines[i]);

            return dataArray;
        }

        private void UpdatePersistentData()
        {
            var aliveHeroNames = BattleManager.GetAliveHeroNames();
            
            var dataArray = LoadDataFromJson();
            
            foreach (var _name in aliveHeroNames)
            {
                var entry = dataArray.Single(data => data.Name == _name);
                int index = Array.IndexOf(dataArray, entry);
                
                entry.Experience++;

                if (entry.Experience % stepForProgress == 0)
                {
                    entry.MaxHealth *= 1.1f;
                    entry.AttackPower *= 1.1f;

                    entry.Level++;
                }

                dataArray[index] = entry;
            }
            
            battleCount++;
            PlayerPrefs.SetInt(prefsKeyForBattleCount, battleCount);
            
            if (battleCount % stepForProgress != 0)
            {
                SaveDataToJson(dataArray);
                return;
            }
            
            int unlockedCount = dataArray.Count(entry => entry.IsUnlocked);

            if (unlockedCount > 9)
                return;

            dataArray[unlockedCount].IsUnlocked = true;
            
            SaveDataToJson(dataArray);
            
            battleCount = 0;
            PlayerPrefs.SetInt(prefsKeyForBattleCount, battleCount);
        }
    }
}
