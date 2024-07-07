using UnityEngine;

public class Box : CheckpointDependent
{
	public Rigidbody mRig;
	public Collider mCol;
	Vector3 pos;
	Quaternion rot;
	bool hasBeenStarted;
	private void Start()
	{
		pos = transform.position;
		rot = transform.rotation;
		hasBeenStarted = true;
	}
	public override void Checkpoint_Save()
	{
		CancelVelocities();
	}

	private void CancelVelocities()
	{
		mRig.angularVelocity *= 0f;
		mRig.velocity *= 0f;
	}

	public override void Checkpoint_Load()
	{
		if (!hasBeenStarted)
			return;
		CancelVelocities();
		var boxGun = FindObjectOfType<Weapon_CarryGun>();
		if (boxGun)
			boxGun.Box_Drop();
		transform.position = pos;
		transform.rotation = rot;
		objectsTouched = 0;
	}

	public void SetKinematic(bool makeKinematic)
	{
		if (!makeKinematic)
			CancelVelocities();
		mRig.useGravity = !makeKinematic;
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.Return))
			Checkpoint_Load();
		canRelease = objectsTouched == 0;
	}

	int objectsTouched;
	public bool canRelease;
	private void OnTriggerEnter(Collider other)
	{
		if (!other.isTrigger && other.gameObject != Weapon_CarryGun.id)
			objectsTouched++;
	}
	void OnTriggerExit(Collider other)
	{
		if (!other.isTrigger && other.gameObject != Weapon_CarryGun.id && objectsTouched > 0)
			objectsTouched--;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.gameObject == Player.id.gameObject)
		{
			var boxGun = FindObjectOfType<Weapon_CarryGun>();
			if (boxGun)
				boxGun.Box_Drop();
		}
	}
}