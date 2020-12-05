using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCollideManager : MonoBehaviour
{
    [SerializeField] int count;
    public int money;
    public bool infected;
    enum State { Active, NotActive };
    State currentState;
    // Start is cal1led before the first frame update
    void Start()
    {
        count = 0;
        money = 600;
        currentState = State.Active;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int GetMoney() {
        if (money > 0)
        {
            money = money - 3;
            return 3;
        }
        else {
            return 0;
        }
        
    }


    void moneyCount(int value)
    {/*
        //print(count);
        money = 1;
        count = count + value;
        //print(count);
        if (count > 500) {
            transform.gameObject.tag = "Untagged";

        }*/
    }

    private void OnCollisionStay(Collision c)
    {
       /* if (c.gameObject.tag == "Player")
        {
            if (currentState.Equals(State.Active))
            {
                c.gameObject.SendMessage("giveMoney", 1);
            }                     
        }*/
    }

    private void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            //money = 0;
        }
    }


    private void changeState()
    {
        currentState = State.NotActive;
        transform.gameObject.tag = "Untagged";
    }

}
