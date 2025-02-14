using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed; // 移动速度，可以在Unity界面中调整
    [SerializeField] private float moveInterval; // 每隔几秒移动一次
    private float timer; // 计时器
    private Vector2 lookDirection = new Vector2(1,0);// 角色面向的方向
    private float moveScale;// 移动比例，用于动画控制
    private Vector2 move;// 当前移动向量
    private Rigidbody2D rigidbody2d; // 刚体组件引用，为了使用刚体进行移动
    private Animator animator; // 动画控制器组件引用，为了播放动画

    private void Start()
    {
        timer = moveInterval;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing!");
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.canWalkingNPC)
        {
            return; 
        }
        
        float horizontal = Random.Range(-1f, 1f); 
        float vertical = Random.Range(-1f, 1f); 
        
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = moveInterval;
            move = new Vector2(horizontal, vertical);

            if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
            {
                lookDirection.Set(move.x, move.y);
                //lookDirection = move;
                lookDirection.Normalize();
                //animator.SetFloat("MoveValue", 1);
            }
        }

        // 动画的控制
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        moveScale = move.magnitude;
        animator.SetFloat("MoveValue", moveScale);
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        // 根据移动速度和当前移动向量更新角色位置
        position = position + moveSpeed * move * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }
    
}