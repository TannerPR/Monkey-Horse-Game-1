using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject ClickTracker;
    
    
    const string GROUND_LAYER_MASK = "Ground";

    [SerializeField]
    private List<RoomObject> m_RoomObjects;

    [SerializeField]
    private float m_RaycastDistance = 500.0f;

    [SerializeField]
    private GameObject m_Player;

    [SerializeField]
    private Camera m_MainCamera;

    [SerializeField]
    private GameObject m_FocusedObj;
    [SerializeField]
    private bool m_ObjectHeld = false;

    [SerializeField]
    private float m_MouseDownTimeToPickUp = 1.0f;
    [SerializeField]
    private float m_MouseDownTimeToPickUpPassed = 0.0f;


    [SerializeField]
    private MonoBehaviour[] m_ResetableObjects;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (HandleMouseClick() == false)
        {
            if (HandleMouseRelease() == false)
            {
                HandleMouseHold();
            }
        }

        HandleHeldObject();
	}

    private bool HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject objHit = GetObjectUnderMouse(m_RaycastDistance);
            //Debug.Log("objhit is " + objHit.name);

            if (objHit != null)
            {
                //IObjectInteraction objInt = objHit.GetComponent(typeof(IObjectInteraction)) as IObjectInteraction;
                IObjectInteraction objInt = objHit.transform.root.GetComponent(typeof(IObjectInteraction)) as IObjectInteraction;
                if (objInt != null)
                {
                    // removed to remove click activation of objects
                    //objInt.OnInteraction(m_Player);
                    m_FocusedObj = objHit;
                }
            }


            return true;
        }
        else
        {
            return false;
        }
    }

    private bool HandleMouseHold()
    {
        if (Input.GetMouseButton(0))
        {
            if (m_FocusedObj != null && m_ObjectHeld == false)
            {
                m_MouseDownTimeToPickUpPassed += Time.fixedDeltaTime;
                if (m_MouseDownTimeToPickUpPassed > m_MouseDownTimeToPickUp)
                {
                    GameObject objUnderMouse = GetObjectUnderMouse(m_RaycastDistance);

                    if (objUnderMouse == m_FocusedObj)
                    {
                        m_ObjectHeld = true;
                    }
                    else
                    {
                        m_FocusedObj = null;
                    }

                    m_MouseDownTimeToPickUpPassed = 0.0f;
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private bool HandleMouseRelease()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (m_ObjectHeld)
            {
                //PlayerControl playerCon = m_FocusedObj.GetComponent<PlayerControl>();
                PlayerControl playerCon = m_FocusedObj.transform.root.GetComponent<PlayerControl>();
                if (playerCon != null)
                {
                    Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
                    
                    RaycastHit hit;
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, m_RaycastDistance))
                    //if (Physics.Raycast(ray.origin, ray.direction, out hit, m_RaycastDistance, 1 << LayerMask.GetMask(GROUND_LAYER_MASK)))
                    {
                        Vector3 adjustedMousePosition = new Vector3(hit.point.x, m_FocusedObj.transform.position.y, hit.point.z);
                        Vector3 forceDir = m_FocusedObj.transform.position - adjustedMousePosition;
                        //Debug.Log("adjustedMouse " + adjustedMousePosition + "\nforceDir " + forceDir);
                        playerCon.ApplyFling(forceDir.normalized, forceDir.sqrMagnitude);
                    }
                    else
                    {
                        Debug.Log("raycast fail");
                    }
                }

            }
            
            m_MouseDownTimeToPickUpPassed = 0.0f;
            m_ObjectHeld = false;
            m_FocusedObj = null;
            return true;
        }
        else
        {
            return false;
        }
    }

    // maybe dotn even need this anymore?
    private void HandleHeldObject()
    {
        if (m_ObjectHeld)
        {
            //Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            //Vector3 mousePos = ray.;
        }
    }

    private GameObject GetObjectUnderMouse(float aRayDistance)
    {
        Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, aRayDistance))
        {
            // testing location of clicks
            if (ClickTracker != null)
            {
                ClickTracker.transform.position = hit.point;
            }

            return hit.collider.gameObject;
        }

        return null;
    }

    public RoomObject[] GetAllUsedRoomObjects()
    {
        List<RoomObject> usedObjects = new List<RoomObject>();

        for (int i = 0; i < m_RoomObjects.Count; i++)
        {
            if (m_RoomObjects[i] != null && m_RoomObjects[i].UsedByPlayer)
            {
                usedObjects.Add(m_RoomObjects[i]);
            }
        }

        return usedObjects.ToArray();
    }

    public void Reset()
    {
        //objects
        if (m_ResetableObjects != null)
        {
            for (int i = 0; i < m_ResetableObjects.Length; i++)
            {
                if (m_ResetableObjects[i] != null)
                {
                    IResetable objReset = m_ResetableObjects[i] as IResetable;
                    if (objReset != null)
                    {
                        objReset.OnReset();
                    }
                }
            }
        }
        
        //player
        PlayerControl playerCon = m_Player.GetComponent<PlayerControl>();
        if (playerCon != null)
        {
            IResetable playerReset = playerCon as IResetable;
            if (playerReset != null)
            {
                playerReset.OnReset();
            }
        }
    }
}
