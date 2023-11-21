using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameClear : MonoBehaviour
{
	[SerializeField] private List<GameObject> stories;
	[SerializeField] private List<TextMeshProUGUI> storyText;
	[SerializeField] private GameObject story;

	private void Start()
	{
		StartCoroutine(StoryStart());
	}

	public void Story()
	{
		ResetStory();
		story.SetActive(true);
		StartCoroutine(StoryStart());
	}

	IEnumerator StoryStart()
	{
		storyText[0].maxVisibleCharacters = 0;
		storyText[1].maxVisibleCharacters = 0;
		yield return new WaitForSeconds(0.2f);
		stories[0].transform.DOLocalMoveY(94, 1);
		yield return new WaitForSeconds(1);
		TMPTextTyping(2f, storyText[0]);
		yield return new WaitForSeconds(2);
		stories[1].transform.DOLocalMoveY(-96, 1);
		yield return new WaitForSeconds(1);
		TMPTextTyping(2, storyText[1]);

		yield return new WaitForSeconds(4);
		story.transform.DOLocalMoveY(-1100, 1);
	}

	public void ResetStory()
	{
		story.transform.DOLocalMoveY(0, 0.2f);
		story.SetActive(false);
		StopAllCoroutines();
		foreach (GameObject g in stories)
		{
			Vector3 pos = g.transform.localPosition;
			pos.y = -960;
			g.transform.localPosition = pos;
		}
	}

	private void TMPTextTyping(float time, TextMeshProUGUI txt)
	{
		txt.maxVisibleCharacters = 0;
		DOTween.To(x => txt.maxVisibleCharacters = (int)x, 0f, txt.text.Length, time);
	}
}
