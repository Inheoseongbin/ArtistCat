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

	public void OnYarnTrue()
    {
		print("털실호출");
		StringSkill s = PoolManager.Instance.Pop("String") as StringSkill;
		s.transform.position = transform.position;
    }

	public void OnFishTrue()
    {
		print("물고기호출");
		FishthornSkill f = PoolManager.Instance.Pop("FishThorn") as FishthornSkill;
		f.transform.position = transform.position;
    }
}
