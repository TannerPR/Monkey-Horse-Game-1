﻿using UnityEngine;
using System.Collections;

public class ItemHolder : MonoBehaviour, IObjectInteraction, IResetable
{
    [SerializeField]
    private RoomObject[] m_ItemsHeld;

    [SerializeField]
    private float m_InteractionRange = 1.0f;

    [SerializeField]
    private float m_ItemLaunchForce = 1.0f;

    [SerializeField]
    private bool m_KnockBackPlayer = true;
    [SerializeField]
    private float m_KnockBackPlayerForce = 2.0f;

    [SerializeField]
    private MonoBehaviour m_TypeSpecificBehaviour;
    private IObjectHolder m_OnReleaseBehaviour;

    [SerializeField]
    private bool m_Activated = false;


    
	// Use this for initialization
	void Start () 
    {
        m_OnReleaseBehaviour = m_TypeSpecificBehaviour as IObjectHolder;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter(Collision aCollision)
    {
        if (m_Activated) { return; }

        //PlayerControl playerControl = aCollision.gameObject.GetComponent<PlayerControl>();
        PlayerControl playerControl = aCollision.transform.root.GetComponent<PlayerControl>();

        if (playerControl == null) { return; }

        m_Activated = true;

        if (m_KnockBackPlayer)
        {
            KnockBackPlayer(playerControl);
        }

        if (m_OnReleaseBehaviour != null)
        {
            m_OnReleaseBehaviour.OnObjectRelease();
        }

        ReleaseItems();
    }


    private void ReleaseItems()
    {
        if (m_ItemsHeld == null || m_ItemsHeld.Length < 1)
        { 
            Debug.Log("ItemsHeld is null or contains no elements on ItemHolder " + GetInstanceID());
            return;
        }


        for (int i = 0 ; i < m_ItemsHeld.Length ; i++)
        {
            
            if (m_ItemsHeld[i] !=null)
            {
                m_ItemsHeld[i].RevealObject();

                Vector3 forceDirection = gameObject.transform.forward;
                //Vector3 forceDirection = gameObject.transform.right;

                //randomize directoin
                float angleRadians = UnityEngine.Random.Range(1.0f, 2.0f * Mathf.PI);
                Vector2 unitVector = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
                forceDirection.x += unitVector.x;
                forceDirection.z += unitVector.y;
                
                //forceDirection = gameObject.transform.forward/* * somebullshit random float */;
                //forceDirection = gameObject.transform.right/* * somebullshit random float */;
                Debug.Log("obj forward" + forceDirection);
                //add some up
                forceDirection += new Vector3(0.0f, 0.5f, 0.0f);
                forceDirection.Normalize();
                Debug.Log("Forcedirection " + forceDirection);

                m_ItemsHeld[i].ApplyForce(forceDirection, m_ItemLaunchForce);
            }
        }

    }

    private void KnockBackPlayer(PlayerControl aPlayer)
    {
        //aPlayer.ApplyFling(gameObject.transform.right, m_KnockBackPlayerForce);
        aPlayer.ApplyFling(gameObject.transform.forward, m_KnockBackPlayerForce);
    }

    public void OnInteraction(GameObject aPlayer)
    {
        //check range
        float distSqrd = (transform.position - aPlayer.transform.position).sqrMagnitude;
        if (distSqrd > m_InteractionRange) { return; }

        // check if its the player
        //PlayerControl playerCon = aPlayer.GetComponent<PlayerControl>();
        PlayerControl playerCon = aPlayer.transform.root.GetComponent<PlayerControl>();
        if (playerCon == false) { return; }

        if (m_KnockBackPlayer)
        {
            KnockBackPlayer(playerCon);
        }

        ReleaseItems();
    }

    public void OnReset()
    {
        // reset items
        IResetable behaviourReset = m_TypeSpecificBehaviour as IResetable;
        if (behaviourReset != null)
        {
            behaviourReset.OnReset();
        }

        if (m_ItemsHeld != null)
        {
            for (int i = 0 ; i < m_ItemsHeld.Length ; i++)
            {
                if (m_ItemsHeld[i] != null)
                {
                    IResetable itemReset = m_ItemsHeld[i] as IResetable;
                    if (itemReset != null)
                    {
                        itemReset.OnReset();
                    }
                }
            }
        }
    }
}
