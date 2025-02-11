// 使用必要的命名空间
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 定义一个类SlotUI，用于管理游戏中的槽位界面显示，实现IPointerClickHandler接口以处理点击事件
public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    // 槽位的索引号
    public int index = 0;
    // 槽位数据，私有成员，仅在类内部访问
    private SlotData data;

    // 槽位图标图像组件
    public Image iconImage;
    // 物品计数文本组件
    public TextMeshProUGUI countText;

    // 设置槽位数据的方法
    public void SetData(SlotData data)
    {
        this.data = data;
        data.AddListener(OnDataChange);

        UpdateUI();
    }

    // 获取槽位数据的方法
    public SlotData GetData()
    {
        return data;
    }

    // 当槽位数据改变时调用的方法
    private void OnDataChange()
    {
        UpdateUI();
    }

    // 更新槽位界面显示的方法
    private void UpdateUI()
    {
        if (data.item == null)
        {
            iconImage.enabled = false;
            countText.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            countText.enabled = true;
            iconImage.sprite = data.item.sprite;
            countText.text = data.count.ToString();
        }
    }

    // 实现IPointerClickHandler接口的方法，处理槽位点击事件
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemMoveHandler.Instance.OnSlotClick(this);
    }
}