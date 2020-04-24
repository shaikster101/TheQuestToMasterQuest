using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IKController : MonoBehaviour
{
    private GameObject wristGoal;
    private GameObject handGoal;
    private GameObject wristMesh;
    private GameObject elbowJoint;
    private GameObject wristJoint;
    private GameObject shoulderMesh;
    private GameObject shoulderJoint;
    private GameObject wristGoalMesh1;
    private Vector3 PreviousUp;

    public float translationalSpeed = 0.01f;
    public float rotationalSpeed = 1.0f;
    private float previwristPos;
    private float forearmL = 0.3f;
    float rotationDirection = 0;
    private float armL = 0.3f;
    Vector3 offset;
 
    void Start()
    {
        shoulderMesh = GameObject.Find("SJMesh");
        wristGoal = GameObject.Find("wristGoal1");
        wristGoalMesh1 = GameObject.Find("wristGoalMesh1");
        handGoal = GameObject.Find("handGoalMesh1");
        wristMesh = GameObject.Find("WJ");
        wristJoint = GameObject.Find("WJ1");
        elbowJoint = GameObject.Find("EJ1");
        shoulderJoint = GameObject.Find("SJ1");
        Cursor.lockState = CursorLockMode.Locked;
        PreviousUp = handGoal.transform.up;
        previwristPos = wristJoint.transform.position.z;
    }

    void Update()
    {
        //translate with mouse
        wristGoal.transform.position = new Vector3(
            wristGoal.transform.position.x,
            wristGoal.transform.position.y + (Input.GetAxis("Mouse Y") * translationalSpeed),
            wristGoal.transform.position.z - (Input.GetAxis("Mouse X") * translationalSpeed)
        );
        //rotate with W/S
        //RotateAround is way better than trying to set eulerAngles manually b/c Unity has this annoying inability to work with negative angles.... and print statement angles don't
        //always match the angle that you see in the editor. Really solid engine 10/10
        // restrict hand-wrist rotation

        Vector3 handGoalUp = handGoal.transform.up;
        Vector3 forearmUp = wristMesh.transform.position - elbowJoint.transform.position;
        float dir = d(handGoal.transform.position + Vector3.Normalize(forearmUp), handGoal.transform.position, handGoal.transform.position + handGoalUp);
        float handRotation = Mathf.Acos(Vector3.Dot(Vector3.Normalize(handGoalUp), Vector3.Normalize(forearmUp)));
        if (handRotation < 1.5708 || dir > 0)
        {
            wristGoal.transform.RotateAround(wristGoal.transform.position, new Vector3(-1, 0, 0), ((Input.GetKey(KeyCode.W) ? 1 : 0) * rotationalSpeed));
        }
        if (handRotation < 1.5708 || dir < 0)
        {
            wristGoal.transform.RotateAround(wristGoal.transform.position, new Vector3(-1, 0, 0), ((Input.GetKey(KeyCode.S) ? -1 : 0) * rotationalSpeed));
        }

        wristJoint.transform.rotation = wristGoal.transform.rotation;
        // Calculate elbow position

        elbowJoint.transform.rotation = Quaternion.LookRotation(wristJoint.transform.position-elbowJoint.transform.position);

        if (wristGoal.transform.position.z > 0)
        {
            if (Vector3.Distance(wristGoalMesh1.transform.position, shoulderMesh.transform.position) >= 0.60f)
            {
                shoulderJoint.transform.LookAt(wristGoal.transform.position);
                Vector3 elbowDirection = Vector3.Normalize(wristGoal.transform.position - elbowJoint.transform.position);
                wristJoint.transform.position = elbowDirection * 0.3f + elbowJoint.transform.position ;
                elbowJoint.transform.rotation = Quaternion.LookRotation(wristJoint.transform.position-elbowJoint.transform.position);

            }
            else
            {

                float shoulderDistance = Vector3.Distance(wristMesh.transform.position, shoulderMesh.transform.position);
                wristJoint.transform.position = wristGoal.transform.position;
                //Update the elbow position
                float deltaX = wristJoint.transform.position.z - previwristPos;
                float targetAngle = Mathf.Acos((Mathf.Pow(shoulderDistance, 2) + Mathf.Pow(armL, 2) - Mathf.Pow(forearmL, 2)) / (2 * shoulderDistance * armL));
                //get the current angle
                Vector3 shoulderToWrist = wristJoint.transform.position - shoulderMesh.transform.position;
                float currentAngle = Mathf.Acos(Vector3.Dot(Vector3.Normalize(shoulderToWrist), Vector3.Normalize(shoulderJoint.transform.forward)));
                float rotateAngle = currentAngle - targetAngle;
                shoulderJoint.transform.RotateAround(shoulderJoint.transform.position, new Vector3(-1, 0, 0), rotateAngle * 15);

                elbowJoint.transform.rotation = Quaternion.LookRotation(wristJoint.transform.position-elbowJoint.transform.position);


            }
        }
        previwristPos = wristJoint.transform.position.z;
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
    public static float d(Vector3 pointToTest, Vector3 vectorSource, Vector3 vectorDestination)
    {
        float d = (pointToTest.y - vectorSource.y) * (vectorDestination.z - vectorSource.z) - (pointToTest.z - vectorSource.z) * (vectorDestination.y - vectorSource.y);
        return d;
    }
}