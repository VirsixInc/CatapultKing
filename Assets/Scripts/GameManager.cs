using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System;
using System.Linq;

public class BrickData{
  public int maxHealth, currHealth;
  public bool dead;
  public BrickData(int maxHealthToSet){
    maxHealth = maxHealthToSet;
    currHealth = maxHealth;
  }
  
  public bool updateBlock(int healthToSet){
    currHealth = healthToSet;
    if(currHealth < 1){
      dead = true;
    }
    return dead;
  }
}

public class GameManager : MonoBehaviour
{
    public Dictionary<string,int> damageValues = new Dictionary<string,int>(){
      //{"fragment",5},
      {"ball",9999}
    };
    public static GameState CurrentGameState = GameState.Start;
    private List<GameObject> Blocks;
    private List<GameObject> Balls;
    private List<GameObject> Enemies;
    public GUIStyle progress_empty;
    public GUIStyle progress_full;
    public Texture aTexture;
    public GameObject enemy;
    public Progressbar currBar;

    public BrickData[] allBricks; 
    float totalLevelHealth;

    void Start()
    {
        CurrentGameState = GameState.Start;
          //find all relevant game objects
        Blocks = new List<GameObject>(GameObject.FindGameObjectsWithTag("block"));
        allBricks = new BrickData[Blocks.Count];
        for(int i = 0; i<allBricks.Length; i++){
          brick currBrick = Blocks[i].GetComponent<brick>();
          currBrick.id = i;
          allBricks[i] = new BrickData((int)currBrick.maxHealth);
        }
        totalLevelHealth = GetTotalLevelHealth();
        Balls = new List<GameObject>(GameObject.FindGameObjectsWithTag("ball"));
        Enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("enemy"));
        //unsubscribe and resubscribe from the event
        //this ensures that we subscribe only once
        //**********************Maybe add state to make sure that only one ball can be thrown*******************************//




    }
    void Update()
    {
      currBar.updateBar(((float)GetDestroyedBlocks()/(float)allBricks.Length));
      switch (CurrentGameState)
      {
          case GameState.Start:
              break;
          case GameState.BallMovingtoPos:
              //Can add pause here or timer
              break;
          case GameState.Playing:
              //Add timer if ball is being thrown or timer is activated
              break;
          case GameState.Won:
          case GameState.Lost:
              if (Input.GetMouseButtonUp(0)) ;
              {
                  Application.LoadLevel(Application.loadedLevel);

              }
              break;
          default:
              break;
      }
    }
    public int GetDestroyedBlocks(){
      int amtOfBlocksDestroyed = 0;
      foreach(BrickData currData in allBricks){
        if(currData.dead){
          amtOfBlocksDestroyed++;
        }
      }
      return amtOfBlocksDestroyed;
    }
    public float GetTotalLevelHealth(){
      float healthToReturn = 0f;
      foreach(BrickData currData in allBricks){
        healthToReturn = healthToReturn + currData.maxHealth;
      }
      return healthToReturn;
    }
    public float GetCurrentHealth(){
      float healthToReturn = 0f;
      foreach(BrickData currData in allBricks){
        healthToReturn = healthToReturn + currData.currHealth;
      }
      return healthToReturn;
    }

    public void UpdateBlock(int id, float currHealth){


    }

    private bool Blockcheck()
    {
        return Blocks.All((x => x == null));
    }
    public static void AutoResize(int screenWidth, int screenHeight)
    {
        //Incase of different quality projectors or load settings
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }
    public void achivement()
    {
        Debug.Log("achivment");


    }
    void OnGui()
    {
        AutoResize(800, 480);
        switch (CurrentGameState)
        {
            case GameState.Start:
                GUI.Label(new Rect(0, 150, 200, 100), "Throw a ball to start ");
                break;
            case GameState.Won:
                GUI.Label(new Rect(0, 150, 200, 100), "You Won! a ball to start ");
                break;
            case GameState.Lost:
                GUI.Label(new Rect(0, 150, 200, 100), "You Lost a ball to start ");
                break;
            default:
                break;
        }


    }



   
}
