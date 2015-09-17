using UnityEngine;
using System.Collections;

public class SoundFloor : MonoBehaviour
{
    private AudioSource m_AudioSource;

    public AudioClip[] m_Impact = new AudioClip[5];

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        int rand = Random.Range(0, m_Impact.Length);

        PlayerControl m_PlayerControl = other.transform.root.GetComponent<PlayerControl>();

        if (m_PlayerControl == null)
        {
            return;
        }
        if (other.relativeVelocity.magnitude > 5)
        {
            if (other.transform.root.GetComponent<PlayerControl>())
            {
                if (m_AudioSource != null)
                {
                    if (!m_AudioSource.isPlaying)
                    {
                        m_AudioSource.PlayOneShot(m_Impact[rand]);
                    }
                }
            }
        }
    }
}
