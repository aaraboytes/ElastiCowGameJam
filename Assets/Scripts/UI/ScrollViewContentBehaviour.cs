using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewContentBehaviour : MonoBehaviour
{
    [SerializeField] bool _horizontal = true;
    private GridLayoutGroup grid;
    private RectTransform rect;
    private void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();
        rect = GetComponent<RectTransform>();
    }
    public void Resize()
    {
        if (_horizontal)
        {
            
        }
    }
}
