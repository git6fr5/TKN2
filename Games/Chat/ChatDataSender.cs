/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class ChatDataSender : DataSender {

        // Package the data and send it to the react application.
        public static void SendChatData(){
            // Convert the data into a json string.
            ChatDataTracker data = ChatDataTracker.ChatInstance;
            string jsonStr = JsonUtility.ToJson(data);

            Debug.Log("Sending data");

            // Send the JSON packet through the JavaScript function.
            #if UNITY_WEBGL == true && UNITY_EDITOR == false
                JSGet(jsonStr);
            #endif
        }

    }
}