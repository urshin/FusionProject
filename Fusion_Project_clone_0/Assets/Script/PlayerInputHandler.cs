using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NetworkInputData;

public class PlayerInputHandler : MonoBehaviour
{
    //�÷��̾� ��ǲ �ڵ鷯
    // Start is called before the first frame update
    void Start()
    {

    }


    private bool _mouseButton0;
    private bool _mouseButton1;

    // ���콺 ��ư ���¸� �����ϴ� ������Ʈ �޼���
    private void Update()
    {
        _mouseButton0 = _mouseButton0 | Input.GetMouseButton(0);
        _mouseButton1 = _mouseButton1 | Input.GetMouseButton(1);
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        
        return networkInputData;
    }


}
