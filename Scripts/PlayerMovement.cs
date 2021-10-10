using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator; 
    public Vector3 movementDirection = Vector3.zero;
    public float flySpeed = 1f;
    public bool grounded = false;
    public AudioClip landSound;
    public AudioClip jumpSound;
    public AudioSource land; 
    public AudioSource jump;
    
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("OSTintro");
        land = GetComponent<AudioSource> ();
        jump = GetComponent<AudioSource> ();
        movementDirection = new Vector3(0, flySpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!grounded) {
            transform.position = transform.position + movementDirection * Time.deltaTime;
        } else {
            movementDirection = GetTravelDirection() * flySpeed;
            if (Input.GetMouseButtonDown(0)) {
                animator.SetBool("click", true);
                FindObjectOfType<AudioManager>().Play("jump");
                transform.SetParent(null);
                grounded = false;
            }
        }
        
    }
    Quaternion CalculateRotationToMatchTravelDirection(Vector3 currentPosition, Vector3 targetPosition) {
            Vector3 direction = targetPosition - currentPosition;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle + 90f, Vector3.forward);
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "asteroid") {
            grounded = true;
            transform.SetParent(collision.transform);
            FindObjectOfType<AudioManager>().Play("land");
        }
    }
    Vector3 GetTravelDirection() {
        Vector3 newDirection = (transform.parent.transform.position - transform.position) * -1;
        newDirection.z = 0;
        newDirection = Quaternion.AngleAxis(90, Vector3.forward) * newDirection;
        return newDirection;
    }
}
