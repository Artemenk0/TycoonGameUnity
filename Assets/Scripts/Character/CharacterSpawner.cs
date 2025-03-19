using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Transform spawner;
    public GameObject target;
    public GameObject finishPoint;

    public int charactersCount;

    public GameObject[] charactersPrefab;

    private List<GameObject> charactersList = new List<GameObject>();


    private void Start()
    {
    	Invoke("CharacterSpawn", 1f);
    }

    private void Update()
    {
        charactersList.RemoveAll(item => item == null);
    }

    public GameObject FinishPosition()
    {
        return finishPoint;
    }

    private void CharacterSpawn()
    {
    	if (charactersList.Count < charactersCount)
    	{
    		GameObject newCharacter = Instantiate(charactersPrefab[Random.Range(0, charactersPrefab.Length)],
    			spawner.position, Quaternion.identity);

    		charactersList.Add(newCharacter);

    		Character CharacterScript = newCharacter.GetComponent<Character>();
    		CharacterScript.MoveToPoint(target);

    	}


    	Invoke("CharacterSpawn", Random.Range(5f, 10f));
    }
}
