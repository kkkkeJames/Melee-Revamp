using Terraria;
using Terraria.Graphics.CameraModifiers;
using Terraria.ModLoader;

namespace MeleeRevamp.Content.Core
{
    public class CameraSystem : ModSystem
    {
        public static int Shake = 0;
        public static int DirectShake = 0;
        public static int FixedAngleShake = 0;
        public static float ShakeAngle = 0;
        public static int ShakeDecrease = 0;
        public override void PostUpdateEverything()
        {
            if (Shake > 120)
                Shake = 120;

        }

        public override void ModifyScreenPosition()
        {
            Main.instance.CameraModifiers.Add(new PunchCameraModifier(Main.LocalPlayer.position, Main.rand.NextFloat(3.14f).ToRotationVector2(), Shake, 15f, 30, 2000));
            Main.instance.CameraModifiers.Add(new PunchCameraModifier(Main.LocalPlayer.position, Main.rand.NextFloat(3.14f).ToRotationVector2(), DirectShake, 15f, 30, 2000));
            Main.instance.CameraModifiers.Add(new PunchCameraModifier(Main.LocalPlayer.position, ShakeAngle.ToRotationVector2(), FixedAngleShake, 60f, 3, 2000));

            if (Shake > 0)
                Shake--;
            if (DirectShake > 0)
                DirectShake = 0;
            if (FixedAngleShake > 0)
                FixedAngleShake -= ShakeDecrease;
            if (FixedAngleShake < 0)
                FixedAngleShake = 0;
        }

        public static void Reset()
        {
            Shake = 0;
            DirectShake = 0;
            FixedAngleShake = 0;
            ShakeAngle = 0;
            ShakeDecrease = 0;
        }

        public override void OnWorldLoad()
        {
            Reset();
        }

        public override void Unload()
        {
        }
    }
}

