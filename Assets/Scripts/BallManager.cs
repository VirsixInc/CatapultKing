using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallManager : MonoBehaviour {


	public GameObject ballPrefab;
	public float shootStrength = 1000f;
	public GameObject hitParticle;

	float ballShootCooldown = 0.5f;

	public GameObject nearPlane;

	void Start () {
	}

	class ShootData {
		public ShootData(Vector2 p_start, Vector3 p_dest) {
			start = p_start; dest = p_dest;
		}
		public ShootData() { }
		public Vector2 start;
		public Vector3 dest;
	}

	void Update () {
    if(Input.GetMouseButtonDown(0))
      Shoot(Input.mousePosition);
	}

	public void Shoot(Vector2 pos) {
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(pos), out hit)) {
			Shoot(new ShootData(pos, hit.point));
		}
	}
	

	void Shoot(ShootData shootData) {
//		print (shootData.start);

		Vector3 startPos = shootData.start;
		//	startPos.z -= Camera.main.transform.position.z/4f;// - nearPlane.transform.position.z;

		GameObject ball = Instantiate(ballPrefab) as GameObject;//StaticPool.GetObj("red");//ballPrefabDict[shootData.color.ToString()]);
		//ball.GetComponent<Ball>().Reset();

		ball.transform.position = Camera.main.ScreenToWorldPoint(startPos + Camera.main.transform.forward * 4f);
		ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

		Vector3 shootDir = shootData.dest - ball.transform.position;
		shootDir.Normalize();
    ball.GetComponent<Rigidbody>().AddForce(shootDir*shootStrength);
	}
}
