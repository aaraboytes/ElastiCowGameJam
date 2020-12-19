using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UbberDetector : MonoBehaviour
{
    private List<IO_MilkBucket> milkBuckets = new List<IO_MilkBucket>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IO_MilkBucket>())
            milkBuckets.Add(other.GetComponent<IO_MilkBucket>());
    }
    private void OnTriggerExit(Collider other)
    {
        IO_MilkBucket bucket = other.GetComponent<IO_MilkBucket>();
        if (bucket && milkBuckets.Contains(bucket))
            milkBuckets.Remove(bucket);
    }
    public void FillBucket()
    {
        foreach(var bucket in milkBuckets)
        {
            if (!bucket.IsFilled)
            {
                bucket.Fill();
                break;
            }
        }
    }
}
