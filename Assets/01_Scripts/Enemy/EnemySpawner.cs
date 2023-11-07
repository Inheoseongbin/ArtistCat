using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public List<GameObject> enemyList;
	public List<GameObject> bossList;

	public float range;

	private int atOnceCount;
	private float minx;
	private float miny;
	private float maxx;
	private float maxy;

	private Transform player;

	private void Start()
	{
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
	{
		while (true)
		{
			player = GameManager.Instance.playerTrm;
			minx = player.transform.position.x - range;
			miny = player.transform.position.y - range;
			maxx = player.transform.position.x + range;
			maxy = player.transform.position.y + range;

			float time = Random.Range(2, 5);
			atOnceCount = Random.Range(1, 2);

			for (int i = 0; i < atOnceCount; i++)
			{
				Vector2 pos = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
				Enemy e = PoolManager.Instance.Pop(enemyList[0].name) as Enemy;
				e.transform.position = pos;
			}

			yield return new WaitForSeconds(time);	
		}
	}
}
