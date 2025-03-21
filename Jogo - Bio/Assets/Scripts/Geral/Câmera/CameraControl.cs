using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float verticalOffset = 2f;
    public float smoothSpeed = 0.125f;
    private CinemachineFramingTransposer framingTransposer;
    private Vector3 originalOffset;
    private Movimento mv;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); // Certifique-se de que seu Player tem a tag correta!
        if (player != null)
        {
            mv = player.GetComponent<Movimento>();
        }
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Update()
    {
        if (mv.estaNoChao == true)
        {
            Vector3 targetOffset = originalOffset;

            if (Input.GetKey(KeyCode.W))
            {
                targetOffset.y += verticalOffset;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                targetOffset.y -= verticalOffset;
            }

            framingTransposer.m_TrackedObjectOffset = Vector3.Lerp(framingTransposer.m_TrackedObjectOffset, targetOffset, smoothSpeed);
        }
    }
}
