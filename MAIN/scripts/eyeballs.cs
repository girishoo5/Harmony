using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EyeController : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 5f;
    public float eyeSpeed = 2f;

    private Vector3 originalEyePosition;
    private Quaternion originalEyeRotation;

    void Start()
    {
        originalEyePosition = transform.position;
        originalEyeRotation = transform.rotation;
    }

    void Update()
    {
        // calculate the distance between player and the eyeball
        float distanceToTarget = Vector3.Distance(transform.position, player.position);

        // eyeballs start to stare at player
        if (distanceToTarget <= maxDistance)
        {
            // direction of eyeball
            Vector3 lookDirection = (player.position - transform.position).normalized;


            transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * eyeSpeed);
        }
        else
        {
            //if distance is too long, eyeball will go back to original position
            transform.position = originalEyePosition;
            transform.rotation = Quaternion.Lerp(transform.rotation, originalEyeRotation, Time.deltaTime * eyeSpeed);
        }
    }
}