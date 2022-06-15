using UnityEngine;
using Photon.Pun;

public class BulletInMulitplayer : MonoBehaviourPun
{
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (photonView.IsMine && collision.tag != "Player")
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
