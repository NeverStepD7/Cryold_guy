using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageExit : MonoBehaviour
{
	public int match;
	int triggersActivated = 0;
	public GameObject activateWhenReached;

	public void GrantAccess()
	{
		triggersActivated++;
		DeathAnimationControllers.id.switchgood.SetActive(true);
		Player.id.Player_Lock();
		Debug.Log("Exit = " + triggersActivated + "/" + match);
	}
	private void FixedUpdate()
	{

		if (triggersActivated >= match && !DeathAnimationControllers.id.switchgood.activeSelf)
		{
			activateWhenReached.SetActive(true);
			Player.id.Player_Lock();
			gameObject.SetActive(false);
		}
	}
}
