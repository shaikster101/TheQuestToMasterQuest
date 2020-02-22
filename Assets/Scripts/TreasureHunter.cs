using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //<---------------------THIS MIGHT BREAK WHEN BUILDING :) BECAUSE THE EDITOR IS NOT AVALIABLE WHEN BUILDING :) FIND ANOTHER WAY MUH MAN

public class TreasureHunter : MonoBehaviour
{
    // Start is called before the first frame update

    
    private TreasureHunterInventory GameInventory; 


    public TextMesh updateText;
    public TextMesh itemNumerText; 
    public TextMesh scoreText; 
    private int numItems = 0; 

    private int score = 0; 

    GameObject BlueMagic;
    GameObject GreenMagic;
    GameObject RedMagic;

    GameObject YellowMagic; 
  
   
    void Start()
    {
        GameInventory = GetComponent<TreasureHunterInventory>(); 
        BlueMagic = Resources.Load("BlueMagic", typeof(GameObject)) as GameObject; 
        GreenMagic = Resources.Load("GreenMagic", typeof(GameObject)) as GameObject;
        RedMagic = Resources.Load("RedMagic", typeof(GameObject)) as GameObject; 
        YellowMagic = Resources.Load("YellowMagic", typeof(GameObject)) as GameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        
        // Does the ray intersect any objects excluding the player layer

        // if(Input.GetMouseButtonDown(0)){

        //     RaycastHit hit;
        //     if (Physics.Raycast(RayCastTransform.position, RayCastTransform.forward, out hit, 10))
        //     {
        //         if(hit.collider.gameObject.tag == "Collectible"){

        //             numItems++; 

        //             if(hit.collider.gameObject.name.Contains("BlueMagic")){
        //                  GameInventory.Inventory.Add(BlueMagic);
        //             }else if(hit.collider.gameObject.name.Contains("GreenMagic")){
        //                 GameInventory.Inventory.Add(GreenMagic);
        //             }else{
                       
        //                 GameInventory.Inventory.Add(RedMagic);
                        
        //             }

    
        //              updateText.text = "Update: Collected " +  hit.transform.gameObject.name + " with val of " + hit.transform.gameObject.GetComponent<Collectible>().pointVal; 
        //              score = score + hit.transform.gameObject.GetComponent<Collectible>().pointVal; 
        //              Destroy(hit.transform.gameObject); 
        //              scoreText.text = "Husam's Score: " + score; 
                   
        //         }
        //     }

        //     itemNumerText.text = "Items Collected: " + numItems;  
        // }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectible"){
            numItems ++; 

            if(other.transform.gameObject.name.Contains("BlueMagic")){
                    GameInventory.Inventory.Add(BlueMagic);
            }else if(other.transform.gameObject.name.Contains("GreenMagic")){
                GameInventory.Inventory.Add(GreenMagic);
            }else if(other.transform.gameObject.name.Contains("YellowMagic")){
                GameInventory.Inventory.Add(YellowMagic);
            }else{
                GameInventory.Inventory.Add(RedMagic);
            }

             updateText.text = "Update: Collected " +  other.transform.gameObject.name + " with val of " + other.transform.gameObject.GetComponent<Collectible>().pointVal; 
            score = score + other.transform.gameObject.GetComponent<Collectible>().pointVal; 
            Destroy(other.transform.gameObject); 
            scoreText.text = "Husam's Score: " + score; 
        }
    }




}
