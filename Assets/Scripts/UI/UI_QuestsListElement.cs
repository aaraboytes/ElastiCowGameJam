using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_QuestsListElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _description;
    public void Write(string description)
    {
        _description.text = description;
    }
}
