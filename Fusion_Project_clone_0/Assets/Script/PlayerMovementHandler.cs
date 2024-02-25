using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    //플레이어 바디
    [SerializeField] GameObject body;

    [Networked]
    public float DashGage { get; set; }

    [SerializeField] GameObject playerInterfaceUI;



    //플레이어 움직임 관련
    public float movementSpeed;
    public float AttakSpeed;
    float originalSpeed;
    float dashlSpeed;

    //전방 값.
    private Vector3 _forward = Vector3.forward;



    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();

     

    }


    public override void Spawned()
    {
        ingameTeamInfos = FindObjectOfType<IngameTeamInfos>();
        _dataHandler = GetComponent<PlayerDataHandler>();
        bodyAnime = body.GetComponent<Animator>();

        DashGage = 100;




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
            _cc.Move( data.direction * Runner.DeltaTime);
            transform.rotation = data.mouseDirection;

            if (data.buttons.IsSet(NetworkInputButtons.Jump))
            {
                _cc.Jump();
            }
            if (data.buttons.IsSet(NetworkInputButtons.Dash)&&Runner.IsForward)
            {
                //나중에 서서히 증가하게 만들기
                _cc.maxSpeed = _dataHandler.characterInfo.Speed* 3;
                bodyAnime.SetInteger("isDash", 1);
            }
            else
            {
                _cc.maxSpeed = _dataHandler.characterInfo.Speed;
                bodyAnime.SetInteger("isDash", 0); 
            }
            
            if (data.direction.sqrMagnitude > 0)
                _forward = data.direction;


           
            bodyAnime.SetInteger("Class", ingameTeamInfos.teamAll[gameObject.name]);

            bodyAnime.SetFloat("X", data.moveDirection.x);
            bodyAnime.SetFloat("Z", data.moveDirection.z);



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






    public void FindingAnimator()
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

