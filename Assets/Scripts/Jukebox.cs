using UnityEngine;

public class Jukebox : MonoBehaviour
{
    public AudioSource mAudio;
    public AudioClip[] audioClips;
    public float minWaitTime;
    public float maxWaitTime;
    float delay;
	private void FixedUpdate()
	{
        if (mAudio.isPlaying)
            return;
        delay -= Time.deltaTime;
        if (delay > 0)
            return;
        RandomizeDelay();
		var randomMusic = Random.Range(0, audioClips.Length);
        mAudio.clip = audioClips[randomMusic];
        mAudio.Play();
	}
    public void RandomizeDelay()
	{
        delay = Random.Range(minWaitTime, maxWaitTime);
    }
	private void Start()
	{
        RandomizeDelay();
	}
}
