using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	DataManager dataManager;

	public GameObject resourceGrid;
	private List<GameObject> resourcePanels = new List<GameObject>();

	public GameObject shopPanel;

    private void Start()
    {
    	dataManager = FindObjectOfType<DataManager>();

    	for (int i = 0; i < resourceGrid.transform.childCount; i++)
        {
            Transform child = resourceGrid.transform.GetChild(i);
            resourcePanels.Add(child.gameObject);
        }

        shopPanel.SetActive(false);
    }

    private void Update()
    {
    	ShowResource();

    }

    private void UpdateText()
    {

    	foreach (GameObject panel in resourcePanels)
    	{
    		ResourcePanel rp = panel.GetComponent<ResourcePanel>();
    		
 			rp.ChangeText("" + dataManager.ReturnResource(rp.type));
    	}


    }

    private void ShowResource()
    {
    	foreach (GameObject panel in resourcePanels)
    	{
    		ResourcePanel rp = panel.GetComponent<ResourcePanel>();
    		
    		int count = dataManager.ReturnResource(rp.type);

    		if (count == 0 && rp.type != "money")
    		{
    			panel.SetActive(false);
    		}
    		else
    		{
    			panel.SetActive(true);

    			TextMeshProUGUI panelText = panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    			panelText.text = "" + count;
    		}
    	}
    }

    public void ShopButton()
    {
    	if (!shopPanel.activeSelf)
    	{
    		shopPanel.SetActive(true);
    	}
    	else
    	{
    		shopPanel.SetActive(false);
    	}
    }
}
