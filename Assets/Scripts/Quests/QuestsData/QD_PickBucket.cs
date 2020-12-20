using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pick bucket",menuName = "Quests/Pick bucket")]
public class QD_PickBucket : QuestData
{
    public override void Check()
    {
        if(PlayerGrab.GrabbedObject!= null && PlayerGrab.GrabbedObject.GetType().Equals(typeof(IO_MilkBucket)))
        {
            Finished = true;
            QuestManager.Instance.FinishTask(this);
        }
    }
}
