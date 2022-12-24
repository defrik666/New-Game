using UnityEngine;

#if UNITY_EDITOR
 using UnityEditor;
#endif

public class AttachmentStats : MonoBehaviour
{
    public enum AttachmentTypes{
        None,
        Magazine,
        Handguard,
        GunStock,
        Handle,
        Forearm
    }
    public AttachmentTypes _type = AttachmentTypes.None;
    [HideInInspector]
    [SerializeField] public int ammo;
    [HideInInspector]
    [SerializeField] public float spread;
    [HideInInspector]
    [SerializeField] public float recoil;

    private void Awake() {

    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(AttachmentStats))]
public class AttachmentTypes_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    
        AttachmentStats script = (AttachmentStats)target;

        if(script._type != AttachmentStats.AttachmentTypes.None){

            EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
        } 
        switch(script._type){
                
            case AttachmentStats.AttachmentTypes.None:
                serializedObject.FindProperty("ammo").intValue = 0;
                serializedObject.FindProperty("spread").floatValue = 0;
                serializedObject.FindProperty("recoil").floatValue = 0;
                
                break;
            case AttachmentStats.AttachmentTypes.Magazine:
                serializedObject.FindProperty("spread").floatValue = 0;
                serializedObject.FindProperty("recoil").floatValue = 0;

                script.ammo = EditorGUILayout.IntField("Ammo capacity", script.ammo);
                serializedObject.FindProperty("ammo").intValue = script.ammo;
                serializedObject.ApplyModifiedProperties();
                break;
            case AttachmentStats.AttachmentTypes.Handguard:
                serializedObject.FindProperty("ammo").intValue = 0;

                script.spread = EditorGUILayout.FloatField("Spread reduction", script.spread);
                script.recoil = EditorGUILayout.FloatField("Recoil reduction", script.recoil);
                serializedObject.FindProperty("spread").floatValue = script.spread;
                serializedObject.FindProperty("recoil").floatValue = script.recoil;
                serializedObject.ApplyModifiedProperties();
                break;
            case AttachmentStats.AttachmentTypes.GunStock:
                serializedObject.FindProperty("ammo").intValue = 0;

                script.spread = EditorGUILayout.FloatField("Spread reduction", script.spread);
                script.recoil = EditorGUILayout.FloatField("Recoil reduction", script.recoil);
                serializedObject.FindProperty("spread").floatValue = script.spread;
                serializedObject.FindProperty("recoil").floatValue = script.recoil;
                serializedObject.ApplyModifiedProperties();
                break;
            case AttachmentStats.AttachmentTypes.Handle:
                serializedObject.FindProperty("ammo").intValue = 0;

                script.spread = EditorGUILayout.FloatField("Spread reduction", script.spread);
                script.recoil = EditorGUILayout.FloatField("Recoil reduction", script.recoil);
                serializedObject.FindProperty("spread").floatValue = script.spread;
                serializedObject.FindProperty("recoil").floatValue = script.recoil;
                serializedObject.ApplyModifiedProperties();
                break;
            case AttachmentStats.AttachmentTypes.Forearm:
                serializedObject.FindProperty("ammo").intValue = 0;

                script.spread = EditorGUILayout.FloatField("Spread reduction", script.spread);
                script.recoil = EditorGUILayout.FloatField("Recoil reduction", script.recoil);
                serializedObject.FindProperty("spread").floatValue = script.spread;
                serializedObject.FindProperty("recoil").floatValue = script.recoil;
                serializedObject.ApplyModifiedProperties();
                break;
        }
    }
}  
#endif

