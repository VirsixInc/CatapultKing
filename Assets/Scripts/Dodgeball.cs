using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Dodgeball : MonoBehaviour {
  public float partLifeTime;
  void OnCollisionEnter(Collision coll) {
    SpecialEffectsHelper.Instance.Explosion(transform.position);
    Destroy(GameObject.FindGameObjectWithTag("particle"), partLifeTime);
    Destroy(this);
  }

}
