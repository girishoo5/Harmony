using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System.Collections.Generic;

public class StillInteraction : MonoBehaviour
{
    public Slider uiLoader;
    public AudioSource tadah;
    public float movementThreshold = 0.1f;
    public float fillSpeed = 0.1f;
    public bool notDone = true;
    public float loaderValue = 0f;
    private Vector3 lastPosition;
    public float stillDistanceBoundary;
    public GameObject player;
    public float distanceToPlayer;
    public GameManager GM;
    public AudioSource noise;
    public AudioSource areabgm;

    public Transform right;
    public float maxDelta = 0;
    public Transform halo;

    public AudioSource calmbg;
    void Start()
    {
        if (uiLoader != null)
        {
            uiLoader.maxValue = 100;
            uiLoader.value = loaderValue;
        }
        lastPosition = GetControllerPosition();
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);
        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }
    }

    void Update()
    {
        Vector3 currentPosition = GetControllerPosition();
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(distanceToPlayer);
       
        if (distanceToPlayer < stillDistanceBoundary)
        {
            //Debug.Log("activate still");
            float movementDelta = Mathf.FloatToHalf(Vector3.Distance(currentPosition, lastPosition));
            if (movementDelta > maxDelta)
            {
                maxDelta = movementDelta;
            }
            if (notDone)
            {
                Debug.Log("current position" + currentPosition +"last position"+ lastPosition +"Movement Delta"+ movementDelta);
                if (movementDelta > -movementThreshold && movementDelta < movementThreshold)
                {
                    loaderValue = fillSpeed * Time.deltaTime + loaderValue;
                    UpdateLoader();
                }
                else
                {
                    // Reset loader if there's significant movement
                    ResetLoader();
                }
            }
            lastPosition = currentPosition;
        }
    }

  
    Vector3 GetControllerPosition()
    {
        return right.position;
        //InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand); // Adjust for left hand if needed
            /*if (device.isValid)
            {
                if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position))
                {
                    return position;
                }
                else
                {
                    Debug.LogWarning("Failed to get device position");
                }
            }
            else
            {
                Debug.LogWarning("Device not valid");
            }

            return Vector3.zero;*/

    }
    void UpdateLoader()
    {
        // Update the UI loader or progress bar based on loaderValue
        // You may use a UI slider or other UI element to represent the loader
        if (uiLoader != null)
        {
            // Normalize loaderValue to the range [0, 1]
            float normalizedValue = loaderValue / 100f;

            // Set the scale of the halo based on the normalized loaderValue
            halo.transform.localScale = normalizedValue * new Vector3 (2,2,2);

            if (loaderValue > 88)
            {
                uiLoader.value = 100;
                CompleteLoading();
            }

            noise.volume = (loaderValue / 100)/2;
            calmbg.volume = (1 - loaderValue / 100)/2;
        }
    }
    
    public void resetGame()
    {
        ResetLoader();
        notDone = true;
    }

    void ResetLoader()
    {
        loaderValue = 0f;
        UpdateLoader();
    }

    void CompleteLoading()
    {
        areabgm.Stop();
        noise.Stop();
        tadah.Play();
        GM.StillDone = true;
        notDone = false;    
    }
}
