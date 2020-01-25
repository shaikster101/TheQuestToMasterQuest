using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureHunter : MonoBehaviour
{
    // Start is called before the first frame update

    
    private TreasureHunterInventory GameInventory; 


    public TextMesh InventoryStatus;
    public TextMesh GameStatus; 


    private GameObject nicksHead; 
    private GameObject nicksBody; 
    private GameObject nicksSoul; 


    void Start()
    {
       GameInventory = GetComponent<TreasureHunterInventory>(); 
       nicksHead = GameObject.Find("NickHead"); 
       nicksBody = GameObject.Find("NickBody");
       nicksSoul = GameObject.Find("NickSoul");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameInventory.Inventory.Count >= 3){
            GameStatus.text = "You Win"; 
        }
        

         if (Input.GetKeyDown("1"))
        {
            if(nicksHead != null){
                if(!GameInventory.Inventory.Contains(nicksHead)){
                    GameInventory.Inventory.Add(nicksHead);
                    InventoryStatus.text = "Inventory Status: You found " + nicksHead.name + " with a point value of " + nicksHead.GetComponent<Collectible>().pointVal;  
                }
            }
            
        }

         if (Input.GetKeyDown("2"))
        {
             if(nicksBody != null){
                if(!GameInventory.Inventory.Contains(nicksBody)){
                    GameInventory.Inventory.Add(nicksBody);
                    InventoryStatus.text = "Inventory Status: You found " + nicksBody.name + " with a point value of " + nicksBody.GetComponent<Collectible>().pointVal;  
                }
            }
        }

         if (Input.GetKeyDown("3"))
        {
            if(nicksSoul != null){
                if(!GameInventory.Inventory.Contains(nicksSoul)){
                   GameInventory.Inventory.Add(nicksSoul);
                   InventoryStatus.text = "Inventory Status: You found " + nicksBody.name + " with a point value of " + nicksSoul.GetComponent<Collectible>().pointVal;  
                }
            }
        }

        if (Input.GetKeyDown("4"))
        {

            int pointValue = 0; 

            string InventoryOverview = "You Have Collected: " + GameInventory.Inventory.Count + " parts.( ";  

            foreach(GameObject GO in GameInventory.Inventory){
                pointValue += GO.GetComponent<Collectible>().pointVal; 
                InventoryOverview += GO.name + "[" + GO.GetComponent<Collectible>().pointVal + "] ";

            }
            
            InventoryOverview += ") with a total value of: " + pointValue; 

            InventoryStatus.text =  InventoryOverview; 
        }

    }




}
