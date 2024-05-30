using System.Collections;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject open;
    public Transform pivot;
    public Vector3 rotation;
    public float targetAngle = 20f;  // Target angle in degrees
    public float rotationSpeed = 1f; // Degrees per frame
    public float delay = 1f; // Delay before opening the door

    void Start()
    {
        StartCoroutine(Open());
    }

    IEnumerator Open()
    {
        float currentAngle = 0f;  
        yield return new WaitForSeconds(delay);

        while (currentAngle < targetAngle)
        {
            // Calculate rotation amount for this frame
            float rotationAmount = Mathf.Min(rotationSpeed, targetAngle - currentAngle); 

            open.transform.RotateAround(pivot.position, rotation, rotationAmount);
            currentAngle += rotationAmount;

            yield return null; // Wait for the next frame
        }
    }
}