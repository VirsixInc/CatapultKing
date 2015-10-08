using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System;
using System.Linq;

public class BrickData{
  public int maxHealth, currHealth;
  public bool isDead;
  public int id;
  public brick assocBlock;
  public BrickData(int maxHealthToSet, int idToSet, brick assocBlockToSet, GameManager gmToSet){
    maxHealth = maxHealthToSet;
    currHealth = maxHealth;
    id = idToSet;
    assocBlock = assocBlockToSet;
    assocBlock.gameObject.name = id.ToString();
    assocBlock.currManager = gmToSet;
  }
  
  public bool updateBlock(int healthToSet){
    currHealth = healthToSet;
    if(currHealth <= 0){
      isDead = true;
    }
    return isDead;
  }
}

public class GameManager : MonoBehaviour{
  public enum GameState{
    Title,
    ConfigureLevel,
    Start,
    Playing,
    Won,
    Lost
  }
  //Add tag of the objects to collide with damage value
  //This is not the damage inflicted, this is the maximum amount of damage
    public Dictionary<string,int> damageValues = new Dictionary<string,int>(){
      {"fragment",1},
      {"explosion",50},
      {"ball",999999}
    };
    public static GameState CurrentGameState;
    private List<GameObject> Blocks;
    public GUIStyle progress_empty;
    public GUIStyle progress_full;
    public Texture aTexture;
    public GameObject enemy;
    public Progressbar currBar;

    public BrickData[] allBricks; 
    float totalLevelHealth;

    void Awake(){
      DontDestroyOnLoad(gameObject);
    }
<<<<<<< HEAD
    void Update()
    {
<<<<<<< HEAD
=======
      
>>>>>>> e0b60e0e3a91346a0374f709801b896c3f828828
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
=======
    void Update(){
      print(CurrentGameState);
      if(Application.loadedLevelName == "Title"){
        CurrentGameState = GameState.Title;
      }
      switch (CurrentGameState){
        case GameState.Title:
          if(Input.GetMouseButtonDown(0)){
            Application.LoadLevel("Level1");
            CurrentGameState = GameState.ConfigureLevel;
          }
          break;
        case GameState.ConfigureLevel:
          Blocks = new List<GameObject>(GameObject.FindGameObjectsWithTag("block"));
          allBricks = new BrickData[Blocks.Count];
          for(int i = 0; i<allBricks.Length; i++){
            brick currBrick = Blocks[i].GetComponent<brick>();
            currBrick.id = i;
            allBricks[i] = new BrickData((int)currBrick.maxHealth, i, currBrick, this);
          }
          currBar = GameObject.Find("HealthOverlay").GetComponent<Progressbar>();
          totalLevelHealth = GetTotalLevelHealth();
          print(Blocks.Count);
          print(allBricks.Length);
          CurrentGameState = GameState.Playing;
          break;
        case GameState.Start:
          break;
        case GameState.Playing:
          currBar.updateBar(((float)GetDestroyedBlocks()/(float)allBricks.Length));
          foreach(BrickData data in allBricks){
            if(!data.isDead){
              print("BLOCK ID: " + data.id + "    BLOCK HEALTH: " + data.currHealth);
            }
          }
          //Add timer if ball is being thrown or timer is activated
          break;
        default:
            break;
>>>>>>> wyattDevBranch
      }
    }
    public int GetDestroyedBlocks(){
      int amtOfBlocksDestroyed = 0;
      foreach(BrickData currData in allBricks){
        if(currData.isDead){
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
    void percent()
    {
    }


   
}
