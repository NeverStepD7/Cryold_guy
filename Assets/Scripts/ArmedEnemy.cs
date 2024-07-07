using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedEnemy : TrapActivator
{
	public bool activatedByDefault;
	public Transform aimingPart;
	bool aimToPlayer;
	bool playerInsideBox;
	public float countToKill_Max;
	float countToKill;
	public AudioSource alarm;
	public override void SwitchPress()
	{
		aimToPlayer = !aimToPlayer;
		CountToKillPlayer_Reset();
	}

	private void CountToKillPlayer_Reset()
	{
		countToKill = countToKill_Max;
	}
	private void CountToKillPlayer_Count()
	{
		PlaySoundOnce();

		countToKill -= Time.deltaTime;
		if (countToKill <= 0f)
			CountToKillPlayer_Kill();
	}
	private void CountToKillPlayer_Kill()
	{
		playerInsideBox = false;
		CountToKillPlayer_Reset();
		Player.id.Player_Lock();
		DeathAnimationControllers.id.turret.SetActive(true);
	}

	// Start is called before the first frame FixedUpdate
	void Start()
    {
		aimToPlayer = activatedByDefault;
		CountToKillPlayer_Reset();
	}

    // FixedUpdate is called once per frame
    void FixedUpdate()
	{
		if (!aimToPlayer || !playerInsideBox)
			return;
		aimingPart.LookAt(Player.id.transform);

		CountToKillPlayer_Count();
	}
	bool hasPlayedSound = true;
	private void PlaySoundOnce()
	{
		if (countToKill_Max != countToKill)
			return;
		hasPlayedSound = true;
		alarm.Play();
	}
	private bool AimPlayer(Collider other, bool aim)
	{
		var isPlayer = other.gameObject == Player.id.gameObject;
		if (isPlayer)
			playerInsideBox = aim;
		return isPlayer;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (AimPlayer(other, true) && aimToPlayer)
			return;
	}
	private void OnTriggerExit(Collider other)
	{
		if(AimPlayer(other, false))
			CountToKillPlayer_Reset();
	}
}
