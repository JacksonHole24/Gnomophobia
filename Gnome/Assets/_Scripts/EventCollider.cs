using PathCreation.Examples;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum ExitEvent
{
    None,
    Timer,
    ExitCollider,
    TargetReached
}

public enum OnExit
{
    Nothing,
    ReturnToDefaultSpeed,
    ReturnToPreviousSpeed
}

public enum OnEnter
{
    Nothing,
    ChangeSpeed
}

public class EventCollider : MonoBehaviour
{
    PathFollower m_lastPathFollower;

    [Header("Enter")]

    [SerializeField] OnEnter m_onEnter;

    [ConditionalEnumHide("m_onEnter", (int)global::OnEnter.ChangeSpeed, HideInInspector = true)]
    [SerializeField] float m_targetSpeed, m_enterAcceleration = 20;
    float m_previousSpeed;

    [Header("Exit")]

    [SerializeField] ExitEvent m_exitEvent;

    [ConditionalEnumHide("m_exitEvent", (int)ExitEvent.Timer, HideInInspector = true)]
    [SerializeField] float m_timeUntilExit;

    [ConditionalEnumHide("m_exitEvent", (int)ExitEvent.TargetReached, HideInInspector = true)]
    [SerializeField] float m_timeDelay;
    bool m_hasStartedDelay;
    
    [Space(10)]

    [ConditionalEnumHide("m_exitEvent", 0, HideInInspector = true, Inverse = true)]
    [SerializeField] OnExit m_onExit;

    [ConditionalEnumHide("m_onExit", 1, 2, HideInInspector = true, ConditionalSourceField2 = "m_exitEvent", Enum2Value1 = 0, Inverse2 = true)]
    [SerializeField] float m_exitAcceleration = 20;

    [Space(20)]
    
    public UnityEvent Enter;
    public UnityEvent Exit;

    bool m_isCollided = false;


    private void OnTriggerEnter(Collider other)
    {
        PathFollower pathFollower = other.GetComponent<PathFollower>();
        if (pathFollower)
        {
            OnEnter(pathFollower);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PathFollower pathFollower = other.GetComponent<PathFollower>();
        if (pathFollower)
        {
            if (m_exitEvent == ExitEvent.ExitCollider)
            {
                OnExit(pathFollower);
            }
        }
    }

    private void Update()
    {
        HandleTargetReachedDelay();

    }

    void HandleTargetReachedDelay()
    {
        if (!m_hasStartedDelay)
        {
            if (m_exitEvent == ExitEvent.TargetReached)
            {
                if (m_lastPathFollower)
                {
                    if (m_targetSpeed == m_lastPathFollower.currentSpeed)
                    {
                        m_hasStartedDelay = true;
                        TimerManager.Instance.RunAfterTime(OnExit, m_timeDelay);
                    }
                }
            }
        }
    }

    void OnEnter(PathFollower pathFollower)
    {
        if (!m_isCollided)
        {
            m_isCollided = true;

            m_lastPathFollower = pathFollower;

            m_previousSpeed = pathFollower.currentSpeed;
            pathFollower.ChangeSpeed(m_targetSpeed, m_enterAcceleration);

            if (m_exitEvent == ExitEvent.Timer)
            {
                TimerManager.Instance.RunAfterTime(OnExit, m_timeUntilExit);
            }

            Enter.Invoke();
        }
    }

    void OnExit(PathFollower pathFollower)
    {
        if (m_isCollided)
        {
            m_isCollided = false;

            m_hasStartedDelay = false;

            if (m_onExit == global::OnExit.ReturnToDefaultSpeed)
            {
                pathFollower.ChangeSpeed(pathFollower.defaultSpeed, m_exitAcceleration);
            }
            else if (m_onExit == global::OnExit.ReturnToPreviousSpeed)
            {
                Debug.Log(m_previousSpeed);
                pathFollower.ChangeSpeed(m_previousSpeed, m_exitAcceleration);
            }

            Exit.Invoke();
        }
    }

    void OnExit()
    {
        PathFollower pathFollower = m_lastPathFollower;
        if (pathFollower)
        {
            OnExit(pathFollower);
        }
    }

    

    
}