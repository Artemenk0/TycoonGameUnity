using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    private string filePath;
    private string directoryPath;

    private void Awake()
    {
        directoryPath = Path.Combine(Application.persistentDataPath, "Saves");
        filePath = Path.Combine(directoryPath, "saveData.json");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);

        }
    }

    public void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

    }

    public PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            return data;
        }
        else
        {

            return new PlayerData(); 
        }
    }

}

[System.Serializable]
public class PlayerData
{
    public int money = 0;  

    public int potato = 0;  
    public int tomato = 0; 
    public int carrot = 0; 
    public int eggplants = 0; 
    public int pumpkin = 0; 

    public List<BuildingData> buildings = new List<BuildingData>();
    public List<string> progres = new List<string>();
}

[System.Serializable]
public class BuildingData
{
	public string name;
    public float x, y, z;

    public BuildingData(Vector3 position, string type)
    {
        x = position.x;
        y = position.y;
        z = position.z;

        name = type;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }

}
