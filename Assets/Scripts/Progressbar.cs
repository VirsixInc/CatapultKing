using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Image image = GetComponent<Image>();
        image.fillAmount = Mathf.Lerp(image.fillAmount, 1f, Time.deltaTime * .5f);
	}
}
