using UnityEngine;
using System.Collections.Generic;
using NobleMuffins.MuffinSlicer;

public class TSD4BurstApartWhenSliced : AbstractSliceHandler
{
	public float burstForce = 100f;
	
	public override void handleSlice( GameObject[] results )
	{
		IList<Vector3> centers = new Vector3[results.Length];
		
		for(int i = 0; i < results.Length; i++) centers[i] = results[i].GetComponent<Collider>().bounds.center;
		
		var center = centers.Average();
		
		for(int i = 0; i < results.Length; i++)
		{
			GameObject go = results[i];
			Rigidbody rb = go.GetComponent<Rigidbody>();
			if(rb != null)
			{
				Vector3 v = centers[i] - center;
				v.Normalize();
				v *= burstForce;
				rb.AddForce(v);
			}
		}
	}
}
