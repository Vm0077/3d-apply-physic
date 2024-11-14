using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRB : MonoBehaviour {

  public Rigidbody rb;
  [SerializeField]
  public float speed = 5f;
  [SerializeField]
  public Transform cam;
  Vector3 _movement;
  public bool isGround;
  void Awake() {
    rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;
  }
  void Start() {}

  void Update() {
    isGround = checkIfGrounded();

    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    _movement = new Vector3(horizontal, 0f, vertical).normalized;
    if (isGround && _movement.magnitude < 0.1f) {
      HandleMovement();
    }
  }
  void FixedUpdate() {
  }
  void HandleMovement() {
    Vector3 Move = transform.TransformDirection(_movement);
    float targetAngle = Mathf.Atan2(Move.x,Move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
    transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    Vector3 moveDir = Quaternion.Euler(0f,targetAngle, 0f) * Vector3.forward;
    Vector3 move = moveDir.normalized  * speed;
    Debug.Log(move);
    rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
  }
  bool checkIfGrounded() {
    return Physics.Raycast(transform.position, Vector3.down,
                           transform.localScale.y / 2 + 2f);
  }
}
