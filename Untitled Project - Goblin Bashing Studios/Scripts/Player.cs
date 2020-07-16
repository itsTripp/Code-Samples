using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrounded;

    [SerializeField] float MoveSpeed = 1.0f;

    Rigidbody rigidbody;

    [SerializeField] float Health = 100.0f;

    float rotateX;

    float rotateY;

    [SerializeField] float lookSens = 5.0f;

    [SerializeField] float jumpForce = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0.1f;
        rigidbody = GetComponent<Rigidbody>();
        
    }

    Vector3 GetMovingPlayerVelocity()
    {
        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            velocity += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            velocity += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            velocity += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            velocity += Vector3.right;
        }

        return velocity * MoveSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
   
        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidbody.AddForce(new Vector3(0, 10, 0) * jumpForce);
            isGrounded = false;
        }


        Vector3 velocity = GetMovingPlayerVelocity();

        rotateX -= Input.GetAxis("Mouse Y") * lookSens;

        rotateY += Input.GetAxis("Mouse X") * lookSens;

        rotateX = Mathf.Clamp(rotateX, -80, 100);

        transform.rotation = Quaternion.Euler(rotateX, rotateY, 0);

        rigidbody.velocity = transform.TransformDirection(velocity);

    }

    public void TakeDamage()
    {
        Health -= 10.0f;
    }

    public void OnCollisionStay()
    {
        isGrounded = true;
    }
}
