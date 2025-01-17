using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class GameState
{
    public static bool isDay { get; set; }
    public static bool isFpv { get; set; }
    public static float batteryLevel { get; set; }
    public static bool isRecharge { get; set; }
    public static Dictionary<String, object> collectedItems {get;set;} = new();
    #region  Game events
    private const string broadcastKey = "Broadcast";

    public static void TriggerGameEvent(String eventName, object data){
        if(subscribers.ContainsKey(eventName)){
            foreach(var action in subscribers[eventName]){
                action(eventName,data);
            }
        }

        if(subscribers.ContainsKey(broadcastKey)){
            foreach(var action in subscribers[broadcastKey]){
                action(eventName,data);
            }
        }
    }
    private static Dictionary<string, List<Action<String,object>>> subscribers = new();
    public static void Subsribe(Action<String,object> action, String eventName = null){
        eventName ??=broadcastKey;
        if(subscribers.ContainsKey(eventName)){
            subscribers[eventName].Add(action);
        }
        else{
            subscribers[eventName] = new(){action};
        }
    }

    public static void Unsubscribe(Action<String,object> action, String eventName = null){
        eventName ??=broadcastKey;
        if(subscribers.ContainsKey(eventName)){
            subscribers[eventName].Remove(action);
        }
        else{
            Debug.LogWarning("Unsubscribe of not subscribed key" + eventName);
        }
    }
   

    #endregion
}
