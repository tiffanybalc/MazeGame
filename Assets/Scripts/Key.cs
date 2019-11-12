using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
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
            GameController.instance.updateText.text = "You have obtained the key!";
            Debug.Log("Reached Key");
            GameController.instance.obtainedKey = true;
            //PlayerController.instance.score++;
            //PlayerPrefs.SetInt("Score", PlayerController.instance.score);
            Destroy(gameObject);
        }
    }
}
