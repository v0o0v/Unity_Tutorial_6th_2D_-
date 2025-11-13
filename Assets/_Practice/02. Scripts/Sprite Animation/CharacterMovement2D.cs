using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    private Rigidbody2D characterRb;
    public SpriteRenderer[] renderers;
    
    public float moveSpeed = 5f;
    public float jumpPower = 10f;
    private float h;
    private float v;

    void Start()
    {
        characterRb = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>(); // 자신을 포함한 SpriteRenderer 컴포넌트가 있는 오브젝트를 모두 가져오는 기능
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal"); // A, D 키보드에 대한 입력값
        v = Input.GetAxis("Vertical"); // W, S 키보드에 대한 입력값

        ChangeSprite();
        // Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void ChangeSprite()
    {
        // if (h == 0) // 키를 누르지 않았을 때
        // {
        //     renderers[0].gameObject.SetActive(true);
        //     renderers[1].gameObject.SetActive(false);
        // }
        // else // 키를 눌렀을 때
        // {
        //     renderers[0].gameObject.SetActive(false);
        //     renderers[1].gameObject.SetActive(true);
        // }

        renderers[0].gameObject.SetActive(h == 0);
        renderers[1].gameObject.SetActive(h != 0);

        if (h < 0) // 왼쪽 바라보는 상태
        {
            // renderers[0].flipX = true;
            // renderers[1].flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h > 0)
        {
            // renderers[0].flipX = false;
            // renderers[1].flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        // // 삼항 연산자 활용
        // if (h != 0)
        // {
        //     Vector3 scale = h < 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        //
        //     transform.localScale = scale;
        // }

        // if (h != 0)
        // {
        //     renderers[0].flipX = h < 0;
        //     renderers[1].flipX = h < 0;
        //     renderers[2].flipX = h < 0;
        // }
    }

    private void Move()
    {
        characterRb.linearVelocityX = h * moveSpeed; // x축으로 속도를 변경하는 기능
        characterRb.linearVelocityY = v * moveSpeed; // y축으로 속도를 변경하는 기능
        
        // characterRb.linearVelocity = new Vector2(h, v) * moveSpeed;
    }

    // private void Jump()
    // {
    //     if (Input.GetButtonDown("Jump"))
    //     {
    //         characterRb.AddForceY(jumpPower, ForceMode2D.Impulse);
    //     }
    // }
}