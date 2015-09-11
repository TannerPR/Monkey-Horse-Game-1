using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private CharacterController m_Controller;

    [SerializeField]
    private float m_MoveSpeed = 2.0f;

    [SerializeField]
    private float m_TurnSpeed = 2.0f;

	// Use this for initialization
	void Start () 
    {
	    if (m_Controller == null)
        {
            m_Controller = GetComponent<CharacterController>();
            if (m_Controller == null)
            {
                Debug.Log("Cannot find character contoller");
                this.enabled = false;
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * m_TurnSpeed);

        Vector3 localMoveDir = Input.GetAxis("Vertical") * transform.forward;
        m_Controller.Move(localMoveDir * Time.deltaTime * m_MoveSpeed);
	}
}
