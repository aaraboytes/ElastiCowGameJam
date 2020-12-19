using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Cow : MonoBehaviour
{
    [SerializeField] Vector2 _randomTimeLapse;
    [SerializeField] float _patrolRadius;
    private bool customTargetFollow;
    private float nextPatrolTime;
    private float timer = 0;
    private NavMeshAgent agent;
    private Vector3 randomLocation;
    private IO_Corn targetCorn;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.2f);
        Gizmos.DrawSphere(transform.position, _patrolRadius);
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        SearchRandomLocation();
        nextPatrolTime = Random.Range(_randomTimeLapse.x, _randomTimeLapse.y);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (!customTargetFollow)
        {
            if (timer >= nextPatrolTime)
            {
                SearchRandomLocation();
                timer = 0;
                nextPatrolTime = Random.Range(_randomTimeLapse.x, _randomTimeLapse.y);
            }
        }
        else
        {
            float remainingDistance = agent.remainingDistance;
            if (remainingDistance <= 0.1f)
            {
                if (timer >= 1)
                {
                    targetCorn.Eat();
                    timer = 0;
                    if (targetCorn == null)
                        StopTargetFollowing();
                }
            }
        }
    }
    private void SearchRandomLocation()
    {
        randomLocation = transform.position + Vector3.forward * Random.Range(-_patrolRadius, _patrolRadius) + Vector3.right * Random.Range(-_patrolRadius, _patrolRadius);
        agent.destination = randomLocation;
    }
    public void FollowTarget(IO_Corn corn)
    {
        targetCorn = corn;
        customTargetFollow = true;
        agent.destination = corn.transform.position;
    }
    public void StopTargetFollowing()
    {
        customTargetFollow = false;
    }
}
