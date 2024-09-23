using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVGACord : MonoBehaviour
{
    public GameObject vgaCord;
    public GameObject otherObject;
    public bool isConnected= false;
    public GameObject VGACordCorrectPosition;

    void Start(){
        VGACordCorrectPosition.SetActive(false);
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject == otherObject){
            isConnected = true;
            AttachVGA();
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.gameObject == otherObject){
            isConnected = false;
            DetachVGA();
        }
    }
    private void AttachVGA(){
        Debug.Log("A trigger has occured");
        if (vgaCord != null && isConnected){
            //Get the ends of the VGA Cord
            Transform vgaStart = vgaCord.transform.GetChild(0);//Assuming the first Child is the Start
            Transform vgaEnd = vgaCord.transform.GetChild(1);//Assuming the second child is the End
            Debug.Log("The cord should be connected now");
            VGACordCorrectPosition.SetActive(true);
        }
    }
    private void DetachVGA(){
        Debug.Log("The cord has been disconnected");
    }
}
