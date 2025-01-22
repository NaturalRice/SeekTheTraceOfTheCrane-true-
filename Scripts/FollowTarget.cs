using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FollowTarget类用于控制摄像机跟随玩家移动
public class FollowTarget : MonoBehaviour
{
    // 玩家对象的Transform组件，通过此引用获取玩家的位置信息
    public Transform player; 
    // 摄像机与玩家之间的偏移量，用于保持摄像机与玩家的相对位置
    private Vector3 offset; 

    // Start方法在脚本实例被启用后第一次更新之前调用
    void Start()
    {
        // 如果Inspector中没有设置player，则通过标签查找
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        // 计算初始偏移量
        offset = transform.position - player.position;
    }

    // FixedUpdate方法用于物理计算和路径更新，这里用于更新摄像机位置
    private void FixedUpdate()
    {
        // 使用差值计算平滑地更新摄像机位置
        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime);
    }
}