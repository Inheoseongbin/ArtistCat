using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Story : MonoBehaviour
{
	[SerializeField] private List<GameObject> openingStories;
	[SerializeField] private List<TextMeshProUGUI> storyText;

	#region ÀÎÆ®·Î
	public void IntroStory()
	{
		IResetStory();
		StartCoroutine(IStoryStart());
	}

	IEnumerator IStoryStart()
	{
		storyText[0].maxVisibleCharacters = 0;
		storyText[1].maxVisibleCharacters = 0;
		storyText[2].maxVisibleCharacters = 0;
		yield return new WaitForSeconds(0.2f);
		openingStories[0].transform.DOLocalMoveY(190, 1);
		yield return new WaitForSeconds(1);
		TMPTextTyping(1.3f, storyText[0]);
		yield return new WaitForSeconds(1.3f);
		openingStories[1].transform.DOLocalMoveY(-205, 1);
		yield return new WaitForSeconds(1);
		TMPTextTyping(1.3f, storyText[1]);
		yield return new WaitForSeconds(1.3f);
		openingStories[2].transform.DOLocalMoveY(203, 1);
		yield return new WaitForSeconds(1);
		TMPTextTyping(1.3f, storyText[2]);
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


	private void TMPTextTyping(float time, TextMeshProUGUI txt)
	{
		txt.maxVisibleCharacters = 0;
		DOTween.To(x => txt.maxVisibleCharacters = (int)x, 0f, txt.text.Length, time);
	}
	#endregion
}
