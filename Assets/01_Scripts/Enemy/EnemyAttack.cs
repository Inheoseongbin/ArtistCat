using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[SerializeField] private float delay;

	private void OnEnable()
	{
		StartCoroutine(Attack());
	}

	IEnumerator Attack()
	{
		EnemyBullet b = PoolManager.Instance.Pop("EBullet") as EnemyBullet;
		Vector3 dir = GameManager.Instance.playerTrm.position - transform.position;
		b.Move(dir);
		yield return new WaitForSeconds(delay);
	}
}
