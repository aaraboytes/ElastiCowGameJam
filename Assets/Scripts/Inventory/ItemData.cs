using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData",menuName = "Interactuable/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;
    public Sprite ItemImage;
    public string Description;
}
