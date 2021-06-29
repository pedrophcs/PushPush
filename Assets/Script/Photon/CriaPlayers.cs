using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class CriaPlayers : MonoBehaviour
{
    void Start()
    {
        PhotonNetwork.Instantiate(PhotonConnection._photonConnect._player[PhotonConnection._photonConnect.ID].name, new Vector3(Random.Range(1, 8), 2, Random.Range(1, 8)), Quaternion.identity);
        
    }
}
