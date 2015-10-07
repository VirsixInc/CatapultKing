using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class brick : MonoBehaviour{
  public GameManager currManager;
  public int id;
  private List<GameObject> Blocks;

  public int maxHealth = 150; //todo make maxhealth
  private int currentHealth;



  void Start(){
    if(gameObject.tag == "broken"){
      Destroy(this);
    }

    currManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    currentHealth = maxHealth;
  }

  void DestroyBlock(){
    currManager.allBricks[id].updateBlock(0); //KILL BLOCK
    Destroy(gameObject);
  }

  void OnTriggerExit(Collider col){
    if(col.gameObject.tag == "ground"){
      DestroyBlock();
    }
  }
  void OnCollisionEnter(Collision col){
    if(col.gameObject.tag == "block"){
      return;
    }

    if(!currManager.damageValues.ContainsKey(col.gameObject.tag)){
      print("DOES NOT CONTAIN");
      return;
    }
    int damageToReceive;
    if(currManager.damageValues.TryGetValue(col.gameObject.tag, out damageToReceive)){
      print("OUT");
    }else{
      print("ELSE");
      return;
    }
    if(col.gameObject.GetComponent<Rigidbody>()){
      damageToReceive = (int)(damageToReceive*col.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
      if(damageToReceive < 5){
        return;
      }
    }
    if(gameObject.tag == "block"){
      currentHealth = currentHealth-damageToReceive;
      if(currManager != null){
        bool isDead = currManager.allBricks[id].updateBlock((int)currentHealth);
        if(isDead){
          GameObject[] shatters = TurboSlice.instance.shatter(gameObject, 2);
          foreach (GameObject shattered in shatters){
            shattered.gameObject.tag = "fragment";
            shattered.GetComponent<Rigidbody>().velocity = shattered.GetComponent<Rigidbody>().velocity / 2;
            shattered.GetComponent<fragment>().enabled = true;
          }
          Destroy(gameObject);
        }
      }
    }
  }
}
