using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GemPopupItem : MonoBehaviour
{
    [HideInInspector] public string gemName;
    [SerializeField] private Image gemImage;
    [SerializeField] private TMP_Text countText;

    public void Init(string gemName, Sprite icon)
    {
        this.gemName = gemName;
        gemImage.sprite = icon;
    }
    public void UpdateInfo() => countText.text = GameManager.Instance.GetGemCount(gemName).ToString();
    
}