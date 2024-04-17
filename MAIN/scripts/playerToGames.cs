using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerToGames : MonoBehaviour
{
    public GameObject happy;
    public GameObject sad;
    public GameObject calm;//clam
    public GameObject anger;//anger
    public GameObject podium;

    public Transform happyHome;
    public Transform sadHome;
    public Transform calmHome;
    public Transform angerHome;
    public Transform podiumHome;

    public float happyDist;
    public float sadDist;
    public float calmDist;
    public float angerDist;
    public float podiumDist;

    public GameObject currentEffect;

    public float distThreshold;

    public GameManager gm;
    
    public void playerToAnger()
    {
        currentEffect = anger;
        StartCoroutine(MoveObject(anger, transform.position, angerHome.position, 3));
    }
    public void playerToSad()
    {
        currentEffect = sad;
        StartCoroutine(MoveObject(sad, transform.position, sadHome.position, 3));
    }
    public void playerToCalm()
    {
        currentEffect = calm;
        StartCoroutine(MoveObject(calm, transform.position, calmHome.position, 3));
    }
    public void playerToHappy()
    {
        currentEffect = happy;
        StartCoroutine(MoveObject(happy, transform.position, happyHome.position, 3));
    }
    public void playerToPodium()
    {
        currentEffect = podium;
        StartCoroutine(MoveObject(podium, transform.position,podiumHome.position, 3));
    }
    public void Update()
    {
        happyDist = Vector3.Distance(transform.position, happyHome.position);
        sadDist = Vector3.Distance(transform.position, sadHome.position);
        calmDist = Vector3.Distance(transform.position, calmHome.position);
        angerDist = Vector3.Distance(transform.position, angerHome.position);
    }


    IEnumerator MoveObject(GameObject obj, Vector3 startPosition, Vector3 targetPosition, int moveTime)
    {
        Vector3 updateTarget = new Vector3(targetPosition.x, 6, targetPosition.z);
        float elapsedTime = 0f; // Elapsed time
        // Loop until elapsed time is greater than or equal to move time
        while (elapsedTime < moveTime)
        {
            // Calculate the percentage of completion based on elapsed time and move time
            float t = elapsedTime / moveTime;
            obj.transform.position = Vector3.Lerp(startPosition, updateTarget, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the object reaches the exact target position
        obj.transform.position = targetPosition;

        yield return new WaitForSeconds(2f);
        if (currentEffect == happy && obj == happy && !gm.SplashDone && happyDist > distThreshold)
        {
            StartCoroutine(MoveObject(obj, transform.position, happyHome.position,3));
        }
        if(currentEffect == sad && obj == sad && !gm.MemoryDone && sadDist > distThreshold)
        {
            StartCoroutine(MoveObject(obj, transform.position, sadHome.position, 3));
        }
        if(currentEffect == calm && obj ==calm && !gm.StillDone && calmDist > distThreshold)
        {
            StartCoroutine(MoveObject(obj, transform.position, calmHome.position, 3));
        }
        if (currentEffect == anger && obj == anger && !gm.GazeDone && angerDist > distThreshold)
        {
            StartCoroutine(MoveObject(obj, transform.position, angerHome.position, 3));
        }
        if (currentEffect == podium && obj == podium && podiumDist > distThreshold)
        {
            StartCoroutine(MoveObject(obj, transform.position, angerHome.position, 3));
        }
    }
}
