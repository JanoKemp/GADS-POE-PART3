using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera camera;
    public Rigidbody rb;
    public Transform head;

    public float speed = 20f;
    public float jump = 5f;
    public float lookSpeed = 2f;
    public float lookXLim = 45f;
    public float gravity = 20f;

    Vector3 movement;
    bool isGrounded = false;
    bool isJumping = false;
    public AudioSource walking;

    // Start is called before the first frame update
    void Start()
    {
       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);



        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if(!walking.isPlaying)
            {
                walking.Play();
            }
        }
        else
        {
            walking.Stop();
        }
        // Calculate movement direction based on input
         movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        // Move the player based on the calculated movement
        rb.MovePosition(rb.position + transform.TransformDirection(movement));

        if (isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
                isJumping = true;
            }
        }
       
        
    }

     void LateUpdate()
    {
        Vector3 vert = head.eulerAngles;
        vert.x -= Input.GetAxis("Mouse Y") * 2f;
        vert.x = RestrictionAngle(vert.x, -85f, 85f);
        head.eulerAngles = vert;

    }

    public static float RestrictionAngle(float angle, float min, float max)
    {
        if (angle > 180)
        {
            angle -= 360;
        }else if(angle < -180)
        {
            angle += 360;
        }
         if (angle>max)
        {
            angle = max;    
        }
         if(angle < min) 
        {
            angle = min;
        }

         return angle;
    }
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
        isJumping = false;
        Debug.Log("isgrounded");
    }
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
