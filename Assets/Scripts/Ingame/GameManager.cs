using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static string goldCountPref = "gold";
    private static string gemCountPref = "gem_counts";

    private void Start()
    {
        UIManager.Instance.UpdateGoldText();
        UIManager.Instance.UpdateGemCountUI();
    }

    public int goldCount
    {
        get => PlayerPrefs.GetInt(goldCountPref, 0);
        set
        {
            PlayerPrefs.SetInt(goldCountPref, value);
            UIManager.Instance.UpdateGoldText();
        }
    }

    public int GetGemCount(string gemName)
    {
        if (gemName.Contains(" "))
            gemName = gemName.Replace(" ", "_");
        gemName = gemName.ToLower();
        
        return PlayerPrefs.GetInt(gemName, 0);
    }

    public void IncreaseGemCount(string gemName)
    {
        if (gemName.Contains(" "))
            gemName = gemName.Replace(" ", "_");
        gemName = gemName.ToLower();
        
        PlayerPrefs.SetInt(gemName, GetGemCount(gemName) + 1);
        UIManager.Instance.UpdateGemCountUI();
    }
}
