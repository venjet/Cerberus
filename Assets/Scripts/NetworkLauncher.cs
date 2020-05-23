using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public List<CinemachineVirtualCamera> childCameraLst = new List<CinemachineVirtualCamera>();

    public GameObject vcam;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster(){
        base.OnConnectedToMaster();
        Debug.Log("Hello,player!");

        PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions(){MaxPlayers = 6},default);
    }

    public override void OnJoinedRoom(){
        GameObject player = PhotonNetwork.Instantiate("PlayerDog",new Vector3(-14,0,0),Quaternion.identity,0);
        CinemachineVirtualCamera[] cameras = vcam.transform.GetComponentsInChildren<CinemachineVirtualCamera>();     
        cameras[0].Follow = player.transform;
    }
    
}
