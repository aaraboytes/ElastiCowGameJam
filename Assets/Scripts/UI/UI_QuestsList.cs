using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_QuestsList : MonoBehaviour
{
    [SerializeField] GameObject _element;
    [SerializeField] Transform _content;
    private Dictionary<QuestData, GameObject> quests = new Dictionary<QuestData, GameObject>();
    public void Add(QuestData data)
    {
        UI_QuestsListElement element = Instantiate(_element, _content).GetComponent<UI_QuestsListElement>();
        element.Write(data.QuestDescription);
        element.gameObject.SetActive(true);
        quests[data] = element.gameObject;
    }
    public void Remove(QuestData data)
    {
        if (quests.ContainsKey(data))
        {
            Destroy(quests[data]);
            quests.Remove(data);
        }
    }
}
