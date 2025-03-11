using UnityEngine;
using Photon.Pun;

public class LightingDataSwitcher : MonoBehaviourPunCallbacks
{
    [Header("Lightmap-текстуры для оператора (не MasterClient)")]
    public Texture2D[] operatorLightmapTextures;

    [Header("Lightmap-текстуры для игрока (MasterClient)")]
    public Texture2D[] playerLightmapTextures;

    // Закрытые поля, чтобы хранить сформированные LightmapData[]
    private LightmapData[] operatorLightmaps;
    private LightmapData[] playerLightmaps;

    void Start()
    {
        // 1) Собираем LightmapData[] для оператора
        operatorLightmaps = new LightmapData[operatorLightmapTextures.Length];
        for (int i = 0; i < operatorLightmapTextures.Length; i++)
        {
            operatorLightmaps[i] = new LightmapData();
            operatorLightmaps[i].lightmapColor = operatorLightmapTextures[i];
        }

        // 2) Собираем LightmapData[] для игрока
        playerLightmaps = new LightmapData[playerLightmapTextures.Length];
        for (int i = 0; i < playerLightmapTextures.Length; i++)
        {
            playerLightmaps[i] = new LightmapData();
            playerLightmaps[i].lightmapColor = playerLightmapTextures[i];
        }

        // 3) Применяем
        ApplyLighting();
    }

    void ApplyLighting()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Применяем lightmaps для игрока (MasterClient)");
            LightmapSettings.lightmaps = playerLightmaps;
        }
        else
        {
            Debug.Log("Применяем lightmaps для оператора (не MasterClient)");
            LightmapSettings.lightmaps = operatorLightmaps;
        }
    }
}
