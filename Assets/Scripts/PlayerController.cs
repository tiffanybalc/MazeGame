using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //Outlets
    Rigidbody rb;
    //public Image imageHealthBar;
    public Animator animator;

    //Configuration
    public float speed;
    public float jumpHeight;
    public float groundDistance;
    public float dashDistance;
    public LayerMask ground;
    public float runSpeed;
    public float rotateSpeed;

    public float gravity = 20.0f;

    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    // Methods
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);

        //canFight = instance.canFight;
        //add money to player prefs
        animator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        if (GameController.instance.currentPerspective != GameController.Perspective.FPS)
        {
            rb.MovePosition(rb.position + _inputs * speed * Time.fixedDeltaTime);
            //if (grounded)
            //{
            //    // We are grounded, so recalculate movedirection directly from axes
            //    moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

            //    moveDirection = transform.TransformDirection(moveDirection);

            //    if (Input.GetButton("Jump"))
            //    {
            //        moveDirection *= runSpeed;
            //    }
            //    else
            //    {
            //        moveDirection *= speed;
            //    }
            //}

            ////Apply gravity
            //moveDirection.y -= gravity * Time.deltaTime;

            ////Move the controller
            //CharacterController controller = GetComponent<CharacterController>();

            //CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);


            //transform.Rotate(0, rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0);

            //grounded = (flags & CollisionFlags.CollidedBelow) != 0;


            //float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");

            //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            //rb.AddForce(movement * speed);
        }



        






    }

    void Update()
    {
        if (GameController.instance.isPaused)
        {
            return;
        }

        if (GameController.instance.currentPerspective != GameController.Perspective.FPS)
        {
 

            if (Input.GetKey(KeyCode.T))
            {
                if (GameController.instance.canFight == true)
                {
                    Debug.Log("canFight is true and T entered");
                    GameController.instance.boxing = true;
                    if (animator)
                    {
                        animator.SetBool("isBoxing", true);
                        Debug.Log("currently boxing");

                    }
                }
            }

            else
            {
                GameController.instance.boxing = false;
                _isGrounded = Physics.CheckSphere(_groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
                _inputs = Vector3.zero;
                _inputs.x = Input.GetAxis("Horizontal");
                _inputs.z = Input.GetAxis("Vertical");
                if (_inputs != Vector3.zero)
                {
                    transform.forward = _inputs;
                }
                if (Input.GetKey(KeyCode.Space) && _isGrounded)
                {
                    rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                }
                if (Input.GetKey(KeyCode.B))
                { //Dash
                    Vector3 dashVelocity = Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * rb.drag + 1)) / -Time.deltaTime)));
                    rb.AddForce(dashVelocity, ForceMode.VelocityChange);
                }
                if (animator)
                {
                    animator.SetBool("isBoxing", false);
                    //Debug.Log("No longer boxing");
                    animator.SetFloat("Speed", rb.velocity.magnitude);
                    if (rb.velocity.magnitude > 0)
                    {
                        animator.speed = rb.velocity.magnitude / 3f;
                    }
                    else
                    {
                        animator.speed = 1f;
                    }
                }

            }
        }
       
        if (Input.GetKeyDown(KeyCode.E)) {

            float dist = 1.1f;
            Ray ray;
            RaycastHit hit;
            if (GameController.instance.currentPerspective == GameController.Perspective.FPS)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            }
            else {
                Vector3 direction = new Vector3(this.transform.localPosition.x + 1, this.transform.position.y, this.transform.localPosition.z + 1);
                ray = new Ray(this.transform.position, direction);
                dist = 4.5f;
            }
                if (Physics.Raycast(ray, out hit, dist))
                { 
                    //Handle First Person Interactions Here
                    //print("Interacted with " + hit.transform.name + " from " + hit.distance + "m.");
                DogController dog = hit.transform.GetComponent<DogController>();
                    if (dog)
                    {
                    Debug.Log("raycasted to dog");
                    GameController.instance.updateText.text = "You have found a new friend. He will help distract the robot so you can find your destination.";
                    DogController.instance.isTarget = true;
                    //DisablePlayerControl();
                        //dog.Interact(EnablePlayerControl);
                    }
                else if (hit.transform.GetComponent<Enemy>())
                {
                    Debug.Log("raycasted to enemy");
                    GameController.instance.updateText.text = "This is your enemy";
                }

            }

        }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //DialogueManager.instance.EndConversation();
                //EnablePlayerControl();
            }
       
        //else {
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        //        Vector3 direction = new Vector3(this.transform.localPosition.x + 1, this.transform.position.y, this.transform.localPosition.z + 1);
        //        Ray ray = new Ray(this.transform.position, direction);
        //        RaycastHit hit;
        //        if (Physics.Raycast(ray, out hit, 4.5f))
        //        { //was 1.1f
        //            //Handle First Person Interactions Here
        //            print("Interacted with " + hit.transform.name + " from " + hit.distance + "m.");
        //            DogController dog = hit.transform.GetComponent<DogController>();
        //            if (dog)
        //            {
        //                Debug.Log("raycasted to dog");
        //                GameController.instance.updateText.text = "You have found a new friend.";
        //                DogController.instance.isTarget = true;
        //                //DisablePlayerControl();
        //                //dog.Interact(EnablePlayerControl);
        //            }
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        //DialogueManager.instance.EndConversation();
        //        //EnablePlayerControl();
        //    }
        //}
    }

    public void TakeDamage(float damageAmount)
    {
        GameController.instance.health -= damageAmount;
        if (GameController.instance.health <= 0) {
            Die();
        }
        //imageHealthBar.fillAmount = GameController.instance.health / GameController.instance.healthMax;
    }

    void Die() {
        GameController.instance.updateText.text = "You have died";
        Debug.Log("Dead");
        GameController.instance.money = 0;
        PlayerPrefs.SetInt("Money", GameController.instance.money);
        GameController.instance.score = 0;
        PlayerPrefs.SetInt("Score", GameController.instance.score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>()) {
            TakeDamage(10f);
        }
        //Debug.Log("Collision");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            TakeDamage(10f);
        }
        Debug.Log("Trigger");
    }

    public void Win() {
        GameController.instance.updateText.text = "You have won this level!";
        Debug.Log("you have won");
        MenuController.instance.SaveHighScore();
        GameController.instance.money = 0;
        PlayerPrefs.SetInt("Money", GameController.instance.money);
        GameController.instance.score = 0;
        PlayerPrefs.SetInt("Score", GameController.instance.score);

        float curr = Time.deltaTime;
        
       
        if (SceneManager.GetActiveScene().name == "SampleScene") {
            SceneManager.LoadScene("Level2");
        }
        else if (SceneManager.GetActiveScene().name == "Level2") {
            
            SceneManager.LoadScene("Level2");
        }
        else {
            Die();
        }

    }

}
