using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public static UnityAction OnQuestFinished;
    public static QuestData lastFinishedQuest;

    [SerializeField] UI_QuestsList _uiList;

    private List<QuestData> quests = new List<QuestData>();
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        foreach (var quest in quests.ToArray())
            quest.Check();
    }
    public void AddNewTask(QuestData questData)
    {
        quests.Add(questData);
        _uiList.Add(questData);
    }
    public void FinishTask(QuestData questData)
    {
        lastFinishedQuest = questData;
        quests.Remove(questData);
        _uiList.Remove(questData);
        OnQuestFinished?.Invoke();
        if(questData.SequentQuests != null && questData.SequentQuests.Length > 0)
        {
            foreach(var sequentQuest in questData.SequentQuests)
                AddNewTask(sequentQuest);
        }
    }
}
