using System.Collections.Generic;
using UnityEngine;

public class FlashlightCollider : LightingEntity
{
	public override void Checkpoint_Load()
	{
	}

	public override void Checkpoint_Save()
	{
	}
	public void Switch(bool enabled)
	{
		activated = enabled;
		transform.localScale = enabled ? Vector3.one : Vector3.zero;
		transform.localPosition = enabled ? Vector3.forward * 0.5f : Vector3.one * 20f;
	}
	public Collider mCol;
	bool activated = false;
	public List<Switch_LightSensitive> switches = new();
	bool Condition(Collider other, out Switch_LightSensitive ret)
	{
		return other.TryGetComponent<Switch_LightSensitive>(out ret);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (Condition(other, out var r))
		{
			switches.Add(r);
			r.SwitchState(this, null);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (Condition(other, out var r))
		{
			switches.Remove(r);
			r.SwitchState(null, this);
		}
	}
}