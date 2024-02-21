using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Fusion.NetworkBehaviour;
using static NetworkInputData;

public class PlayerMovementHandler : NetworkBehaviour
{
    public NetworkCharacterController _cc;
    PlayerDataHandler _dataHandler;

    [SerializeField] IngameTeamInfos ingameTeamInfos;

    //�ִϸ��̼�
    [SerializeField] Animator bodyAnime;

    //�÷��̾� �ٵ�
    [SerializeField] GameObject body;


    //�÷��̾� ������ ����
    public float movementSpeed;
    public float AttakSpeed;
    float originalSpeed = 10;
    float dashlSpeed;

    //���� ��.
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
        bodyAnime = body.GetComponentInChildren<Animator>();
        

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

            //�� �ִϸ��̼�
            Vector2 runVector = new Vector2(_cc.Velocity.x, _cc.Velocity.z);
            runVector.Normalize();

            float speed = runVector.magnitude;
            bodyAnime.SetFloat("X", data.moveDirection.x);
            bodyAnime.SetFloat("Z", data.moveDirection.z);


           

            //    bodyAnime.SetFloat("Direction", speed);




            if (ingameTeamInfos.TickToggle)
            {
                UpdatingPlayerCharacter();


            }



        }
    }


    IEnumerator Co_UpdatingPlayerCharacter()
    {
        yield return new WaitForSeconds(0.5f); // 1�� ���
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
                bodyAnime = childAnimator;
                gameObject.GetComponent<NetworkMecanimAnimator>().Animator = childAnimator;
                break;
            }
        }
    }
}

