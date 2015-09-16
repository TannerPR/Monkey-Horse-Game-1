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



    IEnumerator<YieldInstruction> RotateObject(GameObject aObject, float aSpeedOfRotation, float aDuration)
    {
        float time = 0.0f;
        while (time < aDuration)
        {
            time += Time.fixedDeltaTime;
            aObject.transform.Rotate(Vector3.up, aSpeedOfRotation * Time.fixedDeltaTime);
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

        if (m_Doors[0] != null)
        {
            float speed = m_DoorOpenSpeed + Random.Range(-m_DoorOpenSpeedVariance, m_DoorOpenSpeedVariance);
            float openTime = m_DoorOpenTime + Random.Range(-m_DoorOpenTimeVariance, m_DoorOpenTimeVariance);
            StartCoroutine(RotateObject(m_Doors[0], speed, openTime));
        }

        if (m_Doors[1] != null)
        {
            float speed = m_DoorOpenSpeed + Random.Range(-m_DoorOpenSpeedVariance, m_DoorOpenSpeedVariance);
            float openTime = m_DoorOpenTime + Random.Range(-m_DoorOpenTimeVariance, m_DoorOpenTimeVariance);
            StartCoroutine(RotateObject(m_Doors[1], speed, openTime));
        }
        
    }
}