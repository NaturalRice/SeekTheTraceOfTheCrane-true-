using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 管理植物相关的操作和状态
/// </summary>
public class PlantManager : MonoBehaviour
{
    /// <summary>
    /// PlantManager的单例实例
    /// </summary>
    public static PlantManager Instance { get; private set; }

    /// <summary>
    /// 交互地图，用于表示玩家可以与之交互的区域
    /// </summary>
    public Tilemap interactableMap;

    /// <summary>
    /// 交互瓦片，用于标记地图上可交互的位置
    /// </summary>
    public Tile interactableTile;
    
    /// <summary>
    /// 耕过的地面瓦片，用于表示已经耕作过的土地
    /// </summary>
    public Tile groundHoedTile;
    
    /// <summary>
    /// 在Awake方法中初始化PlantManager实例
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 在Start方法中初始化交互地图
    /// </summary>
    private void Start()
    {
        InitInteractableMap();
    }

    /// <summary>
    /// 初始化交互地图，将所有非空瓦片设置为交互瓦片
    /// </summary>
    void InitInteractableMap()
    {
        // 遍历交互地图上的所有位置
        foreach(Vector3Int position in interactableMap.cellBounds.allPositionsWithin)
        {
            // 获取当前位置的瓦片
            TileBase tile = interactableMap.GetTile(position);
            // 如果当前位置有瓦片，则将其设置为交互瓦片
            if (tile != null)
            {
                interactableMap.SetTile(position, interactableTile);
            }
        }
    }

    /// <summary>
    /// 耕作地面，将指定位置的瓦片设置为耕过的地面瓦片
    /// </summary>
    /// <param name="position">要耕作的地面位置</param>
    public void HoeGround(Vector3 position)
    {
        // 将世界坐标转换为瓦片坐标
        Vector3Int tilePosition = interactableMap.WorldToCell(position);
        // 获取当前位置的瓦片
        TileBase tile = interactableMap.GetTile(tilePosition);

        // 如果当前位置有瓦片且为交互瓦片，则将其设置为耕过的地面瓦片
        if (tile != null && tile.name == interactableTile.name)
        {
            interactableMap.SetTile(tilePosition, groundHoedTile);
            //Debug.Log($"HoeGround called at position: {position}. Tile changed to {groundHoedTile.name}.");
        }
        else
        {
            //Debug.Log($"HoeGround called at position: {position}. No valid tile to hoe.");
        }
    }
}