using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkDepositTrigger : MonoBehaviour
{
    [SerializeField] MilkDeposit _milkDeposit;
    private void OnTriggerEnter(Collider other)
    {
        IO_MilkBucket milkBucket = other.GetComponent<IO_MilkBucket>();
        if (milkBucket && milkBucket.IsFilled)
        {
            milkBucket.Throw();
            _milkDeposit.Fill();
        }
    }
}
