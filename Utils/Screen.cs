/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Screen.
/// </summary>
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(PixelPerfectCamera))]
public class Screen : MonoBehaviour {

    /* --- Variables --- */
    #region Variables

    // Instance.
    public static Screen Instance;

    // Camera.
    [HideInInspector] private Camera m_MainCamera;
    public static Camera MainCamera => Instance.m_MainCamera;
    public static Vector2 MousePosition => (Vector2)Instance.m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
    [SerializeField] private PixelPerfectCamera m_PixelPerfectCamera;

    // Settings.
    [SerializeField, ReadOnly] private Vector2 m_ScreenSize;
    public static Vector2 ScreenSize => Instance.m_ScreenSize;
    [SerializeField, ReadOnly] private Vector3 m_Origin;
    public static Vector3 Origin => Instance.m_Origin;

    #endregion

    /* --- Unity --- */
    #region Unity

    void Awake() {
        Instance = this;
        m_Origin = (Vector2)transform.position;
        m_MainCamera = Camera.main;
    }

    #endregion
    
    /* --- Debugging --- */
    #region Debugging

    void OnDrawGizmos() {
        m_ScreenSize = new Vector3((float)m_PixelPerfectCamera.refResolutionX, (float)m_PixelPerfectCamera.refResolutionY, 0f) / m_PixelPerfectCamera.assetsPPU;
        Gizmos.DrawWireCube(transform.position, m_ScreenSize);
    }

    #endregion


}
