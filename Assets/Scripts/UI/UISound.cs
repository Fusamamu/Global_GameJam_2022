using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip mouseOver;
    [SerializeField] private AudioClip mouseClick;
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX(mouseOver);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX(mouseClick);
    }
}
