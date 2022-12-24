using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossCuoi : Enemy
{
    public int health = 100;
    public Slider healbar;
    [Header("AngryPig spesific")]
    [SerializeField] private float agroSpeed;
    [SerializeField] private float shockTime;
    private float shockTimeCounter;



    protected override void Start()
    {
        base.Start();
        invincible = true;

    }
    void Update()
    {

        CollisionChecks();
        AnimatorControllers();

        if (!playerDetection)
        {
            WalkAround();
            return;
        }

        if (playerDetection.collider.GetComponent<Player>() != null && playerDetection)
            aggresive = true;

        if (!aggresive)
        {
            WalkAround();
        }
        else
        {
            if (!groundDetected)
            {
                aggresive = false;
                Flip();
            }
            rb.velocity = new Vector2(agroSpeed * facingDirection, rb.velocity.y);

            if (wallDetected && invincible)
            {
                invincible = false;
                shockTimeCounter = shockTime;
            }

            if (shockTimeCounter <= 0 && !invincible)
            {
                invincible = true;
                Flip();
                aggresive = false;
            }
            shockTimeCounter -= Time.deltaTime;
        }


    }
    public override void Damage()
    {
        if (health > 0)
        {
            health -= 10;
        }
        if (health < 0)
        {
            health = 0;
        }
        healbar.value = health;

        if (health <= 0)
        {
            Debug.Log("dame");
            base.Damage();
        }
    }
    private void AnimatorControllers()
    {
        anim.SetBool("invincible", invincible);
        anim.SetFloat("xVelocity", rb.velocity.x);
    }
}
