using System.IO;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class DataManager : Manager
    {
        [SerializeField] private HeroDataContainerSO m_dataContainer;

        private const string prefsKeyForJson = "is_json_created";
        
        protected override void Awake()
        {
            m_dependencyContainer.Bind<DataManager>(this);

            if (PlayerPrefs.HasKey(prefsKeyForJson))
                return;
            
            GenerateAndSaveAttributes();
        }

        private void GenerateAndSaveAttributes()
        {
            var identityArray = m_dataContainer.IdentityArray;

            var totalHeroCount = identityArray.Length;
            var dataArray = new HeroData[totalHeroCount];
            
            for (var i = 0; i < totalHeroCount; i++)
            {
                var maxHealth = Random.Range(m_dataContainer.MinHealth, m_dataContainer.MaxHealth + 1);
                var attackPower = Random.Range(m_dataContainer.MinAttackPower, m_dataContainer.MaxAttackPower + 1);

                dataArray[i] = new HeroData
                {
                    Name = identityArray[i].Name,
                    Color = identityArray[i].Color,
                    
                    MaxHealth = maxHealth,
                    AttackPower = attackPower,
                    Experience = 0,
                    Level = 1,
                    
                    Unlocked = i < 3 ? 1 : 0
                };
            }
            
            CreateJsonFromDataArray(dataArray);
        }
        
        private static void CreateJsonFromDataArray(HeroData[] array)
        {
            var path = Path.Combine(Application.persistentDataPath, "HeroAttributeData.json");

            var fileStream = new FileStream(path, FileMode.Create);
            var writer = new StreamWriter(fileStream);
            
            for (var i = 0; i < array.Length; i++)
            {
                var line = JsonUtility.ToJson(array[i]);
                writer.WriteLine(line);
            }
            
            writer.Close();
            fileStream.Close();
            
            PlayerPrefs.SetInt(prefsKeyForJson, 1);
            
            Debug.Log("Json created & saved.");
        }
    }
}
