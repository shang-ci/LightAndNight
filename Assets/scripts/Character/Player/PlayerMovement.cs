using UnityEngine;
using UnityEngine.InputSystem; // 引入新命名空间

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;

    // 引用 Input Actions
    private PlayerControls controls;

    // 场景边界
    private float minX, maxX, minY, maxY;

    public float boundDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 获取场景边界（假设场景边界为摄像机视口边界）
        Camera mainCamera = Camera.main;
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;
        minY = screenBottomLeft.y;
        maxY = screenTopRight.y;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;
    }

    // 输入事件回调
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // 检测移动方向并反转图片
        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true; // 向左移动时反转图片
        }
        else if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false; // 向右移动时恢复图片
        }
    }

    private void FixedUpdate()
    {
        // 应用移动
        rb.velocity = moveInput * moveSpeed;

        // 检测边界并反弹
        Vector3 newPosition = rb.position;

        if (newPosition.x < minX)
        {
            newPosition.x = minX;
            rb.velocity = new Vector2(boundDistance, rb.velocity.y); // 向右反弹10px
        }
        else if (newPosition.x > maxX)
        {
            newPosition.x = maxX;
            rb.velocity = new Vector2(-boundDistance, rb.velocity.y); // 向左反弹10px
        }

        if (newPosition.y < minY)
        {
            newPosition.y = minY;
            rb.velocity = new Vector2(rb.velocity.x, boundDistance); // 向上反弹10px
        }
        else if (newPosition.y > maxY)
        {
            newPosition.y = maxY;
            rb.velocity = new Vector2(rb.velocity.x, -boundDistance); // 向下反弹10px
        }

        rb.position = newPosition;
    }

}