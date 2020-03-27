using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2CRedirection : MonoBehaviour
{

    //Public Vars: 
    public Camera headCamera;
    
    public Transform trackingSpace;


    private Vector3 prevForwardVector;
    private float prevYawRelativeToCenter; 
    private Vector3 prevLocation;
    
    //usually 50%-100%
    public float translationalGainThreshold;
    public TextMesh TranslationText; 


    //Direction function
    public static float d (Vector3 pointToTest, Vector3 vectorSource, Vector3 vectorDestination)
    {
        //(pointToTest.x−vectorSource.x)(vectorDestination.y−vectorSource.y)−(pointToTest.y−vectorSource.y)(vectorDestination.x−vectorSource.x)

        float d = (pointToTest.x - vectorSource.x) * (vectorDestination.z - vectorSource.z) - (pointToTest.z - vectorSource.z) * (vectorDestination.x - vectorSource.x);

        return d; 
    }

    //Angle Fucniton
    public static float angleBetweenVectors (Vector3 A, Vector3 B)
    {
        Vector3 correctCameraVector = new Vector3(A.x, 0, A.z);

        Vector3 correctTrackingVector = new Vector3(B.x, 0, B.z);

        float angle = Mathf.Acos(Vector3.Dot(Vector3.Normalize(correctCameraVector), Vector3.Normalize(correctTrackingVector)));
        angle = angle*180/Mathf.PI;
        return angle; 
    }


    // Start is called before the first frame update
    void Start()
    {
        prevLocation=headCamera.transform.position;
        prevForwardVector = headCamera.transform.forward;
        prevYawRelativeToCenter = angleBetweenVectors(headCamera.transform.forward, trackingSpace.position - headCamera.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // translational gain
        Vector3 trajectoryVector=headCamera.transform.position-prevLocation;
        Vector3 howMuchToTranslate=Vector3.Normalize(trajectoryVector)*translationalGainThreshold;
        trackingSpace.position+=howMuchToTranslate;




        float howMuchUserRotated = angleBetweenVectors(prevForwardVector, headCamera.transform.forward);

        int directionUserRotated = (d(headCamera.transform.position + prevForwardVector, headCamera.transform.position, headCamera.transform.position + headCamera.transform.position) < 0) ? 1 : -1;

        float deltaYawRelativeToCenter = prevYawRelativeToCenter - angleBetweenVectors(headCamera.transform.forward, trackingSpace.position - headCamera.transform.position); 


        float distanceFromCenter = (headCamera.transform.position - trackingSpace.position).magnitude;

        float longestDimensionOfPE = 4f;


        float decelerateThreshold = 0.13f;

        float accelerateThreshold = 0.3f;


        float howMuchToAccelerate = ((deltaYawRelativeToCenter < 0) ? -decelerateThreshold : accelerateThreshold) *
            howMuchUserRotated * directionUserRotated * Mathf.Clamp(distanceFromCenter / longestDimensionOfPE / 2, 0, 1);

        if(Mathf.Abs(howMuchToAccelerate) > 0)
        {
            trackingSpace.RotateAround(headCamera.transform.position, new Vector3(0, 1, 0), howMuchToAccelerate);
        }

       



        prevForwardVector = headCamera.transform.forward;

        prevYawRelativeToCenter = angleBetweenVectors(headCamera.transform.forward, trackingSpace.position - headCamera.transform.forward);

       
        prevLocation=headCamera.transform.position;
        TranslationText.text = "Translational Gain Threshold " + translationalGainThreshold;
    }


}
