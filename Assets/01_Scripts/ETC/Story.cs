using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Story : MonoBehaviour
{
	[SerializeField] private List<GameObject> openingStories;

	#region ¿Œ∆Æ∑Œ
	public void IntroStory()
	{
		IResetStory();
		StartCoroutine(IStoryStart());
	}

	IEnumerator IStoryStart()
	{
		yield return new WaitForSeconds(0.2f);
		openingStories[0].transform.DOLocalMoveY(190, 1);
		yield return new WaitForSeconds(1);
		openingStories[1].transform.DOLocalMoveY(-205, 1);
		yield return new WaitForSeconds(1);
		openingStories[2].transform.DOLocalMoveY(203, 1);
	}

	public void IResetStory()
	{
		StopAllCoroutines();
		foreach(GameObject g in openingStories)
		{
			Vector3 pos = g.transform.localPosition;
			pos.y = -960;
			g.transform.localPosition = pos;
		}
	}
	#endregion
}
