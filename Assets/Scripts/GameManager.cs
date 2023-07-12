using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class GameManager : MonoBehaviour
{
    public static event Action OnFailingRun;
    public static event Action OnSucceedingRun;
    public static UnityEvent<float> OnTimeLeft;
    ArrayList players;
    public float amountOfTime;
    float timer, bestTime;
    bool startedCourse;
    bool finishedCourse;
    int amountOfTargets, amountOfTargetsHit;
    public static GameManager GetManager()
    {
        return currentManager;
    }
    static GameManager currentManager = null;

    //Awake is called before even Start
    private void Awake()
    {
        if (currentManager != null)
        {
            Destroy(gameObject); //we are not needed
        }
        else
        {
            OnTimeLeft = new UnityEvent<float>();
            DontDestroyOnLoad(gameObject);
            currentManager = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        players = new ArrayList();
        StartTimer.OnStartingTimer += TimerStart;
        HowManyTargets.OnHowManyTargetsThereAre.AddListener(AmountOfTargets);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (finishedCourse)
            return;
        if (amountOfTargets != 0 && amountOfTargetsHit >= amountOfTargets)
        {
            Debug.Log("Finished Level!");
            finishedCourse = true;
            startedCourse = false;
            OnSucceedingRun?.Invoke();
            float finalTime = timer - Time.time;
            if (finalTime > bestTime)
                bestTime = finalTime;
            return;
        }
        if (startedCourse)
        {
            OnTimeLeft?.Invoke(Mathf.Clamp(timer - Time.time, 0, timer));
            if (Time.time > timer)
            {
                Debug.Log("Time's up!");
                OnFailingRun?.Invoke();
            }
        }
    }

    void AmountOfTargets(int amount)
    {
        amountOfTargets = amount;
        //Debug.Log("amountOfTargets: " + amountOfTargets);
    }

    public void TimerStart()
    {
        Debug.Log("start timer in GameManager");
        timer = Time.time + amountOfTime;
        amountOfTargetsHit = 0;
        finishedCourse = false;
        startedCourse = true;
    }

    public void TargetHit(GameObject player, Vector3 direction,RaycastHit hitInfo)
    {
        //Debug.Log("a target got hit!")
        Hittable hittable = hitInfo.collider.gameObject.GetComponent<Hittable>();
        if (!players.Contains(player))
            players.Add(player);
        if (hittable.allowedToBeHitTimes >= 1)
        {
            hittable.TakeHit(direction, 90, hitInfo.point);
            player.GetComponent<PlayerScript>().hitCount++;
            amountOfTargetsHit++;
        }
    }
    private void OnDestroy()
    {
        HowManyTargets.OnHowManyTargetsThereAre.RemoveListener(AmountOfTargets);
        StartTimer.OnStartingTimer -= TimerStart;
    }
}
