using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    private DataManager dataManager;
	private GardenUI gadrenUI;

	private List<Transform> spawners = new List<Transform>();

	private List<GameObject> vegetablesList = new List<GameObject>();
	
    public string productType;

	public GameObject vegPrefab;

    public int resourceCount;
	public float growthTime;
	private float time;
	private bool isGrowth;

	private bool isVegGrown;

    private bool isSpawnRateEnd;

    void Start()
    {
        Invoke("SpawnRate", 1f);

        dataManager = FindObjectOfType<DataManager>();
    	gadrenUI = GetComponent<GardenUI>();

        foreach (Transform child in transform)
        {
            if (child.name == "Spawner")
            {
                spawners.Add(child.gameObject.transform);

            }

        }

        gadrenUI.ChangeState(0);
    }

    void Update()
    {
    	if (isGrowth)
    	{
    		time += Time.deltaTime;

    		float t = time / growthTime;
    		Vector3 scale = Vector3.Lerp(new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0.5f, 0.5f, 0.5f), t);
    		ScaleVegetable(scale);

    		gadrenUI.ChangeProgres(Mathf.RoundToInt(t * 100f));


    		if (time >= growthTime)
    		{
    			EndGrowthVegetables();
    		}
    	}

    }

    private void SpawnRate()
    {
        isSpawnRateEnd = true;
    }

    public void SpawnVegetables(string vegType)
	{

        if(!isSpawnRateEnd)
        {
            return;
        }

	    foreach (Transform spawner in spawners)  
	    {
	        GameObject newVegetable = Instantiate(vegPrefab, spawner.position, Quaternion.identity);
	        vegetablesList.Add(newVegetable);
	    }

	    gadrenUI.ChangeState(2);
	    isGrowth = true;
	    time = 0f;
	}

    private void ScaleVegetable(Vector3 scale)
    {
    	foreach (GameObject vegetable in vegetablesList)
    	{
    		vegetable.transform.localScale = scale;
    	}
    }

    private void EndGrowthVegetables()
    {
    	isGrowth = false;
    	isVegGrown = true;

    	gadrenUI.ChangeState(3);
    }

    void OnMouseDown()
    {
        if (isGrowth)
        {
        	return;
        }

        if (isVegGrown)
        {
        	isVegGrown = false;
        	
            dataManager.AddResource(productType, resourceCount);

        	gadrenUI.ChangeState(0);

        	foreach (GameObject veg in vegetablesList)  
		    {
		        Destroy(veg);
		    }
		    vegetablesList.Clear();
        }
        
        else
        {
            SpawnVegetables(productType);
        }
    }
}	
