using UnityEngine;
using TMPro;

public class HeroKnight_MathPlatform : MonoBehaviour 
{
    [Header("Movement")]
    [SerializeField] float m_speed = 10.0f;
    [SerializeField] float m_jumpForce = 12.0f; // 调高跳跃力适应平台玩法

    [Header("Components")]
    [SerializeField] Animator m_animator;
    [SerializeField] Rigidbody2D m_body2d;
    [SerializeField] LayerMask m_platformLayer; // 用于检测平台的Layer

    [Header("Math Interaction")]
    [SerializeField] TextMeshProUGUI m_questionText; // 关联UI题目文本

    private bool m_grounded = false;
    private int m_facingDirection = 1;
    private float m_groundCheckRadius = 0.2f;
    private Vector2 m_groundCheckOffset = new Vector2(0, -0.5f); // 底部检测点偏移

    void Update()
    {
        // 检测是否在地面
        CheckGrounded();

        // 水平移动输入
        float inputX = Input.GetAxis("Horizontal");
        HandleMovement(inputX);

        // 跳跃输入（仅在地面时生效）
        if (Input.GetKeyDown(KeyCode.Space) && m_grounded)
        {
            Jump();
        }

        // 更新动画状态
        UpdateAnimations(inputX);
    }

    void CheckGrounded()
    {
        Vector2 checkPos = (Vector2)transform.position + m_groundCheckOffset;
        m_grounded = Physics2D.OverlapCircle(checkPos, m_groundCheckRadius, m_platformLayer);
        m_animator.SetBool("Grounded", m_grounded);
    }

    void HandleMovement(float inputX)
    {
        // 水平移动
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // 转向
        if (inputX > 0 && m_facingDirection != 1)
        {
            FlipSprite(false);
            m_facingDirection = 1;
        }
        else if (inputX < 0 && m_facingDirection != -1)
        {
            FlipSprite(true);
            m_facingDirection = -1;
        }
    }

    void Jump()
    {
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        m_animator.SetTrigger("Jump");
        m_grounded = false;
        m_animator.SetBool("Grounded", false);
    }

    void FlipSprite(bool flipX)
    {
        GetComponent<SpriteRenderer>().flipX = flipX;
    }

    void UpdateAnimations(float inputX)
    {
        // 跑步/空闲状态
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            m_animator.SetInteger("AnimState", 0);
        }

        // 空中速度（用于下落动画）
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);
    }

    // === 数学平台交互部分 ===
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MathPlatform"))
        {
            MathPlatform platform = other.GetComponent<MathPlatform>();
            if (platform != null)
            {
                // 显示题目（假设题目管理器已处理生成）
                m_questionText.text = platform.GetQuestion();
            }
        }
    }

    // 提供给外部调用的答案检查方法
    public bool SubmitAnswer(int answer)
    {
        // 实际逻辑应由MathQuestionManager处理
        Debug.Log($"Player submitted answer: {answer}");
        return false; // 暂留接口
    }
}