using System;
using System.Collections.Generic;
using UnityEngine;

public class Switch_LightSensitive : Switch_Activator
{

    public GameObject lightOn, lightOff;
    public AudioSource source;
	public override void Checkpoint_Load()
	{
	}

	public override void Checkpoint_Save()
	{
	}
    public List<LightingEntity> lightings = new List<LightingEntity>();
    int lastCount = 0;

    public void SwitchState(LightingEntity add, LightingEntity remove)
	{
		if (!lightings.Find(i => i == add) && add != null)
			lightings.Add(add);
		lightings.Remove(remove);
		if (lightings.Count == 0 && lastCount > 0)
            SwithActivator(false);
        if (lightings.Count > 0 && lastCount == 0)
            SwithActivator(true);
	}

	private void SwithActivator(bool setActivated)
	{
		Use_Activator(setActivated);
		UpdateLightbulb(setActivated);

		source.Play();
		lastCount = lightings.Count;
	}
	private void Start()
	{
		UpdateLightbulb(false);
	}
	private void UpdateLightbulb(bool setActivated)
	{
		var posGO = setActivated ? lightOn : lightOff;
		var negGO = !setActivated ? lightOn : lightOff;

		posGO.SetActive(true);
		negGO.SetActive(false);
	}
}