using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_InteractionDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _message;
    public void Write(string title,string message)
    {
        _title.text = title;
        _message.text = message;
    }
}
