using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    private bool isJumping = false;
    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    //Movement of the player
    public InputAction jumpAction;

    // Start is called before the first frame update
    void Start()
    {
        // Gravity Modifier
        Physics.gravity *= gravityModifier;

        // Enable the input action
        jumpAction.Enable();

        //audio
        playerAudio = GetComponent<AudioSource>();

        // Initialize the playerRb
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }


    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (jumpAction.triggered && !gameOver)
        {
            Jump();

        }
    }
    void Jump()
    {
        playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        isJumping = true;
    }


    private void OnCollisionEnter(Collision collison)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (collison.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(collison.gameObject);

        }

        // if player collides with money, fireworks
        else if (collison.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(collison.gameObject);
        }
        if (collison.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }

    }
}
