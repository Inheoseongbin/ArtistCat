using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Story : MonoBehaviour
{
	[SerializeField] private List<GameObject> openStroies;

	[SerializeField] private List<GameObject> endingStories;

	#region ¿Œ∆Æ∑Œ
	public void IntroStory()
	{
		IResetStory();
		StartCoroutine(IStoryStart());
	}

	IEnumerator IStoryStart()
	{
		yield return new WaitForSeconds(0.2f);
		openStroies[0].transform.DOLocalMoveY(190, 1);
		yield return new WaitForSeconds(1);
		openStroies[1].transform.DOLocalMoveY(-205, 1);
		yield return new WaitForSeconds(1);
		openStroies[2].transform.DOLocalMoveY(203, 1);
	}

	public void IResetStory()
	{
		StopAllCoroutines();
		foreach(GameObject g in openStroies)
		{
			Vector3 pos = g.transform.localPosition;
			pos.y = -960;
			g.transform.localPosition = pos;
		}
	}
	#endregion

	public void EndingStory()
	{
		EResetStory();
		StartCoroutine(EStoryStart());
	}

	IEnumerator EStoryStart()
	{
		yield return new WaitForSeconds(0.2f);
		openStroies[0].transform.DOLocalMoveY(190, 1);
		yield return new WaitForSeconds(1);
		openStroies[1].transform.DOLocalMoveY(-205, 1);
		yield return new WaitForSeconds(1);
		openStroies[2].transform.DOLocalMoveY(203, 1);
	}

	public void EResetStory()
	{
		StopAllCoroutines();
		foreach (GameObject g in endingStories)
		{
			Vector3 pos = g.transform.localPosition;
			pos.y = -960;
			g.transform.localPosition = pos;
		}
	}

}
