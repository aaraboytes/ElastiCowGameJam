using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuableObject : MonoBehaviour
{
    public enum InteractionType { Activable, Grabbable, Pickable };
    public InteractionType interactionType = InteractionType.Activable;
    public ItemData itemData;
    [Header("Item description")]
    public string Name;
    public string interactionMessage;
    public virtual void Activate()
    {
        Debug.Log("Activate");
    }
    public virtual ItemData Pick()
    {
        Debug.Log("Picked " + itemData.Name);
        gameObject.SetActive(false);
        return itemData;
    }
}
