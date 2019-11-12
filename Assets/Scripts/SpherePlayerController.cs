using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpherePlayerController : MonoBehaviour
{

    public static SpherePlayerController instance;

    Rigidbody rb;

    public float speed;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isPaused)
        {
            return;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        GameController.instance.health -= damageAmount;
        if (GameController.instance.health <= 0)
        {
            Die();
        }
        //imageHealthBar.fillAmount = GameController.instance.health / GameController.instance.healthMax;
    }

    void Die()
    {
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
        if (collision.gameObject.GetComponent<Enemy>())
        {
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

    public void Win()
    {
        GameController.instance.updateText.text = "You have won this level!";
        Debug.Log("you have won");
        MenuController.instance.SaveHighScore();
        GameController.instance.money = 0;
        PlayerPrefs.SetInt("Money", GameController.instance.money);
        GameController.instance.score = 0;
        PlayerPrefs.SetInt("Score", GameController.instance.score);

        float curr = Time.deltaTime;


        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {

            SceneManager.LoadScene("Level2");
        }
        else
        {
            Die();
        }

    }
}
