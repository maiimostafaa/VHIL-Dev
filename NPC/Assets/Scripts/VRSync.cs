using Photon.Pun;
using UnityEngine;

public class VRSync : MonoBehaviourPun, IPunObservable
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private Vector3 headPos;
    private Quaternion headRot;

    private Vector3 leftPos;
    private Quaternion leftRot;

    private Vector3 rightPos;
    private Quaternion rightRot;

    public float smoothSpeed = 10f;

    void Update()
    {
        if (!photonView.IsMine)
        {
            head.position = Vector3.Lerp(head.position, headPos, Time.deltaTime * smoothSpeed);
            head.rotation = Quaternion.Slerp(head.rotation, headRot, Time.deltaTime * smoothSpeed);

            leftHand.position = Vector3.Lerp(leftHand.position, leftPos, Time.deltaTime * smoothSpeed);
            leftHand.rotation = Quaternion.Slerp(leftHand.rotation, leftRot, Time.deltaTime * smoothSpeed);

            rightHand.position = Vector3.Lerp(rightHand.position, rightPos, Time.deltaTime * smoothSpeed);
            rightHand.rotation = Quaternion.Slerp(rightHand.rotation, rightRot, Time.deltaTime * smoothSpeed);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(head.position);
            stream.SendNext(head.rotation);

            stream.SendNext(leftHand.position);
            stream.SendNext(leftHand.rotation);

            stream.SendNext(rightHand.position);
            stream.SendNext(rightHand.rotation);
        }
        else
        {
            headPos = (Vector3)stream.ReceiveNext();
            headRot = (Quaternion)stream.ReceiveNext();

            leftPos = (Vector3)stream.ReceiveNext();
            leftRot = (Quaternion)stream.ReceiveNext();

            rightPos = (Vector3)stream.ReceiveNext();
            rightRot = (Quaternion)stream.ReceiveNext();
        }
    }
}