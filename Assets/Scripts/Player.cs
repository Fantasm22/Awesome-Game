using UnityEngine;
using UnityEngine.SceneManagement;




public class Player : MonoBehaviour
{

    public float moveSpeed;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (touchPos.x < 0)
            {
                // FIX: Removed AddForce, setting velocity directly for instant speed
                rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
            }
            else
            {
                // FIX: Removed AddForce, setting velocity directly for instant speed
                rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            }
        }


        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }







    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            SceneManager.LoadScene("Game");
        }
    }
}
