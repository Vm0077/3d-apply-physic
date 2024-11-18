using UnityEngine;

public class PlayerController : MonoBehaviour {

  public CharacterController controller;
  public Vector3 _playerVelocity;
  public Vector3 _movement;
  // camera
  public Transform cam;
  public bool isGround;
  public bool isDash;
  public float dashDistance = 3f;
  public Vector3 markPoint;
  public float gravityValue = -9.81f;
  public float jumpHeigth = 1.0f;

  [SerializeField]
  public float speed = 5f;
  void Awake() { controller = GetComponent<CharacterController>(); }
  void Start() {}

  void Update() {
    isGround = controller.isGrounded;
    if (isGround && _playerVelocity.y < 0) {
      _playerVelocity.y = 0f;
    }
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    _movement = new Vector3(horizontal, 0f, vertical).normalized;

    HandleMovement();
  }


  void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("PickUp")) {
      // other.gameObject.SetActive(false);
      // Destroy(other.gameObject);
    }
  }

  void HandleMovement () {

    float targetAngle = Mathf.Atan2(_movement.x, _movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
    Vector3 _newMovement =  Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    Vector3 move = _newMovement * speed * _movement.magnitude;
    _playerVelocity.z = move.z;
    _playerVelocity.x = move.x;
    if (move != Vector3.zero) {
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }
    if (Input.GetButtonDown("Jump") && isGround) {
      _playerVelocity.y += Mathf.Sqrt(jumpHeigth * -2.0f * gravityValue);
    }
    if (Input.GetMouseButtonDown(1)) {
      _playerVelocity.x += speed * 10  * move.x;
      _playerVelocity.z += speed * 10  * move.z;
      markPoint = transform.position;
      isDash = true;
    }
    if (isDash) {
      float dis = Vector3.Distance(markPoint, transform.position);
      if (dis > dashDistance) {
        isDash = false;
        _playerVelocity.x = 0;
        _playerVelocity.z = 0;
      }
    }
    _playerVelocity.y += gravityValue * Time.deltaTime;
    controller.Move(_playerVelocity * Time.deltaTime);
  }


  void CounterMovement() {

  }

  void Dashing() {}

  // void OnTriggerStay(Collider other)
  //{
  //     if (other.gameObject.CompareTag("PickUp"))
  //     {
  //         other.gameObject.GetComponent<Renderer>().material.color =
  //         Color.blue;
  //     }
  // }
}
