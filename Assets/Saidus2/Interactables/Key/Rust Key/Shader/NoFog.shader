Shader "Custom/UnlitIgnoreFog" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
    }
        SubShader{
            Tags {"Queue" = "Geometry"}
            LOD 100

            Pass {
                Color[_Color]
                Fog { Mode Off } // Cette ligne d�sactive la brume pour ce shader
            }
    }
}