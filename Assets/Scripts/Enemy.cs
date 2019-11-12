using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;

    //Outlets
    NavMeshAgent navAgent;

    //State Tracking 
    //public Transform targetOverview;
    //public Transform targetFPS;
    //public Transform targetThirdPerson;
    private Transform currentTarget;
    public float health = 50f;

    //Methods
    void Start()
    {
        instance = this;
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if(DogController.instance.isTarget) {
            currentTarget = DogController.instance.transform;
        }

        else if(GameController.instance.currentPerspective == GameController.Perspective.FPS) {
            currentTarget = GameController.instance.characterFPS.transform;
        }
        else if (GameController.instance.currentPerspective == GameController.Perspective.Overview)
        {
            currentTarget = GameController.instance.characterOverview.transform;
        }
        else if (GameController.instance.currentPerspective == GameController.Perspective.ThirdPerson) {
            currentTarget = GameController.instance.character.transform;
        }

        if (currentTarget) {
            navAgent.destination = currentTarget.position;
        }
        /*
        else if(BaseController.instance) {
            target = BaseController.instance.transform;
        }
        */
    }

    void OnEnable()
    {
        //GameController.instance.enemies.Add(this);
    }

    void OnDisable()
    {
        GameController.instance.enemies.Remove(this);
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if(health <= 0) {
            Debug.Log("The enemy has died");
            GameController.instance.updateText.text = "The enemy has died";
            GameController.instance.numberOfEnemies--;
            OnDisable();
            Destroy(gameObject);

        }
    }

    void OnCollisionEnter(Collision other) //was onTriggerEnter
    {

        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if(player) {
            if (GameController.instance.boxing == true) {
                GameController.instance.EarnPoints(20);
                GameController.instance.updateText.text = "The enemy has suffered!";
                Debug.Log("You have given damage to the enemy");
                 if (GameController.instance.numberOfEnemies == 0)
                {
                    GameController.instance.updateText.text = "All enemies have been defeated!";
                }
                TakeDamage(20f);

            }
            else {
                GameController.instance.updateText.text = "Fight back using the T key.";
                player.TakeDamage(10f);
            }

        }


    }


}
