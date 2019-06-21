using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    //static reference of the singleton class
    private static EventManager _instance;

    //public static reference of this manager class
    public static EventManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<EventManager>();
            }

            return _instance;
        }
    }

    private EventWrapper _cachedWrapper = new EventWrapper();

    private Dictionary<string, Action<IEventWrapper>> _eventDict = new Dictionary<string, Action<IEventWrapper>>();

    //This helps to ensure callbacks are only registered once per event
    private Dictionary<string, Dictionary<Delegate, Action<IEventWrapper>>> _eventSearchDict = new Dictionary<string, Dictionary<Delegate, Action<IEventWrapper>>>();

    #region Add Listener Methods
    public void AddListener(string key, Action action) {
        Action<IEventWrapper> wrapperDel = (param) => action();

        InternalAddListener(key, action, wrapperDel);
    }

    public void AddListener<DataType>(string key, Action<DataType> action) {
        Action<IEventWrapper> wrapperDel = (param) => action(((EventWrapper<DataType>) param).dataType);

        InternalAddListener(key, action, wrapperDel);
    }

    public void AddListener<DataType1, DataType2>(string key, Action<DataType1, DataType2> action) {
        Action<IEventWrapper> wrapperDel = (param) => action(((EventWrapper<DataType1, DataType2>) param).dataType1,
                                                             ((EventWrapper<DataType1, DataType2>) param).dataType2);

        InternalAddListener(key, action, wrapperDel);
    }
    #endregion

    #region Send Event Methods
    public void SendEvent(string key) {
        InternalSendNotification(key, _cachedWrapper);
    }

    public void SendEvent<DataType>(string key, DataType data) {
        InternalSendNotification(key, new EventWrapper<DataType>(data));
    }

    public void SendEvent<DataType1, DataType2>(string key, DataType1 data1, DataType2 data2) {
        InternalSendNotification(key, new EventWrapper<DataType1, DataType2>(data1, data2));
    }
    #endregion

    #region Remove Listener Method
    public void RemoveListener(string key, Action action) {
        InternalRemoveListener(key, action);
    }

    public void RemoveListener<DataType>(string key, Action<DataType> action) {
        InternalRemoveListener(key, action);
    }

    public void RemoveListener<DataType1, DataType2>(string key, Action<DataType1, DataType2> action) {
        InternalRemoveListener(key, action);
    }


    #endregion

    private void InternalAddListener(string key, Delegate func, Action<IEventWrapper> action) {
        //if new key, add listener to delegate
        if (_eventDict.ContainsKey(key) == false) {
            //Key = key string name for this case
            //action in this case, is the value
            _eventDict.Add(key, action);
            _eventSearchDict.Add(key, new Dictionary<Delegate, Action<IEventWrapper>>());
            _eventSearchDict[key].Add(func, action);
        }

        //Add listener to delegate if needed
        else if (_eventSearchDict[key].ContainsKey(func) == false) {
            _eventDict[key] += action;
            _eventSearchDict[key].Add(func, action);
        }
    }

    private void InternalRemoveListener(string key, Delegate func) {
        //Make sure key exsists before trying to remove it from dictionary
        if (_eventDict.ContainsKey(key) == true) {
            if (_eventSearchDict[key].ContainsKey(func) == true) {
                //Remove the listener from the delegate
                _eventDict[key] -= _eventSearchDict[key][func];

                //Remove the listener from the search dictionary
                _eventSearchDict[key].Remove(func);

                //If there is no listener in the delegate, remove the delegate
                if (_eventDict[key] == null) {
                    _eventDict.Remove(key);
                    _eventSearchDict.Remove(key);
                }
            }
        }
    }

    private void InternalSendNotification(string key, IEventWrapper eventWrapper) {
        if (_eventDict.ContainsKey(key) == true && _eventDict[key] != null) {
            //send event with delegates to listeners
            _eventDict[key](eventWrapper);
        }
    }

    private void OnDestroy() {
        if (_eventDict != null) {
            _eventDict.Clear();
            _eventDict = null;
        }

        if (_eventSearchDict != null) {
            _eventSearchDict.Clear();
            _eventSearchDict = null;
        }
    }
}
