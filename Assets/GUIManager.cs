using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class GUIManager : MonoBehaviour {

  Dictionary<string,GameObject> allWindows = new Dictionary<string,GameObject>();
  
  string currentWindowToDisplay;
  GameObject panelToUse;

  float timeTillRemove;
  bool pullWindow;
  public bool displayingWindow;

  float lerpStart,
        lerpDistanceStart;

  public float lerpSpeed;
  public string finishedDisplaying;

  Vector2 lerpDestination;


  void Awake(){
    foreach(Transform trans in transform){
      allWindows.Add(trans.gameObject.name.ToLower(), trans.gameObject);
      panelToUse = trans.gameObject;
      resetWindow();
    }
  }

  void Update(){
    if(displayingWindow){
      if((Vector2)(panelToUse.transform.position) != lerpDestination){
        float distCovered = (Time.time-lerpStart)*lerpSpeed;
        float fracJourney = distCovered/lerpDistanceStart;
        panelToUse.transform.position = Vector2.Lerp(
            panelToUse.transform.position, 
            lerpDestination, fracJourney);
      }else if(Time.time > timeTillRemove){
        setWindowDest(false);
      }
      if(Vector2.Distance(
            panelToUse.transform.position, 
            lerpDestination) < 0.9f){
        panelToUse.transform.position = lerpDestination;
        if(Time.time > timeTillRemove){
          displayingWindow = false;
          resetWindow();
        }
      }
    }
  }

  void setWindowDest(bool location){ //true == onscreen, false == offscreen
    lerpStart = Time.time;
    if(location){
      lerpDestination = new Vector2(Screen.width/2,Screen.height/2);
    }else{
      lerpDestination = new Vector2(Screen.width*-2,Screen.height/2);
    }
    lerpDistanceStart = Vector2.Distance(
        panelToUse.transform.position,
        lerpDestination);
  }
  public void resetWindow(){
    displayingWindow = false;
    finishedDisplaying = panelToUse.name;
    panelToUse.SetActive(false);
    panelToUse.transform.position = new Vector2(Screen.width*2, Screen.height/2);
  }

  public bool displayWindowFor(string windowTitle, float windowDuration){
    if(displayingWindow){
      return true;
    }
    windowTitle = windowTitle.ToLower();
    if(allWindows.TryGetValue(windowTitle, out panelToUse)){
      timeTillRemove = Time.time + windowDuration;
      currentWindowToDisplay = windowTitle;
      displayingWindow = true;
      panelToUse.SetActive(true);
      setWindowDest(true);
      return true;
    }else{
      return false;
    }
  }
}
