using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace UI
{
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