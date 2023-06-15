using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonPopAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    RectTransform thisRect;
    Vector3 scaleDefault;
    public Transform target;
    public bool isScaleForcedToOne = false;
    private float _popScaleDown = .85f;

    void Awake()
    {
        if (target == null)
            target = transform;
        thisRect = target.gameObject.GetComponent<RectTransform>();
        scaleDefault = isScaleForcedToOne ? Vector3.one : thisRect.localScale;
    }

    void OnEnable()
    {
        if (thisRect != null)
            thisRect.localScale = scaleDefault;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        target.transform.DOScale(scaleDefault * _popScaleDown, 0.15f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        target.transform.DOScale(scaleDefault, 0.15f);
    }

    public void ResetScaleToDefault()
    {
        scaleDefault = thisRect.localScale;
    }
}