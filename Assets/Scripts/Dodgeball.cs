using UnityEngine;
using System.Collections;

public class Dodgeball : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
	
	}



  void OnCollisionEnter(Collision coll){
    if(coll.collider.gameObject.tag == "block")
        {
            coll.collider.gameObject.GetComponent<blockScript>().broken = true;
            GameObject[] shatters = TurboSlice.instance.shatter(coll.collider.gameObject, 2);
            
            foreach (GameObject shattered in shatters)
                {
                    shattered.GetComponent<Rigidbody>().velocity = shattered.GetComponent<Rigidbody>().velocity/2;

                }
        }
  }
}
