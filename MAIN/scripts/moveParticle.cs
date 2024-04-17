using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveParticle : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float moveTime;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(move());
    }

    // Update is called once per frame
    IEnumerator move()
    {
        particle.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < moveTime)
        {
            // Calculate the percentage of completion based on elapsed time and move time
            float t = elapsedTime / moveTime;
            particle.transform.position = Vector3.Lerp(start.position, end.position, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the object reaches the exact target position
        particle.transform.position = end.position;
        yield return new WaitForSeconds(1f);
        particle.SetActive(false);
        particle.transform.position = start.position;
        StartCoroutine(move());

    }
}
