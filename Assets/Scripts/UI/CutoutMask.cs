using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace UI
{
    // I created this originally for the circle cutout on death, but ended up using it another way, but this seemed useful.
    /// <summary>
    /// Used to make the opposite of an image mask in the UI.
    /// </summary>
    public class CutoutMask : Image
    {
        private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");

        public override Material materialForRendering {
            get
            {
                var rendering = new Material(base.materialForRendering);
                rendering.SetInt(StencilComp, (int)CompareFunction.NotEqual);
                return rendering;
            }
        }
    }
}