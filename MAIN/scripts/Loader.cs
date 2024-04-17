using UnityEngine;

public class Loader : MonoBehaviour
{
    public float duration = 5f; // Set the duration in seconds

    private void Start()
    {
        StartCoroutine(AnimateLoader());
    }

    private System.Collections.IEnumerator AnimateLoader()
    {
        float elapsedTime = 0f;
        Vector3 startScale = new Vector3(1f, 0f, 1f);
        Vector3 endScale = new Vector3(1f, 1f, 1f);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
}
