using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class TSD4TargetSlicer : MonoBehaviour
{
	public new Camera camera;
	
	public float minimumSpeedForSlicing = 1f;
	public int sampleCount = 3;
	public float cooldownTime = 0.1f;
	
	private const int mouseIndex = -1;
	private readonly IDictionary<int,Vector2> priorPositionByIndex = new Dictionary<int, Vector2>();
	private readonly IDictionary<TSD4Target,float> cooldownByTarget = new Dictionary<TSD4Target, float>();

	private static readonly ICollection<TouchPhase> ValidTouchPhases = new HashSet<TouchPhase>(new [] {
		TouchPhase.Began, TouchPhase.Moved, TouchPhase.Stationary
	});

	// Update is called once per frame
	void Update ()
	{	
		bool touchesAreDown = Input.touchCount > 0;
		bool mouseIsDown = (Input.GetMouseButton(0) || Input.GetButton("Fire1") ) && !touchesAreDown;
		
		List<int> observedIndices = new List<int>(8);
		
		IList<TSD4Target> targets = null;
		Vector3[] targetCirclesByIndex = null;
		
		if(mouseIsDown || touchesAreDown)
		{
			targets = TSD4Target.targets;
		
			targetCirclesByIndex = DeriveCirclesFromTargets(targets);
		}
		
		if(touchesAreDown)
		{
			foreach(Touch t in Input.touches)
			{
				int index = t.fingerId;
				if(t.phase == TouchPhase.Moved)
				{					
					ProcessInput(index, t.position, targets, targetCirclesByIndex);
				}
				if(ValidTouchPhases.Contains(t.phase))
				{
					observedIndices.Add(index);
				}
			}
		}
	
		if(mouseIsDown)
		{
			ProcessInput(mouseIndex, Input.mousePosition, targets, targetCirclesByIndex);
			
			observedIndices.Add(mouseIndex);
		}
		
		List<int> removalQueue = new List<int>();
		
		foreach(int key in priorPositionByIndex.Keys)
		{
			bool continuesToBeObserved = observedIndices.Contains(key);
			
			if(!continuesToBeObserved)
			{
				removalQueue.Add(key);
			}
		}
		
		foreach(int key in removalQueue)
		{
			priorPositionByIndex.Remove(key);

		}

		var cooldownKeys = new TSD4Target[cooldownByTarget.Count];
		cooldownByTarget.Keys.CopyTo(cooldownKeys, 0);

		foreach(var key in cooldownKeys)
		{
			float f = cooldownByTarget[key];
			f -= Time.deltaTime;
			if(f < 0f) cooldownByTarget.Remove(key);
			else cooldownByTarget[key] = f;
		}
	}
	
	private void ProcessInput(int key, Vector2 currentPosition, IList<TSD4Target> targets, Vector3[] targetCirclesByIndex)
	{
		if(priorPositionByIndex.ContainsKey(key))
		{
			Vector2 priorPosition = priorPositionByIndex[key];
			
			float pixelsPerSecond = (currentPosition - priorPosition).magnitude / Time.deltaTime;
			
			bool maySlash = pixelsPerSecond > minimumSpeedForSlicing;
			
			List<GameObject> objectsToSlice = new List<GameObject>();
			
			if(maySlash)
			{	
				for(int i = 0; i < targetCirclesByIndex.Length; i++)
				{
					if(cooldownByTarget.ContainsKey(targets[i]) == false) {
						Vector3 circle = targetCirclesByIndex[i];
						
						float deltaSquared = (currentPosition.x - circle.x) * (currentPosition.x - circle.x) +
							(currentPosition.y - circle.y) * (currentPosition.y - circle.y);
						
						bool fallsWithin = deltaSquared < (circle.z * circle.z);
						
						if(fallsWithin) {
							objectsToSlice.Add(targets[i].gameObject);
						}
					}
				}
			}
			
			foreach(GameObject go in objectsToSlice)
			{
				//REMINDER: The current and prior positions here convey where on the screen the touch
				//is and where it came from, rather than the position of the object itself.

				var results = TurboSlice.instance.splitByLine(go, camera, currentPosition, priorPosition);

				if(results.Length > 0)
				{
					foreach(var result in results)
					{
						var target = result.GetComponent<TSD4Target>() as TSD4Target;
						if(target != null)
						{
							cooldownByTarget[target] = cooldownTime;
						}
					}
				}
			}
		}

		priorPositionByIndex[key] = currentPosition;
	}
	
	private Vector3[] DeriveCirclesFromTargets(IList<TSD4Target> allTargets)
	{
		Vector3 cameraPosition = camera.transform.position;
		
		Vector3[] circles = new Vector3[ allTargets.Count ];
		
		for(int i = 0; i < allTargets.Count; i++)
		{
			Vector3 positionInThreeSpace = allTargets[i].transform.position;
			
			Quaternion q = Quaternion.LookRotation(positionInThreeSpace - cameraPosition);
			Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, q, Vector3.one);

//#warning I changed here the renderer to a collider. Review this; it was done naively.
			Vector3 _size = allTargets[i].GetComponent<Collider>().bounds.size;
			float size = Mathf.Max(_size.x, _size.y, _size.z) / 2f;
			
			Vector3 upFrom = m.MultiplyVector(Vector3.up) * size + positionInThreeSpace;

			Vector3 positionInTwoSpace = camera.WorldToScreenPoint(positionInThreeSpace);
			Vector3 upFromPositionInTwoSpace = camera.WorldToScreenPoint(upFrom);
			
			float radius = (positionInTwoSpace - upFromPositionInTwoSpace).magnitude;
			
			Vector3 circle = new Vector3(positionInTwoSpace.x, positionInTwoSpace.y, radius);
			
			circles[i] = circle;
		}

		return circles;
	}
}
