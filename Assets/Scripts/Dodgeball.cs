using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Dodgeball : MonoBehaviour {
  public float partLifeTime, radius, explForce;
  private bool exploded;
  public int amtOfBricksHit, amtOfBricksDestroyed;
  public bool destroyMe = false;
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
            brick brickToHurt = hit.GetComponent<brick>();
            float explDistance = (Vector3.Distance(explosionPos, hit.gameObject.transform.position)/radius); //normalized explosion distance
            if(!brickToHurt.isDead){
              bool destroyed = brickToHurt.ReceiveDamage("explosion", explDistance);
              if(destroyed){
                amtOfBricksDestroyed++;
              }
            }
            if(explDistance < 0.5f){
              amtOfBricksHit++;
            }

          }
          rb.AddExplosionForce(explForce, explosionPos, radius, 3.0F);
        }
      }
      Destroy(GameObject.FindGameObjectWithTag("particle"), partLifeTime);
      destroyMe = true;
      //Destroy(this);
    }
  }

}
