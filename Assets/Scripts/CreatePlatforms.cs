using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script should probably have been broken up better.
 * This will create a platform, set it's location in the game,
 * as well each fill each platform that it creates with potential
 * enemies, walls, balls, and power ups/downs
 * 
 * Since many objects are of difference sizes they need different thershholds to generate to on the platform
 */

public class CreatePlatforms : MonoBehaviour
{
    // game objects to initialize via the editor
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject deathPlatform;
    [SerializeField] private GameObject powerUp;
    [SerializeField] private GameObject powerDown;

    
    //details for Platforms, walls, enemies, balls
    [SerializeField] private float distanceThreshold = 100;
    [SerializeField] private float platX = 7f;
    [SerializeField] private float platY = 2f;
    [SerializeField] private float platZ = 55f;
    [SerializeField] private float enemyX = 3f;
    [SerializeField] private float enemyZ = 23f;
    [SerializeField] private float wallX = 2.5f;
    [SerializeField] private float wallZ = 22f;
    [SerializeField] private float ballX = 4.5f;
    [SerializeField] private float ballZ = 24.5f;
    [SerializeField] private int numBalls = 5;

    //objects to find via tag
    private GameObject player;
    private GameControlScript gmCtrl;

    //positions in the game
    private Vector3 nextPlatformPos = Vector3.zero;
    private Vector3 enemyPosition;
    private Vector3 wallPosition;
    private Vector3 ballPosition;
    private Vector3 powUpPosition;
    private Vector3 powDownPosition;

    //public variables
    public bool isPowerUpUsed = false;
    public bool isPowerDownUsed = false;


    private void Awake()
    {
        //get the components from the game controller script
        if (!gmCtrl)
            gmCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlScript>();

        //find the player
        player = GameObject.FindGameObjectWithTag("Player");

        //Create a platform at 0,0,0 and a death platform right under it
        platform = Instantiate(platform, nextPlatformPos, Quaternion.identity);
        deathPlatform = Instantiate(deathPlatform, nextPlatformPos + new Vector3(0, -5, 0), Quaternion.identity);

        //Fill platform with things
        fillPlatform();

        //next platform at a random x and y position, they'll always be the same distance apart though
        nextPlatformPos += new Vector3(Random.Range(-platX, platX), Random.Range(-platY, platY), platZ);
    }

    private void Update()
    {
        // if distance from players location to the next platform distance is less than the threshold
        if (Vector3.Distance(player.transform.position, nextPlatformPos) < distanceThreshold)
        {
            // create a new platform at that location and do all the things that are tied to it
            platform = Instantiate(platform, nextPlatformPos, Quaternion.identity);
            deathPlatform = Instantiate(deathPlatform, nextPlatformPos + new Vector3(0, -5, 0), Quaternion.identity);
           
            fillPlatform();

            nextPlatformPos += new Vector3(Random.Range(-platX, platX), Random.Range(-platY, platY), platZ);
        }
    }

    // fill the platform with enemies, walls, balls, and power ups/downs
    private void fillPlatform()
    {

        //walls
        for (int i = 0; i < gmCtrl.numWalls; i++)
        {
            wallPosition = nextPlatformPos + new Vector3(Random.Range(-wallX, wallX), 1.5f, Random.Range(-wallZ, wallZ));
            wall = Instantiate(wall, wallPosition, Quaternion.identity);
        }

        //enemies
        for (int i = 0; i < gmCtrl.numEnemies; i++)
        {
            enemyPosition = nextPlatformPos + new Vector3(Random.Range(-enemyX, enemyX), 2f, Random.Range(-enemyZ, enemyZ));
            enemy = Instantiate(enemy, enemyPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().platformZ = nextPlatformPos.z;
        }

        // balls will always have a set number, it won't change with the levels
        //balls
        for (int i = 0; i < numBalls; i++)
        {
            ballPosition = nextPlatformPos + new Vector3(Random.Range(-ballX, ballX), 1f, Random.Range(-ballZ, ballZ));
            ball = Instantiate(ball, ballPosition, Quaternion.identity);
        }

        //power ups/downs can only be activated once each per level and are randomly generated
        // Since I made these in probuilder they need a rotation set because their identity one isn't a good view
        //TODO add points to getting a power up, subtrack points to getting a power down

        //powerup
        if(Random.Range(0,100) >= 75 && !isPowerUpUsed)
        {
            powUpPosition = nextPlatformPos + new Vector3(Random.Range(-enemyX, enemyX), 1.75f, Random.Range(-enemyZ, enemyZ));
            powerUp = Instantiate(powerUp, powUpPosition, Quaternion.Euler(new Vector3(20, 20, -222)));
   
        }

        //powerdown
        if (Random.Range(0, 100) <= 25 && !isPowerDownUsed)
        {
            powDownPosition = nextPlatformPos + new Vector3(Random.Range(-enemyX, enemyX), 1.25f, Random.Range(-enemyZ, enemyZ));
            powerDown = Instantiate(powerDown, powDownPosition, Quaternion.Euler(new Vector3(-42, 29, 0)));
        }

    }

}
