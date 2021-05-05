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
    
    class FreeThrowGame : GameScreen
    {
        public static int score = 0; 
        public static List<boundingSphere> rimSpheres;
        public static BoundingBox goalBox;
        public static boundingSphere targetSphere;
        public FreeThrowGame()

        {
            Vector3 scale, trans;
            Quaternion rota;

            ScreenManager.player.Position = new Vector3(750.0f, 0.0f, 450.0f);
            ScreenManager.player.Direction = Vector3.Backward;
            ScreenManager.camera.State = 1;
            ScreenManager.game = 0;
            ScreenManager.ball.state = 2;

            ScreenManager.goalBox = new BoundingBox(new Vector3(698.4f, 156.8f, 13.2f), new Vector3(801.6f, 225.5f, 20.7f));

            ScreenManager.rimSpheres = new List<boundingSphere>();

            Matrix[] transforms = new Matrix[ScreenManager.freeThrowGoal.Bones.Count];
            ScreenManager.freeThrowGoal.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in ScreenManager.freeThrowGoal.Meshes)
            {

                if (mesh.Name != "Goal" && mesh.Name != "BackBoard" )
                {
                    transforms[mesh.ParentBone.Index].Decompose(out scale, out rota, out trans);

                    if (mesh.Name == "Target")
                        ScreenManager.targetSphere = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));
                    else
                        ScreenManager.rimSpheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X))));


                }
            }




            //ScreenManager.ball.

        }

        public void checkCollisions()
        {





        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                               bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            Console.WriteLine(score);
          //  ScreenManager.player.Update(gameTime);
            
            
          //  ScreenManager.ball.update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                   Color.CornflowerBlue, 0, 0);
            ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            //bool draw = false;
            //Matrix[] transforms = new Matrix[ScreenManager.playerModel.Bones.Count];
            //ScreenManager.playerModel.CopyAbsoluteBoneTransformsTo(transforms);
            //foreach (ModelMesh mesh in ScreenManager.playerModel.Meshes)
            //{
            //    foreach (SkinnedEffect effect in mesh.Effects)
            //    {
            //        effect.SetBoneTransforms(ScreenManager.player.SkinTrans);
            //        effect.View = ScreenManager.camera.View;
            //        effect.Projection = ScreenManager.camera.Projection;
            //        effect.EnableDefaultLighting();
            //        if (mesh.Name == "Alecto")
            //            draw = true;
            //        else if (mesh.Name == "Basic" && ScreenManager.gameCharacters[ScreenManager.playerCharSelected].hair == 0)
            //            draw = true;
            //        else if (mesh.Name == "Shag" && ScreenManager.gameCharacters[ScreenManager.playerCharSelected].hair == 1)
            //            draw = true;
            //        else if (mesh.Name == "Afro" && ScreenManager.gameCharacters[ScreenManager.playerCharSelected].hair == 2)
            //            draw = true;
            //        else if (mesh.Name == "Clothes")
            //            draw = true;
            //        else
            //            draw = false;


            //    }
            //    if (draw)
            //        mesh.Draw();
            //}


            DrawModel(ScreenManager.player);
            DrawBallAndCourt();
        }
        public void DrawBallAndCourt()
        {

   
            Vector3 scale, trans;
            Quaternion rota;

            Matrix[] transforms = new Matrix[ScreenManager.court.Bones.Count];
            ScreenManager.court.CopyAbsoluteBoneTransformsTo(transforms);
            ScreenManager.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            Matrix targetMat;

            ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (ModelMesh mesh in ScreenManager.court.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = true;
                    //effect.Texture = otherTex;
                    // if (mesh.Name == "court")
                    // effect.Texture = courtTex;

                    effect.EnableDefaultLighting();

                    effect.View = ScreenManager.camera.View;
                    effect.Projection = ScreenManager.camera.Projection;
                    effect.World =
                        transforms[mesh.ParentBone.Index] *
                        Matrix.CreateTranslation(new Vector3(0.0f, -0.05f, 0.0f));

                  //  effect.World.Decompose(out scale, out rota, out trans);
                   // ScreenManager.player.ballPosition = transforms[mesh.ParentBone.Index]
                    if (mesh.Name == "LHandControl")
                    {
                       // if (ScreenManager.ball.state == 2 || ScreenManager.ball.state == 5)
                       // {

                        targetMat = transforms[mesh.ParentBone.Index] * ScreenManager.player.shotSTrans[ScreenManager.lHand];
                        ScreenManager.player.LShotBallPosition = new Vector3(targetMat.Translation.X, targetMat.Translation.Y, targetMat.Translation.Z);
                            targetMat = transforms[mesh.ParentBone.Index] * ScreenManager.player.SkinTrans[ScreenManager.lHand];
                            effect.World = targetMat;
                            targetMat.Decompose(out scale, out rota, out trans);
                          
                       // }
                        if (ScreenManager.ball.state != 4 & !ScreenManager.ball.positionSet)
                            ScreenManager.ball.Translation = targetMat.Translation;

                        if (ScreenManager.ball.state == 3)
                        {

                            effect.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(ScreenManager.ball.position);
                            effect.World.Decompose(out scale, out rota, out trans);

                        }
                        ScreenManager.ball.bs = new boundingSphere(ScreenManager.ball.bs.Name, (new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X) + 1.0f)));

                    }
                 /*   if (mesh.Name == "LHandControl")
                    {
                        if (basketBall.state == 0)
                        {
                            targetMat = transforms[mesh.ParentBone.Index] * playerTeam[playerIndex].SkinTrans[lHand];
                            targetMat.Decompose(out scale, out rota, out trans);
                            effect.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(basketBall.position);
                            targetMat = effect.World;

                        }
                        else if (playerTeam[playerIndex].teamOnOffense)
                        {

                            targetMat = transforms[mesh.ParentBone.Index] * playerTeam[playerIndex].SkinTrans[lHand];
                            targetMat.Decompose(out scale, out rota, out trans);
                            if (basketBall.state == 2 || basketBall.state == 5)
                            {
                                targetMat = transforms[mesh.ParentBone.Index] * playerTeam[playerIndex].SkinTrans[lHand];
                                effect.World = targetMat;

                            }
                            if (basketBall.state == 8)
                            {

                                targetMat = transforms[mesh.ParentBone.Index] * playerTeam[1].SkinTrans[lHand];
                                effect.World = targetMat;

                            }

                            else if (basketBall.state == 3)
                            {
                                if (!basketBall.stateSet)
                                {


                                    basketBall.stateSet = true;
                                    basketBall.ballTime = 0.0f;
                                    //effect.World = transforms[mesh.ParentBone.Index] * player.SkinTrans[lHand] * Matrix.CreateTranslation(basketBall.position);
                                    basketBall.Translation = trans;
                                    //if(basketBall.stateChange)
                                    // basketBall.oldPosition = (Matrix.CreateScale(scale) * Matrix.CreateTranslation(basketBall.position)).Translation;

                                    basketBall.oldPosition = trans;

                                }

                                else
                                    effect.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(basketBall.position);

                            }
                            if (basketBall.state == 4 || basketBall.state == 6 || basketBall.state == 10)
                            {
                                effect.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(basketBall.position);


                            }
                            if (basketBall.state != 4 & !basketBall.positionSet)
                                basketBall.Translation = targetMat.Translation;
                            //targetMat = transforms[mesh.ParentBone.Index] * A.JustBones[lHand];

                            if (basketBall.state == 1 || basketBall.state == 7)
                                effect.World = Matrix.CreateScale(scale) * playerTeam[playerIndex].JustBones[lHand] * Matrix.CreateTranslation(basketBall.position);


                        }
                        else if (opposingTeam[ballIndex].teamOnOffense)
                        {

                            targetMat = transforms[mesh.ParentBone.Index] * opposingTeam[ballIndex].SkinTrans[lHand];
                            targetMat.Decompose(out scale, out rota, out trans);
                            if (basketBall.state == 2 || basketBall.state == 5)
                            {
                                targetMat = transforms[mesh.ParentBone.Index] * opposingTeam[ballIndex].SkinTrans[lHand];
                                effect.World = targetMat;

                            }
                            else if (basketBall.state == 3)
                            {
                                if (!basketBall.stateSet)
                                {


                                    basketBall.stateSet = true;
                                    basketBall.ballTime = 0.0f;
                                    //effect.World = transforms[mesh.ParentBone.Index] * player.SkinTrans[lHand] * Matrix.CreateTranslation(basketBall.position);
                                    basketBall.Translation = trans;
                                    //if(basketBall.stateChange)
                                    // basketBall.oldPosition = (Matrix.CreateScale(scale) * Matrix.CreateTranslation(basketBall.position)).Translation;

                                    basketBall.oldPosition = trans;

                                }

                                else
                                    effect.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(basketBall.position);

                            }
                            if (basketBall.state == 4 || basketBall.state == 6)
                            {
                                effect.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(basketBall.position);


                            }
                            if (basketBall.state != 4)
                                basketBall.Translation = targetMat.Translation;
                            //targetMat = transforms[mesh.ParentBone.Index] * A.JustBones[lHand];

                            if (basketBall.state == 1 || basketBall.state == 7)
                                effect.World = Matrix.CreateScale(scale) * opposingTeam[ballIndex].JustBones[lHand] * Matrix.CreateTranslation(basketBall.position);


                        }



                        effect.World.Decompose(out scale, out rota, out trans);

                        basketBall.bs = new boundingSphere(basketBall.bs.Name, (new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X) + 1.0f)));


                    }*/


                }
                if (mesh.Name == "LHandControl" || mesh.Name == "court" || mesh.Name == "BackBoard")
                    mesh.Draw();



            }
            BoundingSphereRenderer.Render(ScreenManager.ball.bs.BS, ScreenManager.GraphicsDevice, ScreenManager.camera.View, ScreenManager.camera.Projection, Color.Yellow);
            
            ScreenManager.primitiveBatch.Begin(PrimitiveType.LineList);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Max.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Max.Z), Color.Red);

            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Max.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Max.Z), Color.Red);

            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Max.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Max.Z), Color.Red);

            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Max.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Max.Z), Color.Red);


            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Min.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Min.Z), Color.Red);

            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Min.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Min.Z), Color.Red);

            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Min.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Min.Z), Color.Red);

            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Min.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Min.Z), Color.Red);



            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Min.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Max.Z), Color.Red);

            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Min.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Min.Y, ScreenManager.goalBox.Max.Z), Color.Red);

            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Min.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Max.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Max.Z), Color.Red);

            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Min.Z), Color.Red);
            ScreenManager.primitiveBatch.AddVertex(new Vector3(ScreenManager.goalBox.Min.X, ScreenManager.goalBox.Max.Y, ScreenManager.goalBox.Max.Z), Color.Red);




            ScreenManager.primitiveBatch.End();


            ScreenManager.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
            
            ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;

            transforms = new Matrix[ScreenManager.freeThrowGoal.Bones.Count];
            ScreenManager.freeThrowGoal.CopyAbsoluteBoneTransformsTo(transforms);
            //ScreenManager.GraphicsDevice.RasterizerState.
            foreach (ModelMesh mesh in ScreenManager.freeThrowGoal.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {

                     effect.TextureEnabled = true;
                     effect.Texture = ScreenManager.goalTex;
                    // if (mesh.Name == "court")
                    // effect.Texture = courtTex;

                    //effect.AlphaFunction= BlendState.AlphaBlend.
                    //effect.
                    //effect.
                    //effect.AlphaFunction = CompareFunction.Greater;
                    //effect.ReferenceAlpha = 128;
                    //effect.ena
                    effect.View = ScreenManager.camera.View;
                    effect.Projection = ScreenManager.camera.Projection;
                    effect.World =
                        transforms[mesh.ParentBone.Index];
                    effect.EnableDefaultLighting();
                    if (mesh.Name == "Goal")
                        effect.Alpha = 1.0f;
                    if (mesh.Name == "BackBoard")
                        effect.Alpha = 0.4f;
                    //effect.LightingEnabled = 
                    //effect.
                    //effect. 
                    //effect.Alpha
                    
                    // *
                        //Matrix.CreateTranslation(new Vector3(0.0f, -120.05f, 750.0f));
                    //effect.T



                }
                if(mesh.Name == "Goal" || mesh.Name == "BackBoard")
                mesh.Draw();
            }

            ScreenManager.GraphicsDevice.BlendState = BlendState.Opaque;
            ScreenManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
          //  foreach (boundingSphere bs in rimSpheres)
           //     BoundingSphereRenderer.Render(bs.BS, ScreenManager.GraphicsDevice, ScreenManager.camera.View, ScreenManager.camera.Projection, Color.AliceBlue);
        }
        public void DrawModel(Actor A)
        {
            bool draw = false;
            Vector3 scale, trans;
            Quaternion rota;
            int index = 0;
            Matrix targetMat = new Matrix();
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


       /*     transforms = new Matrix[ScreenManager.court.Bones.Count];
            ScreenManager.court.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in ScreenManager.court.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.TextureEnabled = true;
                    effect.Texture = otherTex;
                    // if (mesh.Name == "court")
                    // effect.Texture = courtTex;

                    effect.EnableDefaultLighting();

                    effect.View = ScreenManager.camera.View;
                    effect.Projection = ScreenManager.camera.Projection;
                    effect.World =
                        transforms[mesh.ParentBone.Index] *
                        Matrix.CreateTranslation(new Vector3(0.0f, -0.05f, 0.0f));
                    if (mesh.Name == "RHandControl")
                    {

                        targetMat = transforms[mesh.ParentBone.Index] * A.SkinTrans[rHand];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sRHC] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));


                    }
                    if (mesh.Name == "chest")
                    {

                        targetMat = Matrix.CreateRotationY(MathHelper.PiOver2) * transforms[mesh.ParentBone.Index] * A.SkinTrans[spine1];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sChest] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));


                    }
                    if (mesh.Name == "hips")
                    {

                        targetMat = transforms[mesh.ParentBone.Index] * A.SkinTrans[hips];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sHips] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));

                    }
                    if (mesh.Name == "LHandControl")
                    {

                        targetMat = transforms[mesh.ParentBone.Index] * A.SkinTrans[lHand];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sLHC] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));

                    }
                    if (mesh.Name == "lHand")
                    {

                        targetMat = transforms[mesh.ParentBone.Index] * A.SkinTrans[lHand];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sLHand] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));


                    }
                    if (mesh.Name == "rHand")
                    {

                        targetMat = transforms[mesh.ParentBone.Index] * A.SkinTrans[rHand];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sRHand] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));


                    }
                    if (mesh.Name == "HipBall")
                    {

                        targetMat = transforms[mesh.ParentBone.Index] * A.SkinTrans[hips];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sHipBall] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));


                    }
                    if (mesh.Name == "pickUp")
                    {

                        targetMat = transforms[mesh.ParentBone.Index] * A.SkinTrans[hips];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sPickUp] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));


                    }
                    if (mesh.Name == "highCatch")
                    {

                        targetMat = transforms[mesh.ParentBone.Index] * A.SkinTrans[hips];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sHighCatch] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));


                    }
                    if (mesh.Name == "perfectRip")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * A.SkinTrans[hips];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sPRip] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));



                    }
                    if (mesh.Name == "perfectBlock")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * A.SkinTrans[hips];
                        targetMat.Decompose(out scale, out rota, out trans);
                        A.bodySpheres[sPBlock] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * Math.Abs(scale.X)));



                    }




                }
            }


            foreach (boundingSphere bs in A.bodySpheres)
                BoundingSphereRenderer.Render(bs.BS, screenManager.GraphicsDevice, camera.View, camera.Projection, Color.White);


            */
           // if (A.playerTeam && A.Type == 0)
            {

                // Start drawing lines
                ScreenManager.primitiveBatch.Begin(PrimitiveType.LineList);

                // Add the tank's position as the first vertex
             //   ScreenManager.primitiveBatch.AddVertex(A.Position, Color.White);

              /*  for (int i = 1; i < A.powerPts.Count; i++)
                {
                    // Add the next waypoint location to our line list
                    ScreenManager.primitiveBatch.AddVertex(A.powerPts[i], Color.Yellow);

                    // If we're not at the end of our waypoint list, add this point again to act as the
                    // first point for the next line.
                    if (i < A.powerPts.Count - 1)
                        ScreenManager.primitiveBatch.AddVertex(A.powerPts[i], Color.Yellow);
                }*/

              //  ScreenManager.primitiveBatch.AddVertex(A.Position, Color.White);
              //  if(A.arcPts.Count >0)
              //  ScreenManager.primitiveBatch.AddVertex(A.arcPts[0], Color.Red);
                if (A.arcPts.Count > 0)
                ScreenManager.primitiveBatch.AddVertex(A.arcPts[0], Color.Red);

                for (int i = 2; i < A.arcPts.Count; i++)
                {
                    // Add the next waypoint location to our line list
                    ScreenManager.primitiveBatch.AddVertex(A.arcPts[i], Color.Red);

                    // If we're not at the end of our waypoint list, add this point again to act as the
                    // first point for the next line.
                    if (i < A.arcPts.Count - 1)
                        ScreenManager.primitiveBatch.AddVertex(A.arcPts[i], Color.Red);
                }

                //ScreenManager.primitiveBatch.AddVertex(A.arcPts[A.arcPts.Count - 1], Color.Red);
                ScreenManager.primitiveBatch.End();

                

            /*    ScreenManager.primitiveBatch.AddVertex(ScreenManager.ball.position, Color.Blue);

                for (int i = 1; i < ScreenManager.ball.arcpts.Count; i++)
                {
                    // Add the next waypoint location to our line list
                    ScreenManager.primitiveBatch.AddVertex(ScreenManager.ball.arcpts[i], Color.Red);

                    // If we're not at the end of our waypoint list, add this point again to act as the
                    // first point for the next line.
                    if (i < ScreenManager.ball.arcpts.Count - 1)
                        ScreenManager.primitiveBatch.AddVertex(ScreenManager.ball.arcpts[i], Color.Red);
                }

                ScreenManager.primitiveBatch.End();

                //   primitiveBatch.AddVertex(A.Position, Color.White);
                /*
                if (A.arcPts.Count > 0)
                    primitiveBatch.AddVertex(A.arcPts[A.arcPts.Count - 1], Color.Purple);
                for (int i = 1; i < A.arc2Pts.Count; i++)
                {

                    // Add the next waypoint location to our line list
                    primitiveBatch.AddVertex(A.arc2Pts[i], Color.Purple);

                    // If we're not at the end of our waypoint list, add this point again to act as the
                    // first point for the next line.
                    if (i < A.arc2Pts.Count - 1)
                        primitiveBatch.AddVertex(A.arc2Pts[i], Color.Purple);

                }

                primitiveBatch.End();
                /*    primitiveBatch.Begin(PrimitiveType.LineList);
                    primitiveBatch.AddVertex(A.Position, Color.White);

                    for (int i = 1; i < A.powerPts.Count; i++)
                    {
                        // Add the next waypoint location to our line list
                        primitiveBatch.AddVertex(A.arcPts[i], Color.Red);

                        // If we're not at the end of our waypoint list, add this point again to act as the
                        // first point for the next line.
                        if (i < A.powerPts.Count - 1)
                            primitiveBatch.AddVertex(A.powerPts[i], Color.Red);
                    }
                    primitiveBatch.End();
                 */


            }



        }





    }
}
