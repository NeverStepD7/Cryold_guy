using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
	public void GoToEscene(int i)
	{
		SceneManager.LoadScene(i);
	}
	public void ExitGame()
	{
		Application.Quit();
	}
	public void SaveCheckpoint() => Player.Checkpoint_Save();
	public void LoadCheckpoint() => Player.Checkpoint_Load();
	public void FreezePlayerMovevent1s()
	{
		Player player = FindObjectOfType<Player>(true);
		player.DelayPlayerMovement_1S();
	}
	public bool deactivateOnContact;

	public void DeactivateGameobject() => gameObject.SetActive(false);

	public GameObject []setActiveInTrigger;
	public GameObject []setDeactivatedInTrigger;
	public void TeleportPlayerToLocation(Transform location)
	{
		var player = FindObjectOfType<Player>(true);
		player.TeleportToLocation(location);
		player.DelayPlayerMovement_1S();
		player.DelayPlayerMovement_1S();
	}
	private void OnTriggerEnter(Collider other)
	{
		other.TryGetComponent<Player>(out var player);
		if (player)
			TriggerActivate();
	}

	public UnityEngine.Events.UnityEvent activateWhenTriggered;
	public UnityEngine.Events.UnityEvent[] triggerExternally;
	public void TriggerActivate()
	{
		activateWhenTriggered.Invoke();
		gameObject.SetActive(!deactivateOnContact);
		foreach (var item in setActiveInTrigger)
			item.SetActive(true);
		foreach (var item in setDeactivatedInTrigger)
			item.SetActive(false);
	}

	public void Note(string note)
	{
		Debug.Log(note);
	}
	public void ExternalTrigger(int index)
	{
		triggerExternally[index].Invoke();
	}
}
