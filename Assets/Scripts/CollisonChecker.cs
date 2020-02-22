using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonChecker : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject triggered; 
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Collectible")
        {
            triggered = other.transform.gameObject; 
            Debug.Log("Grabbed"); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collectible")
        {
            triggered = null; 
            Debug.Log("Dropped"); 
        }
    }
    
}
