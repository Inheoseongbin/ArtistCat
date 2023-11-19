using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Popup : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI cntTxt;
	[SerializeField] private ParticleSystem comboParticle;

	public void ComboTxt(int cnt)
	{
		cntTxt.text = $"x {cnt}";
	}

	public IEnumerator Pop()
	{
		comboParticle.Play();
		transform.DOLocalMoveY(-490, 1);
		yield return new WaitForSeconds(1.5f);
		transform.DOLocalMoveY(-660, 1);
	}
}
