using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowsManager : MonoBehaviour
{
    public static CowsManager Instance;

    [SerializeField] Transform[] cowSpawnLocations;
    [SerializeField] LayerMask _groundLayer;
    private Elasticow elasticow;
    private Cow[] cows;

    private void Awake()
    {
        Instance = this;
        elasticow = FindObjectOfType<Elasticow>();
        cows = FindObjectsOfType<Cow>();
    }
    private Transform GetFarestLocation(Vector3 position)
    {
        float distance = 0;
        Transform selectedLocation = null;
        for (int i = 0; i < cowSpawnLocations.Length; i++)
        {
            float currentDistance = Vector3.Distance(position, cowSpawnLocations[i].position);
            if (currentDistance > distance)
            {
                selectedLocation = cowSpawnLocations[i];
            }
        }
        return selectedLocation;
    }
    public void SwitchElasticow(Cow cow)
    {
        elasticow.transform.rotation = cow.transform.rotation;
        elasticow.transform.position = cow.transform.position;
        elasticow.gameObject.SetActive(true);
        Transform spawnLocation = GetFarestLocation(cow.transform.position);
        RaycastHit hit;
        Physics.Raycast(spawnLocation.position + Vector3.up,Vector3.down, out hit, _groundLayer);
        cow.Agent.Warp(hit.point + Vector3.up * 0.5f);
        cow.ResetAgent();
        cow.SwitchModelToNormalCow();
    }

    public void ScareCows(Vector3 position, float radius)
    {
        if (Vector3.Distance(elasticow.transform.position, position) <= radius)
            elasticow.Scare(position, radius);
        foreach (var cow in cows)
        {
            if (Vector3.Distance(cow.transform.position, position) <= radius)
                cow.Scare(position, radius);
        }
    }
}
