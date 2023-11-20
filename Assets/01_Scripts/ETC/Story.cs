using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Story : MonoBehaviour
{
	[SerializeField] private List<GameObject> stories;
	
	public void StoryPlay()
	{
		ResetStory();
		StartCoroutine(StoryStart());
	}

	IEnumerator StoryStart()
	{
		yield return new WaitForSeconds(0.2f);
		stories[0].transform.DOLocalMoveY(190, 1);
		yield return new WaitForSeconds(1);
		stories[1].transform.DOLocalMoveY(-205, 1);
		yield return new WaitForSeconds(1);
		stories[2].transform.DOLocalMoveY(203, 1);
	}

	public void ResetStory()
	{
		StopAllCoroutines();
		foreach(GameObject g in stories)
		{
			Vector3 pos = g.transform.localPosition;
			pos.y = -960;
			g.transform.localPosition = pos;
		}
	}
}
