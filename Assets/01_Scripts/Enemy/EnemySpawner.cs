using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public List<GameObject> enemyList;
	private int atOnceCount;

	public float minx;
	public float miny;
	public float maxx;
	public float maxy;

	private void Start()
	{
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
	{
		while (true)
		{
			float time = Random.Range(2, 5);
			atOnceCount = Random.Range(1, 4);
			for (int i = 0; i < atOnceCount; i++)
			{
				Enemy e = PoolManager.Instance.Pop("Enemy") as Enemy;
				Vector2 pos = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
				e.transform.position = pos;
			}
			yield return new WaitForSeconds(time);
		}
	}
}
