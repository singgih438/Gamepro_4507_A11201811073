using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    Animator anim;
    bool facingRight = true;
    float velX;

    public float jumpValue;
    int health = 1;
    bool isHurt, isDead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
            rb.AddForce(Vector2.up * jumpValue);

        AnimationState();

        if (!isDead)

        velX = Input.GetAxisRaw("Horizontal") * speed;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(velX, rb.velocity.y);
    }

    void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        Vector3 localScale = transform.localScale;
        if (velX > 0)
        {
            facingRight = true;
        }
        else if (velX < 0)
        {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || (!facingRight) && (localScale.x > 0))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }

    void AnimationState()
    {
        if (velX == 0)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isJump", false);
        }
        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }
        if (rb.velocity.y > 0)
            anim.SetBool("isJump", true);
        if (rb.velocity.y > 0)
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", true);
        if (Mathf.Abs(velX) == 5)
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.name.Equals ("Lizard"))
        {
            health -= 1;
        }
        if (col.gameObject.name.Equals ("Lizard") && health > 0)
        {
            anim.SetTrigger("isHurt");
            StartCoroutine("Hurt");
        }
        else
        {
            velX = 0;
            isDead = true;
            anim.SetTrigger("isDead");
        }
    }

    IEnumerator Hurt ()
    {
        isHurt = true;
        rb.velocity = Vector2.zero;

        if (facingRight)
            rb.AddForce(new Vector2(-100f, 100f));
        else
            rb.AddForce(new Vector2(100f, 100f));
        yield return new WaitForSeconds(0.5f);

        isHurt = false;
    }
}
