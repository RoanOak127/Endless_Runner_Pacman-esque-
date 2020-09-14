using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlatforms : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float distanceThreshold = 100;
    private GameObject player;
    private GameObject platform;
    private GameObject gameController;
    private Vector3 nextPlatformPos = Vector3.zero;
    private Vector3 enemyPosition;
    [SerializeField] private float platX = 7f;
    [SerializeField] private float platY = 2f;
    [SerializeField] private float platZ = 55f;
    [SerializeField] private float enemyX = 3f;
    [SerializeField] private float enemyZ = 20f;

    private void Awake()
    {

        platform = Instantiate(platformPrefab, nextPlatformPos, Quaternion.identity);
        nextPlatformPos += new Vector3(Random.Range(-platX, platX), Random.Range(-platY, platY), platZ);
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");

        for (int i = 0; i < 3; i++)
        {
            enemyPosition = platform.transform.localPosition;
            enemyPosition += new Vector3(Random.Range(-enemyX, enemyX), 2f, Random.Range(0, enemyZ));
            gameController.GetComponent<CreateEnemy>().createEnemy(enemyPosition, platform);
        }

    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, nextPlatformPos) < distanceThreshold)
        {
            platform = Instantiate(platformPrefab, nextPlatformPos, Quaternion.identity);
            nextPlatformPos += new Vector3(Random.Range(-platX, platX), Random.Range(-platY, platY), platZ);

            for (int i = 0; i < 3; i++)
            {
                enemyPosition = platform.transform.localPosition;
                enemyPosition += new Vector3(Random.Range(-enemyX, enemyX), 2f, Random.Range(-enemyZ, enemyZ));
                gameController.GetComponent<CreateEnemy>().createEnemy(enemyPosition, platform);
            }
        }
    }

}
