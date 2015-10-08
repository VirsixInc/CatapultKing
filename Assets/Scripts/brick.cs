using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Assets.Scripts;
using System.Linq;
using System;

<<<<<<< HEAD
public class brick : MonoBehaviour
{
  
    public GameManager currManager;
    public int id;
    private List<GameObject> Blocks;
<<<<<<< HEAD
=======
public class brick : MonoBehaviour{
  public GameManager currManager;
  public int id;
  private List<GameObject> Blocks;
>>>>>>> wyattDevBranch

  public int maxHealth = 150; //todo make maxhealth
  private int currentHealth;

  Rigidbody currBody;

=======
    public float shattimer =5.0f;
    private float currentHealth;
>>>>>>> e0b60e0e3a91346a0374f709801b896c3f828828




<<<<<<< HEAD
    void Start(){
      if(gameObject.tag == "broken"){
            
           Destroy(this);
      }

      currManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      currentHealth = maxHealth;
=======
  void Start(){
    currBody = GetComponent<Rigidbody>();
    if(gameObject.tag == "broken"){
      Destroy(this);
>>>>>>> wyattDevBranch
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

<<<<<<< HEAD
<<<<<<< HEAD
         
      if(gameObject.tag == "block"){
        currentHealth = currentHealth-damageToReceive;
        if(currManager != null){
          bool isDead = currManager.allBricks[id].updateBlock((int)currentHealth);
          if(isDead){
            GameObject[] shatters = TurboSlice.instance.shatter(gameObject, 2);
            foreach (GameObject shattered in shatters){
              shattered.gameObject.tag = "fragment";
              shattered.GetComponent<Rigidbody>().velocity = shattered.GetComponent<Rigidbody>().velocity / 2;
                                
=======
           
        if(gameObject.tag == "block"){
          Health -= damage;
          currentHealth = Health;
          if(currManager != null){
            bool isDead = currManager.allBricks[id].updateBlock((int)currentHealth);
            if(isDead){
              GameObject[] shatters = TurboSlice.instance.shatter(gameObject, 2);
              foreach (GameObject shattered in shatters){
                shattered.gameObject.tag = "broken";
                shattered.GetComponent<Rigidbody>().velocity = shattered.GetComponent<Rigidbody>().velocity / 2;
                    }
              Destroy(gameObject);
>>>>>>> e0b60e0e3a91346a0374f709801b896c3f828828
            }
            Destroy(gameObject);
          }
        }
=======
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
>>>>>>> wyattDevBranch
      }
    }
    return isDead;
  }
  void OnCollisionEnter(Collision col){
    if(col.gameObject.tag == "block"){
      return;
    }
<<<<<<< HEAD
    /*IEnumerator shattertimer()
    {
        Debug.Log("before timer");
        
       yield return new WaitForSeconds(4);
        
        Debug.Log("After timer");
        yield break;
    }*/

    private void Destroy(GameObject gameObject, object bulletlifetime)
    {
        throw new NotImplementedException();
    }
=======
    if(!currManager.damageValues.ContainsKey(col.gameObject.tag)){
      return;
    }
    if(gameObject.tag == "block" && col.gameObject.GetComponent<Rigidbody>()){
      bool isDead = ReceiveDamage(col.gameObject.tag, Mathf.Clamp(col.gameObject.GetComponent<Rigidbody>().velocity.magnitude, 0f, 1f));
    }
  }
>>>>>>> wyattDevBranch
}

















