using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private PlayerHealth playerHealth;
	private PlayerMove playerMove;
	private PlayerSkill playerSkill;
	[SerializeField] private CircleCollider2D magnetCollider;

	public int combo;
	private Popup comboPop;

    bool _isSpeed = false;

    private void Awake() 
	{
		playerHealth = GetComponent<PlayerHealth>();
		playerMove = GetComponent<PlayerMove>();
		playerSkill = GetComponent<PlayerSkill>();
		comboPop = FindObjectOfType<Popup>();
	}

	public void ComboCheck()
	{
		if(combo > 1)
		{
			OnHeal(combo);
			comboPop.ComboTxt(combo);
			comboPop.StartCoroutine(comboPop.Pop());
			combo = 0;
		}
	}

	public void OnHeal(int value) // 스킬 힐
	{
		playerHealth.AddHP(value);
	}

	public void OnSpeedUp(float value) // 스킬 스피트 증가
	{
		playerMove.AddSpeed(value);
        if (!_isSpeed)
        {
            _isSpeed = true;
			GetComponentInChildren<TrailRenderer>().enabled = true;
		}
    }

	public void OnMagnetUpgrade(float value) // 스킬 자석 업글
	{
		magnetCollider.radius += value;
	}

	public void OnYarnSpawn(int level)
    {
		playerSkill.String(level);
    }

	public void OnFishSpawn(int level)
    {
		playerSkill.FishThorn(level);
	}

	public void OnPoopSpawn(int level)
    {
		playerSkill.Poop(level);
	}

	public void OnScratchOn(int level)
    {
		playerSkill.Scratch(level);
	}

	public void OnBoomerangOn(int level)
    {
		playerSkill.Boomerang(level);
    }
}
