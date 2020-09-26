using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The ghost enemey will move 1.5 times the speed of the player
 * For now it goes up and down the platform along the z axis
 * 
 * 1.5 is hard set and so is the platform boundary
 * if platform size changes create a variable to change the boundary
 */

public class EnemyController : MonoBehaviour
{
    private GameControlScript gmCtrl;
   
    //variables for position and speed
    public float platformZ; // set to public so create platform can change it

    private float enemyZ;
    private float direction = -1;
    private float enemySpeed;
    
    
    void Start()
    {
        if (!gmCtrl)
            gmCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlScript>();

        enemySpeed = gmCtrl.speed; //setting it here so that if player gets a power down to speed it doesn't speed down the enemies

    }

    
    void Update()
    {
        //get enemy position
        enemyZ = transform.position.z;

        //if direction is going positive and it's below the platform's boundary
        if (direction > 0 && enemyZ < platformZ + 20)
        {
            transform.position += new Vector3(0, 0, direction) * Time.deltaTime * (enemySpeed * 1.5f);
        }
        // else if it's at the boundary go the other direction
        else if (direction > 0)
        {
            direction *= -1;
            transform.position += new Vector3(0, 0, direction) * Time.deltaTime * (enemySpeed * 1.5f);
        }
        //direction is negative and checks the lower bound
        else if (enemyZ > platformZ - 20)
        {
            transform.position += new Vector3(0, 0, direction) * Time.deltaTime * (enemySpeed * 1.5f);
        }
        // lower bound reached and time to go back up
        else
        {
            direction *= -1;
            transform.position += new Vector3(0, 0, direction) * Time.deltaTime * (enemySpeed * 1.5f);
        }
    }

    //every time it collides with an object it goes a different direction
    private void OnCollisionEnter(Collision collision)
    {
        direction *= -1;
    }
}
