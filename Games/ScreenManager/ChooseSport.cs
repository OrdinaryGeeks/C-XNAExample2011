using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkinnedModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SmellOfRevenge2011
{
    class ChooseSport : MenuScreen
    {


        public ChooseSport(): base("Choose Sport")
        {

            MenuEntry basketball = new MenuEntry("BASKETBALL");
            MenuEntry football = new MenuEntry("FOOTBALL");
            MenuEntry skateboard = new MenuEntry("SKATEBOARD");
            MenuEntry baseball = new MenuEntry("BASEBALL");
            MenuEntry boxing = new MenuEntry("BOXING");
            MenuEntry soccer = new MenuEntry("SOCCER");

            basketball.Selected += basketballSelected;
            baseball.Selected += baseballSelected;


            MenuEntries.Add(basketball);
            MenuEntries.Add(baseball);
            MenuEntries.Add(football);
            MenuEntries.Add(skateboard);

            MenuEntries.Add(boxing);
            MenuEntries.Add(soccer);
        }

        void baseballSelected(object sender, PlayerIndexEventArgs e)
        {

            ScreenManager.AddScreen(new ChooseGame(1), e.PlayerIndex);
            ExitScreen();

        }
        void basketballSelected(object sender, PlayerIndexEventArgs e)
        {

            ScreenManager.AddScreen(new ChooseGame(0), e.PlayerIndex);
            ExitScreen();

        }

        public override void Draw(GameTime gameTime)
        {

            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);
            ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            bool draw = false;
            Matrix[] transforms = new Matrix[ScreenManager.playerModel.Bones.Count];
            ScreenManager.playerModel.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in ScreenManager.playerModel.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.SetBoneTransforms(ScreenManager.player.SkinTrans);
                    effect.View = ScreenManager.camera.View;
                    effect.Projection = ScreenManager.camera.Projection;
                    effect.EnableDefaultLighting();
                    if (mesh.Name == "Alecto")
                        draw = true;
                    else if (mesh.Name == "Basic" && ScreenManager.gameCharacters[ScreenManager.playerCharSelected].hair == 0)
                        draw = true;
                    else if (mesh.Name == "Shag" && ScreenManager.gameCharacters[ScreenManager.playerCharSelected].hair == 1)
                        draw = true;
                    else if (mesh.Name == "Afro" && ScreenManager.gameCharacters[ScreenManager.playerCharSelected].hair == 2)
                        draw = true;
                    else if (mesh.Name == "Clothes")
                        draw = true;
                    else
                        draw = false;


                }
                if (draw)
                    mesh.Draw();
            }



            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            Vector2 position = new Vector2(100, 150);

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            if (ScreenState == ScreenState.TransitionOn)
                position.X -= transitionOffset * 256;
            else
                position.X += transitionOffset * 512;

            spriteBatch.Begin();

            // Draw each menu entry in turn.
            for (int i = 0; i < MenuEntries.Count; i++)
            {
                MenuEntry menuEntry = MenuEntries[i];

                bool isSelected = IsActive && (i == SelectedEntry);

                menuEntry.Draw(this, position, isSelected, gameTime);

                position.Y += menuEntry.GetHeight(this);
            }



            // Draw the menu title.
            Vector2 titlePosition = new Vector2(426, 80);
            Vector2 titleOrigin = font.MeasureString(MenuTitle) / 2;
            Color titleColor = new Color(192, 192, 192, TransitionAlpha);
            float titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, MenuTitle, titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();








            base.Draw(gameTime);
        }
















    }
}
