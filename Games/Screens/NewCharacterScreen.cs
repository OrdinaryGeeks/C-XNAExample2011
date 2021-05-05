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
     class NewCharacterScreen : GameScreen
    {
        #region Fields
        List<MenuEntry> menuEntries = new List<MenuEntry>();
        List<MenuListEntry> menuListEntries = new List<MenuListEntry>();
        int selectedEntry = 0;
        string menuTitle;

        #endregion

        Character newCharacter; 


        Camera camera;


        /// <summary>
        /// Gets the list of menu entries, so derived classes can add
        /// or change the menu contents.
        /// </summary>
        protected IList<MenuListEntry> MenuListEntries
        {
            get { return menuListEntries; }
        }

        protected int SelectedEntry
        {
            get
            {
                return selectedEntry;
            }
        }
        protected string MenuTitle
        {
            get
            {
                return menuTitle;
            }
        }

        public NewCharacterScreen()
        {
            menuTitle = "New Character";
            MenuListEntry hairEntry = new MenuListEntry("Select Hair Type");
           // MenuListEntry bodyEntry = new MenuListEntry("Select Body Type");
            MenuEntry saveEntry = new MenuEntry("Save");
            MenuEntry chooseEntry = new MenuEntry("Choose this character and enter game");
            MenuEntry exitEntry = new MenuEntry("Exit");
            //Menu
            newCharacter = new Character();
            hairEntry.choices.Add("Basic");
            hairEntry.choices.Add("Shag");
            hairEntry.choices.Add("Afro");
            

            hairEntry.Selected += HairEntrySelected;
            //bodyEntry.Selected += BodyEntrySelected;
            saveEntry.Selected += SaveEntrySelected;
            exitEntry.Selected += ExitEntrySelected;

            chooseEntry.Selected += ChooseEntrySelected;

            MenuListEntries.Add(hairEntry);
           // MenuListEntries.Add(bodyEntry);
            menuEntries.Add(saveEntry);
            menuEntries.Add(exitEntry);
            menuEntries.Add(chooseEntry);



           
        }
        void ChooseEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            newCharacter.hair = MenuListEntries[0].index;

            ScreenManager.gameCharacters.Add(new Character(newCharacter));
            ScreenManager.AddScreen(new ChooseSport(), e.PlayerIndex);
            ExitScreen();
        }

        void HairEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            MenuListEntries[0].index++;
            if(MenuListEntries[0].index == MenuListEntries[0].choices.Count)
            menuListEntries[0].index = 0; 
           
        }
        void BodyEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            MenuListEntries[1].index++;
            
            if(MenuListEntries[1].index == MenuListEntries[1].choices.Count)
            menuListEntries[1].index = 0; 


        }
        void SaveEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            newCharacter.hair = MenuListEntries[0].index;

            ScreenManager.gameCharacters.Add(new Character(newCharacter));

        }
        void ExitEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ExitScreen();


        }
        public override void LoadContent()
        {



            camera = new Camera();
            camera.LookAtOffset = new Vector3(0.0f, 100.0f, 100.0f);
            camera.DesiredPositionOffset = new Vector3(0.0f, 100.0f, -300.0f);
            camera.NearPlaneDistance = 10.0f;
            camera.FarPlaneDistance = 10000.0f;
            camera.CameraFrustum = new BoundingFrustum(Matrix.Identity);
            camera.FakeFarPlaneDistance = 1000.0f;
            camera.FakeNearPlaneDistance = 1.0f;



        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {

            ScreenManager.player.Update(gameTime);

            // Update each nested MenuEntry object.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                menuEntries[i].Update(this, isSelected, gameTime);
            }
            for (int i = menuEntries.Count; i < menuListEntries.Count + menuEntries.Count; i++)
            {

                bool isSelected = IsActive && (i == selectedEntry);

                menuListEntries[i-menuEntries.Count].Update(this, isSelected, gameTime);


            }


            UpdateCameraChaseTarget();
            camera.Reset();
            base.Update(gameTime, otherScreenHasFocus, false);

        }
        private void UpdateCameraChaseTarget()
        {
            camera.State = 0;

            camera.ChasePosition = ScreenManager.player.World.Translation;
            //camera.NewLookAt = basketBall.position;
            camera.ChaseDirection = Vector3.Backward;



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
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                    effect.EnableDefaultLighting();
                    if (mesh.Name == "Alecto")
                        draw = true;
                    else if (mesh.Name == "Basic" && MenuListEntries[0].index == 0)
                        draw = true;
                    else if (mesh.Name == "Shag" && MenuListEntries[0].index == 1)
                        draw = true;
                    else if (mesh.Name == "Afro" && MenuListEntries[0].index == 2)
                        draw = true;
                    else if (mesh.Name == "Clothes")
                        draw = true;
                    else if (mesh.Name == "Scalp")
                        draw = true;
                    else
                        draw = false;


                }
                if(draw)
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
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];

                bool isSelected = IsActive && (i == selectedEntry);

                menuEntry.Draw(this, position, isSelected, gameTime);

                position.Y += menuEntry.GetHeight(this);
            }

            for (int i = menuEntries.Count; i < menuListEntries.Count + menuEntries.Count; i++)
            {
                MenuListEntry menuListEntry = menuListEntries[i - menuEntries.Count];

                bool isSelected = IsActive && (i == selectedEntry);
                menuListEntry.Draw(this, position, isSelected, gameTime);
                position.Y += menuListEntry.GetHeight(this);



            }

            // Draw the menu title.
            Vector2 titlePosition = new Vector2(426, 80);
            Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
            Color titleColor = new Color(192, 192, 192, TransitionAlpha);
            float titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, menuTitle, titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();








            base.Draw(gameTime);
        }

        /// <summary>
        /// Responds to user input, changing the selected entry and accepting
        /// or cancelling the menu.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            // Move to the previous menu entry?
            if (input.IsMenuUp(ControllingPlayer))
            {
                selectedEntry--;

                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1 + menuListEntries.Count -1;
            }

            // Move to the next menu entry?
            if (input.IsMenuDown(ControllingPlayer))
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count + menuListEntries.Count)
                    selectedEntry = 0;
            }

            // Accept or cancel the menu? We pass in our ControllingPlayer, which may
            // either be null (to accept input from any player) or a specific index.
            // If we pass a null controlling player, the InputState helper returns to
            // us which player actually provided the input. We pass that through to
            // OnSelectEntry and OnCancel, so they can tell which player triggered them.
            PlayerIndex playerIndex;

            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                OnSelectEntry(selectedEntry, playerIndex);
            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                OnCancel(playerIndex);
            }
        }


        /// <summary>
        /// Handler for when the user has chosen a menu entry.
        /// </summary>
        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            if(selectedEntry < menuEntries.Count)
            menuEntries[selectedEntry].OnSelectEntry(playerIndex);
            else
            menuListEntries[selectedEntry-menuEntries.Count].OnSelectEntry(playerIndex);
        }


        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }

        /// <summary>
        /// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        /// </summary>
        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }














    }
}
