using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverRabbit : MonoBehaviour
{
    Animator animator;
    bool right, left;
    Camera followCamera;
    Vector3 direction;
    float speed = 2f;
    float range = 0.5f;
    float x;
    bool punch;
    Vector3 correctedPos;
    void Start()
    {
        animator = GetComponent<Animator>();
        right = false;
        left = false;
        punch = false;
        followCamera = Camera.main;
        direction = followCamera.transform.position - transform.position;
        x = 0;
        correctedPos = transform.position;
        correctedPos.y = 0f;
        transform.position = correctedPos;
        Manager.Instance.SetFeverBar(0);
    }


    void Update()
    {
        if ( !punch )
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            followCamera.transform.position = transform.position + direction;

            if ( Input.GetKeyDown(KeyCode.D) && transform.position.x != range )
            {
                right = true;
                left = false;
                if ( x < range )
                {
                    x += range;
                }
            }

            if ( Input.GetKeyDown(KeyCode.A) && transform.position.x != -range )
            {
                right = false;
                left = true;
                if ( x > -range )
                {
                    x -= range;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if ( right )
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, transform.position.y, transform.position.z), Time.fixedDeltaTime * speed * 2f);

            if ( Mathf.Abs((transform.position.x - x)) <= 0.001f )
            {
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }

        }
        if ( left )
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, transform.position.y, transform.position.z), Time.fixedDeltaTime * speed);

            if ( Mathf.Abs((transform.position.x - x)) <= 0.001f )
            {
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Obstacle") )
        {
            //if ( !punch )
            //{
            //    punch = true;
            //    animator.SetBool("punch", punch);
            //}
            other.GetComponent<Rigidbody>().AddForce(new Vector3(x * 7f + 2f, 3f, 0f), ForceMode.Impulse);
            Destroy(other.gameObject, 1.5f);
            //punch = false;
            //animator.SetBool("punch", punch);
        }
        if ( other.CompareTag("Carrot") ) 
        {
            Manager.Instance.carrotCount++;
            Destroy(other.gameObject);
        }
    }
}
