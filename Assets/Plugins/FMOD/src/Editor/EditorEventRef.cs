using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FMODUnity
{
    public class EditorEventRef : ScriptableObject
    {
        [SerializeField]
        public string Path;

        [SerializeField]
        public FMOD.GUID Guid;

        [SerializeField]
        public List<EditorBankRef> Banks;
        [SerializeField]
        public bool IsStream;
        [SerializeField]
        public bool Is3D;
        [SerializeField]
        public bool IsOneShot;
        [SerializeField]
        public List<EditorParamRef> Parameters;
        [SerializeField]
        public float MinDistance;
        [SerializeField]
        public float MaxDistance;
        [SerializeField]
        public int Length;
	}
}
