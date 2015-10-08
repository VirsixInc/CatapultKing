using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Dodgeball : MonoBehaviour {
  public float partLifeTime, radius, explForce;
  private bool exploded;
  void OnCollisionEnter(Collision coll) {
    if(!exploded){
      exploded = true;
      SpecialEffectsHelper.Instance.Explosion(transform.position);
      Vector3 explosionPos = transform.position;
      Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
      foreach (Collider hit in colliders) {
        Rigidbody rb = hit.GetComponent<Rigidbody>();
        if (rb != null){
          if(hit.GetComponent<brick>()){
            hit.gameObject.GetComponent<brick>().ReceiveDamage("explosion", (Vector3.Distance(explosionPos, hit.gameObject.transform.position)/radius));
          }
          rb.AddExplosionForce(explForce, explosionPos, radius, 3.0F);
        }
      }
      Destroy(GameObject.FindGameObjectWithTag("particle"), partLifeTime);
      Destroy(this);
    }
  }

}
