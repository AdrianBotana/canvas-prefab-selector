using UnityEngine;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine.UIElements;
using System.Linq;
using UnityEditor.SceneManagement;

namespace Adruian.Pages.Editor
{
    [Overlay(typeof(SceneView), "Canvas Prefab Selector")]
    public class CanvasPrefabSelectorOverlay : Overlay
    {
        public override VisualElement CreatePanelContent()
        {
            VisualElement root = new VisualElement();

            foreach (Canvas canvas in Object.FindObjectsOfType<Canvas>(true))
            {
                if (!PrefabUtility.IsPartOfAnyPrefab(canvas)) continue;

                var button = new Button();
                button.clicked += Click;
                button.text = canvas.name;
                root.Add(button);
                void Click() => AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<GameObject>(PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(canvas.gameObject)));
            }

            return root;
        }
    }
}