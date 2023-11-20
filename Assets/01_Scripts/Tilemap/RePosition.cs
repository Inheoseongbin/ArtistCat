using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RePosition : MonoBehaviour
{
    Collider2D coll;
    public int number;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //Debug.Log(Vector2.Distance(GameManager.Instance.playerTrm.position, transform.position));
        //if (Vector2.Distance(GameManager.Instance.playerTrm.position, transform.position) > 50)
        //{
        //    Debug.LogError("Reset");
        //    ResetPosition();
        //}
    }

    //���̶� �����ƶ� �浹�� ��ġ�� 
    //�� ��ũ��Ʈ�� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        //Player�� �Ÿ� Ȯ��. ��� ���ġ�� �ұ�?
        Vector3 playerPos = GameManager.Instance.playerTrm.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(h, v, 0);
        float dirX = movement.x < 0 ? -1 : 1;
        float dirY = movement.y < 0 ? -1 : 1;

        if (transform.tag == "Ground")
        {
            //Debug.LogError()
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

    private void ResetPosition()
    {
        Debug.LogError("Reset");
        switch (number)
        {
            case 0:
                transform.position = new Vector2(GameManager.Instance.playerTrm.position.x, GameManager.Instance.playerTrm.position.y);
                break;
            case 1:
                transform.position = new Vector2(GameManager.Instance.playerTrm.position.x, GameManager.Instance.playerTrm.position.y);
                break;
            case 2:
                transform.position = new Vector2(GameManager.Instance.playerTrm.position.x, GameManager.Instance.playerTrm.position.y);
                break;
            case 3:
                transform.position = new Vector2(GameManager.Instance.playerTrm.position.x, GameManager.Instance.playerTrm.position.y);
                break;
        }
    }
}
