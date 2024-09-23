using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportRay : MonoBehaviour
{
    public GameObject controllerTeleportation;

    public InputActionProperty controllerActive;

    public InputActionProperty controllerCancel;

    public XRRayInteractor controllerRay;

    // Update is called once per frame
    void Update()
    {
        bool isControllerRayHovering = controllerRay.TryGetHitInfo(out Vector3 controllerPos, out Vector3 controllerNormal, out int controllerNumber, out bool controllerValid);

        controllerTeleportation.SetActive(!isControllerRayHovering && controllerCancel.action.ReadValue<float>()==0 && controllerActive.action.ReadValue<float>() >0.1f);

    }
}
