using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class RePosition : MonoBehaviour
{
    Collider2D coll;
    public int number;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }


    //���̶� �����ƶ� �浹�� ��ġ�� 
    //�� ��ũ��Ʈ�� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        // �÷��̾� ��ġ�� ���� ������ ���� ���
        Vector3 playerPos = GameManager.Instance.playerTrm.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        float dirX = playerPos.x < myPos.x ? -1 : 1;
        float dirY = playerPos.y < myPos.y ? -1 : 1;

        if (transform.CompareTag("Ground"))
        {
            if (diffX >= diffY)
            {
                transform.Translate(Vector3.right * dirX * 40);
            }
            else if (diffX <= diffY)
            {
                transform.Translate(Vector3.up * dirY * 40);
            }
            //Debug.Log($"{transform.gameObject.name} : {Vector2.Distance(playerPos, transform.position)}");
        }
    }

}
