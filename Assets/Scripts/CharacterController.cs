using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpIntensity = 5f;
    [SerializeField] private bool isGrounded;
    //[SerializeField] private float verAxis = 0f;


    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 1) * Time.deltaTime * speed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpIntensity, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
