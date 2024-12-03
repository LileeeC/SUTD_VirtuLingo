using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnapToLocation : MonoBehaviour
{   
    //boolean variable used to determine if the object is currently being held by the player
    private bool grabbed;

    //Returns true when the object is within the SnapZone radius
    private bool insideSnapZone;

    //Return true when the object has had it's location updated
    public bool Snapped;

    //Set the specific part we want to snap to this location
    public GameObject SnapZoneBlk;
    //Reference another object we can use to set rotation
    public GameObject SnapRotationReference;

    //Detects when the block has entered snap zone radius
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == SnapZoneBlk.name)
        {
            insideSnapZone = true;
        }
    }

    //Detects when the block game object has left the snap zone radius
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == SnapZoneBlk.name)
        {
            insideSnapZone = false;
        }
    }

    //Checks if the player has released the object AND if the object is within the SnapZone radius
    //If both are true, sets the object location and rotation to the desired snap coordinates
    //Sets the public boolean Snapped to try for reference by SnapObject script
    void SnapObject()
    {
        if (grabbed == false && insideSnapZone == true)
        {
            SnapZoneBlk.gameObject.transform.position = transform.position;
            SnapZoneBlk.gameObject.transform.rotation = SnapRotationReference.transform.rotation;
            Snapped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //set grabbed to equal the boolean value selectingInteractor, which is null when the object isn't being grabbed
        grabbed = SnapZoneBlk.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().isSelected;
        //call our snap object script
        SnapObject();
    }
}
