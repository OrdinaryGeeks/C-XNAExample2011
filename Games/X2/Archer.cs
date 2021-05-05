using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkinnedModel;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace SmellOfRevenge2011
{
    public class Archer : Character
    {
        public int state = 0;
        public int charNum;
        #region playeronly
        Move playerMove;
        TimeSpan playerMoveTime;
        public InputManager inputManager;
        readonly TimeSpan MoveTimeOut = TimeSpan.FromSeconds(1.0);
        #endregion
        public Vector3 formVec = Vector3.Zero;
        public Vector3 rangeVec = Vector3.Zero;

        private Vector3 dirToFormation;
        public Vector3 DirToFormation
        {
            get
            {
                return dirToFormation;
            }
            set
            {
                dirToFormation = value;
            }


        }
        public bool dead = false;
        public int hearts = 3;
        public bool active = false;
        public TimeSpan restTimer = TimeSpan.FromSeconds(1.0);
        Matrix formation;
        
        public bool openStrike = false;
        public bool struck = false;
        public bool shieldEngaged = false;
        public bool isBow = false;
        public bool isTripleBow = false;
        public bool arrow1Fired = false;
        public bool arrow2Fired = false;
        public bool arrow3Fired = false;


        public List<Projectile2> projectiles;


        public List<boundingSphere> spheres;
        public List<boundingSphere> rBow;
        public List<boundingSphere> shieldS;
        public List<boundingSphere> knockBackSphere;
        public List<boundingSphere> spellSpheres;
        public List<boundingSphere> collisionS;

        public Matrix forwardSpell;
        //for thrown weapons use arrow
        public Matrix arrowWorld;
        //for the pin move use spear
        public Matrix spearWorld;


        public TimeSpan currentAnimationTime = TimeSpan.Zero;
        public TimeSpan runTime = TimeSpan.Zero;
        public float thrustAmount = 0.0f;


        public Vector3 scale, scale2, scale3, scale4;
        public Quaternion rota, rota2, rota3, rota4;
        public Vector3 trans, trans2, trans3, trans4;

        public Matrix[] previousAnimation;
        public Matrix[] standing, brace, atk1a, atk1b, atk1c, atk1d,
            atk2a, atk2b, atk3a, atk3b, atk3c,
            lRun1, lRun2, lRun3, lRun4, rRun1, rRun2, rRun3, rRun4,
            shield1, shield2,
            kb1, kb2, kb3, knockDown,
            bow1, aim, release, releaseFollowThru;


        public bool isKnocked; //set in the active screen
        public bool isKnockBack, isKnockDown;

        public bool isStanding, isBrace, isAtk1, isAtk2, isAtk3, isRun,
            isShield;
        protected SkinningData skinningData;
        public SkinningData SkinningData
        {
            get
            {
                return this.skinningData;
            }
            set
            {
                this.skinningData = value;
            }


        }

        public AnimationPlayer masterPlayer;
        public AnimationClip masterClip;

        protected Matrix[] justBones;
        public Matrix[] JustBones
        {
            get
            {
                return justBones;
            }
            set
            {
                justBones = value;
            }
        }

        protected Matrix[] upperBones;
        public Matrix[] UpperBones
        {
            get
            {
                return upperBones;
            }
            set
            {
                upperBones = value;
            }


        }

        protected Matrix[] worldTrans;
        public Matrix[] WorldTrans
        {
            get
            {
                return worldTrans;
            }
            set
            {
                worldTrans = value;
            }
        }
        protected Matrix[] skinTrans;
        public Matrix[] SkinTrans
        {
            get
            {
                return skinTrans;
            }
            set
            {
                skinTrans = value;
            }
        }

        GamePadState currentGamePadState;
        GamePadState oldGamePadState;

        private Vector3 position;
        public Vector3 Position
        {

            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        private Vector3 direction;
        public Vector3 Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        private Matrix world;
        public Matrix World
        {
            get
            {
                return world;
            }
            set
            {
                world = value;
            }
        }
        private Vector3 up;
        public Vector3 Up
        {
            get
            {
                return up;
            }
            set
            {
                up = value;
            }
        }
        private Vector3 right;
        public Vector3 Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }

                public Archer(Vector3 pos, Vector3 dir)
        {

            Position = pos;
            Direction = dir;
            Up = Vector3.Up;
            Right = Vector3.Right;

            collisionS = new List<boundingSphere>();
            shieldS = new List<boundingSphere>();
            spheres = new List<boundingSphere>();
            rBow = new List<boundingSphere>();
            spellSpheres = new List<boundingSphere>();

            knockBackSphere = new List<boundingSphere>();

            projectiles = new List<Projectile2>();


            isKnockBack = false;
            isKnockDown = false;
            isStanding = false;
            isBrace = false;
            isAtk1 = false;
            isAtk2 = false;
            isAtk3 = false;
            isRun = false;
            isShield = false;

            inputManager = new InputManager((PlayerIndex)0, ScreenManager.moveList.LongestMoveLength);

        }
        public void setAnimationPlayers()
        {

            masterPlayer = new AnimationPlayer(skinningData);
            masterClip = skinningData.AnimationClips["Take 001"];
            masterPlayer.StartClip(masterClip);

            previousAnimation = new Matrix[skinningData.BindPose.Count];
            worldTrans = new Matrix[skinningData.BindPose.Count];
            skinTrans = new Matrix[skinningData.BindPose.Count];
            justBones = new Matrix[skinningData.BindPose.Count];
            upperBones = new Matrix[skinningData.BindPose.Count];


            standing = new Matrix[skinningData.BindPose.Count];
            brace = new Matrix[skinningData.BindPose.Count];
            atk1a = new Matrix[skinningData.BindPose.Count];
            atk1b = new Matrix[skinningData.BindPose.Count];
            atk1c = new Matrix[skinningData.BindPose.Count];
            atk1d = new Matrix[skinningData.BindPose.Count];
            atk2a = new Matrix[skinningData.BindPose.Count];
            atk2b = new Matrix[skinningData.BindPose.Count];
            atk3a = new Matrix[skinningData.BindPose.Count];
            atk3b = new Matrix[skinningData.BindPose.Count];
            atk3c = new Matrix[skinningData.BindPose.Count];
            shield1 = new Matrix[skinningData.BindPose.Count];
            shield2 = new Matrix[skinningData.BindPose.Count];
            kb1 = new Matrix[skinningData.BindPose.Count];
            kb2 = new Matrix[skinningData.BindPose.Count];
            kb3 = new Matrix[skinningData.BindPose.Count];
            knockDown = new Matrix[skinningData.BindPose.Count];

            lRun1 = new Matrix[skinningData.BindPose.Count];
            lRun2 = new Matrix[skinningData.BindPose.Count];
            lRun3 = new Matrix[skinningData.BindPose.Count];
            lRun4 = new Matrix[skinningData.BindPose.Count];
            rRun1 = new Matrix[skinningData.BindPose.Count];
            rRun2 = new Matrix[skinningData.BindPose.Count];
            rRun3 = new Matrix[skinningData.BindPose.Count];
            rRun4 = new Matrix[skinningData.BindPose.Count];


            bow1 = new Matrix[skinningData.BindPose.Count];
            aim = new Matrix[skinningData.BindPose.Count];
            release = new Matrix[skinningData.BindPose.Count];
            releaseFollowThru = new Matrix[skinningData.BindPose.Count]; 





            masterPlayer.Update(TimeSpan.FromMilliseconds(500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(standing, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(1000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(brace, 0);

            masterPlayer.Update(TimeSpan.FromMilliseconds(1500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(atk1a, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(2000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(atk1b, 0);

            masterPlayer.Update(TimeSpan.FromMilliseconds(2500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(atk1c, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(3000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(atk1d, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(3500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(atk2a, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(4000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(atk2b, 0);

            masterPlayer.Update(TimeSpan.FromMilliseconds(4500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(atk3a, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(5000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(atk3b, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(5500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(atk3c, 0);

            masterPlayer.Update(TimeSpan.FromMilliseconds(6000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(lRun1, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(6500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(lRun2, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(7000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(lRun3, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(7500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(lRun4, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(8000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(rRun1, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(8500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(rRun2, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(9000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(rRun3, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(9500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(rRun4, 0);


            masterPlayer.Update(TimeSpan.FromMilliseconds(10000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(shield1, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(10500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(shield2, 0);



            masterPlayer.Update(TimeSpan.FromMilliseconds(29000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(kb1, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(29500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(kb2, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(30000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(kb3, 0);

            masterPlayer.Update(TimeSpan.FromMilliseconds(30500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(knockDown, 0);

            masterPlayer.Update(TimeSpan.FromMilliseconds(27000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(bow1, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(27500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(aim, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(28000), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(release, 0);
            masterPlayer.Update(TimeSpan.FromMilliseconds(28500), false, Matrix.Identity);
            masterPlayer.GetBoneTransforms().CopyTo(releaseFollowThru, 0);


        }

        public float TurnToFace(Vector3 position, Vector3 target, Vector3 rotation)
        {
            float x = (target.X - position.X);
            float z = (target.Z - position.Z);

            float desiredAngle = (float)Math.Atan2(x, z);

            Vector3 tempDir = rotation;
            float difference = WrapAngle(desiredAngle - tempDir.Y);

            //  difference = MathHelper.Clamp(difference, -5.0f, 5.0f);

            return WrapAngle(tempDir.Y + difference);
        }

        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

        public void updateRun(GameTime gameTime)
        {

            runTime += new TimeSpan(gameTime.ElapsedGameTime.Ticks * (long)3.0);
            if (runTime.TotalSeconds < 9.0f)
            {

                //        if (runTime.TotalSeconds < 1.0f)
                //        {

                //            for (int i = 0; i < brace.Length; i++)
                //            {
                //                previousAnimation[i].Decompose(out scale, out rota, out trans);
                //                run1a[i].Decompose(out scale2, out rota2, out trans2);
                //                upperBones[i] = Matrix.CreateScale(scale2) *
                //Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(runTime.TotalSeconds) / 1.0f)) *
                //Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(runTime.TotalSeconds) / 1.0f));

                //            }

                //        }
                //        else 
                if (runTime.TotalSeconds < 1.0f)
                {
                    for (int i = 0; i < brace.Length; i++)
                    {
                        previousAnimation[i].Decompose(out scale, out rota, out trans);
                        lRun1[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(runTime.TotalSeconds) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(runTime.TotalSeconds) / 1.0f));

                    }


                }
                else if (runTime.TotalSeconds < 2.0f)
                {
                    for (int i = 0; i < brace.Length; i++)
                    {
                        lRun1[i].Decompose(out scale, out rota, out trans);
                        lRun2[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(runTime.TotalSeconds - 1.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(runTime.TotalSeconds - 1.0) / 1.0f));

                    }


                }
                else if (runTime.TotalSeconds < 3.0f)
                {
                    for (int i = 0; i < brace.Length; i++)
                    {
                        lRun2[i].Decompose(out scale, out rota, out trans);
                        lRun3[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(runTime.TotalSeconds - 2.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(runTime.TotalSeconds - 2.0) / 1.0f));

                    }


                }
                else if (runTime.TotalSeconds < 4.0f)
                {
                    for (int i = 0; i < brace.Length; i++)
                    {
                        lRun3[i].Decompose(out scale, out rota, out trans);
                        lRun4[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(runTime.TotalSeconds - 3.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(runTime.TotalSeconds - 3.0) / 1.0f));

                    }


                }
                else if (runTime.TotalSeconds < 5.0f)
                {
                    for (int i = 0; i < brace.Length; i++)
                    {
                        lRun4[i].Decompose(out scale, out rota, out trans);
                        rRun1[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(runTime.TotalSeconds - 4.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(runTime.TotalSeconds - 4.0) / 1.0f));

                    }


                }
                else if (runTime.TotalSeconds < 6.0f)
                {
                    for (int i = 0; i < brace.Length; i++)
                    {
                        rRun1[i].Decompose(out scale, out rota, out trans);
                        rRun2[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(runTime.TotalSeconds - 5.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(runTime.TotalSeconds - 5.0) / 1.0f));

                    }


                }
                else if (runTime.TotalSeconds < 7.0f)
                {
                    for (int i = 0; i < brace.Length; i++)
                    {
                        rRun2[i].Decompose(out scale, out rota, out trans);
                        rRun3[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(runTime.TotalSeconds - 6.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(runTime.TotalSeconds - 6.0) / 1.0f));

                    }


                }
                else if (runTime.TotalSeconds < 8.0f)
                {
                    for (int i = 0; i < brace.Length; i++)
                    {
                        rRun3[i].Decompose(out scale, out rota, out trans);
                        rRun4[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(runTime.TotalSeconds - 7.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(runTime.TotalSeconds - 7.0) / 1.0f));

                    }


                }
                else if (runTime.TotalSeconds < 9.0f)
                {
                    for (int i = 0; i < brace.Length; i++)
                    {
                        rRun4[i].Decompose(out scale, out rota, out trans);
                        lRun1[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(runTime.TotalSeconds - 8.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(runTime.TotalSeconds - 8.0) / 1.0f));

                    }


                }

            }
            else
            {

                runTime = TimeSpan.Zero;
                lRun1.CopyTo(previousAnimation, 0);
            }
        }

        public void updateKnockDown(GameTime gameTime)
        {


            currentAnimationTime += TimeSpan.FromTicks(gameTime.ElapsedGameTime.Ticks * 3);
            if (currentAnimationTime.TotalSeconds < 1.0f)
            {

                if (currentAnimationTime.TotalSeconds < 1.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        previousAnimation[i].Decompose(out scale, out rota, out trans);
                        knockDown[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds) / 1.0f));

                    }

                }
            }
            else
            {

                knockDown.CopyTo(upperBones, 0);
            }


        }

        public void updateBasic1(GameTime gameTime)
        {
            currentAnimationTime += TimeSpan.FromTicks(gameTime.ElapsedGameTime.Ticks * 3);
            if (currentAnimationTime.TotalSeconds < 4.0f)
            {

                if (currentAnimationTime.TotalSeconds < 1.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        previousAnimation[i].Decompose(out scale, out rota, out trans);
                        atk1a[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds) / 1.0f));

                    }

                }
                else if (currentAnimationTime.TotalSeconds < 2.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        atk1a[i].Decompose(out scale, out rota, out trans);
                        atk1b[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 1.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 1.0) / 1.0f));

                    }

                }
                else if (currentAnimationTime.TotalSeconds < 3.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        atk1b[i].Decompose(out scale, out rota, out trans);
                        atk1c[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 2.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 2.0) / 1.0f));

                    }

                }
                else if (currentAnimationTime.TotalSeconds < 4.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        atk1c[i].Decompose(out scale, out rota, out trans);
                        atk1d[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 3.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 3.0) / 1.0f));

                    }

                }

            }
            else
            {
                isAtk1 = false;
                currentAnimationTime = TimeSpan.Zero;

                if (isAtk2)
                {
                    atk1d.CopyTo(previousAnimation, 0);
                    openStrike = true;
                }
                else
                    active = false;

            }


        }

        public void UpdateBasic2(GameTime gameTime)
        {

            currentAnimationTime += TimeSpan.FromTicks(gameTime.ElapsedGameTime.Ticks * 3);
            if (currentAnimationTime.TotalSeconds < 2.0f)
            {

                if (currentAnimationTime.TotalSeconds < 1.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        previousAnimation[i].Decompose(out scale, out rota, out trans);
                        atk2a[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds) / 1.0f));

                    }

                }
                else if (currentAnimationTime.TotalSeconds < 2.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        atk2a[i].Decompose(out scale, out rota, out trans);
                        atk2b[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 1.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 1.0) / 1.0f));

                    }

                }
            }
            else
            {
                isAtk2 = false;
                currentAnimationTime = TimeSpan.Zero;

                if (isAtk3)
                {
                    atk2b.CopyTo(previousAnimation, 0);
                    openStrike = true;
                }

            }

        }
        public void UpdateBasic3(GameTime gameTime)
        {


            currentAnimationTime += TimeSpan.FromTicks(gameTime.ElapsedGameTime.Ticks * 3);
            if (currentAnimationTime.TotalSeconds < 3.0f)
            {

                if (currentAnimationTime.TotalSeconds < 1.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        previousAnimation[i].Decompose(out scale, out rota, out trans);
                        atk3a[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds) / 1.0f));

                    }

                }
                else if (currentAnimationTime.TotalSeconds < 2.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        atk3a[i].Decompose(out scale, out rota, out trans);
                        atk3b[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 1.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 1.0) / 1.0f));

                    }

                }
                else if (currentAnimationTime.TotalSeconds < 3.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        atk3b[i].Decompose(out scale, out rota, out trans);
                        atk3c[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 2.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 2.0) / 1.0f));

                    }

                }
            }
            else
            {
                isAtk3 = false;
                currentAnimationTime = TimeSpan.Zero;

                atk3c.CopyTo(previousAnimation, 0);

            }

        }

        public void UpdateBow(GameTime gameTime)
        {


            currentAnimationTime += TimeSpan.FromTicks(gameTime.ElapsedGameTime.Ticks * 3);

                    if (currentAnimationTime.TotalSeconds < 4.0f)
                    {

                        if (currentAnimationTime.TotalSeconds < 2.0f)
                        {

                            for (int i = 0; i < brace.Length; i++)
                            {
                                previousAnimation[i].Decompose(out scale, out rota, out trans);
                                aim[i].Decompose(out scale2, out rota2, out trans2);
                                upperBones[i] = Matrix.CreateScale(scale2) *
                Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds) / 2.0f)) *
                Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds) / 2.0f));

                            }

                        }
                        else if (currentAnimationTime.TotalSeconds < 4.0f)
                        {
                            for (int i = 0; i < brace.Length; i++)
                            {
                                aim[i].Decompose(out scale, out rota, out trans);
                                release[i].Decompose(out scale2, out rota2, out trans2);
                                upperBones[i] = Matrix.CreateScale(scale2) *
                Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 2.0) / 4.0f)) *
                Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 2.0) / 4.0f));

                            }


                        }
                    }
                    else if(!isTripleBow)
                    {
                        isBow = false;
                        currentAnimationTime = TimeSpan.Zero;


                        if (arrow1Fired == false)
                        {
                            // arrow1Fired = true;
                            projectiles.Add(new Projectile2("Arrow", arrowWorld, Direction));

                        }



                    }


                    else if (currentAnimationTime.TotalSeconds < 5.0f)
                    {
                        if (arrow1Fired == false)
                        {
                            arrow1Fired = true;
                            projectiles.Add(new Projectile2("Arrow", arrowWorld, Direction));

                        }
                        for (int i = 0; i < brace.Length; i++)
                        {
                            release[i].Decompose(out scale, out rota, out trans);
                            aim[i].Decompose(out scale2, out rota2, out trans2);
                            upperBones[i] = Matrix.CreateScale(scale2) *
            Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 4.0) / 1.0f)) *
            Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 4.0) / 1.0f));

                        }


                    }
                    else if (currentAnimationTime.TotalSeconds < 6.0f)
                    {
                        for (int i = 0; i < brace.Length; i++)
                        {
                            aim[i].Decompose(out scale, out rota, out trans);
                            release[i].Decompose(out scale2, out rota2, out trans2);
                            upperBones[i] = Matrix.CreateScale(scale2) *
            Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 5.0) / 1.0f)) *
            Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 5.0) / 1.0f));

                        }


                    }
                    else if (currentAnimationTime.TotalSeconds < 7.0f)
                    {
                        if (arrow2Fired == false)
                        {
                            arrow2Fired = true;
                            projectiles.Add(new Projectile2("Arrow", arrowWorld, Direction));

                        }
                        for (int i = 0; i < brace.Length; i++)
                        {
                            release[i].Decompose(out scale, out rota, out trans);
                            aim[i].Decompose(out scale2, out rota2, out trans2);
                            upperBones[i] = Matrix.CreateScale(scale2) *
            Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 6.0) / 1.0f)) *
            Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 6.0) / 1.0f));

                        }


                    }
                    else if (currentAnimationTime.TotalSeconds < 9.0f)
                    {
                        for (int i = 0; i < brace.Length; i++)
                        {
                            aim[i].Decompose(out scale, out rota, out trans);
                            release[i].Decompose(out scale2, out rota2, out trans2);
                            upperBones[i] = Matrix.CreateScale(scale2) *
            Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 7.0) / 2.0f)) *
            Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 7.0) / 2.0f));

                        }


                    }

                    else
                    {
                        if (arrow3Fired == false)
                        {
                            arrow3Fired = true;
                            projectiles.Add(new Projectile2("Arrow", arrowWorld, Direction));

                        }
                        currentAnimationTime = TimeSpan.Zero;
                        isBow = false;
                        isTripleBow = false;

                    }

                    



        }

        public void UpdateKnockBack(GameTime gameTime)
        {



            currentAnimationTime += gameTime.ElapsedGameTime;
            if (currentAnimationTime.TotalSeconds < 1.0f)
            {

                if (currentAnimationTime.TotalSeconds < 1.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        previousAnimation[i].Decompose(out scale, out rota, out trans);
                        kb1[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds) / 1.0f));

                    }

                }
                else if (currentAnimationTime.TotalSeconds < 2.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        kb1[i].Decompose(out scale, out rota, out trans);
                        kb2[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 1.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 1.0) / 1.0f));

                    }

                }
                //        else if (currentAnimationTime.TotalSeconds < 3.0f)
                //        {

                //            for (int i = 0; i < brace.Length; i++)
                //            {
                //                kb2[i].Decompose(out scale, out rota, out trans);
                //                kb3[i].Decompose(out scale2, out rota2, out trans2);
                //                upperBones[i] = Matrix.CreateScale(scale2) *
                //Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 2.0) / 1.0f)) *
                //Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds -2.0) / 1.0f));

                //            }

                //        }
            }
            else
            {
                isKnockBack = false;
                currentAnimationTime = TimeSpan.Zero;
                active = false;
                kb2.CopyTo(previousAnimation, 0);

            }








        }

        public void UpdateShield1(GameTime gameTime)
        {



            currentAnimationTime += gameTime.ElapsedGameTime;
            if (currentAnimationTime.TotalSeconds < 2.0f)
            {

                if (currentAnimationTime.TotalSeconds < 1.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        previousAnimation[i].Decompose(out scale, out rota, out trans);
                        shield1[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds) / 1.0f));

                    }

                }
                else if (currentAnimationTime.TotalSeconds < 2.0f)
                {

                    for (int i = 0; i < brace.Length; i++)
                    {
                        shield1[i].Decompose(out scale, out rota, out trans);
                        shield2[i].Decompose(out scale2, out rota2, out trans2);
                        upperBones[i] = Matrix.CreateScale(scale2) *
        Matrix.CreateFromQuaternion(Quaternion.Slerp(rota, rota2, (float)(currentAnimationTime.TotalSeconds - 1.0) / 1.0f)) *
        Matrix.CreateTranslation(Vector3.Lerp(trans, trans2, (float)(currentAnimationTime.TotalSeconds - 1.0) / 1.0f));

                    }

                }
            }
            else
            {
                if (currentGamePadState.IsButtonDown(Buttons.X))
                {
                    shield2.CopyTo(upperBones, 0);
                    shieldEngaged = true;
                }
                if (currentGamePadState.IsButtonUp(Buttons.X))
                {
                    isShield = false;
                    shieldEngaged = false;
                    currentAnimationTime = TimeSpan.Zero;

                    shield2.CopyTo(previousAnimation, 0);
                }
            }









        }

        public void UpdatePlayer(GameTime gameTime)
        {

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            oldGamePadState = currentGamePadState;
            Vector3 oldPosition = Position;
            currentGamePadState = GamePad.GetState(PlayerIndex.One);


            Vector2 rotationAmount = currentGamePadState.ThumbSticks.Left;

            Vector2 inputAmount = currentGamePadState.ThumbSticks.Right;
            if (gameTime.TotalGameTime - playerMoveTime > MoveTimeOut)
            {
                playerMove = null;
            }
            Move newMove = ScreenManager.moveList.DetectMove(ScreenManager.inputManager);
            if (newMove != null)
            {
                playerMove = newMove;

                playerMoveTime = gameTime.TotalGameTime;

                if (playerMove.Name == "A")
                {

                    if (!isAtk1)
                    {
                        isAtk1 = true;
                        currentAnimationTime = TimeSpan.Zero;
                        justBones.CopyTo(previousAnimation, 0);
                        openStrike = true;

                    }

                }
                if (playerMove.Name == "AA")
                {

                    if (!isAtk1)
                    {

                        isAtk1 = true;
                        isAtk2 = true;
                        currentAnimationTime = TimeSpan.Zero;
                        justBones.CopyTo(previousAnimation, 0);


                    }
                    else
                    {
                        isAtk2 = true;

                    }



                }
                if (playerMove.Name == "AAA")
                {

                    if (!isAtk1)
                    {
                        isAtk1 = true;
                        isAtk2 = true;
                        isAtk3 = true;
                        currentAnimationTime = TimeSpan.Zero;
                        justBones.CopyTo(previousAnimation, 0);

                    }
                    else if (!isAtk2)
                    {
                        isAtk2 = true;
                        isAtk3 = true;

                    }
                    else
                        isAtk3 = true;
                }
                if (playerMove.Name == "X")
                {
                    if (!isShield)
                    {
                        isShield = true;
                        currentAnimationTime = TimeSpan.Zero;
                        justBones.CopyTo(previousAnimation, 0);

                    }



                }
            }
            if ((rotationAmount.X) > .3f)
            {

                Direction = Vector3.Transform(Direction, Matrix.CreateFromAxisAngle(Vector3.Up, -.03f));
            }
            if ((rotationAmount.X) < -.3f)
            {
                Direction = Vector3.Transform(Direction, Matrix.CreateFromAxisAngle(Vector3.Up, .03f));

            }

            thrustAmount = -currentGamePadState.ThumbSticks.Left.Y;


            if (Math.Abs(thrustAmount) > 0.0f)
            {
                isRun = true;
                justBones.CopyTo(previousAnimation, 0);
            }
            else
            {
                isRun = false;
                runTime = TimeSpan.Zero;
            }

            Up.Normalize();
            Direction.Normalize();
            Right = Vector3.Cross(Direction, Up);


            Position += Direction * thrustAmount * 5.0f;

            if (isRun)
                updateRun(gameTime);
            if (isAtk1)
                updateBasic1(gameTime);
            else if (isAtk2)
                UpdateBasic2(gameTime);
            else if (isAtk3)
                UpdateBasic3(gameTime);
            else if (isShield)
                UpdateShield1(gameTime);

            if (isRun)
                upperBones.CopyTo(justBones, 0);
            else if (isAtk1)
                upperBones.CopyTo(justBones, 0);
            else if (isAtk2)
                upperBones.CopyTo(justBones, 0);
            else if (isAtk3)
                upperBones.CopyTo(justBones, 0);
            else if (isShield)
                upperBones.CopyTo(justBones, 0);
            else
                brace.CopyTo(justBones, 0);
            world.Forward = new Vector3(Direction.X, Direction.Y, Direction.Z);

            world.Up = Vector3.Up;
            world.Right = Vector3.Cross(world.Forward, world.Up);
            world.Translation = Position;

            // world.Translation = new Vector3(0.0f, 0.0f, 0.0f);
            UpdateWorldTransforms(Matrix.Identity);
            UpdateSkinTransforms();
            //Console.WriteLine(playerMove.Name);



        }
        public void UpdateFollower(GameTime gameTime)
        {

            float dTof = Vector3.Distance(position, formVec);
            float rotationAmount = 0.0f;

            if (dTof < 10)
                state = 0;

            float dToT = Vector3.Distance(position, ScreenManager.ps1.World.Translation);
            //if(inForm)



            Up = Vector3.Up;
            Right = Vector3.Cross(Direction, Up);
            //Console.WriteLine(dToT);

            if (hearts <= 0)
            {
                if (!dead)
                {
                    currentAnimationTime = TimeSpan.Zero;
                    justBones.CopyTo(previousAnimation, 0);
                    isKnockDown = true;

                }
                updateKnockDown(gameTime);
            }
            else
            {
                rotationAmount = TurnToFace(position, formVec, new Vector3(0.0f, (float)Math.Atan((double)(Direction.Z / Direction.X)), 0.0f));

                float x = (float)Math.Sin(rotationAmount);
                float y = (float)Math.Cos(rotationAmount);
                DirToFormation = new Vector3(x, 0.0f, y);


                rotationAmount = TurnToFace(position, ScreenManager.ps1.World.Translation, new Vector3(0.0f, (float)Math.Atan((double)(Direction.Z / Direction.X)), 0.0f));

                x = (float)Math.Sin(rotationAmount);
                y = (float)Math.Cos(rotationAmount);
                //if (!dead)
                Direction = new Vector3(x, 0.0f, y);



                if (!active)
                    restTimer += gameTime.ElapsedGameTime;

                if (restTimer > TimeSpan.FromSeconds(3.0))
                    restTimer = TimeSpan.FromSeconds(3.0);
                if (isKnocked)
                {
                    isKnocked = false;
                    isAtk1 = false;
                    isAtk2 = false;
                    isAtk3 = false;
                    isRun = false;
                    isKnockBack = true;
                    justBones.CopyTo(previousAnimation, 0);
                    currentAnimationTime = TimeSpan.Zero;

                }




                if (dToT < 300.0f)//if it can be seen
                    if (dToT > 80)
                    {
                        //  resetFormation = true;
                        thrustAmount = 1.0f;
                        if (!isRun)
                        {
                            isRun = true;
                            justBones.CopyTo(previousAnimation, 0);
                        }

                    }
                    else
                    {


                        //   resetFormation = false;
                        thrustAmount = 0.0f;
                        runTime = TimeSpan.Zero;
                        isRun = false;

                        if (dToT > 80)
                        {
                            thrustAmount = 0.3f;

                        }
                        else
                        {
                            thrustAmount = 0.0f;

                            if (restTimer > TimeSpan.FromSeconds(.5) & !isAtk1 & !isAtk2 & !isAtk3)
                            {

                                int random = ScreenManager.rand.Next();
                                {
                                    if (random % 3 == 1)
                                    {

                                        active = true;
                                        isAtk1 = true;

                                        openStrike = true;
                                        restTimer = TimeSpan.Zero;
                                    }
                                    if (random % 3 == 2)
                                    {
                                        isAtk2 = true;
                                        isAtk1 = true;
                                    }
                                    if (random % 3 == 0)
                                    {
                                        isAtk3 = true;
                                        isAtk2 = true;
                                        isAtk1 = true;
                                    }

                                }


                                if (!isAtk1)
                                {
                                    isAtk1 = true;
                                    active = true;

                                }
                            }

                        }

                    }
              //  Console.WriteLine(restTimer);
            }

            if (isRun)
                updateRun(gameTime);

            if (isAtk1)
                updateBasic1(gameTime);
            else if (isAtk2)
                UpdateBasic2(gameTime);
            else if (isAtk3)
                UpdateBasic3(gameTime);

            if (isKnockBack)
                UpdateKnockBack(gameTime);
            if (isKnockDown)
                updateKnockDown(gameTime);
            if (state == 1)
                Position += new Vector3(dirToFormation.X, dirToFormation.Y, dirToFormation.Z) * thrustAmount * 2.0f;
            else if (isRun)
                Position += new Vector3(Direction.X, Direction.Y, Direction.Z) * thrustAmount * 2.0f;
            //if(isKnockBack)
            //    Position -= new Vector3(Direction.X, Direction.Y, Direction.Z) * thrustAmount * 2.0f;

            if (isRun)
                upperBones.CopyTo(justBones, 0);
            else if (isAtk1 || isAtk2 || isAtk3)
                upperBones.CopyTo(justBones, 0);
            else if (isKnockBack)
                upperBones.CopyTo(justBones, 0);
            else if (isKnockDown)
                upperBones.CopyTo(justBones, 0);
            else
                brace.CopyTo(justBones, 0);



            int pX = (int)position.X / 100;
            int pZ = (int)position.Z / 100;

            if (Position.X < 0)
                pX = 0;
            if (Position.Z < 0)
                pZ = 0;
            if (position.X > 12799)
                pX = 127;
            if (position.Z > 12799)
                pZ = 127;
            //Console.WriteLine(
            // world.Forward = new Vector3(Direction.X, Direction.Y, Direction.Z);
            world = Matrix.Identity;
            world.Forward = new Vector3(-Direction.X, Direction.Y, -Direction.Z);
            world.Up = Vector3.Up;
            world.Right = Vector3.Cross(world.Forward, world.Up);
            //position.Y = ScreenManager.heightData[pX, pZ];
            world.Translation = Position;




            // world.Translation = new Vector3(0.0f, 0.0f, 0.0f);
            UpdateWorldTransforms(Matrix.Identity);
            UpdateSkinTransforms();

            //if(resetFormation)
            formation = world;
        }
        public void UpdateLeader(GameTime gameTime)
        {
            int shortest = 0;
            float shortDist = Vector3.Distance(ScreenManager.Theseus.CFormation[0], Position);
            for (int i = 0; i < 9; i++)
            {
                if (i != 4)
                {
                    float thisDist = Vector3.Distance(ScreenManager.Theseus.CFormation[i], Position);
                    if (shortDist > thisDist)
                        shortest = i;

                }

            }

            formVec = ScreenManager.Theseus.CFormation[shortest];

            float dTof = Vector3.Distance(position, formVec);
            float rotationAmount = 0.0f;
            rotationAmount = TurnToFace(position, formVec, new Vector3(0.0f, (float)Math.Atan((double)(Direction.Z / Direction.X)), 0.0f));

            float x = (float)Math.Sin(rotationAmount);
            float y = (float)Math.Cos(rotationAmount);
            DirToFormation = new Vector3(x, 0.0f, y);

            rotationAmount = TurnToFace(position, ScreenManager.Theseus.World.Translation, new Vector3(0.0f, (float)Math.Atan((double)(Direction.Z / Direction.X)), 0.0f));
           
            foreach (Projectile2 pro in projectiles)
                pro.update2E(gameTime);


            if (state == 1 && dTof < 20)
                state = 0;



            float dToT = Vector3.Distance(position, ScreenManager.Theseus.World.Translation);
            //if(inForm)

            rotationAmount = TurnToFace(position, ScreenManager.Theseus.World.Translation, new Vector3(0.0f, (float)Math.Atan((double)(Direction.Z / Direction.X)), 0.0f));



            //Console.WriteLine(dToT);

            if (hearts <= 0)
            {
                if (!dead)
                {
                    currentAnimationTime = TimeSpan.Zero;
                    justBones.CopyTo(previousAnimation, 0);
                    isKnockDown = true;
                    dead = true;
                    isAtk1 = isAtk2 = isAtk3 = false;

                }
                updateKnockDown(gameTime);
            }
            else

            {
                
            

             x = (float)Math.Sin(rotationAmount);
             y = (float)Math.Cos(rotationAmount);
             Direction = new Vector3(x, 0.0f, y);

             Up = Vector3.Up;
             Right = Vector3.Cross(Direction, Up);



                if (!active && state == 0)
                    restTimer += gameTime.ElapsedGameTime;

                if (restTimer > TimeSpan.FromSeconds(3.0))
                {
                    restTimer = TimeSpan.FromSeconds(3.0);
                    active = true;
                }
                if (isKnocked)
                {
                    isKnocked = false;
                    isAtk1 = false;
                    isAtk2 = false;
                    isAtk3 = false;
                    isRun = false;
                    isKnockBack = true;
                    justBones.CopyTo(previousAnimation, 0);
                    currentAnimationTime = TimeSpan.Zero;

                }

   
                
                
                    if (dToT < 300.0f)//if it can be seen
                        if (dToT >220.0f)
                        {
                            //  resetFormation = true;
                            thrustAmount = 1.0f;
                            if (!isRun)
                            {
                                isRun = true;
                                justBones.CopyTo(previousAnimation, 0);
                            }

                        }
                        else
                        {


                            //   resetFormation = false;
                            thrustAmount = 0.0f;
                            runTime = TimeSpan.Zero;
                            isRun = false;

                            if (dToT > 190)
                            {
                                thrustAmount = 0.3f;

                            }
                            else
                            {
                                thrustAmount = 0.0f;

                                if (restTimer > TimeSpan.FromSeconds(.5) & !isAtk1 & !isAtk2 & !isAtk3)
                                {

                                    int random = ScreenManager.rand.Next();
                                    {
                                        if (random % 3 == 1)
                                        {

                                            active = true;
                                            isAtk1 = true;

                                            openStrike = true;
                                            restTimer = TimeSpan.Zero;
                                        }
                                        if (random % 3 == 2)
                                        {
                                            isAtk2 = true;
                                            isAtk1 = true;
                                        }
                                        if (random % 3 == 0)
                                        {
                                            isAtk3 = true;
                                            isAtk2 = true;
                                            isAtk1 = true;
                                        }

                                    }


                                    if (!isAtk1)
                                    {
                                        isAtk1 = true;
                                        active = true;

                                    }
                                }

                            }

                        }
                   // Console.WriteLine(restTimer);
                }








            
            if (isRun)
                updateRun(gameTime);

            if (isAtk1)
                UpdateBow(gameTime);
            else if (isAtk2)
                UpdateBasic2(gameTime);
            else if (isAtk3)
                UpdateBasic3(gameTime);

            if (isKnockBack)
                UpdateKnockBack(gameTime);
            if (isKnockDown)
                updateKnockDown(gameTime);

            if (!dead)
            {
                if (state == 1)
                    Position += new Vector3(DirToFormation.X, DirToFormation.Y, DirToFormation.Z) * 2.0f;
                else if (isRun)
                    Position += new Vector3(Direction.X, Direction.Y, Direction.Z) * thrustAmount * 2.0f;
            }
            //if(isKnockBack)
            //    Position -= new Vector3(Direction.X, Direction.Y, Direction.Z) * thrustAmount * 2.0f;

            if (isRun)
                upperBones.CopyTo(justBones, 0);
            else if (isAtk1 || isAtk2 || isAtk3)
                upperBones.CopyTo(justBones, 0);
            else if (isKnockBack)
                upperBones.CopyTo(justBones, 0);
            else if (isKnockDown)
                upperBones.CopyTo(justBones, 0);
            else
                brace.CopyTo(justBones, 0);



            int pX = (int)position.X / 100;
            int pZ = (int)position.Z / 100;

            if (Position.X < 0)
                pX = 0;
            if (Position.Z < 0)
                pZ = 0;
            if (position.X > 12799)
                pX = 127;
            if (position.Z > 12799)
                pZ = 127;
            //Console.WriteLine(
            // world.Forward = new Vector3(Direction.X, Direction.Y, Direction.Z);
            world = Matrix.Identity;
            world.Forward = new Vector3(-Direction.X, Direction.Y, -Direction.Z);
            world.Up = Vector3.Up;
            world.Right = Vector3.Cross(world.Forward, world.Up);
            //position.Y = ScreenManager.heightData[pX, pZ];
            world.Translation = Position;




            // world.Translation = new Vector3(0.0f, 0.0f, 0.0f);
            UpdateWorldTransforms(Matrix.Identity);
            UpdateSkinTransforms();

            //if(resetFormation)
            formation = world;





        }
  
        public void UpdateBrace(GameTime gameTime)
        {

            brace.CopyTo(justBones, 0);
            brace.CopyTo(previousAnimation, 0);
            world.Up = Vector3.Up;
            world.Forward = Vector3.Backward;
            world.Right = Vector3.Cross(world.Forward, world.Up);
            world.Translation = Position;

            UpdateWorldTransforms(Matrix.Identity);
            UpdateSkinTransforms();


        }
        public void UpdateWorldTransforms(Matrix rootTransform)
        {
            // Root bone.
            worldTrans[0] = justBones[0] * world;

            // Child bones.
            for (int bone = 1; bone < worldTrans.Length; bone++)
            {
                int parentBone = skinningData.SkeletonHierarchy[bone];

                worldTrans[bone] = justBones[bone] *
                                             worldTrans[parentBone];
            }
        }
        public void UpdateSkinTransforms()
        {
            for (int bone = 0; bone < skinTrans.Length; bone++)
            {
                skinTrans[bone] = skinningData.InverseBindPose[bone] *
                                            worldTrans[bone];
            }
        }



    }
}
