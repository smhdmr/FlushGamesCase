using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GemStacker : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float collectionSpeed;
    [SerializeField] private float distanceBetweenGems = 1;
    
    private Vector3 _currentGemPoint = Vector3.zero;
    private Vector3 _gemOffset;

    private void Start()
    {
        _gemOffset = new Vector3(0, distanceBetweenGems, 0);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        CollectibleGrid grid;
        if (other.transform.parent.TryGetComponent<CollectibleGrid>(out grid))
        {
            GameObject gem = other.gameObject;
        
            CollectGem(gem);
            grid.CollectGem(gem);
        }
    }

    private void CollectGem(GameObject gem)
    {
        gem.transform.DOKill();
        gem.transform.SetParent(transform);
        gem.transform.DOLocalMove(_currentGemPoint, collectionSpeed).SetSpeedBased();
        _currentGemPoint += _gemOffset;
    }

    public Gem SellGem()
    {
        Gem gem;
        if (!transform.GetLastChild() || !transform.GetLastChild().TryGetComponent(out gem))
            return null;
        
        _currentGemPoint -= _gemOffset;
        return gem;
    }
}
