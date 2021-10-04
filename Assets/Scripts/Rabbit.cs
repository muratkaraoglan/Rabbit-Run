using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public ParticleSystem soilParticle;

    Animator animator;

    bool right, left;
    bool isDeath;
    Camera followCamera;
    Vector3 direction;
    float speed = 2f;
    float range = 0.5f;
    float x;
    int count;
    void Start()
    {
        animator = GetComponent<Animator>();
        right = false;
        left = false;
        isDeath = false;
        followCamera = Camera.main;
        direction = followCamera.transform.position - transform.position;
        x = 0;
    }


    void Update()
    {
        if ( !isDeath )
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

                //transform.position = new Vector3(x, transform.position.y, transform.position.z);

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

            if ( Input.GetKey(KeyCode.S) )
            {
                transform.position = new Vector3(transform.position.x, -0.2f, transform.position.z);
                animator.SetBool("isUnderGround", true);

                if ( !soilParticle.isPlaying )
                {
                    soilParticle.Play();
                }
                soilParticle.transform.position = new Vector3(transform.position.x - 0.09f, -0.3f, transform.position.z - 0.05f);
            }
            else
            {
                animator.SetBool("isUnderGround", false);
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
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
            Debug.Log(other.gameObject.name);
          
            isDeath = true;
            animator.SetBool("isDeath", isDeath);
            Manager.Instance.Retry();

        }
        if ( other.CompareTag("Carrot") )
        {
            count++;

            Manager.Instance.SetFeverBar(count);
            if ( count % 5 == 0 )
            {
                count = 0;
            }
            Destroy(other.gameObject);
        }
        Debug.Log(other.name);
    }
}
