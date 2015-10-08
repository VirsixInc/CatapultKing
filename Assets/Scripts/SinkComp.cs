using UnityEngine;
using System.Collections;

public class SinkComp : MonoBehaviour {

  bool sink;
  Vector3 destVec;
  float startTime = 0f, journeyLength, speed=0.1f, delay = 3f;
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
      if(Vector3.Distance(transform.position, destVec)<1f){
        Destroy(gameObject);
      }
    }
  }

  void OnCollisionEnter(Collision coll){
    if(coll.gameObject.tag == "ground" && 
        (gameObject.tag == "fragment" || 
        gameObject.tag == "ball")){
      startTime = Time.time + delay;
    }
  }
}
