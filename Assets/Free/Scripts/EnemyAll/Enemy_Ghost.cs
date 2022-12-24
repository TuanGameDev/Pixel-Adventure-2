using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy
{
    [Header("Ghost spesifics")]
    [SerializeField] private float activeTime;
    private float activeTimeCounter = 4;

    
    private SpriteRenderer sr;

    [SerializeField] private float[] xOffset;

    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();

        aggresive = true;
        invincible = true;
      
       
    }

    void Update()
    {
        if (player == null)
        {
            anim.SetTrigger("desappear");
            return;
        }
        activeTimeCounter -= Time.deltaTime;
        idleTimeCounter -= Time.deltaTime;

        if (activeTimeCounter > 0)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (activeTimeCounter < 0 && idleTimeCounter < 0 && aggresive)
        {
            anim.SetTrigger("desappear");
            aggresive = false;
            idleTimeCounter = idleTime;
        }
        if (activeTimeCounter < 0 && idleTimeCounter < 0 && !aggresive)
        {
            ChoosePosition();
            anim.SetTrigger("appear");
            aggresive = true;
            activeTimeCounter = activeTime;

        }
        FlipController();
    }
    private void FlipController()
    {
        if (player == null)
            return;
        if (facingDirection == -1 && transform.position.x < player.transform.position.x)
            Flip();
        else if (facingDirection == 1 && transform.position.x > player.transform.position.x)
            Flip();
    }
    private void ChoosePosition()
    {
        float _xOffset = xOffset[Random.Range(0, xOffset.Length)];
        float yOffset = Random.Range(-10, 10);
        transform.position = new Vector2(player.transform.position.x + _xOffset, player.transform.position.y + yOffset);
    }

    public void Desappear() =>sr.enabled = false;


    public void Appear() => sr.enabled = true;


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(aggresive)
        base.OnTriggerEnter2D(collision);
    }
}

