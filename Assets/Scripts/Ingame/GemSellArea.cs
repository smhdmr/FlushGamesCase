using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GemSellArea : MonoBehaviour
{
    [SerializeField] private float sellCooldown;
    [SerializeField] private float gemMoveSpeed;
    
    private Coroutine _gemSellCoroutine;
    private GemStacker _currentGemStacker;
    
    
    private void OnTriggerEnter(Collider other)
    {
        _currentGemStacker = other.GetComponentInChildren<GemStacker>();
        _gemSellCoroutine = StartCoroutine(nameof(SellGems));
    }

    private void OnTriggerExit(Collider other)
    {
        if (_gemSellCoroutine != null)
        {
            StopCoroutine(_gemSellCoroutine);
            _currentGemStacker = null;
        }
    }

    private IEnumerator SellGems()
    {
        while (true)
        {
            SellGem();
            yield return new WaitForSeconds(sellCooldown);
        }
    }

    private void SellGem()
    {
        Gem gem = _currentGemStacker.SellGem();
        if (gem == null && _gemSellCoroutine != null)
        {
            StopCoroutine(_gemSellCoroutine);
            _currentGemStacker = null;
            return;
        }
            
        gem.DOKill();
        gem.transform.SetParent(transform);

        gem.transform.DOMove(transform.position, gemMoveSpeed).SetSpeedBased().OnComplete(() =>
        {
            Destroy(gem.gameObject);

            GameManager.Instance.goldCount += gem.value;
            GameManager.Instance.IncreaseGemCount(gem.name);
        });
    }
}
