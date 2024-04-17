using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadInstruction : MonoBehaviour
{

    public AudioSource ins;
    public MemoryGame game;
    public GameManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="hands" && !manager.MemoryDone)
        {
            ins.Play();
            StartCoroutine(game.ShowPattern());
        }
    }

  
}
