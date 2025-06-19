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
    
    public GameObject TalkPanelGo0;//玩家
    public GameObject TalkPanelGo1;//程慕清
    public GameObject TalkPanelGo2;//参观者1
    public GameObject TalkPanelGo3;//参观者2
    public GameObject TalkPanelGo4;//参观者3
    public GameObject TalkPanelGo5;//参观者4
    public GameObject TalkPanelGo6;//程老

    void Awake()
    {
        Instance = this;
        originalSize = hpMaskImage.rectTransform.rect.width;
        SetHPValue(1);
        TalkPanelGo0.SetActive(false); // 确保玩家面板在游戏开始时没有被激活
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
            if(GameManager.Instance.canControlLuna)
                TalkPanelGo0.SetActive(!TalkPanelGo0.activeSelf);//玩家面板可随时打开关闭

        // 检查玩家是否按下了Delete键
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            QuitGame();
        }
        
        if (Input.GetMouseButtonDown(1))
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
            //允许NPC活动
            GameManager.Instance.canWalkingNPC = true;
        }
    }

    /// <summary>
    /// 显示对话内容（包含人物的切换，名字的更换，对话内容的更换）
    /// </summary>
    /// <param name="name"></param>
    public void ShowDialog(string name)
    {
        switch (name)
        {
            case "程慕清":
                TalkPanelGo1.SetActive(true);
                break;
            case "参观者1":
                TalkPanelGo2.SetActive(true);
                break;
            case "参观者2":
                TalkPanelGo3.SetActive(true);
                break;
            case "参观者3":
                TalkPanelGo4.SetActive(true);
                break;
            case "参观者4":
                TalkPanelGo5.SetActive(true);
                break;
            case "程老":
                TalkPanelGo6.SetActive(true);
                break;
            default:
                TalkPanelGo0.SetActive(true);
                break;
        }
    }
    
    /// <summary>
    /// 退出游戏
    /// </summary>
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 在编辑器模式下停止播放
#else
    Application.Quit(); // 在发布的游戏中退出
#endif
    }
}