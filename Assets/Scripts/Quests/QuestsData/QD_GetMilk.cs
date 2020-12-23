using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Get Milk", menuName = "Quests/Get milk")]
public class QD_GetMilk : QuestData
{
    private int currentMilkedCows;
    private void Start()
    {
        currentMilkedCows = CowsManager.Instance.MilkedCowNumber;
    }
    public override void Check()
    {
        int milkedCows = CowsManager.Instance.MilkedCowNumber;
        if(milkedCows != currentMilkedCows)
        {
            Finished = true;
            QuestManager.Instance.FinishTask(this);
        }
    }
}
