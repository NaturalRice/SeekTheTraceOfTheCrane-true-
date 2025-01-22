using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包用户界面类，负责管理和显示背包中的物品
/// </summary>
public class BackpackUI : MonoBehaviour
{
    /// <summary>
    /// 存储背包UI的父对象
    /// </summary>
    private GameObject parentUI;

    /// <summary>
    /// 背包中所有槽的UI列表
    /// </summary>
    public List<SlotUI> slotuiList;

    /// <summary>
    /// 初始化Awake方法，在脚本实例化时调用
    /// </summary>
    private void Awake()
    {
        // 查找并缓存背包UI的父对象
        parentUI = transform.Find("ParentUI").gameObject;
    }

    /// <summary>
    /// 初始化Start方法，在脚本启用时调用
    /// </summary>
    private void Start()
    {
        // 初始化UI
        InitUI();
    }

    /// <summary>
    /// 每帧调用的Update方法
    /// </summary>
    void Update()
    {
        // 当按下Esc键时，切换UI的显示状态
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUI();
        }
    }

    /// <summary>
    /// 初始化UI方法，包括槽的初始化和数据绑定
    /// </summary>
    void InitUI()
    {
        // 初始化槽UI列表，大小为24
        slotuiList = new List<SlotUI>(new SlotUI[24]);  
        // 获取所有子对象中的SlotUI组件
        SlotUI[] slotuiArray = transform.GetComponentsInChildren<SlotUI>();

        // 遍历所有找到的SlotUI组件，并将其添加到列表中
        foreach(SlotUI slotUI in slotuiArray)
        {
            slotuiList[slotUI.index] = slotUI;
        }

        // 更新UI显示
        UpdateUI();
    }

    /// <summary>
    /// 更新UI方法，根据背包数据更新UI显示
    /// </summary>
    public void UpdateUI()
    {
        // 获取背包中的所有槽数据
        List<SlotData> slotdataList = InventoryManager.Instance.backpack.slotList;
        
        // 遍历所有槽数据，并更新对应的UI显示
        for(int i = 0; i < slotdataList.Count; i++)
        {
            slotuiList[i].SetData(slotdataList[i]);
        }
    }

    /// <summary>
    /// 切换UI显示状态的方法
    /// </summary>
    private void ToggleUI()
    {
        // 设置父UI对象的激活状态为其当前状态的相反值
        parentUI.SetActive(!parentUI.activeSelf);
    }

    /// <summary>
    /// 当点击关闭按钮时调用的方法
    /// </summary>
    public void OnCloseClick()
    {
        // 调用切换UI显示状态的方法
        ToggleUI();
    }
}
