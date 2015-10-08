using UnityEngine;
using System.Collections;

public class AchivementFlash : MonoBehaviour
{
    private Texture2D pixel;
    public Color color = Color.red;
    public float startAlpha = 0.0f;
    public float maxAlpha = 1.0f;
    public float rampUpTime = 0.5f;
    public float holdTime = 0.5f;
    public float rampDownTime = 0.5f;

    enum FLASHSTATE { OFF, UP, HOLD, DOWN }
    Timer timer;
    FLASHSTATE state = FLASHSTATE.OFF;


    // Use this for initialization
    void Start()
    {
        pixel = new Texture2D(1, 1);
        color.a = startAlpha;
        pixel.SetPixel(0, 0, color);
        pixel.Apply();
        // for testing
        //tookachivment(new achivment(1));
    }

    public void Update()
    {
        switch (state)
        {
            case FLASHSTATE.UP:
                if (timer.UpdateAndTest())
                {
                    state = FLASHSTATE.HOLD;
                    timer = new Timer(holdTime);
                }
                break;
            case FLASHSTATE.HOLD:
                if (timer.UpdateAndTest())
                {
                    state = FLASHSTATE.DOWN;
                    timer = new Timer(rampDownTime);
                }
                break;
            case FLASHSTATE.DOWN:
                if (timer.UpdateAndTest())
                {
                    state = FLASHSTATE.OFF;
                    timer = null;
                }
                break;
        }
    }

    private void SetPixelAlpha(float a)
    {
        color.a = a;
        pixel.SetPixel(0, 0, color);
        pixel.Apply();
    }

    public void OnGUI()
    {
        switch (state)
        {
            case FLASHSTATE.UP:
                SetPixelAlpha(Mathf.Lerp(startAlpha, maxAlpha, timer.Elapsed));
                break;
            case FLASHSTATE.DOWN:
                SetPixelAlpha(Mathf.Lerp(maxAlpha, startAlpha, timer.Elapsed));
                break;
        }
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pixel);
    }

    public class Timer
    {
        float _timeElapsed;
        float _totalTime;

        public Timer(float timeToCountInSec)
        {
            _totalTime = timeToCountInSec;
        }

        public bool UpdateAndTest()
        {
            _timeElapsed += Time.deltaTime;
            return _timeElapsed >= _totalTime;
        }

        public float Elapsed
        {
            get { return Mathf.Clamp(_timeElapsed / _totalTime, 0, 1); }

        }
    }
}