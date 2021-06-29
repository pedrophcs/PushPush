using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _panelLogin, _panelSala;
    [SerializeField]
    private InputField _nomeJogador;
    [SerializeField]
    private Text _nickName;
    //private string _nomeSala;

    public GameObject[] _player;

    public static PhotonConnection _photonConnect;
    public void Awake()
    {
        if (_photonConnect == null)
        {
            _photonConnect = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int ID = 0;

    private void Start()
    {
        //_nomeSala = "Sala1";
        PhotonNetwork.AutomaticallySyncScene = true;
        _panelLogin.SetActive(true);
        _panelSala.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("Quantidade de jogadores :" + PhotonNetwork.CurrentRoom.PlayerCount);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public void Login()
    {
        PhotonNetwork.NickName = _nomeJogador.text;
        PhotonNetwork.ConnectUsingSettings();
        _panelLogin.SetActive(false);
        _panelSala.SetActive(true);
    }

    public void CreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("Sala1", new RoomOptions(), TypedLobby.Default);

    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby");

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado");
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);
        Debug.Log("N�o Conectado");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Fail");
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou");
        print(PhotonNetwork.CurrentRoom.Name);
        print(PhotonNetwork.CurrentRoom.PlayerCount);
        print(PhotonNetwork.NickName);
        _nickName.text = PhotonNetwork.NickName;

        //PhotonNetwork.Instantiate(_player[ID].name, new Vector3(0, 5, 0), Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
    public void PlayerSelect(int id)
    {
        ID = id;
    }

}
