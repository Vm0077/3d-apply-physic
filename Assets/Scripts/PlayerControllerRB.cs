using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRB : MonoBehaviour {

  public Rigidbody rb;
  [SerializeField]
  public float speed = 5f;
  [SerializeField]
  public Transform cam;
  // public Transform Feet;

  [SerializeField]
  public Vector3 _movement;
  [SerializeField]
  public Quaternion _rotation;
  public bool grounded;
  public LayerMask whatIsGround;

  [SerializeField]
  public float jumpHeight = 10.0f;

  [SerializeField]
  public Transform Feet;
  void Awake() {
    rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;
  }
  void Start() {}

  void Update() {
    checkIfGrounded();
    HandleInput();
  }
  void FixedUpdate() { HandleMovement(); }

  void HandleInput() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    _movement = new Vector3(horizontal, 0f, vertical).normalized;

    if (Input.GetButtonDown("Jump")) {
      Jump();
    }
  }

  void Jump() { rb.AddForce(Vector3.up * 10, ForceMode.Impulse); }

  void HandleMovement() {
      float targetAngle =
          Mathf.Atan2(_movement.x, _movement.z) * Mathf.Rad2Deg +
          cam.eulerAngles.y;
      if(_movement.magnitude > 0 ) _rotation = Quaternion.Euler(0f, targetAngle, 0f);
      Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
      Vector3 move = moveDir * _movement.magnitude * speed;
      transform.rotation = _rotation;
      rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
  }

  void CounterMovement() {}

  void checkIfGrounded() {
    grounded =  Physics.CheckSphere(
            Feet.position,1.5f,whatIsGround
        );
    Debug.Log(grounded);
  }
}
