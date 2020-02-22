using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaistTracker : MonoBehaviour
{

    public Transform worldPos; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(worldPos.position.x, worldPos.position.y - 0.60f, worldPos.position.z-0.18f) ;   
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
