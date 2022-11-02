using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject RoomPanel;

    public GameObject CreatePanel;
    public Text roomidText;

    public GameObject JoinPanel;
    public InputField roomidInput;
    public void Connect() => PhotonNetwork.ConnectUsingSettings(); // ���� ���� �õ�

    void Awake()
    {
        Screen.SetResolution(1020, 1980, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        Connect();
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("���� ���� �Ϸ�");
    }

    public void OnClickCreateBtn()
    {
        string roomid = Random.Range(0, 1000000).ToString("D6");
        PhotonNetwork.CreateRoom(roomid, new RoomOptions { MaxPlayers = 2 });
        roomidText.text = "�� ��ȣ : " + roomid;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("�� ���� �Ϸ�");
        RoomPanel.SetActive(false);
        CreatePanel.SetActive(true);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private void Update()
    {
        // �ȵ���̵� �ڷΰ���� ��ǻ�� EscŰ��
        if(Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom();
            Debug.Log("�� ����");
            RoomPanel.SetActive(true);
            CreatePanel.SetActive(false);
            JoinPanel.SetActive(false);
        }
    }

    public void OnClickJoinBtn()
    {
        RoomPanel.SetActive(false);
        JoinPanel.SetActive(true);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomidInput.text);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
