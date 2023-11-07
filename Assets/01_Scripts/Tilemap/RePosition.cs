using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    [SerializeField] GameObject player;
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
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(h, v, 0);
        float dirX = movement.x < 0 ? -1 : 1;
        float dirY = movement.y < 0 ? -1 : 1;

        //��, ����
        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
                //���ʹ� ���ġ �� �� ���� ����
            //case "Enemy":
            //    //���� ���ġ
            //    if (coll.enabled)
            //    {
            //        transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
            //    }
            //    break;
        }
    }
}
