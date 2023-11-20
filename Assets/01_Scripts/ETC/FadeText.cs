using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FadeText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI titleTxt;

	private void Awake()
	{
		WarningText();
	}

	private void WarningText()
    {
        titleTxt.DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
