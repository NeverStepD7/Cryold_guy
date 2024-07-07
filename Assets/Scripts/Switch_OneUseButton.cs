using System;
using UnityEngine;

public class Switch_OneUseButton : Switch_Activator
{
	private bool hasBeenActivated = false;
	[SerializeField]
	private Light lightSource;
	[SerializeField]
	private AudioSource activationSound;

	private Player player;
	private void Start()
	{
		UpdateLight();
	}

	private void FixedUpdate()
	{
		if (player && Input.GetKey(KeyCode.E) && !hasBeenActivated)
			SwitchOn();
	}
	public void SwitchOn()
	{
		if(!hasBeenActivated)
			activationSound.Play();
		hasBeenActivated = true;
		Use_Activator(true);
		UpdateLight();
	}

	private void UpdateLight()
	{
		lightSource.color = hasBeenActivated? Color.green: Color.red;
	}

	private void OnTriggerEnter(Collider other)
	{
		other.TryGetComponent<Player>(out var playerDetected);
		if (playerDetected)
			player = playerDetected;
	}
	private void OnTriggerExit(Collider other)
	{
		other.TryGetComponent<Player>(out var playerDetected);
		if (playerDetected)
			player = null;
	}

	public override void Checkpoint_Save()
	{
		checkpointSaveActivation = hasBeenActivated;
	}
	private bool checkpointSaveActivation;
	public override void Checkpoint_Load()
	{
		if (hasBeenActivated != checkpointSaveActivation)
			SwitchOn();
		hasBeenActivated = checkpointSaveActivation;
		UpdateLight();
	}
}