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


    //맵이랑 에리아랑 충돌을 마치면 
    //이 스크립트는 맵임
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        //Player의 거리 확인. 어디에 재배치를 할까?
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(h, v, 0);
        float dirX = movement.x < 0 ? -1 : 1;
        float dirY = movement.y < 0 ? -1 : 1;

        //맵, 몬스터
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
                //에너미 재배치 할 때 쓸까 말까
            //case "Enemy":
            //    //몬스터 재배치
            //    if (coll.enabled)
            //    {
            //        transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
            //    }
            //    break;
        }
    }
}
