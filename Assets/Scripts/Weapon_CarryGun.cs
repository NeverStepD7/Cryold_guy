using UnityEngine;

public class Weapon_CarryGun : WeaponMechanic
{
	public static Weapon_CarryGun id { get; private set; }
	public override void ExtraRemoveCall()
	{
		Box_Drop();
	}

	public override void Use_Down()
	{
		if (!boxHeld && boxAvailable)
			Box_Grab();
		else if (boxHeld)
			Box_Drop();
	}

	public override void Use_Held()
	{
	}
	public Transform pivot;
	void Box_Grab()
	{

		if (!Player.id.mController.isGrounded)
		{
			return;
		}
		boxHeld = boxAvailable;
		boxAvailable = null;
		boxHeld.SetKinematic(true);
		boxHeld.mRig.velocity = (Player.id.transform.position-boxHeld.transform.position) * 6f;
	}
	public void Box_Drop()
	{
		if (boxHeld)
			boxHeld.SetKinematic(false);
		boxHeld = null;
		boxAvailable = null;
	}
	private void Start()
	{
		id = this;
	}
	private void FixedUpdate()
	{
		if (boxHeld)
		{
			boxHeld.mRig.velocity *= 0.9f;
			boxHeld.mRig.velocity += (pivot.position - boxHeld.transform.position + Vector3.down);

		}
	}
	public override void Use_RightDown()
	{
		Box_Drop();
	}
	Box boxHeld;
	public Box boxAvailable;
	private void OnTriggerStay(Collider other)
	{
		if (!boxHeld)
			if (other.TryGetComponent<Box>(out var getToTryCarry))
				boxAvailable = getToTryCarry;

	}
}
