using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimationControllers : MonoBehaviour
{
	public static DeathAnimationControllers id { private set; get; }

	private void Awake()
	{
		id = this;
	}

	public GameObject
		piston,
		fire,
		turret,
		laser,
		claw,
		persecutor,
		switchgood;
}
