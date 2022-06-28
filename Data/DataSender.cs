/* --- Libraries --- */
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class DataSender {

        // Package the data and send it to the react application.
        public static void Send(){
            // Convert the data into a json string.
            DataTracker data = DataTracker.Instance;
            string jsonStr = JsonUtility.ToJson(data);

            // Send the JSON packet through the JavaScript function.
            #if UNITY_WEBGL == true && UNITY_EDITOR == false
                JSGet(jsonStr);
            #endif
        }

        // Imports the JavaScript function from the JS Library.
        [DllImport("__Internal")]
        protected static extern void JSGet(string jsonStr);

    }

}