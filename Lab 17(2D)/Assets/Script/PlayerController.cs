﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public int healthCount;
    public int coinCount;

    private Rigidbody2D rb;
    private Animator animator;

    public GameObject Healthtxt;
    public GameObject Cointxt;

    private AudioSource audioSource;
    public AudioClip[] AudioClipArr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float hVelocity = 0;
        float vVelocity = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hVelocity = -moveSpeed;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            hVelocity = moveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetFloat("xVelocity", 0);
            
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            vVelocity = jumpForce;
            animator.SetTrigger("JumpTrig");
            audioSource.PlayOneShot(AudioClipArr[3]);


        }

        hVelocity = Mathf.Clamp(rb.velocity.x + hVelocity, -5, 5);

        rb.velocity = new Vector2(hVelocity, rb.velocity.y + vVelocity);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Mace")
        {
            healthCount -= 10;
            Healthtxt.GetComponent<Text>().text = "Health: " + healthCount;
            audioSource.PlayOneShot(AudioClipArr[1]);
        }
        if(collision.gameObject.tag == "Coins")
        {
            coinCount++;
            Cointxt.GetComponent<Text>().text = "Coins: " + coinCount;
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(AudioClipArr[2]);
        }
    }
}
