using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public IntroSplit introSplit;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(waittobusrt());
    }
    public IEnumerator waittobusrt()
    {
        yield return new WaitForSeconds(0.5f);
        introSplit.Break();
    }
}
