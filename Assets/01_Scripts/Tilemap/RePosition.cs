//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RePosition : MonoBehaviour
//{
//    Collider2D coll;

//    private void Awake()
//    {
//        coll = GetComponent<Collider2D>();
//    }


//    //맵이랑 에리아랑 충돌을 마치면 
//    //이 스크립트는 맵임
//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (!collision.CompareTag("Area"))
//            return;

//        //Player의 거리 확인. 어디에 재배치를 할까?
//        Vector3 playerPos = GameManager.instance.player.transform.position;
//        Vector3 myPos = transform.position;
//        float diffX = Mathf.Abs(playerPos.x - myPos.x);
//        float diffY = Mathf.Abs(playerPos.y - myPos.y);

//        Vector3 playerDir = GameManager.instance.player.inputVec;
//        float dirX = playerDir.x < 0 ? -1 : 1;
//        float dirY = playerDir.y < 0 ? -1 : 1;

//        //맵, 몬스터
//        switch (transform.tag)
//        {
//            case "Ground":
//                if (diffX > diffY)
//                {
//                    transform.Translate(Vector3.right * dirX * 40);
//                }
//                else if (diffX < diffY)
//                {
//                    transform.Translate(Vector3.up * dirY * 40);
//                }
//                break;

//            case "Enemy":
//                //몬스터 재배치
//                if (coll.enabled)
//                {
//                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
//                }

//                break;

//        }

//    }

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
