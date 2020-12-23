using UnityEngine;

public class QuestData : ScriptableObject
{
    public string QuestName;
    [TextArea(1, 4)] public string QuestDescription;
    public bool Finished = false;
    public QuestData[] SequentQuests;
    public virtual void Check()
    {
    }
}
