using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    int level = 0;
    [SerializeField] private Rotater _rotater;

    List<Rotater> _rotaterList = new List<Rotater>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && level < 6)//6���� �����ϴ°� ������ �����ϴ�.
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;

        Rotater r = Instantiate(_rotater, transform);
        //r.transform.parent = transform;
        _rotaterList.Add(r);
        for (int i = 0; i < _rotaterList.Count; i++)
        {
            _rotaterList[i].degTheta = (360 / level) * i;
        }
    }
}
