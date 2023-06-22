using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // Ű����, ���콺, ��ġ�� �̺�Ʈ�� ������Ʈ�� ���� �� �ִ� ����� ����

//public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;   
    private RectTransform rectTransform;

    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    private Vector2 inputVector;    
    private bool isInput;

    float moveSpeed = 10f;

    public enum JoystickType { Move, Rotate }
    public JoystickType joystickType;

    // ���� ���̽�ƽ Ŭ�������� ��Ʈ�ѷ��� ����
    public CharacterController characterController;
    public GameObject playerController;

    private void Awake()    
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float amount = moveSpeed * Time.deltaTime;

        if (isInput)
        {
            //InputControlVector();
            InputControlVector(amount);
        }
    }

    //  �巡�� ������ ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");

        /*
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        // lever.anchoredPosition = inputDir;
        lever.anchoredPosition = clampedDir;   
        */
        isInput = true;
        ControlJoystickLever(eventData);  
    }  

    // ������Ʈ�� Ŭ���ؼ� �巡�� �ϴ� ���߿� ������ �̺�Ʈ
    // ������ Ŭ���� ������ ���·� ���콺�� ���߸� �̺�Ʈ�� ������ ����    
    // �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        /*
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        // lever.anchoredPosition = inputDir;
        lever.anchoredPosition = clampedDir;   
        */
        ControlJoystickLever(eventData);    
        //isInput = false;
    }

    // �巡�� ���� ��
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");

        isInput = false;
        lever.anchoredPosition = Vector2.zero;
    }


    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        
        lever.anchoredPosition = clampedDir;
        inputVector = clampedDir / leverRange;
    }

    private void InputControlVector()
    {

        //Debug.Log(inputDirection.x + " / " + inputDirection.y);
        Debug.Log(inputVector.x + " / " + inputVector.y);

        // ĳ���Ϳ��� �Էº��͸� ����
        /*
        if (characterController)
        {
            //characterController.Move(inputDirection);
            characterController.Move(inputVector);
        }
        */
        switch (joystickType)
        {
            case JoystickType.Move:
                //controller.Move(inputDirection);
                characterController.Move(inputVector);
                break;

            case JoystickType.Rotate:
                //controller.LookAround(inputDirection);
                //characterController.LookAround(inputVector);
                break;
        }
    }

    private void InputControlVector(float moveAmount)
    {
        //Debug.Log(inputDirection.x + " / " + inputDirection.y);
        //Debug.Log("InputControlVector(float) " + inputVector.x + " / " + inputVector.y);
        
        // ĳ���Ϳ��� �Էº��͸� ����
        if (playerController)
        {
            //characterController.Move(inputDirection);
            //characterController.Move(inputVector);
            playerController.transform.Translate(new Vector3(inputVector.x, 0, inputVector.y) * moveAmount);
        }
    }
}
