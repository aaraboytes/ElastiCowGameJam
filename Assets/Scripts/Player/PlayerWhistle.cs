using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerWhistle : MonoBehaviour
{
    [SerializeField] float _blowRadius;
    [SerializeField] float _coolDownTime;
    [SerializeField] PlayerItems _playerItems;
    [SerializeField] ItemData _whistleyData;
    [SerializeField] AudioClip _whistleySound;
    [SerializeField] TextMeshProUGUI _whistleNumberText;

    private int whistleNumber = 0;
    private float timer = 0;
    private AudioSource audio;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.2f);
        Gizmos.DrawWireSphere(transform.position, _blowRadius);
    }
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _whistleNumberText.text = "x0";
        PlayerItems.OnObjectPicked += AddWhistle;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Whistle") && timer<=0)
        {
            if (_playerItems.Items.Contains(_whistleyData))
            {
                _playerItems.Remove(_whistleyData);
                Blow();
                timer = _coolDownTime;
            }
        }
        if (timer > 0)
            timer -= Time.deltaTime;
    }
    private void AddWhistle()
    {
        if(PlayerItems.LastPickedObject == _whistleyData)
        {
            whistleNumber++;
            _whistleNumberText.text = $"x{whistleNumber}";
        }
    }
    private void Blow()
    {
        audio.PlayOneShot(_whistleySound);
        CowsManager.Instance.ScareCows(transform.position, _blowRadius);
    }
}
