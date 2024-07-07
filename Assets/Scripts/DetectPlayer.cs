using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [HideInInspector]
    public Player playerDetected;
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Player>(out var player);
        if (player)
        {
            playerDetected = player;
            Debug.Log("Player Detected");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.TryGetComponent<Player>(out var player);
        if (player)
        {
            playerDetected = null;

            Debug.Log("Player Removed");
        }
    }
    public Player DetectPlayerOnce()
	{
        var p = playerDetected;
        playerDetected = null;
        return p;
	}
}
