using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int goalExp;
	public int curExp;
	public int curLevel;

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
		playerHealth.PlayerHP += value;
	}

	public void OnSpeedUp(float value) // 스킬 스피트 증가
	{
		playerMove.PlayerSpeed += value;
	}

	public void OnMagnetUpgrade(float value) // 스킬 자석 업글
	{
		magnetCollider.radius += value;
	}
}
