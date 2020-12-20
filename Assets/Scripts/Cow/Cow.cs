using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Cow : MonoBehaviour
{
    public bool IsFake { get { return isFake; } set { isFake = value; } }
    public NavMeshAgent Agent => agent;
    [SerializeField] Vector2 _randomTimeLapse;
    [SerializeField] float _patrolRadius;
    [SerializeField] private bool isFake = true;

    [Header("Elasticow transition")]
    [SerializeField] GameObject _normalCowModel;
    [SerializeField] GameObject _elasticowModel;
    [SerializeField] GameObject _particlePrefab;

    private bool customTargetFollow;
    private float nextPatrolTime;
    private float timer = 0;
    private NavMeshAgent agent;
    private Vector3 randomLocation;
    private IO_Corn targetCorn;
   
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        ResetAgent();
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
    private void ReplaceObject()
    {
        CowsManager.Instance.SwitchElasticow(this);
    }
    private void SearchRandomLocation()
    {
        randomLocation = transform.position + Vector3.forward * Random.Range(-_patrolRadius, _patrolRadius) + Vector3.right * Random.Range(-_patrolRadius, _patrolRadius);
        agent.destination = randomLocation;
    }
    private void SwitchModelToElasticow()
    {
        _normalCowModel.SetActive(false);
        _elasticowModel.SetActive(true);
        Invoke(nameof(ReplaceObject), 1);
    }
    private void SwitchModelToNormalCow()
    {
        _normalCowModel.SetActive(true);
        _elasticowModel.SetActive(false);
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
    public void TransitionToElasticow()
    {
        targetCorn = null;
        StopTargetFollowing();
        agent.SetDestination(transform.position);
        GameObject particle = Instantiate(_particlePrefab, transform.position, Quaternion.identity);
        Destroy(particle, 6f);
        Invoke(nameof(SwitchModelToElasticow), 2f);
    }
    public void ResetAgent()
    {
        SearchRandomLocation();
        nextPatrolTime = Random.Range(_randomTimeLapse.x, _randomTimeLapse.y);
    }
}
