using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerCalmInstruction : MonoBehaviour
{
    public AudioSource ins;
    public GameManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="hands" && !manager.GazeDone)
        {
            ins.Play(); 
        }

        if (other.gameObject.tag == "hands" && !manager.StillDone)
        {
            ins.Play();
        }
    }

  
}
