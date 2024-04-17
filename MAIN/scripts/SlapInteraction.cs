using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;

public class SlapInteraction : MonoBehaviour
{
    public AudioClip splash1;
    public AudioClip splash2;
    public AudioSource currentSong;
    public float maxLoaderValue = 100.0f; // Maximum loader value
    public float intervalDuration = 1f; // Time interval during which clicks are more effective
    public float normalIncreaseRate = 4.0f;
    public float normalDecreaseRate = 0.2f; // Regular rate at which loader increases per click
    public float intervalIncreaseRate = 10.0f; // Increased rate during the interval
    public bool complete = false;
    public bool colliding = false;
    public float currentLoaderValue = 0.0f;
    private float timeSinceLastClick = 0.0f;
    public GameManager GM;
    public AudioSource tadah;
    public AudioSource areabgm;
    // UI elements
    public Slider loaderSlider;

    public GameObject happyBuddha;
    public Transform startP;
    public Transform endP;  

    void Start()
    {
        // Initialize UI elements
        if (loaderSlider != null)
        {
            loaderSlider.maxValue = maxLoaderValue;
            loaderSlider.value = currentLoaderValue;
        }
    }

    public void playmusic()
    {

        if (currentSong.clip == splash1)
            currentSong.clip = splash2;
        else
            currentSong.clip = splash1;
        currentSong.Play();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "hands")
        {
            Debug.Log("colL");
            colliding = true;
            playmusic();
            StartCoroutine(setCollidingFalse());
        }
    }
    private IEnumerator setCollidingFalse()
    {
        yield return new WaitForSeconds(0.02f);
        colliding = false;
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "hands")
        {
            colliding = false;
        }
    }
  

    void Update()
    {
        if (!complete)
        {
            if(colliding == false)
            {
                DecreaseLoader();
            }
            else
            {
                IncreaseLoader();
            }
            UpdateUISlider();
        }
    }

    void IncreaseLoader()
    {
        if (Time.time - timeSinceLastClick < intervalDuration)
        {
            currentLoaderValue = intervalIncreaseRate + currentLoaderValue;
        }
        else
        {
            currentLoaderValue += normalIncreaseRate + currentLoaderValue;
        }
        // Clamp the loader value to the maximum
        currentLoaderValue = Mathf.Clamp(currentLoaderValue, 0.0f, maxLoaderValue);
        if (currentLoaderValue >= 99)
        {
            complete = true;
            GM.SplashDone = true;
            PlaySuccessSound();
        }
        // Update the time of the last click
        timeSinceLastClick = Time.time;
    }

    void DecreaseLoader()
    {
        currentLoaderValue -= normalDecreaseRate;
        // Clamp the loader value to the minimum
        currentLoaderValue = Mathf.Clamp(currentLoaderValue, 0.0f, maxLoaderValue);
    }

    void UpdateUISlider()
    {
        // Update the UI Slider value
        if (loaderSlider != null)
        {
            loaderSlider.value = currentLoaderValue;
            MoveObject(startP.position, endP.position, currentLoaderValue);
        }
    }
    private void PlaySuccessSound()
    {
        areabgm.Stop();
        tadah.Play();
    }
    public void resetGame()
    {
        complete = false;
        currentLoaderValue = 0f;
    }

    public void MoveObject( Vector3 startPosition, Vector3 targetPosition, float t )
    {
        Vector3 calculatePosition;
        calculatePosition=new Vector3(0, t/16.5f,0);
        happyBuddha.transform.localPosition = calculatePosition;
       

        
    }
}
