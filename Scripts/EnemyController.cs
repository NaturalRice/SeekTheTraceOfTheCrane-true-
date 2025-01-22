using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敌人控制器类，用于控制敌人的移动和行为
public class EnemyController : MonoBehaviour
{
    // 控制敌人是垂直还是水平移动
    public bool vertical;
    // 敌人移动的速度
    public float speed = 5;
    // 敌人移动的方向，1表示右或上，-1表示左或下
    private int direction = 1;
    // 敌人改变方向的时间间隔
    public float changeTime = 5;
    // 计时器，用于计算方向改变的时间
    private float timer;
    // 刚体组件引用，为了使用刚体进行移动
    private Rigidbody2D rigidbody2d;
    // 动画控制器组件引用，为了播放动画
    private Animator animator;

    // 在游戏开始前初始化组件
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // 每帧更新逻辑，主要用于控制敌人的方向改变
    void Update()
    {
        if (GameManager.Instance.enterBattle)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    // 固定更新逻辑，主要用于控制敌人的移动
    private void FixedUpdate()
    {
        if (GameManager.Instance.enterBattle)
        {
            return;
        }
        Vector3 pos = rigidbody2d.position;
        if (vertical)//垂直轴向移动
        {
            animator.SetFloat("LookX", 0);
            animator.SetFloat("LookY", direction);
            pos.y = pos.y + speed * direction * Time.fixedDeltaTime;
        }
        else//水平轴向移动
        {
            animator.SetFloat("LookX", direction);
            animator.SetFloat("LookY", 0);
            pos.x = pos.x + speed * direction * Time.fixedDeltaTime;
        }
        rigidbody2d.MovePosition(pos);
    }

    // 碰撞检测，当敌人与玩家碰撞时触发
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.EnterOrExitBattle();
            GameManager.Instance.SetMonster(gameObject);
        }
    }
}