using UnityEditor;
using UnityEditor.UI;

namespace Core.Tools.UiComponents.Editor
{
    [CanEditMultipleObjects, CustomEditor(typeof(EmptyGraphics), false)]
    public class NonDrawingGraphicEditor : GraphicEditor
    {
        public override void OnInspectorGUI ()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Script);
            // skipping AppearanceControlsGUI
            serializedObject.ApplyModifiedProperties();
        }
    }
}