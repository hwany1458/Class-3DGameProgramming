using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // 키보드, 마우스, 터치를 이벤트로 오브젝트에 보낼 수 있는 기능을 지원

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

    // 가상 조이스틱 클래스에서 컨트롤러를 소유
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

    //  드래그 시작할 때
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

    // 오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트
    // 하지만 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음    
    // 드래그 중
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

    // 드래그 끝날 때
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

        // 캐릭터에게 입력벡터를 전달
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
        
        // 캐릭터에게 입력벡터를 전달
        if (playerController)
        {
            //characterController.Move(inputDirection);
            //characterController.Move(inputVector);
            playerController.transform.Translate(new Vector3(inputVector.x, 0, inputVector.y) * moveAmount);
        }
    }
}
