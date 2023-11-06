using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject chooseImage;

    private void Start()
    {
        chooseImage.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        chooseImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        chooseImage.SetActive(false);
    }
}