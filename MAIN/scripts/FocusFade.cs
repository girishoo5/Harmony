using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FocusFade : MonoBehaviour
{
    public SceneTransitionManager sceneTransitionManager;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitforNextScene());
    }

    IEnumerator WaitforNextScene()
    {
        yield return new WaitForSeconds(5);
        sceneTransitionManager.GoToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
