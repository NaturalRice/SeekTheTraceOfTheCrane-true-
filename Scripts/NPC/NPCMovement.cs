using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private float moveSpeed = 2f; // 移动速度，可以在Unity界面中调整
    private Vector2 targetPosition; // 目标位置
    private float moveInterval = 2f; // 每2秒移动一次
    private float timer; // 计时器

    private void Start()
    {
        // 初始化计时器
        timer = moveInterval;
    }

    private void Update()
    {
        if (GameManager.Instance.canWalkingNPC)
        {
            // 移动逻辑
            Move();
        }
    }

    private void Move()
    {
        // 计时器递减
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // 重置计时器
            timer = moveInterval;

            // 随机生成一个方向向量
            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            // 计算目标位置
            targetPosition = (Vector2)transform.position + direction * Random.Range(1f, 3f);

            // 移动NPC
            StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    private IEnumerator MoveToPosition(Vector2 target)
    {
        while (Vector2.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}