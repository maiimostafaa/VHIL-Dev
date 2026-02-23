using Photon.Pun;
using UnityEngine;

public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Transform localHead;
    public Transform localLeftHand;
    public Transform localRightHand;

    void Start()
    {
        if (photonView.IsMine)
        {
            localHead = Camera.main.transform;
            localLeftHand = GameObject.Find("LeftHandAnchor").transform;
            localRightHand = GameObject.Find("RightHandAnchor").transform;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            head.position = localHead.position;
            head.rotation = localHead.rotation;

            leftHand.position = localLeftHand.position;
            leftHand.rotation = localLeftHand.rotation;

            rightHand.position = localRightHand.position;
            rightHand.rotation = localRightHand.rotation;
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
            head.position = (Vector3)stream.ReceiveNext();
            head.rotation = (Quaternion)stream.ReceiveNext();
            leftHand.position = (Vector3)stream.ReceiveNext();
            leftHand.rotation = (Quaternion)stream.ReceiveNext();
            rightHand.position = (Vector3)stream.ReceiveNext();
            rightHand.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}