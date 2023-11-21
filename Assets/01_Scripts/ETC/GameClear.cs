using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameClear : MonoBehaviour
{
	[SerializeField] private List<GameObject> stories;
	[SerializeField] private GameObject story;

	private void Start()
	{
		StartCoroutine(StoryStart());
	}

	public void Story()
	{
		ResetStory();
		StartCoroutine(StoryStart());
	}

	IEnumerator StoryStart()
	{
		yield return new WaitForSeconds(0.2f);
		stories[0].transform.DOLocalMoveY(94, 1);
		yield return new WaitForSeconds(1);
		stories[1].transform.DOLocalMoveY(-96, 1);

		yield return new WaitForSeconds(2);
		story.transform.DOLocalMoveY(-1100, 1);
	}

	public void ResetStory()
	{
		StopAllCoroutines();
		foreach (GameObject g in stories)
		{
			Vector3 pos = g.transform.localPosition;
			pos.y = -960;
			g.transform.localPosition = pos;
		}
	}
}
