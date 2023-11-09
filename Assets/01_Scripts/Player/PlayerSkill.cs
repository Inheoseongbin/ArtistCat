using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
	private int stringLevel = 0;
	private int fishLevel = 0;
	private int poopLevel = 0;
	private int scratchLevel = 0;

	public void String(int level)
	{
		stringLevel = level;
		StartCoroutine(StringTime());
	}

	IEnumerator StringTime()
	{
		while(true)
		{
			StringSkill s = PoolManager.Instance.Pop("String") as StringSkill;
			s.transform.position = transform.position;
			yield return new WaitForSeconds(12 / stringLevel);
		}
	}

	public void FishThorn(int level)
	{
		fishLevel = level;
		StartCoroutine(FishThornTime());
	}

	IEnumerator FishThornTime()
	{
		while (true)
		{
			FishthornSkill f = PoolManager.Instance.Pop("FishThorn") as FishthornSkill;
			f.transform.position = transform.position;
			yield return new WaitForSeconds(10 / fishLevel);
		}
	}

	public void Poop(int level)
	{
		poopLevel = level;
		StartCoroutine(PoopTime());
	}

	IEnumerator PoopTime()
	{
		while(true)
		{
			PoopSkill p = PoolManager.Instance.Pop("Ddong") as PoopSkill;
			p.transform.position = transform.position;
			yield return new WaitForSeconds(10 / poopLevel);
		}
	}

	public void Scratch(int level)
	{
		scratchLevel = level;
		StartCoroutine(ScratchTime());
	}

	IEnumerator ScratchTime()
	{
		while (true)
		{
			ScratchSkill s = PoolManager.Instance.Pop("Scratch") as ScratchSkill;
			s.transform.position = transform.position;
			yield return new WaitForSeconds(10 / scratchLevel);
		}
	}
}