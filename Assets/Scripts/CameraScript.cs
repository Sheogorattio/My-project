using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    private Transform character;
    private InputAction lookAction;
    private PlayerInputActions playerInputActions;
    private Vector3 cameraAngles;
    private Vector3 r;

    private float sensitivityH = 5.0f;
    private float sensitivityV = 3.0f;

    private float maxVerticalAngle = 75f;
    private float minVerticalAngle = 35f;

    private float minFpvDistance = 1.0f;
    private float maxFpvDistance;

    private bool isPos3;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find ("Character").transform;
        playerInputActions = new PlayerInputActions();
        lookAction = playerInputActions.Player.Look;
        lookAction.Enable();
        cameraAngles = this.transform.eulerAngles;
        r= this.transform.position - character.position;
        maxFpvDistance = r.magnitude;
        isPos3 = maxFpvDistance == r.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 wheel = Input.mouseScrollDelta;
        if(wheel.y !=0){ // обробка скролів
            if(r.magnitude > minFpvDistance){
                float rr =  r.magnitude*(1-wheel.y/10); //розрахунок нової аідстані від камери до персонажа після скрола
                if(rr <= minFpvDistance){ // увімкнення FPV режиму
                    Debug.Log("увімкнення FPV режиму");
                    GameState.isFpv = true;
                    minVerticalAngle = -10f;
                    maxVerticalAngle = 40f;
                    r*= 0.01f;
                }
                else { // обробка скролів у режимі огляду з дальньої дистанції   
                    if(rr < maxFpvDistance){
                        Debug.Log("обробка скролів у режимі огляду з дальньої дистанції");
                        r *= 1-wheel.y/10;
                        isPos3 = false;
                    }  
                    else {
                        Debug.Log("фіксування камери");
                        r = r.normalized * maxFpvDistance;
                        isPos3 = true;
                    }
                }
            }
            else{
                if(wheel.y < 0){ //Вихід з режиму 
                    Debug.Log("Вихід з режиму FPV");
                    GameState.isFpv = false;
                    minVerticalAngle = 30f ;
                    maxVerticalAngle = 75f;
                    r*=100f;
                }
            }
        }
        if(!isPos3){
            Vector2 lookValue = lookAction.ReadValue<Vector2>();
            if(lookValue != Vector2.zero){
                cameraAngles.x = Mathf.Clamp( cameraAngles.x-(lookValue.y*  Time.deltaTime *sensitivityV), minVerticalAngle, maxVerticalAngle );
                cameraAngles.y += lookValue.x* Time.deltaTime * sensitivityH;
                this.transform.eulerAngles = cameraAngles;
            }
            this.transform.position = character.position + Quaternion.Euler(0,cameraAngles.y ,0) * r;
        }
    }
}
