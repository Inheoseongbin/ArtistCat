using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
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
            yield return new WaitForSeconds(3f);
        }
    }
}
