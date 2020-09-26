using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Destroys platforms that are x distance away from the player
 * while also having already been stepped on by the player
 */
public class PlatformDestroyer : MonoBehaviour
{
    [SerializeField] private float distanceThreshold = 100;
    private GameObject player;
    private bool isStepped = false;

    //Find the player opbject 
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // if this platform has been stepped on and is far enough away
    // destroy it
    void Update()
    {
        if (isStepped && Vector3.Distance(player.transform.position, transform.position) > distanceThreshold)
            Destroy(gameObject);
    }

    // when player steps on platform set isStepped to be true
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            isStepped = true;
        }
    }
}
