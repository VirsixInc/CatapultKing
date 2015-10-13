using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class GUIManager : MonoBehaviour {
  //NOTIFICATION WINDOWS DECLARATIONS
  Dictionary<string,GameObject> allWindows = new Dictionary<string,GameObject>();
  
  string currentWindowToDisplay;
  GameObject panelToUse, notiWindowParent;

  float timeTillRemove;
  bool pullWindow;
  public bool displayingWindow = false;
  bool windowOffScreen;

  float lerpStart,
        lerpDistanceStart;

  public float lerpSpeed;
  public string finishedDisplaying;
  Vector2 lerpDestination;
  
  //-------------------
  //
  //IN GAME GUI DECLARATIONS
  //

  Dictionary<string,GameObject> uiElements = new Dictionary<string,GameObject>();
  public float timeLeft;
  public bool displayingUi = false;
  Text timer;
  GameObject uiParent;


  void Awake(){
    notiWindowParent = transform.Find("NotificationWindows").gameObject;
    foreach(Transform trans in notiWindowParent.transform){
      allWindows.Add(trans.gameObject.name.ToLower(), trans.gameObject);
      panelToUse = trans.gameObject;
      resetWindow();
    }
    uiParent = transform.Find("ingameGui").gameObject;
    foreach(Transform trans in uiParent.transform){
      uiElements.Add(trans.gameObject.name.ToLower(), trans.gameObject);
      if(trans.gameObject.name.ToLower() == "timer"){
        timer = trans.gameObject.GetComponent<Text>();
      }
    }
  }

  void Update(){
    if(displayingUi){
      if(!uiParent.activeSelf){
        uiParent.SetActive(true);
      }
      timer.text = timeLeft.ToString();
    }else if(uiParent.activeSelf){
      uiParent.SetActive(false);
    }
    if(displayingWindow){
      if((Vector2)(panelToUse.transform.position) != lerpDestination){
        float distCovered = (Time.time-lerpStart)*lerpSpeed;
        float fracJourney = distCovered/lerpDistanceStart;
        panelToUse.transform.position = Vector2.Lerp(
            panelToUse.transform.position, 
            lerpDestination, fracJourney);
      }else if(Time.time > timeTillRemove){
        setWindowDest(false);
        windowOffScreen = true;
      }
    }
    if(Vector2.Distance(
          panelToUse.transform.position, 
          lerpDestination) < 0.9f && windowOffScreen){
      panelToUse.transform.position = lerpDestination;
      if(Time.time > timeTillRemove){
        displayingWindow = false;
        resetWindow();
      }
    }
  }

  public void updateSlider(string sliderTitle,float magnitude){
    GameObject currSlider;
    if(uiElements.TryGetValue(sliderTitle.ToLower(), out currSlider)){
      currSlider.GetComponent<Slider>().value = magnitude;
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
    windowOffScreen = false;
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
      if(!notiWindowParent.activeSelf){
        notiWindowParent.SetActive(true);
      }
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
