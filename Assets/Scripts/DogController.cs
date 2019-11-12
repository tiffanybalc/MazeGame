using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{

    public static DogController instance;

    public bool isTarget;

    Rigidbody rigidbody;

    Animator animator;
    


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
        if(rigidbody.velocity.magnitude > 0) {
            animator.speed = rigidbody.velocity.magnitude / 3f;
        }
        else {
            animator.speed = 1f;
        }
    }


}
