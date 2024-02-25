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
        // �÷��̾� �̵� �Է� ���
        var cameraRotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        var inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        networkInputData.direction = cameraRotation * inputDirection;
        networkInputData.moveDirection = inputDirection;
        networkInputData.mouseDirection = cameraRotation;
        // �����̽��ٿ� ����� ���� �Է� ����
        networkInputData.buttons.Set(NetworkInputButtons.Jump, Input.GetKey(KeyCode.Space));


        networkInputData.buttons.Set(NetworkInputButtons.Dash, Input.GetKey(KeyCode.LeftShift));
        

        // ���콺 �Է� ����
        networkInputData.mouseX = Input.GetAxis("Mouse X");
        networkInputData.mouseY = Input.GetAxis("Mouse Y");

        // ���콺 ��ư 0 ���� ����
        networkInputData.buttons.Set(NetworkInputData.MOUSEBUTTON0, _mouseButton0);
        _mouseButton0 = false;

        // ���콺 ��ư 0 ���� ����
        networkInputData.buttons.Set(NetworkInputData.MOUSEBUTTON1, _mouseButton1);
        _mouseButton1 = false;

        return networkInputData;


    }


}
