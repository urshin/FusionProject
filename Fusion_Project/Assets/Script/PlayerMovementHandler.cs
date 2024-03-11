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
    public NetworkMecanimAnimator networkMecanimAnimator;

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
        networkMecanimAnimator = GetComponentInChildren<NetworkMecanimAnimator>();

    }


    public bool isdashing =false;
    public Vector3 dashDirection;
    public float dashSpeed;
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            inputData.direction.Normalize();


            if (isdashing)
            {
                _cc.Dash(dashSpeed);
            }
            else
            {

                _cc.Move(inputData.direction * Runner.DeltaTime);
            }

            //_cc.Move(inputData.direction * Runner.DeltaTime);


            transform.rotation = inputData.mouseDirection;

            HandleJump(inputData);
            HandleDash(inputData);

            if (networkMecanimAnimator != null && ingameTeamInfos.gameState == IngameTeamInfos.GameState.Gaming)
            {
                UpdateAttackAnimation(inputData);
                UpdatePlayerAnimation(inputData);

            }
            if (ingameTeamInfos.gameState == IngameTeamInfos.GameState.CharactorSelect)
                UpdatingPlayerCharacter();
            // UpdatePlayerCharacterIfNeeded();
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

            if (inputData.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
            {
                networkMecanimAnimator.Animator.SetInteger("Attack", 1);
            }
            else if (inputData.buttons.IsSet(NetworkInputData.MOUSEBUTTON1))
            {
                networkMecanimAnimator.Animator.SetInteger("Attack", 2);
            }
            else if (inputData.buttons.IsSet(NetworkInputButtons.Spell))
            {
                networkMecanimAnimator.Animator.SetInteger("Attack", 3);
            }
            else if (inputData.buttons.IsSet(NetworkInputButtons.Ultimate))
            {
                networkMecanimAnimator.Animator.SetInteger("Attack", 4);
            }
            else
            {
                networkMecanimAnimator.Animator.SetInteger("Attack", 0);
            }

        }
    }

    private void UpdatePlayerAnimation(NetworkInputData inputData)
    {

        //bodyAnime.SetFloat("X", inputData.moveDirection.x);
        //bodyAnime.SetFloat("Z", inputData.moveDirection.z);



        networkMecanimAnimator.Animator.SetFloat("X", inputData.moveDirection.x);
        networkMecanimAnimator.Animator.SetFloat("Z", inputData.moveDirection.z); 
       // networkMecanimAnimator.Animator.SetFloat("X", _cc.Velocity.normalized.x);
      //  networkMecanimAnimator.Animator.SetFloat("Z", _cc.Velocity.normalized.z);




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

    }


    //public void Dashing(float Timer, float Speed)
    //{
    //    if (GetInput(out NetworkInputData inputData))
    //    {

    //        StartCoroutine(Dash(Timer, Speed, inputData));
    //    }
    //}

    //public IEnumerator Dash(float Timer, float Speed, NetworkInputData inputData)
    //{
    //    float startTime = Runner.SimulationTime;
    //    while (Runner.SimulationTime < startTime + Timer)
    //    {
    //        _cc.Move(inputData.direction * Runner.DeltaTime);
    //        _cc.maxSpeed = _cc.maxSpeed *Speed;
    //        yield return null;

    //    }
    //    _cc.maxSpeed /= Speed;
    //}



}

