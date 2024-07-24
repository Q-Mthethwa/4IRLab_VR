using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScreenControllerScript : MonoBehaviour
{
    public GameObject Screen;
    public GameObject PC;
    public GameObject unityIcon;
    public Material pcMaterial;
    public Material screenMaterial;
    //public Material screenMaterial2;
    //public XRSimpleInteractable xrSimpleInteractable;
    public Material pcMaterialOriginal;
    public Material screenMaterialOriginal;
    // This will be called once the PC has been turned on or off
    public void UpdateScreen()
    {
        Material currentScreenMaterial = Screen.GetComponent<MeshRenderer>().material;
        Material currentPCMaterial = PC.GetComponent<MeshRenderer>().material;
        if (AreMaterialsEqual(currentScreenMaterial,screenMaterialOriginal))
        {
            Screen.GetComponent<MeshRenderer>().material= screenMaterial;
            PC.GetComponent<MeshRenderer>().material= pcMaterial;
            unityIcon.SetActive(true);
            /*GetComponent<MeshRenderer>().material= iconMaterial;
            xrSimpleInteractable.enabled= true;*/
        }     
        else if (AreMaterialsEqual(currentPCMaterial,pcMaterial)&&(!(AreMaterialsEqual(currentScreenMaterial,screenMaterialOriginal)))&&(!(AreMaterialsEqual(currentScreenMaterial,pcMaterialOriginal)))){
            PC.GetComponent<MeshRenderer>().material= pcMaterialOriginal;
            Screen.GetComponent<MeshRenderer>().material= screenMaterialOriginal;
            unityIcon.SetActive(false);
        /*GetComponent<MeshRenderer>().material= pcMaterialOriginal;
        
        xrSimpleInteractable.enabled= false;*/
        } 
        else{
            Material newMaterial= changePCMaterial(currentPCMaterial);
            PC.GetComponent<MeshRenderer>().material= newMaterial;
            unityIcon.SetActive(false);
        }
    }
    private Material changePCMaterial(Material PCMaterialInUse){
        if (AreMaterialsEqual(PCMaterialInUse,pcMaterialOriginal)){
            return pcMaterial;
        }
        if (AreMaterialsEqual(PCMaterialInUse,pcMaterial)){
            return pcMaterialOriginal;
        }
        return null;
    }
    public bool AreMaterialsEqual(Material mat1, Material mat2){
        if (mat1== null || mat2 == null){
            return false;
        } 
        if(mat1.shader!=mat2.shader){
            return false;
        }
        if (mat1.mainTexture!=mat2.mainTexture){
            return false;
        }
        return true;
    } 
}
