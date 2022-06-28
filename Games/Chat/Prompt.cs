/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    [System.Serializable]
    public class Prompt {

        // The interval between printing letters.
        public static float PrintInterval = 0.05f; 

        // The lines this prompt has.
        [SerializeField] private string[] m_Lines;
        public string[] Lines => m_Lines;

    }

}