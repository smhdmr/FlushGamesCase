using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("UI Components")]
    [SerializeField] private TMP_Text goldText;

    [Header("Other")] 
    [SerializeField] private GameObject gemCountPrefab;
    [SerializeField] private Transform gemCountsParent;
    [SerializeField] private GameObject[] availableGems;
    
    private void Start()
    {
        InitGemCountUI();
    }

    public void UpdateGoldText() => goldText.text = GameManager.Instance.goldCount.ToString();

    public void InitGemCountUI()
    {
        availableGems.ToList().ForEach(x =>
        {
            Gem gem = x.GetComponent<Gem>();
            
            GemPopupItem gemItem = Instantiate(gemCountPrefab).GetComponent<GemPopupItem>();
            gemItem.transform.SetParent(gemCountsParent);
            
            gemItem.Init(gem.name, gem.icon);
            gemItem.UpdateInfo();
        });
    }

    public void UpdateGemCountUI()
    {
        gemCountsParent.GetComponentsInChildren<GemPopupItem>(true).ToList().ForEach(x => x.UpdateInfo());
    }
}
