using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongSwitch : Switch_Activator
{
	public float delay_Max;
	float delay;
	public float timeToActivate = 1f;
	float countdownActivate = 1f;
	float timerSave;
	bool savedActivation;
	bool currentActivation;
	public override void Checkpoint_Load()
	{
		if (currentActivation != savedActivation)
			Use_Activator(savedActivation);
		currentActivation = savedActivation;
		countdownActivate = timerSave;
	}

	public override void Checkpoint_Save()
	{
		timerSave = countdownActivate;
		savedActivation = currentActivation;
	}

	// Start is called before the first frame FixedUpdate
	void Start()
    {
		countdownActivate = timeToActivate;
		delay = delay_Max;
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
		if (delay >= 0f)
		{
			delay -= Time.deltaTime;
			return;
		}
		countdownActivate -= Time.deltaTime;
		if (countdownActivate <= 0)
			SwitchPress();
    }

	private void SwitchPress()
	{
		countdownActivate += timeToActivate;
		currentActivation = !currentActivation;
		Use_Activator(currentActivation);
	}
}
