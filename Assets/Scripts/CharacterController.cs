using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script controls the player's movement
 * as well as the interactions with the game world
 * 
 * interactions include: leaving the platform, falling to their death,
 * and various game objects: walls, balls, enemies, power ups/downs
 */
public class CharacterController : MonoBehaviour
{
    [SerializeField] private bool isGrounded;
    private GameControlScript gmCtrl;

    private void Start()
    {
        if (!gmCtrl)
            gmCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlScript>();
    }

    // pushes the player forward automatically
    // but also checks if the user is pushing a key to either
    // change position or bring up the menu
    private void Update()
    {
        if (!gmCtrl.isDead)
        {
            //moves the player forward as well as any horizontal direction if the user pushes a button mapped to it
            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 1) * Time.deltaTime * gmCtrl.speed;

            //if user pushes a button to jump jump and tell the scripts the player isn't grounded so that they can't jump again until they land
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * gmCtrl.jmpIntensity, ForceMode.Impulse);
                isGrounded = false;
            }
            // if player pushes down and is in the air fall fast back to the ground
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W)) && !isGrounded)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * gmCtrl.jmpIntensity * 2, ForceMode.Impulse);
            }

            // if player pushes z bring up the pause menu and pause the game
            // or unpause if already in the menu
            if (Input.GetKeyDown(KeyCode.Z))
                if (!gmCtrl.isPaused)
                    gmCtrl.PauseGame();
                else
                    gmCtrl.ResumeGame();
        }
    }
    
    //if player hits an object that has collision
    private void OnCollisionEnter(Collision collision)
    {
        //if it's the ground tell the script it's on the ground
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        //if it's a wal, take off health
        if (collision.gameObject.tag == "Wall")
        {
            gmCtrl.health -= 10;
            gmCtrl.healthText.text = gmCtrl.health.ToString();
        }
        // if an enemy insta death
        if (collision.gameObject.tag == "Enemy")
        {
            gmCtrl.setGameOver();
        }
    }

    // when player leaves the ground (primarily for falling off the platform)
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }      
        
    }

    //if player passed through an object with a trigger
    private void OnTriggerEnter(Collider other)
    {
        //if they've made contact with death platform game over
        if (other.gameObject.tag == "Death")
        {
            gmCtrl.setGameOver();
        }
        
        // else these objects need to be destroyed 

        //balls give points and should reflect on the screen
        if (other.gameObject.tag == "Ball")
        {
            Destroy(other.gameObject);
            gmCtrl.points += 10;
            gmCtrl.pointsText.text = "Points: " + gmCtrl.points.ToString();
            
            //they also give health for every 50
            if (gmCtrl.points % 50 == 0)
            {
                gmCtrl.health += 10;
                gmCtrl.healthText.text = gmCtrl.health.ToString();
            }
        }

        // power ups stop all enemies movement for that level
        if(other.gameObject.tag == "PowerUp" && gmCtrl.GetComponent<CreatePlatforms>().isPowerUpUsed == false)
        {
            gmCtrl.GetComponent<CreatePlatforms>().isPowerUpUsed = true; 
            Destroy(other.gameObject);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<EnemyController>().enabled = false;
            }
        }

        //power downs slows the player's speed for that level
        if(other.gameObject.tag == "PowerDown" && gmCtrl.GetComponent<CreatePlatforms>().isPowerDownUsed == false)
        {
            gmCtrl.GetComponent<CreatePlatforms>().isPowerDownUsed = true;
            gmCtrl.speed -= 3;
        }
    }

}
