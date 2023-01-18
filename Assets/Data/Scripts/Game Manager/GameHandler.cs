using UnityEngine;
using TMPro;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    public int[] floorSequence;
    public int floorSequenceIndex = 0;



    public enum DoorState { Closed, Open, Closing, Opening }
    public DoorState state;

    [SerializeField] private CameraShake Shake;
    [SerializeField] private GameObject doorLeft, doorRight;

    [Header("Correct Game Scene")]
    [SerializeField] private GameObject Scene1, Scene2, Scene3, Scene4, EndScene;

    [Header("InCorrect Game Scene")]
    [SerializeField] private GameObject IScene1, IScene2, IScene3, IScene4, IEndScene;



    // Open and close distance checkers
    [SerializeField] private GameObject OpenPosLeft, OpenPosRight, ClosePosLeft, ClosePosRight;


    //  Condition to open doors

    private bool _canOpen, _canClose;
    private bool _openInput = true;
    private bool _closeInput;
    public bool _canInteract;


    private int selectedFloor;


    // level input bools

   




    [HideInInspector] public bool _FollowOrder;


    [Header("Door Configuration")]
    [Range(1, 5)]
    private float speed = 1f;

    [SerializeField]
    private
        TextMeshProUGUI FloorText;


    // new bools 

    private bool _doorOpen;






    // Start is called before the first frame update
    void Start()
    {



        state = DoorState.Open;

        _closeInput = false;
        _canOpen = true;




        Scene1.SetActive(true);
        Scene2.SetActive(false);
        Scene3.SetActive(false);
        Scene4.SetActive(false);


        //IScene2.SetActive(false);
        //IScene3.SetActive(false);
        //IScene4.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (state == DoorState.Opening)
            DoorOpen();

        else if (state == DoorState.Closing)
            DoorClose();



        //DoorCO();

        // player input
        PlayerInput();

        // dist check 
        MinimalDistCheck();

        // bool check
        ConditionChecker();


  

        /*TEST CODE;

       // print("Can open bool " + CanOpen);
       // print("Can close bool " + CanOpen);
        */
        //Debug.Log("UPDATE");

    }

    #region Input region
    private void PlayerInput()
    {

        if (_canInteract && state == DoorState.Open)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                print("CLOSING");
                state = DoorState.Closing;

                selectedFloor = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedFloor = 2;
                state = DoorState.Closing;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selectedFloor = 3;
                state = DoorState.Closing;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                selectedFloor = 4;
                state = DoorState.Closing;
            }



        }
        /*
        else if (_canInteract && state == DoorState.Closed)
        {


        }
        */

        /*

        if (Input.GetKeyDown(KeyCode.E) && _canInteract)
        {
            if (state == DoorState.Open)
            {
                DoorClose();
            }


        }
        */

    }

    public void DoorCO()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //_canInteract = true;
        }
        */

    }


    #endregion

    #region Ditance Check


    #endregion

    private void ConditionChecker()
    {


    }




    private void SceneLoader(int Floor)
    {

        switch (Floor)
        {


            case 0:
                {
                    // DIALOG WITH PLAYER
                    FloorText.text = "0";
                    break;
                }

            case 1:
                {
                    // TO DO: 
                    // disable current scene

                    Scene1.SetActive(false);
                    Scene2.SetActive(true);

                    FloorText.text = "1";
                    Debug.Log("Scene 1: disabled / Scene 2: enabled");
                    break;
                }

            case 2:
                {
                    Scene2.SetActive(false);
                    Scene3.SetActive(true);

                    FloorText.text = "2";

                    Debug.Log("Scene 2: disabled / Scene 3: enabled");
                    break;
                }

            case 3:
                {
                    Scene3.SetActive(false);
                    Scene4.SetActive(true);

                    FloorText.text = "3";

                    Debug.Log("Scene 3: disabled / Scene 4: enabled");
                    break;
                }

            case 4:
                {
                    Scene4.SetActive(false);

                    FloorText.text = "4";
                    //EndScene.SetActive(true);   
                    //SceneManager.LoadScene("_EndGame");

                    break;
                }



            default: { break; }

        }

        // dispara 

        // TO DO
        // ADD SOM 


        state = DoorState.Opening;

    }
    private void MinimalDistCheck()
    {

        if (state == DoorState.Opening)
        {
            float minDist = 0.1f;
            bool leftOpen = false;
            bool RightOpen = false;



            if ((OpenPosLeft.transform.position - doorLeft.transform.position).magnitude < minDist)
            {
                doorLeft.transform.position = OpenPosLeft.transform.position;


                leftOpen = true;

            }

            if ((OpenPosRight.transform.position - doorRight.transform.position).magnitude < minDist)
            {
                doorRight.transform.position = OpenPosRight.transform.position;
                RightOpen = true;
            }

            if (leftOpen && RightOpen)
            {
                state = DoorState.Open;
                //_canInteract = true;

                //_openInput = false;
                //_closeInput = true;

                //_canOpen = false;
                //_canClose = true;
                //Doors();
                print("OPEN");

            }

        }

        else if (state == DoorState.Closing)
        {
            float CloseDist = 0.1f;

            bool leftClosed = false;
            bool RightClosed = false;



            if ((ClosePosLeft.transform.position - doorLeft.transform.position).magnitude < CloseDist)
            {
                doorLeft.transform.position = ClosePosLeft.transform.position;
                leftClosed = true;
            }
            if ((ClosePosRight.transform.position - doorRight.transform.position).magnitude < CloseDist)
            {
                doorRight.transform.position = ClosePosRight.transform.position;
                RightClosed = true;

            }

            if (leftClosed && RightClosed)
            {
               

                //_canInteract = true;
                //_closeInput = false;

                //_canOpen = true;
                //_canClose = false;
                state = DoorState.Closed;
                print("CLOSED");

                CheckSequence();

                // TO DO 
                // CALL IN COROTINA

                StartCoroutine(TimeOut());


                SceneLoader(selectedFloor);


            }

            else
            {
                //state= DoorState.Transition;

            }




        }

    }

    private void CheckSequence()
    {
        if (floorSequence[floorSequenceIndex] == selectedFloor)
        {
            // to do 
            // garantir que floor index nao é out of bounds
            floorSequenceIndex++;
            if (floorSequenceIndex == floorSequence.Length)
            {


                print("final");
                // nivel final

                // selected floor = fim 


            }

        }
        else
        {
            print("errou");
            floorSequenceIndex = 0;
        }
    }

    private void DoorOpen()
    {
        //Debug.Log("ABRE");
        doorLeft.transform.Translate(Vector3.left * speed * Time.deltaTime); // move left door left
        doorRight.transform.Translate(Vector3.right * speed * Time.deltaTime);// move right door rigt
    }

    private void DoorClose()
    {
        //Debug.Log("FECHA");
        doorLeft.transform.Translate(Vector3.right * speed * Time.deltaTime); // move left door right
        doorRight.transform.Translate(Vector3.left * speed * Time.deltaTime); // move right door left
    }

    private void FloorTimeOut()
    {
        
    }


    private IEnumerator TimeOut()
    {

        new WaitForSeconds(5f);
        state = DoorState.Opening;

        return;

    }


    private void CameraShake()
    {

    }

}

   
