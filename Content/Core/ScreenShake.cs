// TODO : Debug and upgrade screenshake effects
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MeleeRevamp.Content.Core
{
    public class ScreenShake : ModPlayer
    {
        public int SSStimer = 3;
        public int SSSrange = 0;
        public float SSSradius = 0;
        public void ScreenShakeShort(int length, float radius)
        {
            Main.screenPosition += new Vector2(length, 0).RotatedBy(radius);
            SSSrange = (int)(length * (SSStimer - 1f / SSStimer));
            SSSradius = radius;
            SSStimer--;
            if (SSStimer == 0) SSStimer = 3;
        }
        public int SSCtimer = 0;
        public int SSCrange = 0;

        public void ScreenShakeContinue(int time, int range)
        {
            SSCtimer = time;
            SSCrange = range;
            Main.screenPosition += new Vector2(Main.rand.Next(-range, range), Main.rand.Next(-range, range));
            SSCtimer--;
        }
        public override void ModifyScreenPosition()
        {
            if (SSStimer > 0 && SSStimer != 3)
                ScreenShakeShort(SSSrange, SSSradius);
            if (SSCtimer > 0)
                ScreenShakeContinue(SSCtimer, SSCrange);
        }

    }
}

