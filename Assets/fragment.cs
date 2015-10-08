using UnityEngine;
using System.Collections;

public class fragment : MonoBehaviour {

  bool sink;
  Vector3 destVec;
  float startTime = 0f, journeyLength, speed=0.1f, delay = 5f;
  void Update(){
    if(startTime != 0 && !sink && startTime < Time.time){
      destVec = transform.position;
      destVec.y = destVec.y - 10f;
      journeyLength = Vector3.Distance(transform.position,destVec);
      gameObject.GetComponent<Collider>().enabled = false;
      Destroy(gameObject.GetComponent<Rigidbody>());
      sink = true;
    }
    if(sink){
      float distCovered = (Time.time - startTime) * speed;
      float fracJourney = distCovered / journeyLength;
      transform.position = Vector3.Lerp(transform.position, destVec, fracJourney); 
      if(fracJourney > 0.8f){
        Destroy(gameObject);
      }
    }
  }

  void OnCollisionEnter(Collision coll){
    if(coll.gameObject.tag == "ground" && gameObject.tag == "fragment"){
      startTime = Time.time + delay;
    }
  }
}
