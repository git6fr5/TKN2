/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class SpotUI : MonoBehaviour {

        public int m_Found = 0;
        public int m_NumSpots = 3;

        public static SpotUI MainSpotUI;
        public Text m_FoundText;

        void Awake() {
            MainSpotUI = this;
        }

        public void Add() {
            m_Found += 1;
            m_FoundText.text = m_Found.ToString() + "/" + m_NumSpots.ToString();
        }

    }
}