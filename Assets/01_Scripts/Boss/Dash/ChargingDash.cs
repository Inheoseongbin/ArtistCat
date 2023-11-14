using System.Collections;
using UnityEngine;

public class ChargingDash : BossSkill
{
    private void Start()
    {
        StartCoroutine(Charging());
    }

    IEnumerator Charging()
    {
        float sizeY = _dashingTime * 1000;
        while (true)
        {
            if(_isAiming)
            {
                Vector2 lerpSize = Vector2.Lerp(new Vector2(300, 0), new Vector2(300, sizeY), _waitDashTime);
                transform.GetComponent<RectTransform>().sizeDelta = lerpSize;

                if(lerpSize.y  >= sizeY)
                    _isAiming = false;
            }
        }
    }
}