using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// 表示工具栏槽的UI逻辑，继承自SlotUI类
public class ToolbarSlotUI : SlotUI
{
    /// 用于表示槽未高亮时的Sprite
    public Sprite slotLight;
    
    /// 用于表示槽高亮时的Sprite
    public Sprite slotDark;
    
    /// 引用槽的Image组件，用于更改其外观
    private Image image;
    
    /// 在对象创建时初始化Image组件引用
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    
    /// 将槽的外观更改为高亮状
    public void Highlight()
    {
        image.sprite = slotDark;
    }

    /// 将槽的外观更改为未高亮状态
    public void UnHighlight()
    {
        image.sprite = slotLight;
    }
}