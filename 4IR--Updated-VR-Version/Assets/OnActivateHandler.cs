using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnActivateHandler : MonoBehaviour
{
    public GameObject Screen;
    public GameObject unityIcon;
    public Material screenMaterial;
    public Material screenMaterialOff;
    public XRGrabInteractable grabInteractable;
    protected float count = 0;
    
    // Start is called before the first frame update
    void Awake(){
        unityIcon.SetActive(false);
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(OnActivated);
        count = 0;
        }
    void OnDestroy(){
        grabInteractable.activated.RemoveListener(OnActivated);
    }
    private void OnActivated(ActivateEventArgs args){
        Debug.Log("Interactable object activated");
        count += 1;
        if ((count%2)!=0){
            Screen.GetComponent<MeshRenderer>().material= screenMaterial;
        } else{
            Screen.GetComponent<MeshRenderer>().material= screenMaterialOff;
            unityIcon.SetActive(false);
        }
    }
}
