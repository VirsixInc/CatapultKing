using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour {
    public Texture Achivement;
    public bool visable;
    float flashLength;
    
    // Use this for initialization
    void Start () {
	
	}
  public void updateBar(float amt){
    print("AMOUNT IS: " + amt);
    Image image = GetComponent<Image>();
    image.fillAmount = amt;
              /* if (amt =< 0.25f)
        {
            Debug.Log("showmeachivemtn");
            OnGui();

        }
        */

  }

}
