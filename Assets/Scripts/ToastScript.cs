using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ToastScript : MonoBehaviour
{
 

    private  TMPro.TextMeshProUGUI toastTMP;
    private static ToastScript instance;
    private float timeout = 1f;
    private float leftTime;
    private  Queue<ToastMessage> messages = new Queue<ToastMessage>();
    private GameObject content;

    public static void ShowToast(string message, string author = null, float? timeout = null){
        if(instance.messages.Count > 0 && 
            instance.messages.Peek().message == message)
        {
            return;
        }
        ToastMessage newMessage = new ToastMessage
        {
            author = author,
            message = message, 
            timeout = timeout?? instance.timeout 
        };
        if(!instance.messages.Any(m => m.author == author && m.message == message)){
            instance.messages.Enqueue(newMessage);
            Debug.Log("Повідомлення було успішно додано до черги\n"     +
            ( newMessage.author != null ? newMessage.author+ ": " : "") +
                                                    newMessage.message);
        }
        else{
            Debug.Log("Це повідомлення вже є у черзі");
            return;
        }
        
       
    }
    
    private void OnGameEvent(string eventName, object data){
        if(data is GameEvents.INotifier n){
            ShowToast(n.message);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        content = transform.Find("Content").gameObject;
        toastTMP = content.transform.Find("ToastTMP").GetComponent<TMPro.TextMeshProUGUI>();
        content.SetActive(false);
        GameState.Subsribe(OnGameEvent);
    }
    // Update is called once per frame
    void Update()
    {
        if(leftTime >0){

            leftTime -= Time.deltaTime;
            if(leftTime <= 0){
                messages.Dequeue();
                content.SetActive(false);
            }
        }
        else{
            if(messages.Count > 0){
                var m = messages.Peek();
                toastTMP.text = (m.author != null ? m.author + ": ": "") + m.message;
                leftTime = m.timeout;
                content.SetActive(true);
            }
        }
    }

    void OnDestroy()
    {
        GameState.Unsubscribe(OnGameEvent);
    }

    private class ToastMessage{
        public string author {get;set;}
        public string message {get;set;}
        public float timeout {get;set;}
    }
}
