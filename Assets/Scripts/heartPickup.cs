using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 30) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>()) {
            float addHealth = GameController.instance.healthMax / 3;
            GameController.instance.health += addHealth;
            if (GameController.instance.health > GameController.instance.healthMax) {
                GameController.instance.health = GameController.instance.healthMax;
            }
            PlayerPrefs.SetFloat("Health", GameController.instance.health);
            Destroy(gameObject);
        }
    }
}
