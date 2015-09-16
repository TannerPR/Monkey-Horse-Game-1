using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DresserRelease : MonoBehaviour, IObjectHolder, IResetable
{
    [SerializeField]
    private GameObject[] m_Drawers;

    [SerializeField]
    private float m_DrawerOpenSpeed = 1.0f;

    [SerializeField]
    private float m_DrawerOpenSpeedVariance = 0.2f;

    [SerializeField]
    private float m_DrawerOpenTime = 0.5f;

    [SerializeField]
    private float m_DrawerOpenTimeVariance = 0.15f;

    private Vector3[] m_DrawerInitPositions;


    void Start()
    {
        if (m_Drawers == null)
        {
            m_DrawerInitPositions = new Vector3[m_Drawers.Length];
            for (int i = 0; i < m_Drawers.Length; i++)
            {
                if (m_Drawers[i] != null)
                {
                    m_DrawerInitPositions[i] = m_Drawers[i].transform.position;
                }
            }
        }

    }

    IEnumerator<YieldInstruction> SlideForward(GameObject aObject, float aSpeedOfSlide, float aDuration)
    {
        float time = 0.0f;
        while (time < aDuration)
        {
            time += Time.fixedDeltaTime;
            aObject.transform.position += aObject.transform.forward * aSpeedOfSlide * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    } 

    private void Reset()
    {
        //reset doors and shit
        if (m_Drawers == null || m_DrawerInitPositions == null || m_DrawerInitPositions.Length != m_Drawers.Length)
        {
            Debug.Log("m_Drawers is null or incompatibile with stored initial values for m_Drawers. Cannot initiate reset on DresserRelease " + GetInstanceID());
            return;
        }

        //reset drawer position
        for (int i = 0; i < m_Drawers.Length; i++)
        {
            if (m_Drawers[i] != null)
            {
                m_Drawers[i].transform.position = m_DrawerInitPositions[i];
            }
        }
        
    }

    public void OnObjectRelease()
    {
        Debug.Log("ON RELEASE BEHAVIOUR FROM DRESSER CALLED");

        if (m_Drawers == null || m_Drawers.Length < 1)
        {
            Debug.Log("m_Drawers is null or contains no elements on DresserRelease " + GetInstanceID());
            return;
        }

        for (int i = 0; i < m_Drawers.Length; i++)
        {
            if (m_Drawers[i] != null)
            {
                float speed = m_DrawerOpenSpeed + Random.Range(-m_DrawerOpenSpeedVariance, m_DrawerOpenSpeedVariance);
                float openTime = m_DrawerOpenTime + Random.Range(-m_DrawerOpenTimeVariance, m_DrawerOpenTimeVariance);
                StartCoroutine(SlideForward(m_Drawers[i], speed, openTime));
            }
        }
    }

    public void OnReset()
    {
        Reset();
    }
}
