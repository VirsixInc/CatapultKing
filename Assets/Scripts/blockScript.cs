using UnityEngine;
using System.Collections;

public class blockScript : MonoBehaviour {


  public bool broken;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnCollisionStay(Collision coll){
    if(coll.collider.gameObject.tag == "ground" || coll.collider.gameObject.tag == "groundMtn" && broken){
      Destroy(gameObject);
    }
  }
}
