using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 定义一个槽位界面类，用于在界面上表示一个物品槽位
public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    // 槽位的索引，用于标识槽位的位置
    public int index = 0;
    // 槽位的数据，包含槽位中的物品信息
    private SlotData data;

    // 槽位中物品的图标
    public Image iconImage;
    // 槽位中物品的数量文本
    public TextMeshProUGUI countText;

    /// <summary>
    /// 设置槽位的数据
    /// </summary>
    /// <param name="data">要设置的槽位数据</param>
    public void SetData(SlotData data)
    {
        this.data = data;
        data.AddListener(OnDataChange);

        UpdateUI();
    }

    /// <summary>
    /// 获取槽位的数据
    /// </summary>
    /// <returns>当前槽位的数据</returns>
    public SlotData GetData()
    {
        return data;
    }

    /// <summary>
    /// 当槽位数据变化时调用此方法来更新UI
    /// </summary>
    private void OnDataChange()
    {
        UpdateUI();
    }

    /// <summary>
    /// 更新槽位的UI显示
    /// </summary>
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

    /// <summary>
    /// 当指针点击槽位时调用此方法
    /// </summary>
    /// <param name="eventData">指针事件数据</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemMoveHandler.Instance.OnSlotClick(this);
    }
}