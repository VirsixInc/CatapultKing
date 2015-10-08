using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour {
    public Texture2D Achivement;
    public Rect imageScreenCord;
    GUITexture imageObject;
    public bool visable;
    float flashLength;
    public bool TwentyFive = true;
    public bool Fifty;
    public bool SeventyFive;
    public bool OneHundred;

    // Use this for initialization
    void Start() {

    }
    public void updateBar(float amt) {
        print("AMOUNT IS: " + amt);
        Image image = GetComponent<Image>();
        image.fillAmount = amt;
        achivTewenty();
    }

    public void ImageObject()
    {
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
    IEnumerator mytimer(float seconds)
    {
        ImageObject();
        yield return new WaitForSeconds(seconds);
       
       // Destroy(imageObject);
        
    }
    public void achment()
    {
        if (TwentyFive == true)
        {
            StartCoroutine(mytimer(3));
            TwentyFive = false;
        }
        if (TwentyFive == false)
        {
            Destroy(imageObject);
        }

    }
    public void achivTewenty()
    {
        if(gameObject.GetComponent<Image>().fillAmount >= .25f && gameObject.GetComponent<Image>().fillAmount <= .30f)
        {
            achment();
            if (gameObject.GetComponent<Image>().fillAmount >= .31f && gameObject.GetComponent<Image>().fillAmount <= .39f)
                TwentyFive = false;
        }
        if (gameObject.GetComponent<Image>().fillAmount >= .50f && gameObject.GetComponent<Image>().fillAmount <= .60f)
        {
            achment();
            if (gameObject.GetComponent<Image>().fillAmount >= .61f && gameObject.GetComponent<Image>().fillAmount <= .69f)
                TwentyFive = false;
        }
        if (gameObject.GetComponent<Image>().fillAmount >= .75f && gameObject.GetComponent<Image>().fillAmount <= .80f)
            {
            achment();
                if (gameObject.GetComponent<Image>().fillAmount >= .81f && gameObject.GetComponent<Image>().fillAmount <= .89f)
                    TwentyFive = false;
        }
        if (gameObject.GetComponent<Image>().fillAmount >= 1f)
        {
            achment();
            StartCoroutine(mytimer(1));
            TwentyFive = false;
        }

        



    }
}




