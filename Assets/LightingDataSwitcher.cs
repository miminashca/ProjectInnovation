using UnityEngine;
using Photon.Pun;

public class LightingDataSwitcher : MonoBehaviourPunCallbacks
{

    [Header("Lightmap-textures for player (MasterClient)")]
    public Texture2D[] playerLightmapTextures;

    [Header("Lightmap-textures for operator (not MasterClient)")]
    public Texture2D[] operatorLightmapTextures;
    
    private LightmapData[] operatorLightmaps;
    private LightmapData[] playerLightmaps;

    void Start()
    {
        
        operatorLightmaps = new LightmapData[operatorLightmapTextures.Length];
        for (int i = 0; i < operatorLightmapTextures.Length; i++)
        {
            operatorLightmaps[i] = new LightmapData();
            operatorLightmaps[i].lightmapColor = operatorLightmapTextures[i];
        }

        
        playerLightmaps = new LightmapData[playerLightmapTextures.Length];
        for (int i = 0; i < playerLightmapTextures.Length; i++)
        {
            playerLightmaps[i] = new LightmapData();
            playerLightmaps[i].lightmapColor = playerLightmapTextures[i];
        }

       
        ApplyLighting();
    }

    void ApplyLighting()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log(" (Applying lightmaps for the player MasterClient)");
            LightmapSettings.lightmaps = playerLightmaps;
        }
        else
        {
            Debug.Log("Applying lightmaps for the operator (not MasterClient)");
            LightmapSettings.lightmaps = operatorLightmaps;
        }
    }
}
