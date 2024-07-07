using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawEntity : MonoBehaviour
{
    public AudioClip alarm;
    public float playerKillCount_MaxTime;
    float playerKillCount;
	bool playerInside;
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject != Player.id.gameObject)
			return;
		playerInside = true;
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject != Player.id.gameObject)
			return;
		ResetTimer();
		playerInside = false;
	}
	private void Start()
	{
		ResetTimer();
	}

	private void ResetTimer()
	{
		playerKillCount = playerKillCount_MaxTime;
	}

	private void FixedUpdate()
	{
		if (!playerInside || Weapon_Torch.playerIsProtected)
		{
			playerKillCount += Time.deltaTime * 0.75f;
			if (playerKillCount > playerKillCount_MaxTime)
				playerKillCount = playerKillCount_MaxTime;
			return;
		}

		if(playerKillCount > 0f)
		{
			if (playerKillCount == playerKillCount_MaxTime)
				Player.id.PlayAudioRandom(alarm);
			playerKillCount -= Time.deltaTime;
		}
		if (playerKillCount <= 0f)
		{
			DeathAnimationControllers.id.claw.SetActive(true);
			Player.id.Player_Lock();
			playerInside = false;
		}
	}
}
