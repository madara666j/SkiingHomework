using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float acceleration = 100, turnSpeed = 100,
        minSpeed = 0, maxSpeed = 500, minAcceleration = -100, maxAcceleration = 300;
    [SerializeField] private KeyCode leftInput, rightInput;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private AudioClip crashSound;
    private AudioSource audioSource;

    private float speed = 0;
    private Rigidbody rb;
    private Animator animator;

    private bool isStunned = false;
    [SerializeField] private float stunDuration = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        float angle = Mathf.Abs(transform.eulerAngles.y - 180);
        acceleration = Remap(0, 90, maxAcceleration, minAcceleration, angle);
        speed += acceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        Vector3 velocity = speed * transform.forward * Time.fixedDeltaTime;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        animator.SetFloat("playerSpeed", speed);
    }

    void Update()
    {
        if (!canMove) return;

        bool isGrounded = Physics.Linecast(transform.position, groundPoint.position, groundLayer);
        if (isGrounded)
        {
            if (Input.GetKey(leftInput) && transform.eulerAngles.y < 269)
            {
                transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0), Space.Self);
            }
            if (Input.GetKey(rightInput) && transform.eulerAngles.y > 91)
            {
                transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0), Space.Self);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            Debug.Log("Player crashed with the rock!");
            if (crashSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(crashSound);
            }

            // Calculate horizontal knockback direction
            Vector3 hitDirection = transform.position - collision.transform.position;
            hitDirection.y = 0f; // prevent upward launch
            hitDirection = hitDirection.normalized;

            // Reset full velocity to prevent leftover vertical force
            rb.velocity = Vector3.zero;

            // Apply sideways knockback
            rb.AddForce(hitDirection * 400f, ForceMode.VelocityChange);

            // Start stun effect
            StartCoroutine(StunPlayer());
        }
    }

    private IEnumerator StunPlayer()
    {
        isStunned = true;

        // Optional slow motion effect
        Time.timeScale = 0.4f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(stunDuration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isStunned = false;
    }

    private float Remap(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newValue = (((oldValue - oldMin) / oldRange) * newRange + newMin);
        return newValue;
    }

    private bool canMove = true;

    public void StopMovement()
    {
        canMove = false;
        rb.velocity = Vector3.zero;
        speed = 0;
    }

}
