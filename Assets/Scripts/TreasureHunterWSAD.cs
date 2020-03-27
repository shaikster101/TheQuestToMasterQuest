using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //<---------------------THIS MIGHT BREAK WHEN BUILDING :) BECAUSE THE EDITOR IS NOT AVALIABLE WHEN BUILDING :) FIND ANOTHER WAY MUH MAN

public class TreasureHunterWSAD : MonoBehaviour
{
    // Start is called before the first frame update

    
    private TreasureHunterInventory GameInventory; 


    public TextMesh updateText;
    public TextMesh itemNumerText; 
    public TextMesh scoreText; 
    private int numItems = 0; 

    private int score = 0; 

    private int blueNum = 0; 
    private int greenNum = 0; 

    private int redNum = 0; 

    private int yellowNum = 0; 

    GameObject BlueMagic;
    GameObject GreenMagic;
    GameObject RedMagic;

    GameObject YellowMagic;



    private GameObject grabbedObject; 

    public Transform cameraPos; 
  
   
    void Start()
    {
        GameInventory = GetComponent<TreasureHunterInventory>(); 
        BlueMagic = Resources.Load("BlueMagic", typeof(GameObject)) as GameObject; 
        GreenMagic = Resources.Load("GreenMagic", typeof(GameObject)) as GameObject;
        RedMagic = Resources.Load("RedMagic", typeof(GameObject)) as GameObject; 
        YellowMagic = Resources.Load("YellowMagic", typeof(GameObject)) as GameObject; 
    }

    // Update is called once per frame
    void LateUpdate()
    {   



    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");
        Debug.Log(other.gameObject.tag);
        if(other.CompareTag("Collectible")){
            //updateText.text = "" + dist; 

            //updateText.text = "" + dist; 
                
                

                GameObject temp; 

                numItems ++; 

                itemNumerText.text = "Items Collected: " + numItems;
                if(other.name.Contains("BlueMagic")){
                        GameInventory.Inventory.Add(BlueMagic);
                        temp = BlueMagic; 
                        blueNum++; 
                        Debug.Log("Logged Blue"); 
                }else if(other.name.Contains("GreenMagic")){
                        GameInventory.Inventory.Add(GreenMagic);
                        greenNum++; 
                        temp= GreenMagic; 
                         Debug.Log("Logged Green"); 
                }else if(other.name.Contains("YellowMagic")){
                        GameInventory.Inventory.Add(YellowMagic);
                        yellowNum++; 
                        temp=YellowMagic;
                         Debug.Log("Logged Yello"); 
                }else{
                        temp=RedMagic; 
                        GameInventory.Inventory.Add(RedMagic);
                        redNum++;
                        Debug.Log("Logged Red"); 
                }

                updateText.text = "You Have: Red(10pts): " + redNum + " " +  "Blue(30pts): " + blueNum + " " + "Green(20pts): " + greenNum + " " + "Yellow(50pts): " + yellowNum; 
                score = score + temp.GetComponent<Collectible>().pointVal; 
                scoreText.text = "Score: " + score; 
                 Destroy(other.GetComponent<Collider>().gameObject);
            }
           
    }



}
