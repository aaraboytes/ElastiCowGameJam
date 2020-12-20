using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWhistley : MonoBehaviour
{
    [SerializeField] float _blowRadius;
    [SerializeField] PlayerItems _playerItems;
    [SerializeField] ItemData _whistleyData;
    private void OnDrawGizmosSelected()
    {
        
    }
    private void Update()
    {
        if (Input.GetButtonDown("Whistley"))
        {
            if (_playerItems.Items.Contains(_whistleyData))
            {
                _playerItems.Remove(_whistleyData);
            }
        }
    }
    private void Blow()
    {
        
    }
}
