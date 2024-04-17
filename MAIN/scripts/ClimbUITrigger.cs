using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbUITrigger : MonoBehaviour
{
    
    //public Transform xrorigin;
    public bool handsInside = false;
    //public Animator red;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="hands")
        {
            Debug.Log("Hand entered trigger area");
            handsInside = true;
            //red.SetBool("red", true);
            //controllers.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "hands")
        {
            Debug.Log("Hand exited trigger area");
            handsInside = false;
            //red.SetBool("nonred", true);
            //controllers.SetActive(false);
        }
    }

}
