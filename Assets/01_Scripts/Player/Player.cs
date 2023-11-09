using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private PlayerHealth playerHealth;
	private PlayerMove playerMove;
	[SerializeField] private CircleCollider2D magnetCollider;

	private void Awake() 
	{
		playerHealth = GetComponent<PlayerHealth>();
		playerMove = GetComponent<PlayerMove>();
	}

	public void OnHeal(int value) // 스킬 힐
	{
		print("힐 호출");
		playerHealth.AddHP(value);
	}

	public void OnSpeedUp(float value) // 스킬 스피트 증가
	{
		print("스피드 호출");
		playerMove.AddSpeed(value);
	}

	public void OnMagnetUpgrade(float value) // 스킬 자석 업글
	{
		print("자석 호출");
		magnetCollider.radius += value;
	}

	public void OnYarnSpawn(int level)
    {
		print($"털실호출 현재레벨:{level}");
		StringSkill s = PoolManager.Instance.Pop("String") as StringSkill;
		s.transform.position = transform.position;
    }

	public void OnFishSpawn(int level)
    {
		print($"물고기호출 현재레벨:{level}");
		FishthornSkill f = PoolManager.Instance.Pop("FishThorn") as FishthornSkill;
		f.transform.position = transform.position;
    }

	public void OnPoopSpawn(int level)
    {
		print($"똥호출 현재레벨:{level}");
		PoopSkill p = PoolManager.Instance.Pop("Ddong") as PoopSkill;
		p.transform.position = transform.position;
	}

	public void OnScratchOn(int level)
    {
		print($"스크래치호출 현재레벨:{level}");
		ScratchSkill s = PoolManager.Instance.Pop("Scratch") as ScratchSkill;
		s.transform.position = transform.position;
	}
}
