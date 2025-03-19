using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	private SaveManager saveManager;

	private PlayerData playerData;

	public GameObject potatoGarden;
	public GameObject tomatoGarden;
	public GameObject carrotGarden;
	public GameObject eggplantsGarden;
	public GameObject pumpkinGarden;

	private Dictionary<string, int> resourcePrice = new Dictionary<string, int>();


    private void Start()
    {
    	
    	resourcePrice.Add("potato", 3);
    	resourcePrice.Add("tomato", 5);
        resourcePrice.Add("carrot", 7);
        resourcePrice.Add("eggplants", 8);
        resourcePrice.Add("pumpkin", 15);


    	saveManager = FindObjectOfType<SaveManager>();

    	playerData = saveManager.LoadData();

    	if (playerData.money == 0)
        {
            playerData.money = 100;  
            saveManager.SaveData(playerData);
        }

        //ResetSaves();
        LoadBuildings();
    }


    public void AddResource(string type, int count)
    {
    	switch (type)
    	{
    		case "money":
    			playerData.money += count;
    			break;
    		case "potato":
    			playerData.potato += count;
    			break;
    		case "tomato":
    			playerData.tomato += count;
    			break;
    		case "carrot":
    			playerData.carrot += count;
    			break;
    		case "eggplants":
    			playerData.eggplants += count;
    			break;
    		case "pumpkin":
    			playerData.pumpkin += count;
    			break;

    	}
    	saveManager.SaveData(playerData);
    }

    public bool TakeMoney(int count)
    {	
    	if (playerData.money - count >= 0)
    	{
    		playerData.money -= count;
    		saveManager.SaveData(playerData);

    		return true;
    	}

    	return false;
    	
    }

    public bool TakeResource(string type, int count)
	{
	    bool result = false;
	    int price = 0;

	    if (resourcePrice.TryGetValue(type, out price))
	    {
	        switch (type)
	        {

	            case "potato":
	                if (playerData.potato - count >= 0)
	                {
	                    playerData.potato -= count;
	                    result = true;
	                }
	                break;
	            case "tomato":
	                if (playerData.tomato - count >= 0)
	                {
	                    playerData.potato -= count;
	                    result = true;
	                }
	                break;
	            case "carrot":
	                if (playerData.carrot - count >= 0)
	                {
	                    playerData.carrot -= count;
	                    result = true;
	                }
	                break;
	            case "eggplants":
	                if (playerData.eggplants - count >= 0)
	                {
	                    playerData.eggplants -= count;
	                    result = true;
	                }
	                break;
	            case "pumpkin":
	                if (playerData.pumpkin - count >= 0)
	                {
	                    playerData.pumpkin -= count;
	                    result = true;
	                }
	                break;
	            
	        }

	        if (!result)
	        {
	            return false;
	        }

	        playerData.money += count * price;
	        saveManager.SaveData(playerData);

	        return true;
	    }
	    else
	    {
	        Debug.LogError($"Ресурс {type} не знайдено в словнику!");
	        return false;
	    }
	}


    public int ReturnResource(string type)
    {
    	switch (type)
    	{
    		case "money":
    			return playerData.money;
    			break;
    		case "potato":
    			return playerData.potato;
    			break;
    		case "tomato":
    			return playerData.tomato;
    			break;
    		case "carrot":
    			return playerData.carrot;
    			break;
    		case "eggplants":
    			return playerData.eggplants;
    			break;
    		case "pumpkin":
    			return playerData.pumpkin;
    			break;
    	}

    	return 0;
    }

    public void SaveProgres(string type)
	{
		bool present = false;

		foreach (string lvl in playerData.progres)
		{
			if (type == lvl)
			{
				present = true;
			}
		}

		if (!present)
		{
			playerData.progres.Add(type);
			saveManager.SaveData(playerData);
		}
	}

	public List<string> ReturnProgres()
	{
		if (playerData.progres.Count == 0)
		{
			playerData.progres.Add("potato");
		}
		return playerData.progres;
	}

    public void SaveBuilding(Vector3 position, string type)
	{
	    playerData.buildings.Add(new BuildingData(position, type));
	    saveManager.SaveData(playerData);
	}

	public void LoadBuildings()
	{
		foreach (BuildingData building in playerData.buildings)
	    {
	    	string type = building.name;
	    	GameObject prefab = null;
	    	
	    	switch (type)
	    	{
	    		case "potato":
	    			prefab = potatoGarden;
	    			break;
	    		case "tomato":
	    			prefab = tomatoGarden;
	    			break;
	    		case "carrot":
	    			prefab = carrotGarden;
	    			break;
	    		case "eggplants":
	    			prefab = eggplantsGarden;
	    			break;
	    		case "pumpkin":
	    			prefab = pumpkinGarden;
	    			break;
	    	}

	        Instantiate(prefab, building.GetPosition(), Quaternion.identity);
	    }

	}

    private void ResetSaves()
    {
    	playerData.money = 25;

    	playerData.potato = 0;
    	playerData.tomato = 0;
    	playerData.carrot = 0;
    	playerData.eggplants = 0;
    	playerData.pumpkin = 0;

    	playerData.buildings.Clear();
    	playerData.progres.Clear();

    	saveManager.SaveData(playerData);
    }
}
