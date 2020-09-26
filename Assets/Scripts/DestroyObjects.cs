using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Objects with this scrip will be destroyed
 * if they touch the death platform
 */
public class DestroyObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Death")
        {
            Destroy(gameObject);
        }
    }
}
