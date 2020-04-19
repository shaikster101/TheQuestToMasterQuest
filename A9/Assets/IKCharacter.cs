using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKCharacter : MonoBehaviour
{
    GameObject wristGoal;
    public float translationalSpeed=0.01f;
    public float rotationalSpeed=1.0f;
    // Start is called before the first frame update
    void Start()
    {
        wristGoal=GameObject.Find("wristGoal");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //translate with mouse
        wristGoal.transform.position=new Vector3(
            wristGoal.transform.position.x,
            wristGoal.transform.position.y+(Input.GetAxis("Mouse Y")*translationalSpeed),
            wristGoal.transform.position.z-(Input.GetAxis("Mouse X")*translationalSpeed)
        );

        //rotate with W/S
        //RotateAround is way better than trying to set eulerAngles manually b/c Unity has this annoying inability to work with negative angles.... and print statement angles don't
        //always match the angle that you see in the editor. Really solid engine 10/10
        wristGoal.transform.RotateAround(wristGoal.transform.position,new Vector3(-1,0,0),(((Input.GetKey(KeyCode.W)?1:0)+(Input.GetKey(KeyCode.S)?-1:0))* rotationalSpeed));


        //from here you'll want to pose the joints. update is a good option for manual placement; otherwise, IK plugins use OnAnimatorIK which is a built-in function Unity uses for when it 
        //actually does the IK (using Update instead of that one will cause your changes to get overwritten)

        //the elbow is parented to the shoulder so you can easily position the elbow by rotating the shoulder by the degrees given by law of cosines

        //you can make the head & body meshes invisible if you want

        //Unity doesn't have a dedicated skeletal animation poser system so instead it treats the joints like GameObjects. These can be posed to pose a mesh
        //you can check out a tutorial like this to understand it a bit more https://www.youtube.com/watch?v=a9EBILq2ep8 or https://www.youtube.com/watch?v=af10RJVZGEY

        //assumptions you can make:
        //the character doesn't need to rotate around yaw (you can treat the XZ axis like a 2D graph)
        //all that needs to look right is the character mesh; the joints just need be be positioned correctly
    }
}
