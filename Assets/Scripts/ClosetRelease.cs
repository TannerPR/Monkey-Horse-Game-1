using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClosetRelease : MonoBehaviour, IObjectHolder
{
    [SerializeField]
    private GameObject[] m_Doors;

    [SerializeField]
    private float m_DoorOpenSpeed = 1.0f;

    [SerializeField]
    private float m_DoorOpenSpeedVariance = 0.2f;

    [SerializeField]
    private float m_DoorOpenTime = 0.5f;

    [SerializeField]
    private float m_DoorOpenTimeVariance = 0.15f;

    [SerializeField]
    private Collider m_CollisionDetector;


    IEnumerator<YieldInstruction> RotateObject(GameObject aObject, Vector3 aAxis, float aSpeedOfRotation, float aDuration)
    {
        float time = 0.0f;
        while (time < aDuration)
        {
            time += Time.fixedDeltaTime;
            aObject.transform.Rotate(aAxis, aSpeedOfRotation * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public void OnObjectRelease()
    {
        Debug.Log("ON RELEASE BEHAVIOUR FROM CLOSET CALLED");

        if (m_Doors == null || m_Doors.Length < 2)
        {
            Debug.Log("m_Doors is null or contains less than 2 elements on ClosetRelease " + GetInstanceID());
            return;
        }

        m_CollisionDetector.enabled = false;

        if (m_Doors[0] != null)
        {
            float speed = m_DoorOpenSpeed + Random.Range(-m_DoorOpenSpeedVariance, m_DoorOpenSpeedVariance);
            float openTime = m_DoorOpenTime + Random.Range(-m_DoorOpenTimeVariance, m_DoorOpenTimeVariance);
            StartCoroutine(RotateObject(m_Doors[0], Vector3.forward, speed, openTime));
        }

        if (m_Doors[1] != null)
        {
            float speed = m_DoorOpenSpeed + Random.Range(-m_DoorOpenSpeedVariance, m_DoorOpenSpeedVariance);
            float openTime = m_DoorOpenTime + Random.Range(-m_DoorOpenTimeVariance, m_DoorOpenTimeVariance);
            StartCoroutine(RotateObject(m_Doors[1], -Vector3.up, speed, openTime));
        }
        
    }


    public void Reset()
    {
        //reset doors and shit
        //doors

        m_CollisionDetector.enabled = true;
    }
}