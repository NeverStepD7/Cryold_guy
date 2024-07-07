using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : TrapActivator
{
    public DetectPlayer detector;
    public AudioSource laserActivationSound;
    public bool startActivated;
	public override void SwitchPress()
	{
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
            laserActivationSound.Play();
	}
	private void Start()
	{
        gameObject.SetActive(startActivated);
	}

	// FixedUpdate is called once per frame
	void FixedUpdate()
    {
        if (detector.DetectPlayerOnce())
        {
            Player.id.Player_Lock();
            DeathAnimationControllers.id.laser.SetActive(true);
        }
    }
}
