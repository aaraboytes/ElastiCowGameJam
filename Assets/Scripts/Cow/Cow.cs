using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Cow : MonoBehaviour
{
    public bool IsFake { get { return isFake; } set { isFake = value; } }
    public NavMeshAgent Agent => agent;
    [SerializeField] float _patrolRadius;
    [SerializeField] private bool isFake = true;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] Vector2 _randomTimeLapse;

    [Header("Elasticow transition")]
    [SerializeField] GameObject _normalCowModel;
    [SerializeField] GameObject _elasticowModel;
    [SerializeField] GameObject _particlePrefab;

    [Header("Movement")]
    [SerializeField] float _normalSpeed;
    [SerializeField] float _scapingSpeed;

    private bool scaping = false;

    private bool customTargetFollow;
    private float nextPatrolTime;
    private float timer = 0;
    private NavMeshAgent agent;
    private Vector3 randomLocation;
    private IO_Corn targetCorn;
    private AudioSource audio;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
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
        else if(targetCorn)
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
        }else if (scaping)
        {

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
    public void SwitchModelToNormalCow()
    {
        _normalCowModel.SetActive(true);
        _elasticowModel.SetActive(false);
    }

    private IEnumerator ScareRun(Vector3 dir, float distance)
    {
        Vector3 farTarget = transform.position + dir * distance;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, distance))
            farTarget = hit.point - dir * 0.1f;

        Vector3 destination = transform.position;
        bool groundTargetFound = false;
        while (!groundTargetFound)
        {
            if (Physics.Raycast(farTarget + Vector3.up, Vector3.down, out hit, _groundLayer))
            {
                destination = hit.point;
                groundTargetFound = true;
            }
            farTarget -= dir * 0.1f;
            yield return null;
        }

        scaping = true;
        agent.speed = _scapingSpeed;
        agent.SetDestination(destination);
        while (agent.remainingDistance > 0.1f)
            yield return null;
        scaping = false;
        ResetAgent();
        SearchRandomLocation();
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
        agent.speed = _normalSpeed;
    }
    
    public void Scare(Vector3 scarePos, float radius)
    {
        Debug.Log("Cow scared");
        Vector3 dir = transform.position - scarePos;
        float distance = radius - dir.magnitude;
        dir.Normalize();
        StartCoroutine(ScareRun(dir, distance));
        audio.Play();
    }
}
