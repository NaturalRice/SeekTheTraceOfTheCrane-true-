using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于处理与拖拽和放置游戏对象相关的事件的类。
/// </summary>
public class DragTest : MonoBehaviour
{
    /// <summary>
    /// 当拖拽操作开始时调用。
    /// 参数: gameObject - 被拖拽的游戏对象。
    /// </summary>
    public void BeginDrag(GameObject gameObject)
    {
        print("BeginDrag:" + gameObject);
    }

    /// <summary>
    /// 在拖拽操作过程中调用。
    /// 参数: gameObject - 被拖拽的游戏对象。
    /// </summary>
    public void OnDrag(GameObject gameObject)
    {
        print("OnDrag:" + gameObject);
    }

    /// <summary>
    /// 当拖拽操作结束时调用。
    /// 参数: gameObject - 被拖拽的游戏对象。
    /// </summary>
    public void EndDrag(GameObject gameObject)
    {
        print("EndDrag:" + gameObject);
    }

    /// <summary>
    /// 当游戏对象被放置时调用。
    /// 参数: gameObject - 被放置的游戏对象。
    /// </summary>
    public void OnDrop(GameObject gameObject)
    {
        print("OnDrop:" + gameObject);
    }
}
