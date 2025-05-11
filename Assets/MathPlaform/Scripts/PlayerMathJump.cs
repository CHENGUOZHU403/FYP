using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMathJump : MonoBehaviour 
{
    [Header("Movement")]
    public float speed = 8f;
    public float jumpForce = 12f;
    public LayerMask platformLayer;

    [Header("UI Elements")]
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI feedbackText;
    public bool freezeMovementDuringQuestion = true;
    private bool isAnsweringQuestion = false;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool isNearPlatform = false;
    private Vector2 groundCheckPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheckPos = new Vector2(0, -0.5f);
    }

    void Update()
    {
       
        if (LevelManager.Instance != null && LevelManager.Instance.IsLevelCompleted)
        {
            rb.velocity = Vector2.zero; 
            return; 
        }

        isGrounded = Physics2D.OverlapCircle(
            (Vector2)transform.position + groundCheckPos, 
            0.2f, 
            platformLayer
        );
        anim.SetBool("Grounded", isGrounded);

        if (isAnsweringQuestion && freezeMovementDuringQuestion) 
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }

        anim.SetFloat("AirSpeedY", rb.velocity.y);
        anim.SetInteger("AnimState", Mathf.Abs(moveX) > 0.1f ? 1 : 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MathPlatform"))
        {
            isAnsweringQuestion = true;
            isNearPlatform = true;
            questionPanel.SetActive(true);
            
            // 获取平台问题并显示
            MathPlatform platform = other.GetComponent<MathPlatform>();
            string question = GenerateQuestion(); // 使用本地生成方法
            questionText.text = question;
            platform.SetQuestion(question);
        }
    }

    string GenerateQuestion()
    {
        int a = Random.Range(1, 5);
        int b = Random.Range(1, 5);
        return $"{a} + {b} = ?";
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MathPlatform"))
        {
            isNearPlatform = false;
            questionPanel.SetActive(false);
            feedbackText.text = "";
        }
    }

    public void SubmitAnswer(int playerAnswer)
    {
        if (!isNearPlatform) return;

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
            
            CancelInvoke(nameof(ClearFeedback));
            Invoke(nameof(ClearFeedback), 2f);
        }

        isAnsweringQuestion = false;
        questionPanel.SetActive(false);
    }

    void ClearFeedback() => feedbackText.text = "";
}