using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MouseCharacterController : MonoBehaviour {

    public Vector2 m_MoseSensitivity;
    public float m_MoveSpeed;
    public float m_Gravity;
    public float m_JumpForce;
    public LayerMask m_GrabableMask;
    public Transform m_GrabHook;
    
    private Transform m_CameraTransform;
    private CharacterController m_Controller;

    private float m_Pitch;
    private float m_VelocityY;

    private Grabable m_GrabbedItem;
    private Grabable m_PointedItem;

    private void Awake() {
        m_CameraTransform = GetComponentInChildren<Camera>().transform;
        m_Controller = GetComponent<CharacterController>();
    }

    private void Update() {
        HandleMovement();
        
        m_PointedItem?.Highlight(false);
        m_PointedItem = null;
        
        Ray ray = new Ray(m_CameraTransform.position, m_CameraTransform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, m_GrabableMask)) {
            m_PointedItem = hit.collider.GetComponent<Grabable>();
            if (m_PointedItem) {
                m_PointedItem.Highlight(true);
            }
        }

        if (Input.GetMouseButtonDown(0) && m_PointedItem) {
            m_GrabbedItem = m_PointedItem;
            m_GrabbedItem.Highlight(false);
            m_GrabbedItem.Grab(m_GrabHook);
        }
    }

    void HandleMovement() {
        Vector3 moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"),0,Input.GetAxisRaw ("Vertical")).normalized;
        Vector2 mouseInput = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));
        
        transform.Rotate(Vector3.up * mouseInput.x * m_MoseSensitivity.x);
        m_Pitch += mouseInput.y * m_MoseSensitivity.y;
        
        m_CameraTransform.localRotation = Quaternion.AngleAxis(m_Pitch, Vector3.left);

        if (m_Controller.isGrounded) {
            m_VelocityY = 0;
            if (Input.GetKeyDown(KeyCode.Space)) {
                m_VelocityY = m_JumpForce;
            }
        }

        m_VelocityY -= m_Gravity * Time.deltaTime;
        var move = transform.TransformDirection(moveDir) * m_MoveSpeed;
        move.y = m_VelocityY;
        m_Controller.Move(move * Time.deltaTime);
    }
}
