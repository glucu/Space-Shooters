using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject enemyExplosion;
    public GameObject playerExplosion;

    public PlayerHealth playerHealth;

    public int scoreValue;

    private GameController gameController;



    private void Start()
    {
       
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Name of the given object that collided with the enemy
        var name = other.tag;

        // if the name of the tag is the same as to Boundary tag
        // return and do nothing
        if (name == "Boundary" || name == "Enemy")
        {
            return;
        }

        if(enemyExplosion != null)
        {
            Instantiate(enemyExplosion, this.transform.position, this.transform.rotation);
        }

        if (name == "Player")
        {
            
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

            gameController.GameOver();
            
        }

        gameController.IncrementScore(scoreValue);

        Destroy(other.gameObject);
        Destroy(this.gameObject);

    }
}
