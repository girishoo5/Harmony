using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRed : MonoBehaviour
{
    private ClimbUITrigger trigger;
    public Animator red;


    private void Start()
    {
       
    }

    public void Update()
    {
        // Find the GameObject with the ClimbUITrigger script attached
        GameObject triggerObject = GameObject.Find("Climb UI Trigger");

        // Get the ClimbUITrigger component from the GameObject
        if (triggerObject != null)
        {
            trigger = triggerObject.GetComponent<ClimbUITrigger>();
        }

        if (trigger != null)
        {
            if (trigger.handsInside)
            {
                red.SetBool("red", true);
                red.SetBool("nonred", false);
            }
            else if (!trigger.handsInside)
            {
                red.SetBool("nonred", true);
                red.SetBool("red", false);
            }
        }
 
    }
}
