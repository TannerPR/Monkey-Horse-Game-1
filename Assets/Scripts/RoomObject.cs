using UnityEngine;
using System.Collections;


public class RoomObject : PlayerItem, IObjectInteraction
{
    [SerializeField]
    private float m_InteractionRange = 0.5f;
    public float InteractionRange { get { return m_InteractionRange; } }

    [SerializeField]
    private Collider m_Collider;

    [SerializeField]
    private Rigidbody m_RigidBody;

    [SerializeField]
    private bool m_InitiallyHidden = true;
    [SerializeField]
    private bool m_Hidden = true;

    private bool m_UsedByPlayer = false;
    public bool UsedByPlayer { get { return m_UsedByPlayer; } }

    private Vector3 m_InitialPosition;
    private Quaternion m_InitialRotation;

	// Use this for initialization
	void Start ()
    {
        if (m_Collider == null)
        {
            Debug.Log("Please set non-trigger collider on RoomObject " + GetInstanceID());
            this.enabled = false;
        }

        if (m_RigidBody == null)
        {
            m_RigidBody = GetComponent<Rigidbody>();
            if (m_RigidBody == null)
            {
                Debug.Log("Cannot find rigidbody on RoomObject " + GetInstanceID());
                this.enabled = false;
            }
        }

        m_InitialPosition = transform.position;
        m_InitialRotation = transform.rotation;

        m_Hidden = m_InitiallyHidden;
        ToggleObject(m_InitiallyHidden);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_UsedByPlayer == true) { return; }
	}

    private void Reset()
    {
        m_UsedByPlayer = false;
        m_Hidden = m_InitiallyHidden;
        ToggleObject(m_UsedByPlayer);

        transform.position = m_InitialPosition;
        transform.rotation = m_InitialRotation;
    }

    void OnTriggerEnter(Collider aCollider)
    {
        if (m_UsedByPlayer == true) { return; }
        if (m_Hidden) { return; }

        // check if its the player
        PlayerControl playerCon = aCollider.GetComponent<PlayerControl>();
        if (playerCon == false) { return; }

        ActivatedByPlayer(playerCon);

    }

    private void ActivatedByPlayer(PlayerControl aPlayerControl)
    {
        m_UsedByPlayer = true;
        ToggleObject(m_UsedByPlayer);
        // call some function on player which tells it what to make visible
        aPlayerControl.SetItemVisibility(ItemID);
    }

    private void ToggleObject(bool aCurrentState)
    {
        SetObjectVisibility(!aCurrentState);
        SetObjectCollision(!aCurrentState);
    }

    private void SetObjectVisibility(bool aVisible)
    {
        MakeVisible(aVisible);
    }

    private void SetObjectCollision(bool aDoCollision)
    {
        m_RigidBody.useGravity = aDoCollision;

        m_RigidBody.isKinematic = !aDoCollision;
        m_RigidBody.velocity = m_RigidBody.isKinematic ? Vector3.zero : m_RigidBody.velocity;
        m_RigidBody.angularVelocity = m_RigidBody.isKinematic ? Vector3.zero : m_RigidBody.angularVelocity;

        m_Collider.enabled = aDoCollision;
    }

    public void HideObject()
    {
        m_Hidden = true;
        ToggleObject(m_Hidden);
    }

    public void RevealObject()
    {
        m_Hidden = false;
        ToggleObject(m_Hidden);
    }

    public void ApplyForce(Vector3 aForceDirection, float aForcePower)
    {
        m_RigidBody.AddForce(aForceDirection * aForcePower, ForceMode.Impulse);
    }

    #region INTERFACE
    public void OnInteraction(GameObject aPlayer)
    {
        if (m_UsedByPlayer) { return; }
        if (m_Hidden) { return; }

        //check range
        float distSqrd = (transform.position - aPlayer.transform.position).sqrMagnitude;
        if (distSqrd > m_InteractionRange) { return; }

        // check if its the player
        PlayerControl playerCon = aPlayer.GetComponent<PlayerControl>();
        if (playerCon == false) { return; }

        ActivatedByPlayer(playerCon);
    }
    #endregion
}
