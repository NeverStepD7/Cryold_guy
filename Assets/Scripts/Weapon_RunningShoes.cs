public class Weapon_RunningShoes : WeaponMechanic
{
	public override void Use_Down()
	{
	}

	public override void Use_RightDown()
	{
	}
	public override void Use_Held()
	{
		Player.id.walkSpeed = speedMultiplier * playerSpeedSave;
		Player.id.stepCountdownSound = playerWalkStepSave * stepDivisor;
		isRunning = true;
	}
    bool isRunning;

	private void FixedUpdate()
	{
		if (isRunning == false)
			ExtraRemoveCall();
        isRunning = false;
	}
	public float speedMultiplier = 2;
	float playerSpeedSave;
	float playerWalkStepSave;
	float stepDivisor;
	private void Start()
	{
        playerSpeedSave = Player.id.walkSpeed;
		playerWalkStepSave = Player.id.stepCountdownSound;
		stepDivisor = 1f / speedMultiplier;
	}

	public override void ExtraRemoveCall()
	{
		Player.id.walkSpeed = playerSpeedSave;
		Player.id.stepCountdownSound = playerWalkStepSave;
	}
}