using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Vision")]
    public Camera mCamera;
    [SerializeField]
    private float visionSpeed;
    private Vector2 vision;
    [SerializeField]
    private float maxDelayPlayerControl = 2.5f;
    private float delayPlayerMovement = 0f;
    private float delayPlayerControl;
    [Header("Walk")]
    public CharacterController mController;
    [SerializeField]
    public float walkSpeed;
    [SerializeField]
    private AudioSource stepsAndJumps;

    [SerializeField]
    private AudioClip soundStep, soundJump;
    [SerializeField]
    public float stepCountdownSound;
    private float stepCurrentCount;
    internal void TeleportToLocation(Transform location)
	{
        transform.position = location.position;

        vision = new Vector2(location.rotation.eulerAngles.x, location.rotation.eulerAngles.y);
        upwardsSpeed = 0f;
        delayPlayerControl = 2f;
    }

	private Transform checkPoint;

    [Header("jump")]
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float terminalFallVelocity;
    [SerializeField]
    private float startFallVelocity;

    public float upwardsSpeed;
    [SerializeField]
    private float maxCoyoteTime;
    private float coyoteTime;

    private bool canJump = false;
    public static Player id { get; private set; }
    private void Awake() => id = this;
	[Header("Other")]
    public WeaponMechanic heldWeapon;

    public void Player_Lock() {
        gameObject.SetActive(false);
        TeleportToLocation(checkPoint);
    }
    public void Player_Unlock()
    {
        gameObject.SetActive(true);
        delayPlayerControl = maxDelayPlayerControl;
    }

    public void DelayPlayerMovement_1S()
	{
        delayPlayerMovement += 1f;
	}
    // Start is called before the first frame FixedUpdate
    void Start()
    {
        Checkpoint_Save();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        delayPlayerControl = maxDelayPlayerControl;
    }
	private void OnDestroy()
	{
        Cursor.visible = true;
	}

	// FixedUpdate is called once per frame
	void Update()
    {
        if (delayPlayerControl > 0)
        {
            delayPlayerControl -= Time.deltaTime;
            return;
        }
        CameraLook();
        Walk();

        if (!heldWeapon)
            return;
        if (Input.GetMouseButtonDown(0))
            heldWeapon.Use_Down();
        if (Input.GetMouseButtonDown(1))
            heldWeapon.Use_RightDown();
        if (Input.GetMouseButton(0))
            heldWeapon.Use_Held();
    }

    void CameraLook()
	{
        var mouseDelta = Vector2.zero;
        mouseDelta.x = Input.GetAxisRaw("Mouse X");
        mouseDelta.y = Input.GetAxisRaw("Mouse Y");

        vision.x += mouseDelta.x * Time.deltaTime * visionSpeed;
        vision.y = Mathf.Clamp(vision.y+ mouseDelta.y * Time.deltaTime * visionSpeed,
            -90f,
            90);


        
        mCamera.transform.localRotation = Quaternion.Euler(-vision.y, 0, 0);
        transform.rotation = Quaternion.Euler(0, vision.x, 0);

    }
    void Walk()
	{
        Vector2 walk = Vector2.zero;
        var baseSpeed = walkSpeed * Time.deltaTime;

        walk.x += Input.GetAxisRaw("Horizontal");
        walk.y += Input.GetAxisRaw("Vertical");

        if (delayPlayerMovement > 0)
        {
            delayPlayerMovement -= Time.deltaTime;
            walk *= 0;
        }
        walk = baseSpeed * walk.normalized;

        if (walk.sqrMagnitude != 0 && mController.isGrounded)
            WalkStepSound();

        if (!mController.isGrounded)
            OnAir();
        else
            FixToTheGround();
        if (coyoteTime > 0f && canJump && Input.GetAxis("Jump") > 0f)
            Jump();

        if (!canJump)
            canJump = Input.GetAxis("Jump") == 0f;

        mController.Move(
            walk.x * transform.right +
            walk.y * transform.forward +
            upwardsSpeed * Time.deltaTime * Vector3.up);
	}
    public void RemoveWeapon()
	{
        if (heldWeapon)
            heldWeapon.RemoveFromPlayer();
	}
	private void WalkStepSound()
	{
        stepCurrentCount += Time.deltaTime;
        if (stepCountdownSound > stepCurrentCount)
            return;
        stepCurrentCount = 0f;
        PlayAudioRandom(soundStep);
	}
    public void PlayAudioRandom(AudioClip audio)
	{
        stepsAndJumps.pitch = Random.Range(0.95f, 1.1f);
        stepsAndJumps.volume = Random.Range(0.8f, 0.9f);
        stepsAndJumps.PlayOneShot(audio);

    }

	private void FixToTheGround()
    {
        coyoteTime = maxCoyoteTime;
        upwardsSpeed = 0;
    }

	private void OnAir()
    {
        coyoteTime -= Time.deltaTime;

        upwardsSpeed += Time.deltaTime * Physics.gravity.y;

        if (upwardsSpeed < -terminalFallVelocity)
            upwardsSpeed = -terminalFallVelocity;
    }
    private void Jump()
	{
        canJump = false;
        coyoteTime = 0;
        upwardsSpeed = jumpPower;
        PlayAudioRandom(soundJump);
	}


	public Vector3 Camera_GetPosition() => mCamera.transform.position;
    public Vector3 Camera_GetDirection() => mCamera.transform.forward;

    public static void Checkpoint_Save()
	{
        var player = FindObjectOfType<Player>(true);
        if (!player.checkPoint)
            player.checkPoint = (new GameObject("Checkpoint Spawn")).transform;

        player.checkPoint.position = player.transform.position;
        player.checkPoint.rotation =  player.mCamera.transform.rotation;

        CheckpointDependent.AllCheckpoints_Save();
    }
    public static void Checkpoint_Load()
    {
        var player = FindObjectOfType<Player>(true);
        if (!player.checkPoint)
            throw new System.Exception();

        player.Player_Unlock();
        player.transform.position = player.checkPoint.position;
        var chVision = player.checkPoint.rotation.eulerAngles;
        player.vision = new Vector2(chVision.y, chVision.x);

        CheckpointDependent.AllCheckpoints_Load();
        player.DelayPlayerMovement_1S();
    }
}
