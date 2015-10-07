using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
  public void updateBar(float amt){
    Image image = GetComponent<Image>();
    image.fillAmount = amt;

  }
}
