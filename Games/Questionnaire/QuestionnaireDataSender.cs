/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class QuestionnaireDataSender : DataSender {

        // Package the data and send it to the react application.
        public static void SendQuestionnaireData(){
            // Convert the data into a json string.
            QuestionnaireDataTracker data = QuestionnaireDataTracker.QuestionnaireInstance;
            string jsonStr = JsonUtility.ToJson(data);

            Debug.Log("Sending data");

            // Send the JSON packet through the JavaScript function.
            #if UNITY_WEBGL == true && UNITY_EDITOR == false
                JSGet(jsonStr);
            #endif
        }

    }
}