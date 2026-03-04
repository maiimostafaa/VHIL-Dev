using Photon.Pun;
using UnityEngine;

public class VRPlayerSetup : MonoBehaviourPun
{
    public OVRCameraRig ovrCameraRig;
    public Camera centerEyeCamera;
    public AudioListener audioListener;

    void Start()
    {
        if (!photonView.IsMine)
        {
            // Disable tracking + camera for remote players
            ovrCameraRig.enabled = false;

            if (centerEyeCamera != null)
                centerEyeCamera.enabled = false;

            if (audioListener != null)
                audioListener.enabled = false;
        }
    }
}