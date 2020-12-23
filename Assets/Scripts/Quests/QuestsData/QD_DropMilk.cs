using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Deposit milk", menuName = "Quests/Deposit the milk")]
public class QD_DropMilk : QuestData
{
    private MilkDeposit milkDeposit;
    private void FinishTask()
    {
        Finished = true;
        QuestManager.Instance.FinishTask(this);
    }
    public override void Check()
    {
        if (milkDeposit == null)
        {
            milkDeposit = FindObjectOfType<MilkDeposit>();
            milkDeposit.OnMilkDeposited += FinishTask;
        }
    }
}
