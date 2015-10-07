using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour {
    public Texture2D Achivement;
    public Rect imageScreenCord;
    GUITexture imageObject;
    public bool visable;
    float flashLength;
    float waitTime = 2.0f;

    
// Use this for initialization
    void Start () {
	
    }
    public void updateBar(float amt){
    print("AMOUNT IS: " + amt);
    Image image = GetComponent<Image>();
    image.fillAmount = amt;

    }

    public void ImageObject()
    {
        Debug.Log("showme");
        if (imageObject == null)
        {
            GameObject gameobj = new GameObject("ImageObjcet");
            gameobj.transform.localScale = new Vector3(0, 0, 1);
            imageObject = gameobj.AddComponent<GUITexture>();
            imageObject.texture = Achivement;
            imageObject.pixelInset = imageScreenCord;
        }
        return;
    }
 
    public void FlashImage()
    {
        ImageObject();
        Invoke("HideImage", flashLength);
    }
    IEnumerator mytimer()
    {
        ImageObject();
        yield return new WaitForSeconds(waitTime);
    }
    /*
public void HideImage()
{
 ImageObject.enabled = false;

}*/

}




