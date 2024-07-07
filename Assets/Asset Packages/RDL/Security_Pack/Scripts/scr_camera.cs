using UnityEngine;
using System.Collections;

public class scr_camera : MonoBehaviour {

    public float rotate_amount;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// FixedUpdate is called once per frame
	void FixedUpdate () {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, (Mathf.Sin(Time.realtimeSinceStartup) * rotate_amount) + transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
