using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMathJump : MonoBehaviour 
{
    [Header("Movement")]
    public float speed = 8f;
    public float jumpForce = 12f;
    public LayerMask platformLayer;

    [Header("Math UI")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI feedbackText; // 新增：正确/错误反馈

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private Vector2 groundCheckPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheckPos = new Vector2(0, -0.5f);
    }

    void Update()
    {
        // 地面检测
        isGrounded = Physics2D.OverlapCircle(
            (Vector2)transform.position + groundCheckPos, 
            0.2f, 
            platformLayer
        );
        anim.SetBool("Grounded", isGrounded);

        // 移动控制
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        // 跳跃（仅在地面时生效）
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }

        // 动画控制
        anim.SetFloat("AirSpeedY", rb.velocity.y);
        anim.SetInteger("AnimState", Mathf.Abs(moveX) > 0.1f ? 1 : 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MathPlatform"))
        {
            questionText.text = other.GetComponent<MathPlatform>().GetQuestion();
        }
    }

    // 提供给UI按钮的答案提交方法
    public void SubmitAnswer(int playerAnswer)
    {
        var platform = Physics2D.OverlapCircle(
            (Vector2)transform.position + groundCheckPos, 
            0.3f, 
            platformLayer
        )?.GetComponent<MathPlatform>();

        if (platform != null)
        {
            bool isCorrect = platform.CheckAnswer(playerAnswer);
            feedbackText.text = isCorrect ? "Correct!" : "Wrong!";
            feedbackText.color = isCorrect ? Color.green : Color.red;
            
            // 2秒后清除反馈
            CancelInvoke(nameof(ClearFeedback));
            Invoke(nameof(ClearFeedback), 2f);
        }
    }

    void ClearFeedback() => feedbackText.text = "";
}