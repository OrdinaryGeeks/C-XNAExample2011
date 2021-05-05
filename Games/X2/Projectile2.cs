
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;


namespace SmellOfRevenge2011
{
    public class ProjectileTargeted
    {

        protected int team;
        public int Team
        {
            get
            {
                return team;
            }
            set
            {
                team = value;
            }
        }

        protected int target;
        public int Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }
        public float Scale = 0.0f;
        public Vector3 Direction = Vector3.Zero;
        public Vector3 Translation = Vector3.Zero;
        public string Name = "";
        public TimeSpan currentTime;
        public bool alive = false;
        public Vector3 TravelDirection = Vector3.Zero;

        public Matrix world = Matrix.Identity;

        public ProjectileTargeted(string name, Matrix World, int TeamOn, int Targeted)
        {
            team = TeamOn;
            target = Targeted;

            world = World;
            Name = name;
            Direction = world.Forward;
            Translation = world.Translation;

            //TravelDirection = direction;

        }
        public ProjectileTargeted(float scale, Vector3 direction, Vector3 translation, string name)
        {
            Scale = scale;
            Direction = direction;
            Translation = translation;
            Name = name;
            currentTime = TimeSpan.Zero;
            world = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(direction.X, direction.Y, direction.Z) * Matrix.CreateTranslation(translation);
        }

        public void updateRockUp(float interp)
        {


            Translation = Vector3.Lerp(new Vector3(Translation.X, 0.0f, Translation.Z), Translation, interp);
            world.Translation = Translation;
            if (interp > 1.0)
                alive = false;
         


        }
        public void update2E(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime;

            Translation = Vector3.Add(Translation, new Vector3(TravelDirection.X, TravelDirection.Y, TravelDirection.Z) * (float)currentTime.TotalSeconds * 2);


           // if (currentTime.TotalSeconds > 3.0f)
             //   alive = false;

            //Direction.Normalize();
            
           // world = Matrix.Identity;
           // world.Forward = new Vector3(Direction.X, Direction.Y, Direction.Z);
            world.Translation = Translation;
           // world.Up = Vector3.Up;
           //// Vector3 Right = Vector3.Cross(Direction, Vector3.Up);
            
           // world.Right = Vector3.Cross(Direction, Vector3.Up);


        }
        public void update2ENonMoving(GameTime gameTime)
        {


            world.Translation = Translation;




        }
        public void update2(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime;

            Translation = Vector3.Add(Translation, new Vector3(-TravelDirection.X, TravelDirection.Y, -TravelDirection.Z) * (float)currentTime.TotalSeconds/2 );


            if (currentTime.TotalSeconds > 3.0f)
                alive = false;

            world.Translation = Translation;

        }
        public void update(GameTime gameTime)
        {

            currentTime += gameTime.ElapsedGameTime;
            
            Translation = Vector3.Add(Translation, Direction * (float)currentTime.TotalSeconds * 2.0f);


            if (currentTime.TotalSeconds > 3.0f)
                alive = false;

            world = Matrix.CreateScale(Scale);
            world.Forward = Direction;
            world.Translation = Translation;
            world.Up = Vector3.Up;
            world.Right = Vector3.Cross(Direction, Vector3.Up);

            




        }


    }
}
