using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerSpeed;
    public static bool allowInputs;

    public Slider countdownBar;
    private bool countDown = false;
    static bool contagiusBool;
    public float countDownTime;
    public float refillTime;
    public Text currentmoney,totalmoney,infectedAproximation,infectedAmount,endUITitle,endButtonText;
    public GameObject HUD,StartUI,SickUI,EndUI,Sun,TimeCounter,InfectionBar,InfectionStatus,endParagraphBad,endParagraphGood,EndUIPanel,ApocalypseUI;
    public ParticleSystem VirusEmitter, coinsEmitter;
    public AudioSource ambient,moving,coins,alarm;
    int money = 0;
    int total = 0;
    int threshold_money;
    int infectedPeople,infectedTotal,infectedAprox;
    static int currentLevelInt;
    Rigidbody rb;


    [SerializeField] enum State {Active,NotActive,Contagious};
    [SerializeField] enum Level {lvl1,lvl2,lvl3};

    static State currentState;
    Level currentLevel;
    
    void Start()
    {
        //Set the max value to the refill time
        rb = GetComponent<Rigidbody>();
        if(currentLevelInt == 0)
        {
            threshold_money = 3000;
        }        
        else if (currentLevelInt== 1)
        {
            print("LVL2!");
            threshold_money = 4000;
            StartUI.SetActive(false);
            HUD.SetActive(true);
            ambient.Play();
            TimeCounter.gameObject.GetComponent<TimedownCounter>().SendMessage("ChangeFlag");
            Sun.gameObject.GetComponent<SunDawn>().SetActive();

            if (contagiusBool)
            {
                currentState = State.Contagious;
                threshold_money = threshold_money + 500 * (currentLevelInt + 1);
                totalmoney.text = threshold_money.ToString();
                VirusEmitter.enableEmission = true;
                VirusEmitter.Play();
                playerSpeed = 3;
                InfectionBar.SetActive(false);
                InfectionStatus.gameObject.GetComponent<Text>().text = "COVID POSITIVO";
                InfectionStatus.gameObject.GetComponent<Text>().color = Color.red;
                
            }
            else {
                currentState = State.Active;
            }
        }
        else if (currentLevelInt == 2)
        {
            threshold_money = 5000;
            StartUI.SetActive(false);
            HUD.SetActive(true);
            allowInputs = true;
            ambient.Play();
            //TimeCounter.gameObject.GetComponent<TimedownCounter>().SendMessage("ChangeFlag");
            Sun.gameObject.GetComponent<SunDawn>().SetActive();
            if (contagiusBool)
            {
                currentState = State.Contagious;
                threshold_money = threshold_money + 500 * (currentLevelInt + 1);
                totalmoney.text = threshold_money.ToString();
                VirusEmitter.enableEmission = true;
                VirusEmitter.Play();
                playerSpeed = 3;
                InfectionBar.SetActive(false);
                InfectionStatus.gameObject.GetComponent<Text>().text = "COVID POSITIVO";
                InfectionStatus.gameObject.GetComponent<Text>().color = Color.red;
            }
            else
            {
                currentState = State.Active;
            }
        }
        totalmoney.text = threshold_money.ToString();
    }
    void Awake()
    {
        if (currentLevel != Level.lvl1)
        {
           //StartUI.SetActive(false);
            HUD.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(currentState == State.Active || currentState == State.Contagious)
        {
            InputHandler();
            CountDownController();
        }
        
    }

    private void InputHandler()
    {
        if (allowInputs)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //transform.position = transform.position + Vector3.forward * playerSpeed * Time.deltaTime;
                Vector3 directionVector = Vector3.forward;
                Vector3 directedVelocity = directionVector * playerSpeed;
                rb.velocity = directedVelocity;
                transform.rotation = Quaternion.Euler(Vector3.up * 0);
                if (!moving.isPlaying)
                {
                    moving.Play();
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                //transform.position = transform.position - Vector3.forward * playerSpeed * Time.deltaTime;
                Vector3 directionVector = -Vector3.forward;
                Vector3 directedVelocity = directionVector * playerSpeed;
                rb.velocity = directedVelocity;
                transform.rotation = Quaternion.Euler(Vector3.up * 180);
                if (!moving.isPlaying)
                {
                    moving.Play();
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                //transform.position = transform.position - Vector3.right * playerSpeed * Time.deltaTime;
                Vector3 directionVector = -Vector3.right;
                Vector3 directedVelocity = directionVector * playerSpeed;
                rb.velocity = directedVelocity;
                transform.rotation = Quaternion.Euler(Vector3.up * -90);
                if (!moving.isPlaying)
                {
                    moving.Play();
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                //transform.position = transform.position + Vector3.right * playerSpeed * Time.deltaTime;
                Vector3 directionVector = Vector3.right;
                Vector3 directedVelocity = directionVector * playerSpeed;
                rb.velocity = directedVelocity;
                transform.rotation = Quaternion.Euler(Vector3.up * 90);
                if (!moving.isPlaying)
                {
                    moving.Play();
                }
            }
            if (Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.S)|| Input.GetKeyUp(KeyCode.D))
            {
                if (moving.isPlaying)
                {
                    moving.Pause();
                }
            }
            
        }
        
    }

    private void CountDownController()
    {
        if (countdownBar.maxValue != refillTime)
        {
            //countdownBar.maxValue = refillTime;
            print(refillTime);
        }

        if (countDown)
        {  //Scale the countdown time to go faster than the refill time
            countdownBar.value += Time.deltaTime / countDownTime * refillTime;

        }
        else
        { // 
            countdownBar.value -= Time.deltaTime / countDownTime * refillTime;
        }

        //If we are at 0, start to refill 
        //TO DO: change according to selling status
        if (countdownBar.value <= 0)
        {
            countDown = false;
            //allowInputs = false;
            
        }
        else if (countdownBar.value >= refillTime && countDown)
        {

            countDown = false;           
            currentState = State.NotActive;
            SickUI.SetActive(true);
            TimeCounter.gameObject.GetComponent<TimedownCounter>().SendMessage("SetStopTime");            
            threshold_money = threshold_money + 500 * (currentLevelInt+1);
            totalmoney.text = threshold_money.ToString();
            ambient.Pause();
            coins.Pause();
            coinsEmitter.Stop();
            moving.Stop();
            alarm.Stop();
            VirusEmitter.enableEmission = true;
            VirusEmitter.Play();
            contagiusBool = true;
        }
    }

    private void OnTriggerEnter(Collider c) {

        if (c.gameObject.tag == "Person")
        {
            if (currentState != State.Contagious)
            {
                countDown = true;

            }
            if (!c.gameObject.GetComponent<PersonCollideManager>().infected && currentState == State.Contagious)
            {
                infectedPeople = infectedPeople + 1;
                c.gameObject.GetComponent<PersonCollideManager>().infected = true;
            }
           
            
        }
        if(c.gameObject.tag == "Untagged")
        {
            if (currentState != State.Contagious)
            {
                countDown = true;
                alarm.Play();
            }
        }
    }

    private void OnTriggerExit(Collider c)
    {

        if (c.gameObject.tag == "Person"|| c.gameObject.tag == "Untagged")
        {
            countDown = false;
            if (coins.isPlaying)
            {
                coins.Stop();                
                coinsEmitter.Stop();
            }
            alarm.Stop();
        }
        
    }
    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Person")
        {
            if (!coins.isPlaying && currentState != State.NotActive) 
            {
                coins.Play();
                coinsEmitter.enableEmission = true;
                coinsEmitter.Play();
                if(currentState != State.Contagious)
                {
                    alarm.Play();
                }                
            }
           
            
            
            int actualmoney = c.gameObject.GetComponent<PersonCollideManager>().money;
            print(actualmoney);
            if (actualmoney <= 0)
            {
                c.gameObject.SendMessage("changeState");
                
            }
            
            total= total + c.gameObject.GetComponent<PersonCollideManager>().GetMoney(); 
            //int valueOverTime = money * (int)Time.deltaTime;
            //print("Money is " + money);
            currentmoney.text = total.ToString();
        }
        if(c.gameObject.tag == "Untagged")
        {
            
            coins.Stop();
            coinsEmitter.Stop();
        }


    }

    private void giveMoney(int moneyValue)
    {
        
            money = money + moneyValue;
    }

    public void Activate()
    {

        currentLevel = Level.lvl1;
        allowInputs = true;
        //countdownBar.maxValue = refillTime;
        currentState = State.Active;
        HUD.SetActive(true);        
        StartUI.SetActive(false);
        Sun.gameObject.GetComponent<SunDawn>().SetActive();
        ambient.Play();
        
    }

    public void SetContagious() {
        
        allowInputs = true;
        //countdownBar.maxValue = -10;
        countDown = false;
        currentState = State.Contagious;
        HUD.SetActive(true);
        StartUI.SetActive(false);
        SickUI.SetActive(false);
        Sun.gameObject.GetComponent<SunDawn>().SetActive();
        TimeCounter.gameObject.GetComponent<TimedownCounter>().SendMessage("SetContinueTime");
        InfectionBar.SetActive(false);
        InfectionStatus.gameObject.GetComponent<Text>().text = "COVID POSITIVO";
        InfectionStatus.gameObject.GetComponent<Text>().color = Color.red;
        playerSpeed = 3;
        ambient.Play();
    }

    public void StartEndSequence() 
    {
        ambient.Stop();
        coins.Stop();
        coinsEmitter.Stop();
        moving.Stop();
        if (currentState == State.Contagious)
        {
            int aproxCalc = (int)Mathf.Pow(2, infectedPeople);
            infectedAproximation.text = aproxCalc.ToString();
            infectedAmount.text = infectedPeople.ToString();
        }
        else {
            infectedAproximation.text = "0";
            infectedAmount.text = "0";
        }

        if (total >= threshold_money) 
        {
            endUITitle.text = "¡HAS SOBREVIVIDO EL DIA!";
            endParagraphGood.SetActive(true);
            endButtonText.text = "CONTINUAR";
            EndUIPanel.gameObject.GetComponent<Image>().color = Color.green;
            currentLevelInt = currentLevelInt + 1;
            EndUI.SetActive(true);
            if (currentLevelInt == 3)
            {
                EndUI.SetActive(false);
                ApocalypseUI.SetActive(true);
            }
            

        } 
        else
        {
            endUITitle.text = "!NO HAS CONSEGUIDO EL DINERO SUFICIENTE!";
            endParagraphBad.SetActive(true);
            endButtonText.text = "REINTENTAR";
            EndUIPanel.gameObject.GetComponent<Image>().color = Color.red;
            currentLevelInt = 0;
            EndUI.SetActive(true);
        }
        
    }

    public void loadNextLevel()
    {
        
        SceneManager.LoadScene(currentLevelInt);
        
    }
    public void loadFirstLevel()
    {

        SceneManager.LoadScene(0);

    }
}
