using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControllerScript : MonoBehaviour
{
    public GameObject Screen;
    public GameObject PC;
    public Material pcMaterial;
    public Material screenMaterial;
    public Material pcMaterialOriginal;
    public Material screenMaterialOriginal;
    // This will be called once the PC has been turned on or off
    public void UpdateScreen()
    {
        Material currentScreenMaterial = Screen.GetComponent<MeshRenderer>().material;
        if (AreMaterialsEqual(currentScreenMaterial,screenMaterialOriginal))
        {
            Screen.GetComponent<MeshRenderer>().material= screenMaterial;
        } 
        
        else if(AreMaterialsEqual(currentScreenMaterial,screenMaterial)) {
        PC.GetComponent<MeshRenderer>().material= pcMaterialOriginal;
        Screen.GetComponent<MeshRenderer>().material= screenMaterialOriginal;
        }
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
