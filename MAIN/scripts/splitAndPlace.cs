using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splitAndPlace : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject happy;
    public GameObject sad;
    public GameObject calm;
    public GameObject anger;

   //ublic AudioSource crash;
    public AudioSource unitee;
    //blic AudioSource OhNoo;
    public AudioSource Welcome;

    public Transform happyHome;
    public Transform sadHome;
    public Transform calmHome;
    public Transform angerHome;
    public GameObject unitedCrystal;

    public SceneTransitionManager sceneTransitionManager;

    private void Start()
    {
        Welcome.Play();
    }

    public void Break()
    {
          //crash.Play();
            StartCoroutine(MoveObject(calm, calm.transform.position, new Vector3(calmHome.position.x, 4f, calmHome.position.z), 0.001f, 0));
            StartCoroutine(MoveObject(sad, sad.transform.position, new Vector3(sadHome.position.x, 4f, sadHome.position.z), 0.001f, 0));
            StartCoroutine(MoveObject(happy, happy.transform.position, new Vector3(happyHome.position.x, 4f, happyHome.position.z), 0.001f, 0));
            StartCoroutine(MoveObject(anger, anger.transform.position, new Vector3(angerHome.position.x, 4f, angerHome.position.z), 0.001f, 0));
 
    }

    public void unite()
    {
        StartCoroutine(MoveObject(happy, happy.transform.position,transform.position, 7,1));
        StartCoroutine(MoveObject(sad, sad.transform.position, transform.position, 7,1)); 
        StartCoroutine(MoveObject(calm, calm.transform.position, transform.position, 7,1));
        StartCoroutine(MoveObject(anger, anger.transform.position, transform.position, 7,1));
    }

    IEnumerator MoveObject(GameObject obj,Vector3 startPosition, Vector3 targetPosition,float moveTime, int mode)
    {
        float elapsedTime = 0f; // Elapsed time
        // Loop until elapsed time is greater than or equal to move time
        while (elapsedTime < moveTime)
        {
            // Calculate the percentage of completion based on elapsed time and move time
            float t = elapsedTime / moveTime;
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the object reaches the exact target position
        obj.transform.position = targetPosition;

        if (mode == 1)
        {
            new WaitForSeconds(2f);
            obj.SetActive(false);
            unitedCrystal.SetActive(true);
            unitee.Play();
            new WaitForSeconds(2f);
            sceneTransitionManager.GoToScene(6);
        }

    }


}
