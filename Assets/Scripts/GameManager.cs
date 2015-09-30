using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameState CurrentGameState = GameState.Start;
    private List<GameObject> Blocks;
    private List<GameObject> Balls;
    private List<GameObject> Enemies;

    void Start()
    {
        CurrentGameState = GameState.Start;
          //find all relevant game objects
        Blocks = new List<GameObject>(GameObject.FindGameObjectsWithTag("block"));
        Balls = new List<GameObject>(GameObject.FindGameObjectsWithTag("ball"));
        Enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("enemy"));
        //unsubscribe and resubscribe from the event
        //this ensures that we subscribe only once
        //**********************Maybe add state to make sure that only one ball can be thrown*******************************//
    }
    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Start:
                if (Input.GetMouseButtonUp(0))
                {
                    //Animate ball being loaded

                }
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
    private bool AllEneimesDestroyed()
    {
        return Enemies.All((x => x == null));

    }
    public static void AutoResize(int screenWidth, int screenHeight)
    {
        //Incase of different quality projectors or load settings
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
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
