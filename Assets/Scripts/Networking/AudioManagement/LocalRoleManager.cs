using Photon.Pun;
using UnityEngine;

public enum PlayerRole
{
    MainPlayer,
    CameraMan
}

public class LocalRoleManager : MonoBehaviour
{
    public static PlayerRole LocalRole; // accessible by all scripts

    private PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();

        // Only set the local role for the local player
        if (photonView.IsMine)
        {
            if (gameObject.name.Contains("Player_Networked"))
            {
                LocalRole = PlayerRole.MainPlayer;
            }
            else if (gameObject.name.Contains("CameraMan_Networked"))
            {
                LocalRole = PlayerRole.CameraMan;
            }
        }
    }
}
