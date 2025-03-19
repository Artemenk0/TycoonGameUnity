using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CityPeople;  

public class Character : MonoBehaviour
{
    private DataManager dataManager;
    private CharacterSpawner characterSpawner;
	private CityPeople.CityPeople cpScript;

    private NavMeshAgent agent;

    private GameObject targetMove;

    public List<string> progres = new List<string>();
    
    private string neededVeg;
    private int neededVegCount;

    private bool isMoving;
    private bool isBought;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cpScript = GetComponent<CityPeople.CityPeople>();
        dataManager = FindObjectOfType<DataManager>();
        characterSpawner = FindObjectOfType<CharacterSpawner>();

        progres = dataManager.ReturnProgres();
        neededVeg = progres[Random.Range(0, progres.Count)];
        neededVegCount = Random.Range(1, 4);

        Debug.Log(neededVeg + " " + neededVegCount);
    }

    private void Update()
    {
    	if (targetMove != null)
    	{
    		agent.SetDestination(targetMove.transform.position); 

    		isMoving = true;
    	}

    	if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && isMoving)
		{
		    isMoving = false;

            if (isBought)
            {
                Destroy(gameObject);
            }

            if (dataManager.TakeResource(neededVeg, neededVegCount))
            {
                isBought = true;
                MoveToPoint(characterSpawner.FinishPosition());
            }
		}	

		cpScript.SetWalkState(isMoving); 
    }

    public void MoveToPoint(GameObject target)
    {
    	targetMove = target;
    }
}
