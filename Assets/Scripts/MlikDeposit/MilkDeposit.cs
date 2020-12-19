using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MilkDeposit : MonoBehaviour
{
    public UnityAction OnDepositFilled;
    [SerializeField] TextMeshProUGUI _remainingBucketsText;
    [SerializeField] int remainingBuckets;
    private void Start()
    {
        _remainingBucketsText.text = remainingBuckets.ToString();
    }
    public void Fill()
    {
        remainingBuckets--;
        _remainingBucketsText.text = remainingBuckets.ToString();
        if(remainingBuckets == 0)
        {
            OnDepositFilled?.Invoke();
        }
    }
}
