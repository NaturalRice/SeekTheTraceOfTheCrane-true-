using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏总管理
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject battleGo;//战斗场景游戏物体
    //luna属性
    public int lunaHP;//最大生命值
    public float lunaCurrentHP;//Luna的当前生命值
    public int lunaMP;//最大蓝量
    public float lunaCurrentMP;//luna的当前蓝量
    //Monster属性
    public int monsterCurrentHP;//怪物当前血量
    public int dialogInfoIndex;
    public bool canControlLuna;//玩家是否可以移动
    public bool canWalkingNPC;//NPC是否可以活动
    public bool hasPetTheDog;
    public int candleNum;
    public int killNum;
    public GameObject monstersGo;
    public NPCDialog npc;
    public bool enterBattle;
    public GameObject battleMonsterGo;
    public AudioSource audioSource;
    public AudioClip normalClip;
    public AudioClip battleClip;

    private void Awake()
    {
        Instance = this;
        lunaCurrentHP = 100;
        lunaCurrentMP = 100;
        lunaHP =100;
        lunaMP =100;
        monsterCurrentHP = 50;
        canControlLuna = true; // 确保玩家在游戏开始时可以控制
    }

    private void Update()
    {
        if (!enterBattle)
        {
            if (lunaCurrentMP <= 100)
            {
                AddOrDecreaseMP(Time.deltaTime);
            }
            if (lunaCurrentHP <= 100)
            {
                AddOrDecreaseHP(Time.deltaTime);
            }
        }
    }

    //public void ChangeHeath(int amount)
    //{
    //    lunaCurrentHP = Mathf.Clamp(lunaCurrentHP + amount, 0, lunaHP);
    //    Debug.Log(lunaCurrentHP + "/" + lunaHP);
    //}

    public void EnterOrExitBattle(bool enter = true, int addKillNum = 0)
    {
        UIManager.Instance.ShowOrHideBattlePanel(enter);
        battleGo.SetActive(enter);
        if (!enter)//非战斗状态，或者说战斗结束
        {
            killNum += addKillNum;
            if (addKillNum > 0)
            {
                DestoryMonster();
            }
            monsterCurrentHP = 50;
            PlayMusic(normalClip);
            if (lunaCurrentHP <= 0)
            {
                lunaCurrentHP = 100;
                lunaCurrentMP = 0;
                battleMonsterGo.transform.position += new Vector3(0, 2, 0);
            }
        }
        else
        {
            PlayMusic(battleClip);
        }
        enterBattle = enter;
    }
    public void DestoryMonster()
    {
        Destroy(battleMonsterGo);
    }
    public void SetMonster(GameObject go)
    {
        battleMonsterGo = go;
    }

    /// <summary>
    /// Luna血量改变
    /// </summary>
    /// <param name="value"></param>
    public void AddOrDecreaseHP(float value)
    {
        lunaCurrentHP += value;
        if (lunaCurrentHP>=lunaHP)
        {
            lunaCurrentHP = lunaHP;
        }
        if (lunaCurrentHP<=0)
        {
            lunaCurrentHP = 0;
        }
        UIManager.Instance.SetHPValue(lunaCurrentHP/lunaHP);
    }
    /// <summary>
    /// Luna蓝量改变
    /// </summary>
    /// <param name="value"></param>
    public void AddOrDecreaseMP(float value)
    {
        lunaCurrentMP += value;
        if (lunaCurrentMP >= lunaMP)
        {
            lunaCurrentMP = lunaMP;
        }
        if (lunaCurrentMP <= 0)
        {
            lunaCurrentMP = 0;
        }
        UIManager.Instance.SetMPValue(lunaCurrentMP / lunaMP);
    }
    /// <summary>
    /// 是否可以使用相关技能
    /// </summary>
    /// <param name="value">技能耗费蓝量</param>
    /// <returns></returns>
    public bool CanUsePlayerMP(int value)
    {
        return lunaCurrentMP >= value;
    }
    /// <summary>
    /// Monster血量改变
    /// </summary>
    /// <param name="value"></param>
    public int AddOrDecreaseMonsterHP(int value)
    {
        monsterCurrentHP += value;
        return monsterCurrentHP;
    }
    /// <summary>
    /// 显示怪物
    /// </summary>
    public void ShowMonsters()
    {
        if (!monstersGo.activeSelf)
        {
            monstersGo.SetActive(true);
        }
    }


    public void PlayMusic(AudioClip audioClip)
    {
        if (audioSource.clip != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}