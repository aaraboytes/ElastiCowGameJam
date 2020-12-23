using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Find all milk", menuName = "Quests/Find all milk")]
public class QD_FindAllMilk : QuestData
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
            milkDeposit.OnDepositFilled += FinishTask;
        }
    }
}
