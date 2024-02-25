using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static Fusion.NetworkBehaviour;
using static NetworkInputData;
using static UnityEngine.EventSystems.PointerEventData;

public class PlayerMovementHandler : NetworkBehaviour
{
    public NetworkCharacterController _cc;
    PlayerDataHandler _dataHandler;

    [SerializeField] IngameTeamInfos ingameTeamInfos;

    //애니메이션
    [SerializeField] Animator bodyAnime;
    [SerializeField] NetworkMecanimAnimator networkAnime;

    //플레이어 바디
    [SerializeField] GameObject body;


    //플레이어 움직임 관련
    public float movementSpeed;
    public float AttakSpeed;
    float originalSpeed = 10;
    float dashlSpeed;

    //전방 값.
    private Vector3 _forward = Vector3.forward;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();

        dashlSpeed = movementSpeed * 2;
        movementSpeed = originalSpeed;

    }


    public override void Spawned()
    {
        ingameTeamInfos = FindObjectOfType<IngameTeamInfos>();
        _dataHandler = GetComponent<PlayerDataHandler>();
      
        networkAnime  = GetComponent<NetworkMecanimAnimator>();
        bodyAnime = body.GetComponent<Animator>();  
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

                _cc.Jump();
            }


            if (data.direction.sqrMagnitude > 0)
                _forward = data.direction;

            //몸 애니메이션
            Vector2 runVector = new Vector2(_cc.Velocity.x, _cc.Velocity.z);
            runVector.Normalize();

            float speed = runVector.magnitude;



            bodyAnime.SetInteger("Class", ingameTeamInfos.teamAll[gameObject.name]);

            bodyAnime.SetFloat("X",data.moveDirection.x);
            bodyAnime.SetFloat("Z",data.moveDirection.z);


            







            if (ingameTeamInfos.TickToggle)
            {
                UpdatingPlayerCharacter();


            }



        }
    }


    IEnumerator Co_UpdatingPlayerCharacter()
    {
        yield return new WaitForSeconds(1f); // 1초 대기
        ingameTeamInfos.TickToggle = false;
        
    }

    public void UpdatingPlayerCharacter()
    {

        if (ingameTeamInfos.teamADictionary.ContainsKey(gameObject.name))
        {
            gameObject.transform.position = ingameTeamInfos.spawnPoint[0].position;
        }

        if (ingameTeamInfos.teamBDictionary.ContainsKey(gameObject.name))
        {
            gameObject.transform.position = ingameTeamInfos.spawnPoint[1].position;
        }

        
        StartCoroutine(Co_UpdatingPlayerCharacter());
    }






   public  void FindingAnimator()
    {
        foreach (Transform child in body.transform)
        {
            Animator childAnimator = child.GetComponent<Animator>();
            if (childAnimator != null && child.gameObject.activeSelf)
            {
                
                bodyAnime.avatar = childAnimator.avatar;
                break;
            }
        }
    }
}

