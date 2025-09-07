sampler uImage0 : register(s0);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;
float intense;
float PI = 3.14;
texture2D tex;
sampler2D uImage1 = sampler_state
{
    texture = <tex>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap;
};
float4 PixelShaderFunction(float4 position : SV_POSITION, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 color1 = tex2D(uImage1, coords);
    if (!any(color1))
        return color;
    else
    {
        float2 vec = float2(0, 0);
        float rot = color1.r * 6.28f;
        vec = float2(cos(rot), sin(rot)) * color1.r * intense;
        return tex2D(uImage0, coords + vec);
    }
}
technique Technique1
{
    pass WarpPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}