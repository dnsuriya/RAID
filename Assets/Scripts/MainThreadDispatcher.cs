using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour
{
    static MainThreadDispatcher _inst;
    static MainThreadDispatcher inst
    {
        get
        {
            if (null == _inst)
            {
                GameObject obj = new GameObject("MainThreadDispather");
                _inst = obj.AddComponent<MainThreadDispatcher>();
                GameObject.DontDestroyOnLoad(obj);
            } 
            return _inst;
        }
    }

    public static void Init()
    {
        var reference = inst;
    }

    public static void Invoke(System.Action action, float delay = 0.0f)
    {
        if (null == action)
        {
            return;
        }
        inst.taskList.Add(new TimerTask(action, delay));
    }

    public static void InvokeRepeat(string key, System.Action action, float interval, float delay = 0.0f)
    {
        if (null == action)
        {
            return;
        }
        inst.taskList.Add(new TimerTask(key, action, delay, interval));
    }

    public static void StopRepeat(string key)
    {
        inst.taskList.RemoveAll(x => x.key == key);
    }

    class TimerTask
    {
        public float delay;
        public System.Action action;

        public string key = null;
        public bool repeat = false;
        public float interval = 9999.0f;

        public TimerTask(){}
        public TimerTask(System.Action aNewTask, float aNewDelay)
        {
            delay = aNewDelay;
            action = aNewTask;
        }

        public TimerTask(string aNewKey, System.Action aNewTask, float aNewDelay, float aNewInterval)
        {
            delay = aNewDelay;
            action = aNewTask;
            repeat = true;
            key = aNewKey;
            interval = aNewInterval;
        }
    }
    List<TimerTask> taskList = new List<TimerTask>();

    void Update()
    {
        lock (taskList)
        {
            List<TimerTask> exeList = new List<TimerTask>();
            foreach (var item in taskList)
            {
                item.delay -= Time.deltaTime;
                if (item.delay <= 0.0f)
                {
                    exeList.Add(item);
                }
            }

            foreach (var item in exeList)
            {
                try
                {
                    item.action.Invoke();
                }
                catch{ }
                if (item.repeat)
                {
                    item.delay = item.interval;
                }
                else
                {
                    taskList.Remove(item);
                }
            }
        }
    }
}
