using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 序列化的类，用于表示库存中的一个槽位数据，包括槽位中的物品数据和数量
[Serializable]
public class SlotData
{
    /// 槽位中的物品数据
    public ItemData item;
    
    /// 槽位中物品的数量
    public int count = 0;

    /// 当槽位数据发生变化时调用的委托
    private Action OnChange;

    /// 将当前槽位的数据移动到另一个槽位
    /// <param name="data">要移动到的目标槽位数据</param>
    public void MoveSlot(SlotData data)
    {
        this.item = data.item;
        this.count = data.count;
        OnChange?.Invoke();
    }

    /// 检查槽位是否为空
    /// <returns>如果槽位为空返回true，否则返回false</returns>
    public bool IsEmpty()
    {
        return count == 0;
    }
    
    /// 检查槽位是否可以添加更多物品
    /// <returns>如果可以添加物品返回true，否则返回false</returns>
    public bool CanAddItem()
    {
        return count < item.maxCount;
    }
    
    /// 获取槽位中剩余的空间
    /// <returns>槽位中的剩余空间</returns>
    public int GetFreeSpace()
    {
        return item.maxCount - count;
    }
    
    /// 向槽位中添加指定数量的物品
    /// <param name="numToAdd">要添加的物品数量，默认为1</param>
    public void Add(int numToAdd = 1)
    {
        this.count += numToAdd;
        OnChange?.Invoke();
    }
    
    /// 向槽位中添加一个物品
    /// <param name="item">要添加的物品数据</param>
    /// <param name="count">物品的数量，默认为1</param>
    public void AddItem(ItemData item,int count =1)
    {
        this.item = item;
        this.count = count;
        OnChange?.Invoke();
    }
    
    /// 减少槽位中的物品数量
    /// <param name="numToReduce">要减少的物品数量，默认为1</param>
    public void Reduce(int numToReduce = 1)
    {
        count -= numToReduce;
        if (count == 0)
        {
            Clear();
        }
        else
        {
            OnChange?.Invoke();
        }
    }
    
    /// 清空槽位中的物品和数量
    public void Clear()
    {
        item = null;
        count = 0;
        OnChange?.Invoke();
    }
    
    /// 为槽位数据变化事件添加监听器
    /// <param name="OnChange">当槽位数据发生变化时调用的委托</param>
    public void AddListener(Action OnChange)
    {
        this.OnChange = OnChange;
    }
}
