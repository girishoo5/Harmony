using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSplit : MonoBehaviour
{
    public GameObject happy;
    public GameObject sad;
    public GameObject calm;
    public GameObject anger;
  
    public Transform happyHome;
    public Transform sadHome;
    public Transform calmHome;
    public Transform angerHome;

  //public AudioSource crash;
    public AudioSource Welcome;
    //public AudioSource OhNoo;

    private void Start()
    {
        Welcome.Play();
    }

    public void Break()
    {
        StartCoroutine(MoveObject(calm, calm.transform.position, calmHome.position, 4, 0));
        StartCoroutine(MoveObject(sad, sad.transform.position, sadHome.position, 4, 0));
        StartCoroutine(MoveObject(happy, happy.transform.position, happyHome.position, 4, 0));
        StartCoroutine(MoveObject(anger, anger.transform.position, angerHome.position, 4, 0));

    }


    IEnumerator MoveObject(GameObject obj, Vector3 startPosition, Vector3 targetPosition, int moveTime, int mode)
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
        }

        //else
        //{
        //    OhNoo.Play();
        //}

    }

}
