sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
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
float2 uImageSize;
float alp;
float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
    // 获取每个像素的正确大小
    float dx = 1 / uImageSize.x;
    float dy = 1 / uImageSize.y;
    bool flag = false;
    // 对所有带有颜色的像素周围8格进行判定
    for (int i = -1; i <= 1; i++)
    {
        for (int j = -1; j <= 1; j++)
        {
            float4 c = tex2D(uImage0, coords + float2(dx * i, dy * j));
            // 如果任何一个像素没有颜色
            if (!any(c))
            {
                //直接判定是边缘的，描边
                flag = true;
            }
        }
    }
    
    if (flag)
        return float4(1, 1, 1, 1) * alp;
    if (coords.x <= dx || coords.x >= 1 - dx)
        return float4(1, 1, 1, 1) * alp;
    if (coords.y <= dy || coords.y >= 1 - dy)
        return float4(1, 1, 1, 1) * alp;
    return color;
}
technique Technique1
{
    pass BorderPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}