using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ToolbarUI 类负责管理工具栏的用户界面
/// </summary>
public class ToolbarUI : MonoBehaviour
{
    /// <summary>
    /// 工具栏槽位的 UI 列表
    /// </summary>
    public List<ToolbarSlotUI> slotuiList;

    /// <summary>
    /// 当前选中的工具栏槽位 UI
    /// </summary>
    private ToolbarSlotUI selectedSlotUI;

    /// <summary>
    /// 在第一次帧更新之前调用
    /// </summary>
    void Start()
    {
        // 初始化工具栏 UI
        InitUI();
    }

    /// <summary>
    /// 每帧调用一次
    /// </summary>
    private void Update()
    {
        // 处理工具栏槽位选择控制
        ToolbarSelectControl();
    }

    /// <summary>
    /// 获取当前选中的槽位 UI
    /// </summary>
    /// <returns>返回当前选中的 ToolbarSlotUI 对象</returns>
    public ToolbarSlotUI GetSelectedSlotUI()
    {
        return selectedSlotUI;
    }

    /// <summary>
    /// 初始化工具栏 UI
    /// </summary>
    void InitUI()
    {
        // 初始化槽位 UI 列表，大小为 9
        slotuiList = new List<ToolbarSlotUI>(new ToolbarSlotUI[9]);
        // 获取所有子对象中类型为 ToolbarSlotUI 的组件
        ToolbarSlotUI[] slotuiArray = transform.GetComponentsInChildren<ToolbarSlotUI>();

        // 遍历所有找到的 ToolbarSlotUI 组件并将其添加到列表中
        foreach (ToolbarSlotUI slotUI in slotuiArray)
        {
            slotuiList[slotUI.index] = slotUI;
        }

        // 更新 UI 显示
        UpdateUI();
    }

    /// <summary>
    /// 更新工具栏 UI 显示
    /// </summary>
    public void UpdateUI()
    {
        // 从 InventoryManager 中获取工具栏数据的槽位列表
        List<SlotData> slotdataList = InventoryManager.Instance.toolbarData.slotList;

        // 遍历槽位数据列表并更新对应的 UI
        for (int i = 0; i < slotdataList.Count; i++)
        {
            slotuiList[i].SetData(slotdataList[i]);
        }
    }

    /// <summary>
    /// 处理工具栏槽位选择
    /// </summary>
    void ToolbarSelectControl()
    {
        // 遍历数字键 1-9 的键码
        for (int i = (int)KeyCode.Alpha1; i <= (int)KeyCode.Alpha9; i++)
        {
            // 检查当前数字键是否被按下
            if (Input.GetKeyDown((KeyCode)i))
            {
                // 如果已有槽位被选中，则取消高亮
                if (selectedSlotUI != null)
                {
                    selectedSlotUI.UnHighlight();
                }
                // 计算按下数字键的索引
                int index = i - (int)KeyCode.Alpha1;
                // 更新选中的槽位 UI
                selectedSlotUI = slotuiList[index];
                // 高亮新选中的槽位
                selectedSlotUI.Highlight();
            }
        }
    }
}
