using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDetector : MonoBehaviour
{
    [SerializeField] float _checkTime;
    [SerializeField] Cow _cow;
    private float timer = 0;
    private IO_Corn targetCorn;
    private List<IO_Corn> corns = new List<IO_Corn>();
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _checkTime && targetCorn == null)
        {
            Check();
            timer = 0;
        }
        if (targetCorn)
        {
            if (!targetCorn.IsStill)
            {
                targetCorn = null;
                _cow.StopTargetFollowing();
            }
        }
    }
    private void Check()
    {
        foreach (var corn in corns)
        {
            if(corn == null)
            {
                corns.Remove(corn);
                break;
            }
            if (corn.IsStill)
            {
                targetCorn = corn;
                _cow.FollowTarget(corn);
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IO_Corn corn = other.GetComponent<IO_Corn>();
        if (corn)
        {
            corns.Add(corn);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IO_Corn corn = other.GetComponent<IO_Corn>();
        if (corn && corns.Contains(corn))
        {
            corns.Remove(corn);
            if (corn == targetCorn)
            {
                targetCorn = null;
                _cow.StopTargetFollowing();
            }
        }
    }
}
