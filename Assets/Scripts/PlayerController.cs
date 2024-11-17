using UnityEngine;

public class PlayerController : MonoBehaviour {

  public CharacterController controller;
  public Vector3 _playerVelocity;
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
    Vector3 _movement = new Vector3(horizontal, 0f, vertical).normalized;
    controller.Move(_movement * speed * Time.deltaTime);

    if (_movement != Vector3.zero) {
      gameObject.transform.forward = _movement;
    }
    if (Input.GetButtonDown("Jump") && isGround) {
      _playerVelocity.y += Mathf.Sqrt(jumpHeigth * -2.0f * gravityValue);
    }
    if (Input.GetMouseButtonDown(1)) {
      _playerVelocity.x += speed * 10  * _movement.x;
      _playerVelocity.z += speed * 10  * _movement.z;
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

  void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("PickUp")) {
      // other.gameObject.SetActive(false);
      // Destroy(other.gameObject);
    }
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
