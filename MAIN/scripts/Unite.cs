using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unite : MonoBehaviour
{
    public GameObject happy;
    public GameObject sad;
    public GameObject calm;
    public GameObject anger;
    public AudioSource crash;
    public AudioSource unitee;
    public AudioSource OhNoo;
    public Transform happyHome;
    public Transform sadHome;
    public Transform calmHome;
    public Transform angerHome;
    public GameObject unitedCrystal;



    public void unite()
    {
        StartCoroutine(MoveObject(happy, happy.transform.position, transform.position, 7, 1));
        StartCoroutine(MoveObject(sad, sad.transform.position, transform.position, 7, 1));
        StartCoroutine(MoveObject(calm, calm.transform.position, transform.position, 7, 1));
        StartCoroutine(MoveObject(anger, anger.transform.position, transform.position, 7, 1));
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
            unitedCrystal.SetActive(true);
            unitee.Play();

           
        }
        else
        {
            OhNoo.Play();
        }

    }
}
