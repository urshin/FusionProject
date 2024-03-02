using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Fusion.NetworkBehaviour;
using static NetworkInputData;
using static UnityEngine.EventSystems.PointerEventData;

public class PlayerMovementHandler : NetworkBehaviour
{
    public NetworkCharacterController _cc;
    public PlayerDataHandler _dataHandler;

    [SerializeField] IngameTeamInfos ingameTeamInfos;

    //플레이어 바디
    [SerializeField] GameObject body;
    public Animator bodyAnime;


    [SerializeField] GameObject playerInterfaceUI;



    // 딜레이 타이머
    [Networked] private TickTimer delay { get; set; }

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

    }


    // Update is called once per frame
    void Update()
    {


    }




    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            inputData.direction.Normalize();
            _cc.Move(inputData.direction * Runner.DeltaTime);
            transform.rotation = inputData.mouseDirection;

            HandleJump(inputData);
            HandleDash(inputData);

            UpdateAttackAnimation(inputData);
            if (bodyAnime != null)
            {
                UpdatePlayerAnimation(inputData);

            }
            UpdatePlayerCharacterIfNeeded();
        }
    }

    private void HandleJump(NetworkInputData inputData)
    {
        if (inputData.buttons.IsSet(NetworkInputButtons.Jump))
        {
            _cc.Jump();
        }
    }

    private void HandleDash(NetworkInputData inputData)
    {
        if (inputData.buttons.IsSet(NetworkInputButtons.Dash) && Runner.IsForward)
        {
            //_cc.maxSpeed = _dataHandler.characterInfo.Speed * 3;


        }
        else
        {
            //_cc.maxSpeed = _dataHandler.characterInfo.Speed;
        }
    }

    private void UpdateAttackAnimation(NetworkInputData inputData)
    {
        if (delay.ExpiredOrNotRunning(Runner))
        {
            if (bodyAnime != null)
            {
                if (inputData.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
                {
                    bodyAnime.SetInteger("Attack", 1);
                }
                else if (inputData.buttons.IsSet(NetworkInputData.MOUSEBUTTON1))
                {
                    bodyAnime.SetInteger("Attack", 2);
                }
                else if (inputData.buttons.IsSet(NetworkInputButtons.Spell))
                {
                    bodyAnime.SetInteger("Attack", 3);
                }
                else if (inputData.buttons.IsSet(NetworkInputButtons.Ultimate))
                {
                    bodyAnime.SetInteger("Attack", 4);
                }
                else
                {
                    bodyAnime.SetInteger("Attack", 0);
                }

            }
        }
    }

    private void UpdatePlayerAnimation(NetworkInputData inputData)
    {

        bodyAnime.SetFloat("X", inputData.moveDirection.x);
        bodyAnime.SetFloat("Z", inputData.moveDirection.z);



    }

    private void UpdatePlayerCharacterIfNeeded()
    {
        if (ingameTeamInfos.TickToggle)
        {
            UpdatingPlayerCharacter();
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

}

