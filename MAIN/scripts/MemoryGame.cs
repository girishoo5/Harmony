using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MemoryGame : MonoBehaviour
{
    public GameObject[] cubes; // Assign cubes in the inspector
    public Material normalMat;
    public Material hintMat;
    public Material selectMat;
    public int patternLength = 5;
    public AudioSource tadah;
    public GameManager GM;
    public AudioSource areabgm;
    public AudioSource start;
    private int currentIndex = 0;
    private int[] pattern;
    private bool waitingForInput = false;

    void Start()
    {
        InitializePattern(patternLength);
        
    }
    /*private void Update()
    {
       InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand); // Adjust for left hand if needed
       Debug.Log(device.TryGetFeatureValue(CommonUsages.primaryButton, out bool Abutton));
        if (device.TryGetFeatureValue(CommonUsages.primaryButton,out bool Abuttn))
        {
            Debug.Log("Abutton pressed");
           // ResetGame();
        }
    }*/

    void InitializePattern(int patternLength)
    {
        pattern = new int[patternLength];
        List<int> availableIndices = new List<int>(cubes.Length);
        for (int i = 0; i < cubes.Length; i++)
        {
            availableIndices.Add(i);
        }
        for (int i = 0; i < patternLength; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            pattern[i] = availableIndices[randomIndex];
            availableIndices.RemoveAt(randomIndex);
            //Debug.Log(pattern);
        }
    }

    public IEnumerator ShowPattern()
    {
        start.Play();
        yield return new WaitForSeconds(1f);
        waitingForInput = false;
        foreach (int index in pattern)
        {
            HighlightCube(index);
            yield return new WaitForSeconds(0.75f); // Adjust time between cube highlights
            ClearHighlight();
            yield return new WaitForSeconds(0.5f); // Adjust time between highlights
        }
        waitingForInput = true;
        yield return new WaitForSeconds(0.5f);
        start.Play();
    }

    void HighlightCube(int index)
    {
        // Add highlighting effect (e.g., change color, scale)
        cubes[index].GetComponent<Renderer>().material = hintMat;
    }

    void ClearHighlight()
    {
        // Reset cube colors to default
        foreach (var cube in cubes)
        {
            cube.GetComponent<Renderer>().material = normalMat;
        }
    }

    void CheckUserInput(int tappedIndex)
    {
        if (!GM.MemoryDone)
        {
            if (!waitingForInput)
                return;
            cubes[tappedIndex].GetComponent<Renderer>().material = selectMat; // Highlight the selected cube
            if (tappedIndex == pattern[currentIndex])
            {
                currentIndex++;

                if (currentIndex == pattern.Length)
                {
                    StartCoroutine(WaitAndPlaySuccessSound());
                    Debug.Log("tadah");
                    GM.MemoryDone = true;
                }
            }
            else
            {
                // Handle incorrect input (e.g., play failure sound, reset game)
                Debug.Log("Incorrect pattern! Resetting...");
                ResetGame();
            }
        }
    }

    IEnumerator WaitAndPlaySuccessSound()
    {
        yield return new WaitForSeconds(0.5f);
        PlaySuccessSound();
    }
    public void OnCLick(GameObject clickedObj)
    {
        int cubeIndex = System.Array.IndexOf(cubes,clickedObj);
        if (cubeIndex != -1)
        {
            Debug.Log("Cube hit: " + cubeIndex);
            CheckUserInput(cubeIndex);
        }
    }

    private void PlaySuccessSound()
    {
        areabgm.Stop();
        tadah.Play();
    }

    public void ResetGame()
    {
        currentIndex = 0;
      
        // Reset the material of all cubes
        foreach (var cube in cubes)
        {
            cube.GetComponent<Renderer>().material = normalMat;
        }
        InitializePattern(patternLength);
        StartCoroutine(ShowPattern());
    }

}
