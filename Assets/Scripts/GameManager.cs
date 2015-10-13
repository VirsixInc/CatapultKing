using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System;
using System.Linq;

public class ShootData {
  public ShootData(Vector2 p_start, Vector3 p_dest) {
    start = p_start; dest = p_dest;
  }
  public Vector2 start;
  public Vector3 dest;
}
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

/*
public class DodgeballData{
  public int bricksHit, bricksDestroyed;
  public GameObject assocGameObject; //associated gameobject
  public int id;
  
  public DodgeballData(GameObject assocGameObjectToSet, int idToSet){
    assocGameObject = assocGameObjectToSet;
    id = idToSet;
    assocGameObject.name = id.ToString();
  }
}
*/
public class GameManager : MonoBehaviour{
    public enum GameState{
      Title,
      ConfigureLevel,
      Start,
      Playing,
      Won,
      GameOver,
      Lost
    }
    public static GameManager s_instance;
    public static GameState CurrentGameState;

    //Add tag of the objects to collide with damage value
    //This is not the damage inflicted, this is the maximum amount of damage
    public Dictionary<string,int> damageValues = new Dictionary<string,int>(){
      {"fragment",1},
      {"explosion",10},
      {"kill",9999999},
      {"ball",999999}
    };
    public BrickData[] allBricks; 
    List<BrickData> aliveBricks;
    List<Dodgeball> currDodgeballs = new List<Dodgeball>();
    BallManager ballManager;
    GUIManager guiManager;
    
    public GameObject ballPrefab;
    public float shootStrength;

    float totalLevelHealth;
    float roundClock, roundDuration = 15f;

    string[] titleWindows = new string[2]{
      "Title",
      "HighScores"
    };
    int titleIterator = 0, ballIterator = 0;
    float levelProgress, ticketProgress;

    void Awake(){
      if(GameManager.s_instance == null){
        GameManager.s_instance = this;
      }else{
        Destroy(gameObject);
      }
      ballManager = GetComponent<BallManager>();
      guiManager = transform.GetChild(0).GetComponent<GUIManager>();
      DontDestroyOnLoad(gameObject);
    }
    void Update(){
		if(Application.loadedLevelName == "Title"){
        CurrentGameState = GameState.Title;
      }
      switch (CurrentGameState){
        case GameState.Title:
          if(!guiManager.displayingWindow){
            if(guiManager.displayWindowFor(titleWindows[titleIterator], 5f)){
              titleIterator++;
              if(titleIterator == titleWindows.Length){
                titleIterator = 0;
              }
            }
          }
          if(Input.GetMouseButtonDown(0)){
            Application.LoadLevel("Level1");
            CurrentGameState = GameState.ConfigureLevel;
          }
          break;
        case GameState.ConfigureLevel:
          guiManager.resetWindow();
          GameObject[] Blocks = GameObject.FindGameObjectsWithTag("block");
          allBricks = new BrickData[Blocks.Length];
          for(int i = 0; i<allBricks.Length; i++){
            brick currBrick = Blocks[i].GetComponent<brick>();
            currBrick.id = i;
            allBricks[i] = new BrickData((int)currBrick.maxHealth, i, currBrick, this);
          }
          aliveBricks = new List<BrickData>(allBricks);
          totalLevelHealth = GetTotalLevelHealth();
          roundClock = Time.time + roundDuration;
          guiManager.displayingUi = true;
          CurrentGameState = GameState.Playing;
          break;
        case GameState.Start:
          break;
        case GameState.Playing:
          if(Input.GetMouseButtonDown(0)){
            //ballIterator++;
            currDodgeballs.Add(Shoot(Input.mousePosition).GetComponent<Dodgeball>());//new DodgeballData(Shoot(Input.mousePosition),ballIterator));
          }
          float currProg = ((float)GetDestroyedBlocks()/(float)allBricks.Length);
          if(levelProgress != currProg){
            levelProgress = currProg;
            guiManager.updateSlider("CompletionMeter", levelProgress);
          }
          for(int i =0; i<aliveBricks.Count;i++){
            int aliveBrickId = aliveBricks[i].id;
            if(allBricks[aliveBrickId].checkBlock()){
              aliveBricks.RemoveAt(i);
            }
          }
          if(currDodgeballs.Count > 0){
            for(int i = 0; i<currDodgeballs.Count;i++){
              if(currDodgeballs[i].destroyMe){
                if(currDodgeballs[i].amtOfBricksHit >= 50){
                  guiManager.displayWindowFor("50kills",2f);
                }
                Destroy(currDodgeballs[i]);
                currDodgeballs.RemoveAt(i);
              }
            }
          }
          if(aliveBricks.Count == 0){
            CurrentGameState = GameState.Won;
          }
          guiManager.timeLeft = roundClock-Time.time;
          if(roundClock-Time.time <= 0){
            CurrentGameState = GameState.Lost;
          }
          //Add timer if ball is being thrown or timer is activated
          break;
        case GameState.Won:
          guiManager.displayingUi = false;
          if(guiManager.displayWindowFor("Won", 5f)){
            if(guiManager.finishedDisplaying.ToLower() == "won"){
              guiManager.resetWindow();
              CurrentGameState = GameState.GameOver;
            }
          }
          break;
        case GameState.Lost:
          guiManager.displayingUi = false;
          if(guiManager.displayWindowFor("Lost", 5f)){
            if(guiManager.finishedDisplaying.ToLower() == "lost"){
              guiManager.resetWindow();
              CurrentGameState = GameState.GameOver;
            }
          }
          break;
        case GameState.GameOver:
          guiManager.displayingUi = false;
          if(guiManager.displayWindowFor("gameover", 5f)){
            if(guiManager.finishedDisplaying.ToLower() == "gameover"){
              guiManager.resetWindow();
              CurrentGameState = GameState.Title;
              Application.LoadLevel("Title");
            }
          }
          break;
        default:
            break;
      }
      print(CurrentGameState);
    }

    GameObject Shoot(Vector2 pos){//ShootData shootData) {
      RaycastHit hit;
      if(Physics.Raycast(Camera.main.ScreenPointToRay(pos),out hit)){
        ShootData shootData = new ShootData(pos, hit.point);

        Vector3 startPos = shootData.start;
        //	startPos.z -= Camera.main.transform.position.z/4f;// - nearPlane.transform.position.z;

        GameObject ball = Instantiate(ballPrefab) as GameObject;//StaticPool.GetObj("red");//ballPrefabDict[shootData.color.ToString()]);
        //ball.GetComponent<Ball>().Reset();

        ball.transform.position = Camera.main.ScreenToWorldPoint(startPos + Camera.main.transform.forward * 4f);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Vector3 shootDir = shootData.dest - ball.transform.position;
        shootDir.Normalize();
        ball.GetComponent<Rigidbody>().AddForce(shootDir*shootStrength);
        return ball;
      }else{
        return null;
      }
    }
    public int GetDestroyedBlocks(){
      int amtOfBlocksDestroyed = 0;
      amtOfBlocksDestroyed = allBricks.Length-aliveBricks.Count;
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
}
