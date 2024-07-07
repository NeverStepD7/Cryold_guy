using System.Collections;
using UnityEngine;
public abstract class Switch_Activator : CheckpointDependent
{
	[SerializeField]
	private TrapActivator[] activators;
	[SerializeField]
	public UnityEngine.Events.UnityEvent callEventOnActivated;
	[SerializeField]
	public UnityEngine.Events.UnityEvent callEventOnDeactivated;
	[SerializeField]
	public UnityEngine.Events.UnityEvent callEventOnSwitched;

	public void Use_Activator(bool activate)
	{
		foreach (var item in activators)
			item.SwitchPress();
		if (activate)
			callEventOnActivated.Invoke();
		else
			callEventOnDeactivated.Invoke();

		callEventOnSwitched.Invoke();
	}

}
