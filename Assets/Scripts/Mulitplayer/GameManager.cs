using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    void Start() {
        if (PlayerInMultiplayer.LocalPlayerInstance == null)
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
               PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(6f, 2f, 0f), Quaternion.identity, 0);
            }
            else
            {
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(-8f, 0f, 0f), Quaternion.identity, 0);
            }
        }
        else
        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
    }

    #region Photon Callbacks

    public override void OnLeftRoom() {
        SceneManager.LoadScene("Launcher");
    }

    #endregion

    void LoadArena() 
    {
        if (!PhotonNetwork.IsMasterClient) {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        PhotonNetwork.LoadLevel("MulitplayerLevel");
    }



    #region Public Methods
    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player other) 
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);

            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);


        if (PhotonNetwork.IsMasterClient) 
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            LeaveRoom();
        }
    }

    #endregion
}