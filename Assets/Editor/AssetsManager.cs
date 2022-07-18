using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class AssetsManager : EditorWindow
{


    Sprite m_XSprite = null;
    Sprite m_OSprite = null;
    Sprite m_Background = null;
    string m_name = "";


    [MenuItem("Window/Create AssetBundle")]
    public static void ShowWindow()
    {
      
        GetWindow(typeof(AssetsManager));
    }
    void OnGUI()
    {
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("X Symbol: ", EditorStyles.boldLabel);
        m_XSprite = (Sprite)EditorGUILayout.ObjectField(m_XSprite, typeof(Sprite), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(" O Symbol: ", EditorStyles.boldLabel);
        m_OSprite = (Sprite)EditorGUILayout.ObjectField(m_OSprite, typeof(Sprite), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Background: ", EditorStyles.boldLabel);
        m_Background = (Sprite)EditorGUILayout.ObjectField(m_Background, typeof(Sprite), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Asset bundle name: ", EditorStyles.boldLabel);
        m_name = GUILayout.TextField(m_name,20,"TextField");
        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Create Asset Bundle"))
        {
            string[] Assets = new string[] { AssetDatabase.GetAssetPath(m_XSprite), AssetDatabase.GetAssetPath(m_OSprite), AssetDatabase.GetAssetPath(m_Background) };
            BuildMapAssetBundlesTicTac(Assets, m_name);
            
        }
        GUIUtility.ExitGUI();


    }

    static void BuildAssetBundles(string Main ,Object[] Bundle)
    {
        string assetBundleDirectory = "Assets/StreamingAssets";
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        //BuildPipeline.BuildAssetBundles(Main,Bundle,assetBundleDirectory,);
    }

    static void BuildMapAssetBundlesTicTac(string[] Assets, string Name)
    {   
        string assetBundleDirectory = "Assets/StreamingAssets";
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        

        buildMap[0].assetBundleName = Name;

        buildMap[0].assetNames = Assets;

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);



    }
}
