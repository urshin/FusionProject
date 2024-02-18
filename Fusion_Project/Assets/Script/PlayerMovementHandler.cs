using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NetworkInputData;

public class PlayerMovementHandler : NetworkBehaviour
{
    NetworkCharacterController _cc;

    //애니메이션
    [SerializeField] Animator bodyAnime;


    //플레이어 움직임 관련
    float movementSpeed;
    float originalSpeed = 10;
    float dashlSpeed;

    //전방 값.
    private Vector3 _forward = Vector3.forward;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();

        dashlSpeed = originalSpeed * 2;
        movementSpeed = originalSpeed;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(movementSpeed * data.direction * Runner.DeltaTime);
            transform.rotation = data.mouseDirection;

            if (data.buttons.IsSet(NetworkInputButtons.Jump))
            {
                print("jump");
                _cc.Jump();
            }

            if (data.direction.sqrMagnitude > 0)
                _forward = data.direction;



            //몸 애니메이션
            Vector2 runVector = new Vector2(_cc.Velocity.x, _cc.Velocity.z);
            runVector.Normalize();
            float speed = Mathf.Sqrt(runVector.magnitude);
         




        }
    }
}

