using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyInstruction : MonoBehaviour
{

    public AudioSource ins;
    public GameObject lefthand;
    public GameObject righthand;
    public GameManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="hands" && !manager.SplashDone)
        {
            lefthand.SetActive(true);
            righthand.SetActive(true);
            ins.Play(); 
        }
    }

  
}
