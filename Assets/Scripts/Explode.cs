using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
    public string buttonName = "Fire1";
    public float force;
    public float radius;
    public float upwardsModifier = 0.0f;
    public ForceMode forceMode;

    void OnCollisionEnter(Collision coll){

          if (coll.collider.gameObject.tag == "ground" || coll.collider.gameObject.tag == "block" || coll.collider.gameObject.tag == "groundMtn")
          {
              foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
              {
                  if (col.GetComponent<Rigidbody>() != null)
                  {
                      col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
                  }
              }
          }

      }
}
