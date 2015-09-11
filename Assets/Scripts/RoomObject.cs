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

    private bool m_UsedByPlayer = false;
    public bool UsedByPlayer { get { return m_UsedByPlayer; } }

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
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_UsedByPlayer == true) { return; }
	}

    private void Reset()
    {
        m_UsedByPlayer = false;
        ToggleObject(m_UsedByPlayer);
    }

    void OnTriggerEnter(Collider aCollider)
    {
        if (m_UsedByPlayer == true) { return; }

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
        m_Collider.enabled = aDoCollision;
    }

    #region INTERFACE
    public void OnInteraction(GameObject aPlayer)
    {
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
