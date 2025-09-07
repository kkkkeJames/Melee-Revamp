// An easy way to apply RT2D to the game based on the method of yiyang233, mainly used to apply effects like RT2D
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using MeleeRevamp.Content.Core;

namespace MeleeRevamp.Content.Hooks
{
    public class RT2D : ModSystem
    {
        public static RenderTarget2D render;
        public override void Load()
        {
            On_FilterManager.EndCapture += On_FilterManager_EndCapture;
        }
        private void On_FilterManager_EndCapture(On_FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2, Color clearColor)
        {
            if (render == null)
                CreateRender();

            Save(); // Save the original figure

            DrawWarp(); // Apply warp

            ApplyWarp();
            orig.Invoke(self, finalTexture, screenTarget1, screenTarget2, clearColor);
        }
        // If there is no alternate render, create a new one
        public void CreateRender()
        {
            render = new RenderTarget2D(Main.instance.GraphicsDevice, Main.screenWidth, Main.screenHeight);
        }
        private void Save()
        {
            Main.instance.GraphicsDevice.SetRenderTarget(render); // Open and initialize the self created render
            Main.instance.GraphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(0, BlendState.AlphaBlend);
            Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White); // Copy what is draw to the screen
            Main.spriteBatch.End();
        }

        private void DrawWarp() // Apply warp to another render
        {
            Main.instance.GraphicsDevice.SetRenderTarget(Main.screenTargetSwap); // Open and initialize the alternate render provided by tmod API
            Main.instance.GraphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            for (int i = 0; i < Main.maxProjectiles; i++) // Iterate through all projectiles
                if (Main.projectile[i].active && Main.projectile[i].ModProjectile is IDrawWarp) // If the projectile is active and has IDrawWarp interface
                    (Main.projectile[i].ModProjectile as IDrawWarp).DrawWarp(); // Apply drawwarp in the interface
            Main.spriteBatch.End();
        }

        private void ApplyWarp()
        {
            Main.instance.GraphicsDevice.SetRenderTarget(Main.screenTarget); // Open the main render
            Main.instance.GraphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            // Set the graph of shader to noise map used in swap
            Effect effect = Filters.Scene["Warp"].GetShader().Shader;
            effect.Parameters["tex"].SetValue(Main.screenTargetSwap);
            effect.Parameters["intense"].SetValue(0.04f);
            effect.CurrentTechnique.Passes[0].Apply();
            Main.spriteBatch.Draw(render, Vector2.Zero, Color.White); // Draw the content in the previously stored render to the render Main.screenTarget
            Main.spriteBatch.End();
        }
        public override void Unload()
        {
            render = null;
        }
    }
}
