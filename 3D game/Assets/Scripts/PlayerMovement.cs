using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject prefab;
    public float sensitivity = 250f;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 10f;
    
    private float mouseX = 0;
    private float mouseY = 0;
    GameObject playerCamera;
    Transform feet;
    Rigidbody rb;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] AudioSource jumpSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GameObject.Find("Main Camera");
        feet = transform.Find("Feet");
        Debug.Log(feet);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(prefab);
        }
        float moveX = 0;
        float moveY = 0;
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1;
        }
        
        float yaw = playerCamera.transform.eulerAngles.y;
        Vector3 forward = Quaternion.Euler(0, yaw, 0) * Vector3.forward;
        Vector3 right = Quaternion.Euler(0, yaw, 0) * Vector3.right;
        
        // Move!
        Vector3 moveDirection = (forward * moveY + right * moveX) * moveSpeed;
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);

        if (Input.GetKeyDown(KeyCode.Space) && Physics.CheckSphere(feet.position, 0.2f, groundLayer))
        {
            Jump(true);
        }

        mouseX += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        mouseY = Math.Clamp(mouseY, -80, 80);
        
        playerCamera.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }

    void Jump(bool jump)
    {
        if (jump == true)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z);
            jumpSound.Play();
        } else
        {
         rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower/2, rb.linearVelocity.z);   
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump(false);
        }
    }
}
