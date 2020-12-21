using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip _gameplayMusic;
    [SerializeField] AudioClip _chaseMusic;
    [SerializeField] float _switchSpeed;
    private bool lastCowState;
    private AudioSource audio;
    private Elasticow elasticow;
    private IEnumerator switchCoroutine;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        bool cowAttacking = elasticow.Attacking;
        if (cowAttacking != lastCowState)
        {
            lastCowState = cowAttacking;
            if (cowAttacking)
            {
                if (switchCoroutine != null)
                    StopCoroutine(switchCoroutine);
                switchCoroutine = SwitchToChase();
                StartCoroutine(switchCoroutine);
            }
            else
            {
                if (switchCoroutine != null)
                    StopCoroutine(switchCoroutine);
                switchCoroutine = SwitchToGameplay();
                StartCoroutine(switchCoroutine);
            }
        }
    }
    private void Start()
    {
        elasticow = FindObjectOfType<Elasticow>();
        if (switchCoroutine != null)
            StopCoroutine(switchCoroutine);
        switchCoroutine = SwitchToGameplay();
        StartCoroutine(switchCoroutine);
    }
    private IEnumerator SwitchToChase()
    {
        while(audio.volume>0)
        {
            audio.volume -= Time.deltaTime * _switchSpeed * 2;
            yield return null;
        }
        audio.clip = _chaseMusic;
        audio.volume = 1;
        yield return new WaitForSeconds(3f);
        audio.Play();
    }
    private IEnumerator SwitchToGameplay()
    {
        while(audio.volume > 0)
        {
            audio.volume -= Time.deltaTime * _switchSpeed;
            yield return null;
        }
        audio.clip = _gameplayMusic;
        audio.Play();
        while (audio.volume < 1)
        {
            audio.volume += Time.deltaTime * _switchSpeed;
            yield return null;
        }
    }
}
