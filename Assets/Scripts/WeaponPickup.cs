using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
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
            GameController.instance.updateFightAbility();
            GameController.instance.updateText.text = "You have acquired the ability to fight. Use the T button to fight.";
            Debug.Log("You can fight now");
            //PlayerController.instance.score++;
            //PlayerPrefs.SetInt("Score", PlayerController.instance.score);
            Destroy(gameObject);
        }
    }
}
