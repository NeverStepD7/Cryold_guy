using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonTrap : TrapActivator
{
    [SerializeField]
    private Transform p0fBlue, p1fRed, decorationControl;
    [SerializeField]
    private float timeToReachState;
    [SerializeField]
    private Transform pistonPosition;
    private float normalGrowValue;
    [Range(0f, 1f)]
    [SerializeField]
    private float normalPlacing;
    [Range(0f, 1.1f)]
    [SerializeField]
    private float killPlayerTresshold;
    [SerializeField]
    private bool animateForward;
    [SerializeField]
    private float stretchDecoration = 1f;

    [SerializeField]
    private DetectPlayer playerIsBeingDetected;
    // Start is called before the first frame FixedUpdate
    void Start()
    {
        normalGrowValue = 1f / timeToReachState;
        normalPlacing = animateForward? 0f: 1f;
    }

	private void OnValidate()
	{
        if (!p0fBlue || !p1fRed || Application.isPlaying)
            return;
        SetPosition(normalPlacing);

        if (!decorationControl)
            return;
        if (stretchDecoration < 0.1f)
            stretchDecoration = 0.1f;
        decorationControl.localScale = new Vector3(1f, stretchDecoration, 1f);
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.blue;
        if (p0fBlue)
            Gizmos.DrawWireCube(p0fBlue.position - 0.5f * p0fBlue.transform.forward, Vector3.one * 0.25f);
        Gizmos.color = Color.red;
        if (p1fRed)
            Gizmos.DrawWireCube(p1fRed.position - 0.5f * p1fRed.transform.forward, Vector3.one * 0.25f);
    }

	// FixedUpdate is called once per frame
	void FixedUpdate()
    {
        var animation = animateForward ? +1f : -1f;

        normalPlacing = Mathf.Clamp(normalPlacing + animation * normalGrowValue * Time.deltaTime,
            0f,
            1f);
        SetPosition(normalPlacing);

        if (playerIsBeingDetected.playerDetected && killPlayerTresshold < normalGrowValue)
            KillPlayer(playerIsBeingDetected.DetectPlayerOnce());
    }

	private void SetPosition(in float normalPositioning)
    {
        var pPos = pistonPosition.position;
        pistonPosition.position = Vector3.Lerp(p0fBlue.position, p1fRed.position, normalPositioning);
        if (playerIsBeingDetected.playerDetected)
            playerIsBeingDetected.playerDetected.transform.position += pistonPosition.position - pPos;
    }

    public void KillPlayer(Player player)
	{
        player.Player_Lock();
        DeathAnimationControllers.id.piston.SetActive(true);
    }

	public override void SwitchPress()
	{
		animateForward = !animateForward;
	}
}
