using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            GameController.instance.health = GameController.instance.healthMax;
            PlayerPrefs.SetFloat("Health", GameController.instance.health);
            Destroy(gameObject);
        }
    }
}
