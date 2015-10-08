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

  public bool checkBlock(){
    if(assocBlock == null){
      currHealth = 0;
      isDead = true;
    }else if(currHealth == 0){
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
      {"explosion",10},
      {"kill",9999999},
      {"ball",999999}
    };
    public static GameState CurrentGameState;
    public GUIStyle progress_empty;
    public GUIStyle progress_full;
    public Texture aTexture;
    public GameObject enemy;
    public Progressbar currBar;

    public BrickData[] allBricks; 
    List<BrickData> aliveBricks;
    float totalLevelHealth;

    void Awake(){
      DontDestroyOnLoad(gameObject);
    }
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
          GameObject[] Blocks = GameObject.FindGameObjectsWithTag("block");
          allBricks = new BrickData[Blocks.Length];
          for(int i = 0; i<allBricks.Length; i++){
            brick currBrick = Blocks[i].GetComponent<brick>();
            currBrick.id = i;
            allBricks[i] = new BrickData((int)currBrick.maxHealth, i, currBrick, this);
          }
          aliveBricks = new List<BrickData>(allBricks);
          currBar = GameObject.Find("HealthOverlay").GetComponent<Progressbar>();
          totalLevelHealth = GetTotalLevelHealth();
          CurrentGameState = GameState.Playing;
          break;
        case GameState.Start:
          break;
        case GameState.Playing:
          currBar.updateBar(((float)GetDestroyedBlocks()/(float)allBricks.Length));
          for(int i =0; i<aliveBricks.Count;i++){
            int aliveBrickId = aliveBricks[i].id;
            if(allBricks[aliveBrickId].checkBlock()){
              aliveBricks.RemoveAt(i);
            }
          }
          print(aliveBricks.Count);
          //Add timer if ball is being thrown or timer is activated
          break;
        default:
            break;
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
