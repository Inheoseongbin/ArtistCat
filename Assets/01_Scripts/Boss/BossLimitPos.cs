using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLimitPos : BossMain
{
    private float limitX = 15;
    private float limitY = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _bossValue.myDir.x = Mathf.Clamp(transform.position.x, -limitX, limitX);
        _bossValue.myDir.y = Mathf.Clamp(transform.position.y, -limitY, limitY);

        transform.position = _bossValue.myDir;
        print(transform.position);
    }
}
