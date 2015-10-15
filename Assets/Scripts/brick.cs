using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Assets.Scripts;
using System.Linq;
using System;

public class brick : MonoBehaviour{
  public GameManager currManager;
  public int id;
  private List<GameObject> Blocks;

  public int maxHealth = 150; //todo make maxhealth
  private int currentHealth;

  Rigidbody currBody;
  public bool isDead;
  void Start(){
    currBody = GetComponent<Rigidbody>();
    if(gameObject.tag == "broken"){
      Destroy(this);
    }

    currentHealth = maxHealth;
  }

  void Update(){
    if(currentHealth <= 0 && gameObject.tag == "block"){
      currManager.allBricks[id].updateBlock(0); //KILL BLOCK
      gameObject.tag = "fragment";
      GameObject[] shatters = TurboSlice.instance.shatter(gameObject, 2);
      foreach (GameObject shattered in shatters){
        shattered.gameObject.tag = "fragment";
        shattered.GetComponent<Rigidbody>().velocity = shattered.GetComponent<Rigidbody>().velocity / 2;
        shattered.GetComponent<SinkComp>().enabled = true;
      }
      DestroyBrick();
    }
  }
  public void DestroyBrick(){
    Destroy(gameObject);
  }
  void OnTriggerExit(Collider col){
    if(col.gameObject.tag == "ground"){
      currManager.allBricks[id].updateBlock(0);
    }
  }
  public bool ReceiveDamage(string type, float amt){
    bool dead = false;
    amt = Mathf.Clamp(amt, 0f, 1f);
    if(currManager.damageValues.ContainsKey(type)){
      int dmgToReceive;
      if(currManager.damageValues.TryGetValue(type, out dmgToReceive)){
        dmgToReceive = (int)(dmgToReceive*amt);
        currentHealth = currentHealth-dmgToReceive;
        dead = currManager.allBricks[id].updateBlock((int)currentHealth);
      }
    }
    isDead = dead;
    return dead;
  }
  void OnCollisionEnter(Collision col){
    if(col.gameObject.tag == "block"){
      return;
    }
    if(!currManager.damageValues.ContainsKey(col.gameObject.tag)){
      return;
    }
    if(gameObject.tag == "block" && col.gameObject.GetComponent<Rigidbody>()){
      bool isDead = ReceiveDamage(col.gameObject.tag, Mathf.Clamp(col.gameObject.GetComponent<Rigidbody>().velocity.magnitude, 0f, 1f));
    }
  }
}

















