using System.Collections.Generic;
using UnityEngine;

public class Weapon_Torch : LightingEntity
{
    public static bool playerIsProtected;
    public List<Switch_LightSensitive> lightsConnected = new();
	private void OnTriggerStay(Collider other)
	{
        if (other.gameObject != Player.id.gameObject)
            return;
        playerIsProtected = true;
	}
	private void OnTriggerExit(Collider other)
	{
        if (other.TryGetComponent<Switch_LightSensitive>(out var l))
        {
            l.SwitchState(null, this);
        }
        if (other.gameObject != Player.id.gameObject)
            return;
        playerIsProtected = false;
    }
	private void OnTriggerEnter(Collider other)
	{
        if (other.TryGetComponent<Switch_LightSensitive>(out var l))
        {
            l.SwitchState(this, null);
            lightsConnected.Add(l);
        }
    }

    public void BreakTorch()
	{
        foreach (var item in lightsConnected)
            item.SwitchState(null, this);
        lightsConnected.Clear();
        gameObject.SetActive(false);
    }
    public void PlaceTorch(Vector3 pos, Vector3 upwards, Vector3 forward)
	{
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(upwards, forward);
        gameObject.SetActive(true);
	}

	public override void Checkpoint_Save()
	{
	}

	public override void Checkpoint_Load()
	{
        BreakTorch();
	}
}
