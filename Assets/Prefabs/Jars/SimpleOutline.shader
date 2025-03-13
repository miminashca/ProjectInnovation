Shader "Custom/SimpleOutline"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (1, 0, 1, 1) // Ярко-розовый по умолчанию
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.03
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // Единственный проход, который «выпирает» вершины и рисует обводку
        Pass
        {
            Name "OUTLINE"
            // Cull Front - чтобы отрисовывались только «задние» грани расширенной копии
            Cull Front
            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _OutlineWidth;
            fixed4 _OutlineColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata IN)
            {
                v2f OUT;
                // Преобразуем вершину и нормаль в мировое пространство
                float3 worldNormal = UnityObjectToWorldNormal(IN.normal);
                float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);

                // Смещаем позицию вдоль нормали на _OutlineWidth
                worldPos.xyz += worldNormal * _OutlineWidth;

                // Проецируем в координаты клипа
                OUT.pos = mul(UNITY_MATRIX_VP, worldPos);
                return OUT;
            }

            fixed4 frag (v2f IN) : SV_Target
            {
                // Закрашиваем всё цветом обводки
                return _OutlineColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
