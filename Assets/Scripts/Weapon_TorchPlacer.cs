using UnityEngine;

public class Weapon_TorchPlacer : WeaponMechanic
{
    public Weapon_Torch referenceOfTorch;
    Weapon_Torch[] pool;
    public int poolSize;
    int index;
	public float shootingDistance = 30f;
	public GameObject showWhenPlayerIsProtected;
    public AudioClip placeTorch, breakTorch;
    Weapon_Torch selected
	{
		get
		{
            if (index >= poolSize)
                index = 0;
            return pool[index];
		}
	}
	private void Start()
	{
        pool = new Weapon_Torch[poolSize];
		for (int i = 0; i < pool.Length; i++)
		{
            pool[i] = Instantiate(referenceOfTorch);
            pool[i].BreakTorch();
		}
	}
	public override void ExtraRemoveCall()
	{
	}
	private void FixedUpdate()
	{
		showWhenPlayerIsProtected.SetActive(Weapon_Torch.playerIsProtected);
	}
	public override void Use_Down()
	{
        var rays = Physics.RaycastAll(transform.position, transform.forward, shootingDistance);
		foreach (var item in rays)
		{
            if (item.collider.gameObject == Player.id.gameObject)
                continue;
			if (item.collider.GetComponent<Weapon_Torch>())
				continue;
			if (item.collider.GetComponent<Trigger>())
				continue;
			if (item.collider.isTrigger)
				continue;
			selected.PlaceTorch(item.point, transform.up, -transform.forward);
            Player.id.PlayAudioRandom(placeTorch);
			++index;
            break;
		}
	}

	public override void Use_Held()
	{
	}

	public override void Use_RightDown()
	{
        var active = false;
		foreach (var item in pool)
		{
            if (item.gameObject.activeSelf)
                active = true;
            item.BreakTorch();
		}
        if (active)
            Player.id.PlayAudioRandom(breakTorch);
	}
}
