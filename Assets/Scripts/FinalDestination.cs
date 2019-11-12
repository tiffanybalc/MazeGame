using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDestination : MonoBehaviour
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
            GameController.instance.updateText.text = "You have reached the end of the maze. You must open me up.";
            Debug.Log("Reached Final Destination");
            GameController.instance.updateFinalDest();
            //PlayerController.instance.score++;
            //PlayerPrefs.SetInt("Score", PlayerController.instance.score);
        }
    }
}
