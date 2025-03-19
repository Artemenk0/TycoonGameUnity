using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSlot : MonoBehaviour
{
	public string type;
    public int price;
    public GameObject prefab;

    public Sprite sprite;

    public TextMeshProUGUI priceText;

    private void Start()
    {
    	priceText.text = "" + price;

    	SpriteRenderer sp = GetComponent<SpriteRenderer>();
    	sp.sprite = sprite;
    }
}
