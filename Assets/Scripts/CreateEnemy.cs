using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy: MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    public void createEnemy(Vector3 pos, GameObject platform)
    {
        enemyPrefab = Instantiate(enemyPrefab, pos, Quaternion.identity);
        //enemyPrefab.transform.parent = platform.transform;

    }
}
