using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerGiver : MonoBehaviour
{
    [SerializeField] bool _destroyAfterGive = true;
    [SerializeField] QuestData _data;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.4f, 0.4f, 0.15f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.Instance.AddNewTask(_data);
            if (_destroyAfterGive)
                Destroy(gameObject);
        }
    }
}
