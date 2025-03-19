using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GardenUI : MonoBehaviour
{
	private Garden gardenScript;

	public TextMeshProUGUI progresText;
	public Image endSprite;

	public GameObject infoPanel;

   	private void Start()
   	{
   		gardenScript = GetComponent<Garden>();
   	}

    public void ChangeProgres(int value)
    {
    	endSprite.gameObject.SetActive(false);
    	progresText.text = value + "%";
    }

    public void ChangeState(int stateNumber)
    {
    	switch (stateNumber)
    	{
    		case 0:
    			infoPanel.SetActive(false);
    			break;

    		case 1:
    			infoPanel.SetActive(false);
    			break;

    		case 2:
    			infoPanel.SetActive(true);
    			endSprite.gameObject.SetActive(false);
    			break;

    		case 3:
    			endSprite.gameObject.SetActive(true);
    			progresText.text = "";
    			break;
    	}
    }

    public void VegButton(string vegType)
    {
    	gardenScript.SpawnVegetables(vegType);
    }
    	
}
