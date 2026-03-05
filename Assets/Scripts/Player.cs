using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;




public class Player : MonoBehaviour
{
    public float moveSpeed = 6f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float direction = 0;

        // Touch (Android)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            float touchX = Touchscreen.current.primaryTouch.position.ReadValue().x;

            if (touchX < Screen.width / 2)
                direction = -1;
            else
                direction = 1;
        }

        // Mouse (testing i Unity editor)
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            float mouseX = Mouse.current.position.ReadValue().x;

            if (mouseX < Screen.width / 2)
                direction = -1;
            else
                direction = 1;
        }

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
