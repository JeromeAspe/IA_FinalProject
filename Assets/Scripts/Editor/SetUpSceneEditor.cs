using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;
public class SetUpSceneEditor
{

 #region Custom Methods
    [MenuItem("Project SetUp/Hierarchy")]
    static void DefaultSetUp()
    {
        SetObjectInHierarchy<Camera>("CAMERAS");
        SetObjectInHierarchy<Light>("LIGHTS");
        CreateHierarchySection("PROPS");
        CreateHierarchySection("LOGIC");
    }

    static void SetObjectInHierarchy<T>(string _title) where T:Component
    {
        GameObject _section = FindSection(_title);
        int _sectionIndex = _section.transform.GetSiblingIndex();
        Type _type = typeof(T);
        Object[] _objects = GameObject.FindObjectsOfType(_type);
        for (int i = 0; i < _objects.Length; i++)
        {
            T _object = (T)Convert.ChangeType(_objects[i], _type);
            _object.transform.SetSiblingIndex(_sectionIndex);
        }
        new GameObject($"------------");
    }
    static GameObject FindSection(string _title)
    {
        string _nameTempate = $"------[{_title}]------";
        GameObject _section = null;
        if (!(_section = GameObject.Find(_nameTempate)))
        {
            _section = new GameObject(_nameTempate);
        }
        return _section;
    }
    static void CreateHierarchySection(string _sectionName)
    {
        GameObject _section = FindSection(_sectionName);
        _section = new GameObject("------------");
    }
    #endregion Custom Methods

}
