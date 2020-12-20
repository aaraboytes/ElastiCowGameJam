﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elasticow : MonoBehaviour
{
    [SerializeField] float _fieldOfViewAngle = 110f;
    [SerializeField] float _madTime;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] Transform _eyes;
    private bool mooing = false;
    private bool randomBehaviour = true;
    private bool lookingForPlayer = false;
    private bool playerInSight = false;
    private float outOfSightTimer;
    private AudioSource audio;
    private NavMeshAgent agent;
    private Vector3 lastSight;
    private GameObject player;
    private FPSPlayer playerFPS;
    private SphereCollider sphereTrigger;
    private IEnumerator lookingCoroutine;
    private void OnDrawGizmosSelected()
    {
        if(agent) Gizmos.DrawSphere(agent.destination, 1);
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerFPS = player.GetComponent<FPSPlayer>();
        sphereTrigger = GetComponent<SphereCollider>();
        audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        outOfSightTimer = _madTime;
    }
    private void Update()
    {
        if (!playerInSight)
        {
            outOfSightTimer += Time.deltaTime;
        }
        if (randomBehaviour)
        {
            if (agent.remainingDistance <= 0.1f)
            {
                if (!lookingForPlayer)
                {
                    agent.SetDestination(transform.position);
                    lookingCoroutine = SearchForPlayer();
                    StartCoroutine(lookingCoroutine);
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            if (!mooing)
            {
                playerInSight = false;
                Vector3 dir = player.transform.position - _eyes.position;
                float angle = Vector3.Angle(dir, transform.forward);
                if (angle < _fieldOfViewAngle * 0.5f)
                {
                    Ray ray = new Ray(_eyes.position, dir);
                    Debug.DrawRay(ray.origin, ray.direction * dir.magnitude, Color.green);
                    if (Physics.Raycast(ray, sphereTrigger.radius))
                    {
                        Debug.Log("Following player");
                        randomBehaviour = false;
                        if (lookingCoroutine != null) StopCoroutine(lookingCoroutine);
                        lookingForPlayer = false;
                        playerInSight = true;
                        if (outOfSightTimer >= _madTime)
                        {
                            outOfSightTimer = 0;
                            StartCoroutine(Moo());
                        }
                        lastSight = player.transform.position;
                        agent.SetDestination(lastSight);
                    }
                    else
                    {
                        Debug.Log("Player out of sight");
                        playerInSight = false;
                        randomBehaviour = true;
                    }
                }
                else
                {
                    playerInSight = false;
                    randomBehaviour = true;
                }
                if (playerFPS.IsRunning && CalculatePathLenght(player.transform.position) <= sphereTrigger.radius)
                {
                    Debug.Log("Player heared");
                    playerInSight = true;
                    randomBehaviour = false;
                    if (lookingCoroutine != null) StopCoroutine(lookingCoroutine);
                    lastSight = player.transform.position;
                    agent.SetDestination(lastSight);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
            randomBehaviour = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            randomBehaviour = false;
        }
    }
    private bool SearchRandomLocation()
    {
        Vector3 randomLocation = transform.position + transform.forward * Random.Range(0, sphereTrigger.radius) + transform.right * Random.Range(-sphereTrigger.radius, sphereTrigger.radius);
        if(Physics.Raycast(transform.position, Vector3.down, _groundLayer))
        {
            agent.SetDestination(randomLocation);
            return true;
        }
        else
            return false;
    }
    private float CalculatePathLenght(Vector3 targetPos)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.enabled)
            agent.CalculatePath(targetPos, path);
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPos;
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }
        float pathLenght = 0;
        for (int i = 0; i < allWayPoints.Length-1; i++)
        {
            pathLenght += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }
        return pathLenght;
    }
    private IEnumerator SearchForPlayer()
    {
        Debug.Log("Searching player");
        lookingForPlayer = true;
        yield return new WaitForSeconds(5);
        while (!SearchRandomLocation())
            yield return null;
        lookingForPlayer = false;
    }

    private IEnumerator Moo()
    {
        mooing = true;
        audio.Play();
        yield return new WaitForSeconds(3);
        mooing = false;
    }
}
