using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Destroys objects that don't have gravity to touch the death platform
 * They have to be x amount of distance away from the player
 * this threshold must be larger than the distance to create platforms
 * otherwise they'll dissapear before the player gets to them
 */
public class DestroyNoGravityObjects : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float distanceThreshold = 160;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if ( Vector3.Distance(player.transform.position, transform.position) > distanceThreshold)
            Destroy(gameObject);
    
    }
}
