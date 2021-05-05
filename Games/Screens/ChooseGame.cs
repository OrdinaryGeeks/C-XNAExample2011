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
    class ChooseGame : MenuScreen
    {
        int id;
        public ChooseGame(int id)
            : base("Choose Game")
        {

            ScreenManager.sport = id;
            this.id = id;
            MenuEntry game1;
            MenuEntry game2;
            MenuEntry game3;
            MenuEntry game4;
            MenuEntry game5;

            if (id == 0)
            {
                game1 = new MenuEntry("Free Throw Game");
                game2 = new MenuEntry("Three Point Contest");
                game3 = new MenuEntry("Dunk Contest");
                game4 = new MenuEntry("Dribble Challenge");
            }
            
            else if (id == 1)
            {
                game1 = new MenuEntry("Home Rune Game");
                game2 = new MenuEntry("Batters Up");
                game3 = new MenuEntry("Fast Pitch");
                game4 = new MenuEntry("Golden Gloves");
                
            }
           else if (id == 2)
            {
                game1 = new MenuEntry("Ramp FreeStyle");
                game2 = new MenuEntry("City Race");
                game3 = new MenuEntry("Arena FreeStyle");
                game4 = new MenuEntry("Ramp Duel");
            }
            else if (id == 3)
            {
                game1 = new MenuEntry("HeavyBag");
                game2 = new MenuEntry("SpeedBag");
                game3 = new MenuEntry("Defense");
                game4 = new MenuEntry("Ring The Bell");

            }
            else 
            {
                game1 = new MenuEntry("Running The Ball");
                game2 = new MenuEntry("Passing The Ball");
                game3 = new MenuEntry("Defensive Tackling");
                game4 = new MenuEntry("Hike the Ball");
                //game5 = new MenuEntry("Line Challenge")


            }

            game5 = new MenuEntry("Practice");
            MenuEntries.Add(game1);
            MenuEntries.Add(game2);
            MenuEntries.Add(game3);
            MenuEntries.Add(game4);
            MenuEntries.Add(game5);

            game1.Selected += game1Selected;
            game4.Selected += game4Selected;
            game5.Selected += game5Selected;
            
        }

        /// <summary>
        /// Draws the menu.
        /// </summary>
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


        void game1Selected(object sender, PlayerIndexEventArgs e)
        {
            if (id == 0)
                ScreenManager.AddScreen(new FreeThrowGame(), e.PlayerIndex);
            ExitScreen();
        }

        void game2Selected(object sender, PlayerIndexEventArgs e)
        {

        }
        void game3Selected(object sender, PlayerIndexEventArgs e)
        {

        }
        void game4Selected(object sender, PlayerIndexEventArgs e)
        {
            if (id == 0)
                ScreenManager.AddScreen(new DribbleChallenge(), e.PlayerIndex);
            ExitScreen();

        }
        void game5Selected(object sender, PlayerIndexEventArgs e)
        {
            if (id == 0)
                ScreenManager.AddScreen(new BasketBallPractice(), e.PlayerIndex);
            if (id == 1)
                ScreenManager.AddScreen(new BattingPractice(), e.PlayerIndex);
            ExitScreen();
        }


    }
}
