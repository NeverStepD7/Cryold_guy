using System;
using UnityEngine;

public class Weapon_Flashlight : WeaponMechanic
{
	public Light mLight;
	public FlashlightCollider mFCol;
	bool activated;
	public override void ExtraRemoveCall()
	{
		foreach (var item in mFCol.switches)
			item.SwitchState(null, mFCol);
		mFCol.switches.Clear();
	}

	public override void Use_Down()
	{
		activated = !activated;
		UseFlashlight();
	}
	private void Start()
	{
		UseFlashlight();
	}
	private void UseFlashlight()
	{
		mLight.gameObject.SetActive(activated);
		mFCol.Switch(activated);
	}

	public override void Use_Held()
	{
	}

	public override void Use_RightDown()
	{
	}
}
