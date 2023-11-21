using UnityEngine;

public class RePosition : MonoBehaviour
{
    Collider2D coll;
    public int number;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        // 플레이어 위치에 따른 움직임 방향 계산
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
        }
    }
}
