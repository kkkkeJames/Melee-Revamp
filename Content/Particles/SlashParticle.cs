using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary.Core;
using Terraria;
using Terraria.ModLoader;

namespace MeleeRevamp.Content.Particles
{
    public class SlashParticle : Particle
    {
        public override string Texture => "MeleeRevamp/Content/Assets/CircleBlast";
        public float timer;
        public float extension;
        public float ScaleX;
        public float ScaleY;
        public float DeAcc;
        public bool IsAdditive;
        public int TimerMax;
        public SlashParticle(float deAcc = 0.91f, bool isAdditive = true, int timerMax = 12)
        {
            DeAcc = deAcc;
            IsAdditive = isAdditive;
            TimerMax = timerMax;
        }
        public override void Spawn()
        {
            Velocity *= 0.5f;
            TimeLeft = TimerMax;
            timer = TimerMax - 1;

            TileCollide = false;
            extension = Main.rand.NextFloat(28, 32);
            Rotation = Velocity.ToRotation();
        }
        public override void Update()
        {
            timer--;
            float progress = timer / ((float)TimerMax - 1);
            ScaleX = Scale + extension * progress;
            ScaleY = Scale - Scale * progress;
            Velocity *= DeAcc;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Color a = IsAdditive ? Color with { A = 0 } : Color;
            spriteBatch.Draw(ModContent.Request<Texture2D>("MeleeRevamp/Content/Assets/CircleBlast").Value, location, new Rectangle(0, 0, 3780, 3780), a, Rotation, new Vector2(1890, 1890), new Vector2(ScaleX, Scale) * 0.0005f, 0, 0f);
            spriteBatch.Draw(ModContent.Request<Texture2D>("MeleeRevamp/Content/Assets/CircleBlast").Value, location, new Rectangle(0, 0, 3780, 3780), a, Rotation, new Vector2(1890, 1890), new Vector2(ScaleX, Scale) * 0.000625f, 0, 0f);
        }
    }
}
