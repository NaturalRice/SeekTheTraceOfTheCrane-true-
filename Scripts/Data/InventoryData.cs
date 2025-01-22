using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 通过[CreateAssetMenu()]属性告诉Unity引擎这个ScriptableObject可以在Inspector窗口通过"Create Asset"菜单创建
// 这个类用于存储库存数据，包括一系列的槽位数据
public class InventoryData :ScriptableObject
{
    // slotList列表存储了每个槽位的数据信息
    public List<SlotData> slotList;
}