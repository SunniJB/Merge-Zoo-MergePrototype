using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    public UnityEvent OnPress;

    [Header("Visuals")]
    public float tweenDuration = 0.1f;
    public Color activeColor;
    public Color defaultColor;

    [Header("References")]
    public Image background;
    public Image icon;
    public TextMeshProUGUI text;

    private void Awake()
    {
        DefaultVisuals();
    }
    public void ActiveVisuals()
    {
        if (icon != null)
        {
            icon.DOColor(activeColor, tweenDuration * 0.5f);
        }
        if (text != null)
        {
            text.DOColor(activeColor, tweenDuration * 0.5f);
        }

        background.rectTransform.DOScale(Vector3.one * 1.1f, tweenDuration * 0.5f).SetEase(Ease.OutFlash);
    }
    public void DefaultVisuals()
    {
        if (icon != null)
        {
            icon.DOColor(defaultColor, tweenDuration * 0.5f);
        }
        if (text != null)
        {
            text.DOColor(defaultColor, tweenDuration * 0.5f);
        }

        background.rectTransform.DOScale(Vector3.one, tweenDuration * 0.5f).SetEase(Ease.OutFlash);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ActiveVisuals();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        DefaultVisuals();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPress?.Invoke();
    }

    public void SetBackgroundColor(Color newColor)
    {
        background.color = newColor;
    }
}
