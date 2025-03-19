using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcePanel : MonoBehaviour
{
    public string type;

    public TextMeshProUGUI countText;

    public void ChangeText(string text)
    {
        countText.text = text;
    }
}
