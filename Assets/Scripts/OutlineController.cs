using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private Material _outlineMat;
    private Renderer _renderer;
    private Material[] _originalMats;
    private Material[] _newMats;
    private bool isOpen = false;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        CacheOriginMaterials();
        _newMats = new Material[_originalMats.Length + 1];
    }

    private void CacheOriginMaterials()
    {
        _originalMats  = _renderer.sharedMaterials;
    }

    public void TryOpen()
    {
        if (isOpen) return;
        if (_outlineMat == null) return;
        if (_originalMats == null || _originalMats.Length == 0) return;
        isOpen = true;
        Open();
    }

    public void TryClose()
    {
        if (!isOpen) return;
        if (_originalMats == null || _originalMats.Length == 0) return;
        isOpen = false;
        Close();
    }

    public void Open()
    {
        for (int i = 0; i < _originalMats.Length; i++)
        {
            _newMats[i] = _originalMats[i];
        }
        _newMats[_newMats.Length - 1] = _outlineMat;
        _renderer.materials = _newMats;
    }

    public void Close()
    {
        _renderer.materials = _originalMats;
    }
}
