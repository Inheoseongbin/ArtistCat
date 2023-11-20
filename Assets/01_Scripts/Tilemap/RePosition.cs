using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    Collider2D coll;

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

        //Player�� �Ÿ� Ȯ��. ��� ���ġ�� �ұ�?
        Vector3 playerPos = GameManager.Instance.playerTrm.transform.position;
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
}
