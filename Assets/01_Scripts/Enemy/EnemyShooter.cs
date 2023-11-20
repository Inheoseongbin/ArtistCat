using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float waitTime = 5f;
    private void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            EnemyBullet eb = PoolManager.Instance.Pop("EnemyBullet") as EnemyBullet;
            eb.transform.position = transform.position;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
