using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Image hpMaskImage;
    public Image mpMaskImage;
    private float originalSize; // 血条原始宽度
    public GameObject battlePanelGo;
    
    public GameObject TalkPanelGo1;
    public GameObject TalkPanelGo2;
    public GameObject TalkPanelGo3;
    public GameObject TalkPanelGo4;
    public GameObject TalkPanelGo5;
    public GameObject TalkPanelGo6;
    
    public Sprite[] characterSprites; // 修改: 修正拼写错误
    public Text nameText;
    public Text contentText;

    void Awake()
    {
        Instance = this;
        originalSize = hpMaskImage.rectTransform.rect.width;
        SetHPValue(1);
    }

    /// <summary>
    /// 血条UI填充显示
    /// </summary>
    /// <param name="fillPercent">填充百分比</param>
    public void SetHPValue(float fillPercent)
    {
        hpMaskImage.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, fillPercent * originalSize);
    }

    /// <summary>
    /// 蓝条UI填充显示
    /// </summary>
    /// <param name="fillPercent">填充百分比</param>
    public void SetMPValue(float fillPercent)
    {
        mpMaskImage.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, fillPercent * originalSize);
    }

    public void ShowOrHideBattlePanel(bool show)
    {
        battlePanelGo.SetActive(show);
    }
    
    void Update()
    {
        // 检查玩家是否按下了Esc键
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 关闭对话面板
            TalkPanelGo1.SetActive(false);
            TalkPanelGo2.SetActive(false);
            TalkPanelGo3.SetActive(false);
            TalkPanelGo4.SetActive(false);
            TalkPanelGo5.SetActive(false);
            TalkPanelGo6.SetActive(false);
            // 允许玩家继续控制角色
            GameManager.Instance.canControlLuna = true;
        }
    }

    /// <summary>
    /// 显示对话内容（包含人物的切换，名字的更换，对话内容的更换）
    /// </summary>
    /// <param name="name"></param>
    public void ShowDialog(string name)
    {
        if(name == "程慕清")
            TalkPanelGo1.SetActive(true);
        else if(name == "参观者1")
            TalkPanelGo2.SetActive(true);
        else if(name == "参观者2")
            TalkPanelGo3.SetActive(true);
        else if(name == "参观者3")
            TalkPanelGo4.SetActive(true);
        else if(name == "参观者4")
            TalkPanelGo5.SetActive(true);
        else if(name == "程老")
            TalkPanelGo6.SetActive(true);
    }
}