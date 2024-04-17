using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    anger,
    sad,
    calm,
    happy
}
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool gazeDone = false;
    [SerializeField]
    private bool memoryDone = false;
    [SerializeField]
    private bool splashDone = false;
    [SerializeField]
    private bool stillDone = false;

    public splitAndPlace splitAndPlace;
    public GameObject centralpiece;
    public playerToGames playerToGames;

    //audio source
    public AudioSource yayFollow;
    public AudioSource climbIntro;

    

    public List<ParticleType> pendingGame = new List<ParticleType>();

    
    private void Start()
    {
        pendingGame.Add(ParticleType.anger);
        pendingGame.Add(ParticleType.sad);
        pendingGame.Add(ParticleType.happy);
        pendingGame.Add(ParticleType.calm);
        StartCoroutine(waittobusrt());
    }
    public bool GazeDone
    {
        set 
        { 
            gazeDone = value;
            pendingGame.Remove(ParticleType.anger);
            if (gazeDone && memoryDone && splashDone && stillDone)
            {
                playerToGames.playerToPodium();
                climbIntro.Play();
                centralpiece.SetActive(true);
            }
            else if(gazeDone)
            {
                selectGame();
                StartCoroutine(YayPlay());
            }
            
        }
        get { return gazeDone; }
    }
    public bool MemoryDone
    {
        set 
        { 
            memoryDone = value;
            pendingGame.Remove(ParticleType.sad);
            if (gazeDone && memoryDone && splashDone && stillDone)
            {
                playerToGames.playerToPodium();
                climbIntro.Play();
                centralpiece.SetActive(true);
            }
            else if(memoryDone)
            {
                selectGame();
                StartCoroutine(YayPlay());
            }
            
        }
        get { return memoryDone; }
    }
  
    public bool StillDone
    {
        set
        { 
            stillDone = value;
            pendingGame.Remove(ParticleType.calm);
            if (gazeDone && memoryDone && splashDone && stillDone)
            {
                playerToGames.playerToPodium();
                climbIntro.Play();
                centralpiece.SetActive(true);
            }
            else if (stillDone)
            {
                selectGame();
                StartCoroutine(YayPlay());
            }
            
        }
        get { return stillDone; }

    }
    public bool SplashDone
    {
        set 
        {
            splashDone = value;
            pendingGame.Remove(ParticleType.happy);
            if (gazeDone && memoryDone && splashDone && stillDone)
            {
                playerToGames.playerToPodium();
                climbIntro.Play();
                centralpiece.SetActive(true);
            }
            else if (splashDone)
            {
                selectGame();
                StartCoroutine(YayPlay());
            }
            
        }
        get { return splashDone; }
    }
    public  IEnumerator waittobusrt()
    {
        yield return new WaitForSeconds(0.5f);
        splitAndPlace.Break();
        yield return new WaitForSeconds(1f);
        playerToGames.playerToAnger();
    }

    IEnumerator YayPlay()
    {
        yield return new WaitForSeconds(1);
        yayFollow.Play();

    }

    public void selectGame()
    {
        int r = Random.Range(0, (pendingGame.Count));

        switch (r)
        {
            case 0: playerToGames.playerToAnger();
                break;

            case 1:
                playerToGames.playerToSad();
                break;

            case 2:
                playerToGames.playerToCalm();
                break;

            case 3:
                playerToGames.playerToHappy();
                break;
        }
    }
}

