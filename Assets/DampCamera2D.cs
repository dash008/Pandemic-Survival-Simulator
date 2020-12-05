// Smooth towards the target

using UnityEngine;
using System.Collections;

public class DampCamera2D : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    Quaternion initialRotation;
    private void Start()
    {
        initialRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    void Update()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = new Vector3(target.position.x-8,target.position.y+5,target.position.z-8);

        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, target.eulerAngles.y, transform.rotation.eulerAngles.z);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        //transform.LookAt(target);
        
    }
}