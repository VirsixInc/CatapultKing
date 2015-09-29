using UnityEngine;
using System.Collections;

public class TSD3SoundWhenSliced : AbstractSliceHandler
{
	public AudioClip clip;
	
	public override void handleSlice( GameObject[] results )
	{
		GameObject go = new GameObject();
		
		go.transform.position = transform.position;
		
		AudioSource source = go.AddComponent<AudioSource>();
		
		source.clip = clip;
		source.Play();
		
		GameObject.Destroy(go, clip.length);
	}
}
