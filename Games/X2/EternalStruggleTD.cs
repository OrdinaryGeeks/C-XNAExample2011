using System;
using System.Collections.Generic;
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
    class EternalStruggleTD  : GameScreen
    {
        MouseState oldMouseState;
        MouseState curMouseState;
        Vector3 l0Dir;
        Vector3 l0Dif;
        Vector3 l0Spec;
        Vector3 l1Dir;
        Vector3 l1Dif;
        Vector3 l1Spec;
        Vector3 l2Dir;
        Vector3 l2Dif;
        Vector3 l2Spec;
        Vector3 ambientLightColor;

           public EternalStruggleTD()
        {
            oldMouseState = new MouseState();
            curMouseState = new MouseState();

               
            //openStrikes = new List<bool>();
            //strikeSpheres = new List<List<boundingSphere>>();

            // Key light.
            l0Dir = new Vector3(-0.5265408f, -0.5735765f, -0.6275069f);
            l0Dif = new Vector3(1, 0.9607844f, 0.8078432f);
            l0Spec = new Vector3(1, 0.9607844f, 0.8078432f);


            // Fill light.
            l1Dir = new Vector3(0.7198464f, 0.3420201f, 0.6040227f);
            l1Dif = new Vector3(0.9647059f, 0.7607844f, 0.4078432f);
            l1Spec = Vector3.Zero;


            // Back light.
            l2Dir = new Vector3(0.4545195f, -0.7660444f, 0.4545195f);
            l2Dif = new Vector3(0.3231373f, 0.3607844f, 0.3937255f);
            l2Spec = new Vector3(0.3231373f, 0.3607844f, 0.3937255f);


            ambientLightColor = new Vector3(0.05333332f, 0.09882354f, 0.1819608f);

           
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            ScreenManager.ps1.UpdatePlayer(gameTime);
            ScreenManager.es1.UpdateBrace(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawSpear(ScreenManager.ps1, "SkinnedEffect");
            DrawSpear(ScreenManager.es1, "SkinnedEffect");
            base.Draw(gameTime);
        }
        public void DrawSpear(SpearMan actor, string nameEffect)
        {

            Matrix[] transforms;
            transforms = new Matrix[ScreenManager.juneModel.Bones.Count];
            ScreenManager.juneModel.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in ScreenManager.juneModel.Meshes)
            {
                if (mesh.Name == "Alecto" || mesh.Name == "TorsoPlate" || mesh.Name == "lHairF" || mesh.Name == "ponytail" || mesh.Name == "shin"
                    || mesh.Name == "LSpear" || mesh.Name == "RSpear" || mesh.Name == "GreekPants" ||
                    mesh.Name == "Scalp" || mesh.Name == "eyeball" || mesh.Name == "EyeBrow"
                    )
                {
                    foreach (Effect effect in mesh.Effects)
                    {

                        effect.CurrentTechnique = effect.Techniques[nameEffect];

                        effect.Parameters["LightDirection"].SetValue(ScreenManager.lightDir);
                        effect.Parameters["LightViewProj"].SetValue(ScreenManager.lightViewProjection);

                        effect.Parameters["DiffuseColor"].SetValue(new Vector4(2.0f, 2.0f, 2.0f, 1.0f));
                        effect.Parameters["EmissiveColor"].SetValue(new Vector3(0.05333332f, 0.09882354f, 0.1819608f));


                        effect.Parameters["WorldInverseTranspose"].SetValue(Matrix.Transpose(Matrix.Invert(Matrix.Identity)));
                        effect.Parameters["DirLight0Direction"].SetValue(l0Dir);
                        effect.Parameters["DirLight0DiffuseColor"].SetValue(l0Dif);
                        effect.Parameters["DirLight0SpecularColor"].SetValue(l0Spec);
                        effect.Parameters["DirLight1Direction"].SetValue(l1Dir);
                        effect.Parameters["DirLight1DiffuseColor"].SetValue(l1Dif);
                        effect.Parameters["DirLight1SpecularColor"].SetValue(l1Spec);
                        effect.Parameters["DirLight2Direction"].SetValue(l2Dir);
                        effect.Parameters["DirLight2DiffuseColor"].SetValue(l2Dif);
                        effect.Parameters["DirLight2SpecularColor"].SetValue(l2Spec);
                        effect.Parameters["World"].SetValue(Matrix.Identity);
                        effect.Parameters["EyePosition"].SetValue(Matrix.Invert(ScreenManager.camera.View).Translation);
                        if (mesh.Name == "Alecto")
                            effect.Parameters["Texture"].SetValue(ScreenManager.frostTex);
                        else
                            effect.Parameters["Texture"].SetValue(ScreenManager.gold1);
                        effect.Parameters["Bones"].SetValue(actor.SkinTrans);
                        effect.Parameters["WorldViewProj"].SetValue(ScreenManager.camera.View * ScreenManager.camera.Projection);

                        if (mesh.Name == "sHair")
                            effect.Parameters["Texture"].SetValue(ScreenManager.white);
                        if (mesh.Name == "longHairF")
                            effect.Parameters["Texture"].SetValue(ScreenManager.white);
                        if (mesh.Name == "longHairB")
                            effect.Parameters["Texture"].SetValue(ScreenManager.humanTex);

                    }

                    mesh.Draw();
                }
            }

            Vector3 scale, trans;
            Quaternion rota;
            Matrix targetMat = new Matrix();
            transforms = new Matrix[ScreenManager.spearSphere.Bones.Count];
            ScreenManager.spearSphere.CopyAbsoluteBoneTransformsTo(transforms);
            int i = 0;
            int j = 0;
            foreach (ModelMesh mesh in ScreenManager.spearSphere.Meshes)
            {
                // draw = false;
                foreach (BasicEffect effect in mesh.Effects)
                {
                    if (mesh.Name == "rSpearS1" || mesh.Name == "rSpearS2" || mesh.Name == "rSpearS3")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.rHand];
                        targetMat.Decompose(out scale, out rota, out trans);
                        actor.rSpear[j++] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X));

                    }
                    if (mesh.Name == "chestS")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.spine1];
                        targetMat.Decompose(out scale, out rota, out trans);
                        actor.spheres[i++] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X));
                    }
                    if (mesh.Name == "chestS")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.spine1];
                        targetMat.Decompose(out scale, out rota, out trans);
                        actor.collisionS[0] = new boundingSphere("collisionS", new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X * 2.0f));


                    }
                    if (mesh.Name == "KnockBackCheck")
                    {


                        targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.spine1];
                        targetMat.Decompose(out scale, out rota, out trans);
                        actor.knockBackSphere[0] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X));
                    }

                    if (mesh.Name == "forwardSpell")
                    {
                        actor.World.Decompose(out scale, out rota, out trans);

                        actor.forwardSpell = Matrix.Transform(transforms[mesh.ParentBone.Index], rota) * Matrix.CreateTranslation(trans);

                    }
                    if (mesh.Name == "headS1")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.head];
                        targetMat.Decompose(out scale, out rota, out trans);
                        actor.spheres[i++] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X));


                    }

                    if (mesh.Name == "hipS")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.hips];
                        targetMat.Decompose(out scale, out rota, out trans);
                        actor.spheres[i++] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X));
                    }
                    if (mesh.Name == "lULegS")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.lULeg];
                        targetMat.Decompose(out scale, out rota, out trans);
                        actor.spheres[i++] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X));
                    }
                    if (mesh.Name == "lLLegS")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.lLLeg];
                        targetMat.Decompose(out scale, out rota, out trans);
                        actor.spheres[i++] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X));
                    }
                    if (mesh.Name == "rULegS")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.rULeg];
                        targetMat.Decompose(out scale, out rota, out trans);
                        actor.spheres[i++] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X));
                    }
                    if (mesh.Name == "rLLegS")
                    {
                        targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.rLLeg];
                        targetMat.Decompose(out scale, out rota, out trans);
                        actor.spheres[i++] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X));
                    }
                    //if (mesh.Name == "lFootS")
                    //{

                    //    targetMat = transforms[mesh.ParentBone.Index] * actor.SkinTrans[ScreenManager.rFoot];
                    //    targetMat.Decompose(out scale, out rota, out trans);
                    //    actor.physicalSphere[j++] = new boundingSphere(mesh.Name, new BoundingSphere(trans, mesh.BoundingSphere.Radius * scale.X));

                    //}




                }

            }

            //  BoundingSphereRenderer.Render(actor.collisionS[0].BS, ScreenManager.GraphicsDevice, ScreenManager.camera.View, ScreenManager.camera.Projection, Color.AliceBlue);
            //foreach (boundingSphere bs in actor.rSpear)
            //{
            //    BoundingSphereRenderer.Render(bs.BS, ScreenManager.GraphicsDevice, ScreenManager.camera.View, ScreenManager.camera.Projection, Color.AliceBlue);

            //}
            //foreach (boundingSphere bs in actor.knockBackSphere)
            //{
            //    BoundingSphereRenderer.Render(bs.BS, ScreenManager.GraphicsDevice, ScreenManager.camera.View, ScreenManager.camera.Projection, Color.Red);

            //}
            //foreach (boundingSphere bs in actor.spheres)
            //{

            //    BoundingSphereRenderer.Render(bs.BS, ScreenManager.GraphicsDevice, ScreenManager.camera.View, ScreenManager.camera.Projection, Color.Red);
            //}

            Matrix world = new Matrix();
            //Vector3 scale, trans;
            //Quaternion rota;

            actor.formation.Decompose(out scale, out rota, out trans);

            transforms = new Matrix[ScreenManager.humanFormation.Bones.Count];
            ScreenManager.humanFormation.CopyAbsoluteBoneTransformsTo(transforms);


            foreach (ModelMesh mesh in screenManager.humanFormation.Meshes)
            {
                world = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(trans);

                if (mesh.Name == "0,0")
                {
                    actor.CFormation[0] = world.Translation;
                    ScreenManager.es1.formVec = world.Translation;
                }
                if (mesh.Name == "0,1")
                {
                    actor.CFormation[3] = world.Translation;
                    ScreenManager.es2.formVec = world.Translation;
                }
                if (mesh.Name == "0,2")
                {
                    actor.CFormation[6] = world.Translation;
                    ScreenManager.es3.formVec = world.Translation;
                }
                if (mesh.Name == "1,0")
                {
                    actor.CFormation[1] = world.Translation;
                    ScreenManager.ess1.formVec = world.Translation;
                }
                if (mesh.Name == "1,1")
                    actor.CFormation[4] = world.Translation;
                if (mesh.Name == "1,2")
                {
                    actor.CFormation[7] = world.Translation;

                }
                if (mesh.Name == "2,0")
                {
                    //ScreenManager.a1.formVec = world.Translation;
                    actor.CFormation[2] = world.Translation;
                }
                if (mesh.Name == "2,1")
                {
                    actor.CFormation[5] = world.Translation;
                }
                if (mesh.Name == "2,2")
                    actor.CFormation[8] = world.Translation;
                if (mesh.Name == "E0,0")
                    actor.EFormation[0] = world.Translation;
                if (mesh.Name == "E0,1")
                    actor.EFormation[3] = world.Translation;
                if (mesh.Name == "E0,2")
                    actor.EFormation[6] = world.Translation;
                if (mesh.Name == "E1,0")
                    actor.EFormation[1] = world.Translation;
                if (mesh.Name == "E1,1")
                    actor.EFormation[4] = world.Translation;
                if (mesh.Name == "E1,2")
                    actor.EFormation[7] = world.Translation;
                if (mesh.Name == "E2,0")
                    actor.EFormation[2] = world.Translation;
                if (mesh.Name == "E2,1")
                    actor.EFormation[5] = world.Translation;
                if (mesh.Name == "E2,2")
                    actor.EFormation[8] = world.Translation;
                if (mesh.Name == "W0,0")
                    actor.WFormation[0] = world.Translation;
                if (mesh.Name == "W0,1")
                    actor.WFormation[3] = world.Translation;
                if (mesh.Name == "W0,2")
                    actor.WFormation[6] = world.Translation;
                if (mesh.Name == "W1,0")
                    actor.WFormation[1] = world.Translation;
                if (mesh.Name == "W1,1")
                    actor.WFormation[4] = world.Translation;
                if (mesh.Name == "W1,2")
                    actor.WFormation[7] = world.Translation;
                if (mesh.Name == "W2,0")
                    actor.WFormation[2] = world.Translation;
                if (mesh.Name == "W2,1")
                    actor.WFormation[5] = world.Translation;
                if (mesh.Name == "W2,2")
                    actor.WFormation[8] = world.Translation;
                if (mesh.Name == "N0,0")
                    actor.NFormation[0] = world.Translation;
                if (mesh.Name == "N0,1")
                    actor.NFormation[3] = world.Translation;
                if (mesh.Name == "N0,2")
                    actor.NFormation[6] = world.Translation;
                if (mesh.Name == "N1,0")
                    actor.NFormation[1] = world.Translation;
                if (mesh.Name == "N1,1")
                    actor.NFormation[4] = world.Translation;
                if (mesh.Name == "N1,2")
                    actor.NFormation[7] = world.Translation;
                if (mesh.Name == "N2,0")
                    actor.NFormation[2] = world.Translation;
                if (mesh.Name == "N2,1")
                {
                    actor.NFormation[5] = world.Translation;

                    ScreenManager.a1.formVec = world.Translation;
                }
                if (mesh.Name == "N2,2")
                    actor.NFormation[8] = world.Translation;

                if (mesh.Name == "S0,0")
                    actor.SFormation[0] = world.Translation;
                if (mesh.Name == "S0,1")
                    actor.SFormation[3] = world.Translation;
                if (mesh.Name == "S0,2")
                    actor.SFormation[6] = world.Translation;
                if (mesh.Name == "S1,0")
                    actor.SFormation[1] = world.Translation;
                if (mesh.Name == "S1,1")
                    actor.SFormation[4] = world.Translation;
                if (mesh.Name == "S1,2")
                    actor.SFormation[7] = world.Translation;
                if (mesh.Name == "S2,0")
                    actor.SFormation[2] = world.Translation;
                if (mesh.Name == "S2,1")
                    actor.SFormation[5] = world.Translation;
                if (mesh.Name == "S2,2")
                    actor.SFormation[8] = world.Translation;

                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = ScreenManager.camera.View;
                    effect.Projection = ScreenManager.camera.Projection;

                    if (mesh.Name == "S0,0" || mesh.Name == "S0,1" || mesh.Name == "S0,2"
                    || mesh.Name == "S1,0" || mesh.Name == "S1,1" || mesh.Name == "S1,2"
                        || mesh.Name == "S2,0" || mesh.Name == "S2,1" || mesh.Name == "S2,2")

                        effect.DiffuseColor = Color.WhiteSmoke.ToVector3();


                    if (mesh.Name == "N0,0" || mesh.Name == "N0,1" || mesh.Name == "N0,2"
                    || mesh.Name == "N1,0" || mesh.Name == "N1,1" || mesh.Name == "N1,2"
                        || mesh.Name == "N2,0" || mesh.Name == "N2,1" || mesh.Name == "N2,2")
                        effect.DiffuseColor = Color.Yellow.ToVector3();


                    if (mesh.Name == "E0,0" || mesh.Name == "E0,1" || mesh.Name == "E0,2"
 || mesh.Name == "E1,0" || mesh.Name == "E1,1" || mesh.Name == "E1,2"
 || mesh.Name == "E2,0" || mesh.Name == "E2,1" || mesh.Name == "E2,2")
                        effect.DiffuseColor = Color.Fuchsia.ToVector3();

                    if (mesh.Name == "W0,0" || mesh.Name == "W0,1" || mesh.Name == "W0,2"
 || mesh.Name == "W1,0" || mesh.Name == "W1,1" || mesh.Name == "W1,2"
 || mesh.Name == "W2,0" || mesh.Name == "W2,1" || mesh.Name == "W2,2")
                        effect.DiffuseColor = Color.Firebrick.ToVector3();

                }

                mesh.Draw();
            }


        }


    }
}
