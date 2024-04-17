using UnityEngine;
using UnityEngine.UI;

public class GazeInteraction : MonoBehaviour
{
    public GameObject[] gazeObjects;
    public GameObject Blastable;
    public Slider uiLoader;
    public GameManager GM;
    public float loadingTime = 5f;
    public AudioSource ANGRY;
    public AudioSource blast;
    private float[] gazeTime = new float[3];
    private bool isLoading = false;
    public bool[] done = new bool[3];
    public GameObject currentObject = null;
    public int currentIndex = 0;
    public float stillDistanceBoundary;
    public GameObject player;
    public float distanceToPlayer;
    public float vibrationValue;
    public float shakeSpeed = 1f;
    public Vector3 originalPosition1;
    public Vector3 originalPosition2;
    public Vector3 originalPosition3;

    // shake magnitude
    public float shakeMagnitude = 0.1f;
    public void Start()
    {
        done[0] = false;
        done[1] = false;
        done[2] = false;
        gazeTime[0] = 0f;
        gazeTime[1] = 0f;
        gazeTime[2] = 0f;
        originalPosition1 = gazeObjects[0].transform.position;
        originalPosition2 = gazeObjects[1].transform.position;
        originalPosition3 = gazeObjects[2].transform.position;
        if (uiLoader != null)
        {
            uiLoader.maxValue = loadingTime*3;
            uiLoader.value = gazeTime[0]+ gazeTime[1]+ gazeTime[2];
        }
    }
    void Update()
    {
        uiLoader.value = gazeTime[0] + gazeTime[1] + gazeTime[2]; 
        RaycastHit hit;
        Vector3 rayDirection = Camera.main.transform.forward;
        Vector3 headsetCenter = Camera.main.transform.position;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < stillDistanceBoundary)
        {
            if (Physics.Raycast(headsetCenter, rayDirection, out hit))
            {
                int collidedObjIndex = System.Array.IndexOf(gazeObjects, hit.collider.gameObject);
                if (collidedObjIndex == -1)
                    return;

                currentIndex = collidedObjIndex;
                Debug.Log(collidedObjIndex);
                currentObject = gazeObjects[collidedObjIndex];
                Vector3 tempOposition;
               
                if (!done[currentIndex])
                {
                    if (hit.collider.gameObject == gazeObjects[currentIndex])
                    {
                        if (!isLoading)
                        {
                            StartLoading();
                        }
                        vibrationValue = gazeTime[currentIndex] * 5 * shakeSpeed;
                        float offset = Mathf.Sin(Time.time * vibrationValue) * shakeMagnitude;
                        gazeTime[currentIndex] += Time.deltaTime;
                        switch (currentIndex)
                        {
                            case 0:
                                Vector3 newPosition1 = originalPosition1 + new Vector3(offset, 0f, 0f);
                                currentObject.transform.position = newPosition1;
                                break;
                            case 1:
                                Vector3 newPosition2 = originalPosition2 + new Vector3(offset, 0f, 0f);
                                currentObject.transform.position = newPosition2;
                                break;
                            case 2:
                                Vector3 newPosition3 = originalPosition3 + new Vector3(offset, 0f, 0f);
                                currentObject.transform.position = newPosition3;
                                break;
                        }
                        if (gazeTime[currentIndex] >= loadingTime)
                        {
                            CompleteLoading(currentIndex);
                        }
                    }
                    else
                    {
                        ResetLoading(currentIndex);
                    }
                }
            }
            else
            {
                if (!done[currentIndex])
                {
                    ResetLoading(currentIndex);
                }
            }
        }
    }


    void StartLoading()
    {
        isLoading = true;
    }

    void ResetLoading(int i)
    {
        isLoading = false;
        gazeTime[i]= 0f;
    }

    public void resetGame()
    {
        done[0] = false;
        done[1] = false;
        done[2] = false;
        gazeTime[0] = 0f;
        gazeTime[0] = 0f;
        gazeTime[0] = 0f;
    }
    void CompleteLoading(int i)
    {
        Destroy(gazeObjects[i]);
        Instantiate(Blastable, gazeObjects[i].transform.position, gazeObjects[i].transform.rotation);
        done[i] = true;
        uiLoader.value = gazeTime[0] + gazeTime[1] + gazeTime[2];
        if(done[0]==true&& done[1] == true && done[2] == true)
        {
            ANGRY.Stop();
            GM.GazeDone = true;
        }
        blast.Play();
    }
}
