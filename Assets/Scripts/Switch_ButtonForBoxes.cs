using System.Collections.Generic;
using UnityEngine;

public class Switch_ButtonForBoxes : Switch_Activator
{
	public override void Checkpoint_Load()
	{
		boxes.Clear();
	}

	public override void Checkpoint_Save()
	{
	}
	bool hasPlayer;
	List<Box> boxes = new List<Box>();
	public GameObject offState, onState;
	public AudioSource jingle;
	bool isActivated;
	private void OnTriggerEnter(Collider other)
	{
		if (ItsPlayer(other))
			hasPlayer = true;
		if (other.TryGetComponent<Box>(out var _box))
			boxes.Add(_box);
	}

	private static bool ItsPlayer(Collider other)
	{
		return other.gameObject == Player.id.gameObject;
	}

	private void OnTriggerExit(Collider other)
	{
		if (ItsPlayer(other))
			hasPlayer = false;
		if (other.TryGetComponent<Box>(out var _box))
			boxes.Remove(_box);
	}
	private void Start()
	{
		isActivated = false;
	}
	private void FixedUpdate()
	{
		if (!isActivated)
			if (hasPlayer || boxes.Count > 0)
				UseActivatorOnce(true);
		if (isActivated)
			if (!hasPlayer && boxes.Count == 0)
				UseActivatorOnce(false);

	}
	void UseActivatorOnce(bool activated)
	{
		if (activated == isActivated)
			return;
		isActivated = activated;
		Use_Activator(activated);
		onState.SetActive(activated);
		offState.SetActive(!activated);
		jingle.Play();
	}
}