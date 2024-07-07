using UnityEngine;

public abstract class CheckpointDependent : MonoBehaviour
{
	private static CheckpointDependent [] CallAllCheckpoints()
	{
		return FindObjectsOfType<CheckpointDependent>(true);
	}

	public static void AllCheckpoints_Load()
	{
		foreach (var item in CallAllCheckpoints())
			item.Checkpoint_Load();
	}
	public static void AllCheckpoints_Save()
	{
		foreach (var item in CallAllCheckpoints())
			item.Checkpoint_Save();
	}

	public abstract void Checkpoint_Save();
	public abstract void Checkpoint_Load();
}
