﻿using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class brick : MonoBehaviour{
  public GameManager currManager;
  public int id;
  private List<GameObject> Blocks;

  public int maxHealth = 150; //todo make maxhealth
  private int currentHealth;

  Rigidbody currBody;


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
      GameObject[] shatters = TurboSlice.instance.shatter(gameObject, 3);
      foreach (GameObject shattered in shatters){
        shattered.gameObject.tag = "fragment";
        shattered.GetComponent<Rigidbody>().velocity = shattered.GetComponent<Rigidbody>().velocity / 2;
        shattered.GetComponent<SinkComp>().enabled = true;
      }
      DestroyBrick();
    }
  }
  public void DestroyBrick(){
    currManager.allBricks[id].updateBlock(0); //KILL BLOCK
    Destroy(gameObject);
  }

  void OnTriggerExit(Collider col){
    if(col.gameObject.tag == "ground"){
      DestroyBrick();
    }
  }
  public bool ReceiveDamage(string type, float amt){
    bool isDead = false;
    amt = Mathf.Clamp(amt, 0f, 1f);
    if(currManager.damageValues.ContainsKey(type)){
      int dmgToReceive;
      if(currManager.damageValues.TryGetValue(type, out dmgToReceive)){
        dmgToReceive = (int)(dmgToReceive*amt);
        currentHealth = currentHealth-dmgToReceive;
        isDead = currManager.allBricks[id].updateBlock((int)currentHealth);
      }
    }
    return isDead;
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
