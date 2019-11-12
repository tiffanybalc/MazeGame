using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
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
        if(collision.gameObject.GetComponent<PlayerController>()) {
            GameController.instance.EarnPoints(1);
            //PlayerController.instance.score++;
            //PlayerPrefs.SetInt("Score", PlayerController.instance.score);
            Destroy(gameObject);
        }
    }
}
