using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    /*[Range(0.01f, 1)]*/
    Rigidbody rb;
    //public GameObject boundary;
    public float speed = 10f;
    public float mouseSensitivity = 1f;
    public float jumpHeight = 1f;
    Vector3 velocity;
    //public Vector3 minimumPosition, maximumPosition;
    //public float sensX = 1;
    //public float sensY = 1;
    float xRotation;
    float yRotation;
    bool grounded;
    bool hitSpaceBar, hitShift, shotBullet;
    public Transform rayCastPointOfOrigin;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0, 0, 0);
        rb = GetComponent<Rigidbody>();
        /*if (boundary == null)
        {
            print("no boundary");
        }*/
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hitSpaceBar = true;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            hitShift = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            shotBullet = true;
        }
        //if boundary != null, limit player to boundary
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -15, 15), transform.position.y, Mathf.Clamp(transform.position.z, -15, 15));
    }
    private void FixedUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        Vector3 moveVector = transform.forward * speed * Input.GetAxis("Vertical") * Time.deltaTime + transform.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        if (grounded)
        {
            rb.velocity *= 0.90f;
            if (hitShift)
            {
                moveVector *= 1.5f;
            }
            rb.AddForce(moveVector * 100f);
            if (hitSpaceBar)
            {
                //transform.position += new Vector3(0, 10, 0);
                rb.AddForce(0,jumpHeight,0);
            }
        }
        Vector3 originX = rayCastPointOfOrigin.position;
        Vector3 directionX = rayCastPointOfOrigin.forward;
        //RaycastHit hitInfo;
        //Physics.Raycast(origin, direction, out hitInfo, 100);

        Debug.DrawRay(originX, directionX * 100, Color.magenta);


        if (shotBullet)
        {
            Vector3 origin = rayCastPointOfOrigin.position;
            Vector3 direction = rayCastPointOfOrigin.forward;
            RaycastHit hitInfo;
            Physics.Raycast(origin, direction, out hitInfo, 100);
            if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Shootable"))
            {
                GameObject myObject = hitInfo.collider.gameObject;
                myObject.GetComponent<Hittable>().TakeHit(direction, 90, hitInfo.point);
                /*MeshRenderer renderer = myObject.GetComponent<MeshRenderer>();
                if (renderer != null && renderer.material != null)
                {
                    renderer.material.color = new Color(Random.value, Random.value, Random.value); // hitTarget.color;
                }*/
                //Destroy(hitInfo.collider.gameObject);
            }
        }
        grounded = false;
        hitSpaceBar = false;
        hitShift = false;
        shotBullet = false;
    }
}
