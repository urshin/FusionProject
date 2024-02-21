using Cinemachine;
using TMPro;
using UnityEngine;

public class PlayerAvatarView : MonoBehaviour
{

    private Animator camAnimator;
    [SerializeField] private TextMeshProUGUI nameLabel;

    private void Start()
    {
        camAnimator = GetComponent<Animator>();
    }
    public void SetCameraTarget()
    {

        //StateDrivenCamera
        var cinemachineCamera = FindObjectOfType<CinemachineStateDrivenCamera>();
        cinemachineCamera.LookAt = transform;
        cinemachineCamera.Follow = transform;
        cinemachineCamera.m_AnimatedTarget = gameObject.GetComponent<Animator>();


    }
}