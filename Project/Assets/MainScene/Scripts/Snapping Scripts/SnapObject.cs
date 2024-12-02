using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObject : MonoBehaviour
{   
    //Reference the snap zone collider we'll be using
    public GameObject SnapLocation;

    //Reference the game object that the snapped objects will become a part of
    public GameObject Blkbase;

    //Create a var that will be used by the RocketLaunch script to determine if all three pieces are in place
    public bool isSnapped;

    //boolean var used to reference the "Snapped" boolean from the SnapToLocation script
    private bool objectSnapped;

    //boolean var used to determine if the object is currently being held by player
    private bool grabbed;

    // Update is called once per frame
    void Update()
    {
        //Set grabbed to equal the boolean "isSelected" from the XR Grab interactor script
        grabbed = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().isSelected;

        //Set objectSnapped equal to the Snapped boolean from SnapToLocation
        objectSnapped = SnapLocation.GetComponent<SnapToLocation>().Snapped;

        //Set Object RigidBody to be kinematic after it has been snapped into position
        //Set object to be a parent of the base after it has been snapped
        //Set isSnapped var to be trye to alert the Sentence complete script
        if(objectSnapped == true)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(Blkbase.transform);
            isSnapped = true;
        }

        //Makes sure that the object can still be grabbed by the XR Grab Interactable script
        if(objectSnapped == false && grabbed == false){
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
