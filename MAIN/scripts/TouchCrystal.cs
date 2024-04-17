using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchCrystal : MonoBehaviour
{
    public IntroSplit split;
    public SceneTransitionManager sceneTransitionManager;

    public GameObject brokencrystal;
    public GameObject goodlight;
    public GameObject badlight;
    public GameObject completecrystal;

    public AudioSource OhNoo;
    public AudioSource crash;

    public float shakeSpeed = 1f;
    public float shakeMagnitude = 0.1f;

    public bool isShaking = false;
    public bool nextScene = false;

    public Vector3 originalPosition;
    
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.tag == "hands")
        {
            crash.Play();
            completecrystal.gameObject.SetActive(false);
            brokencrystal.SetActive(true);
            goodlight.SetActive(false);
            StartCoroutine(ShakeManager());
            OhNoo.Play();
        }
    }
    public void Start()
    {
        originalPosition = brokencrystal.transform.position;
    }
    public void Update()
    {
        if (isShaking)
        {
           
            float offset = Mathf.Sin(Time.time * shakeSpeed) * shakeMagnitude;
            Vector3 newPosition1 = originalPosition + new Vector3(offset, 0f, 0f);
            brokencrystal.transform.position = newPosition1;
            
        }
    }
    IEnumerator ShakeManager()
    {
        badlight.SetActive(true);
        isShaking = true;
        yield return new WaitForSeconds(3);

        badlight.SetActive(false);
        isShaking = false;
        split.Break();
        StartCoroutine(WaitforNextScene());
    }
    IEnumerator WaitforNextScene()
    {
        yield return new WaitForSeconds(1.5f);
        sceneTransitionManager.GoToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
