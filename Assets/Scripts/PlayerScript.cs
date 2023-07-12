using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10f;
    public float mouseSensitivity = 1f;
    public float jumpHeight = 1f;
    float yRotation;
    public bool grounded;
    bool hitSpaceBar, hitShift;
    public int hitCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            grounded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hitSpaceBar = true;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            hitShift = true;
        }
    }
    private void FixedUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * 100f;
        yRotation += mouseX * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        if (grounded)
        {
            Vector3 moveVector = transform.forward * speed * Input.GetAxis("Vertical") * Time.deltaTime + transform.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime;
            rb.velocity *= 0.90f;
            if (hitShift)
            {
                moveVector *= 1.5f;
            }
            rb.AddForce(moveVector * 100f);
            if (hitSpaceBar)
            {
                rb.AddForce(0,jumpHeight,0);
            }
        }
        grounded = false;
        hitSpaceBar = false;
        hitShift = false;
    }
}
