 #region File Description
//-----------------------------------------------------------------------------
// ScreenManager.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Input;
using SkinnedModel;
#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif
using System.IO;
using System.IO.IsolatedStorage;
#endregion

namespace SmellOfRevenge2011
{
    /// <summary>
    /// The screen manager is a component which manages one or more GameScreen
    /// instances. It maintains a stack of screens, calls their Update and Draw
    /// methods at the appropriate times, and automatically routes input to the
    /// topmost active screen.
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {

        #region Fields

        //Captain Asterion the mino
        //Lt Bender the Rock
        //Capt Paris 
       

        // DebugFormations

        #region boundingboxes
        public static int x = 1;
        public static int y = 1;


        public static bool[][] open = new bool[11][];
        public static BoundingBox[][] openBoxes = new BoundingBox[11][];


        public static bool[][] bigOpen = new bool[30][];
        public static BoundingBox[][] bigOpenBoxes = new BoundingBox[30][];
        #endregion
        //for minolvl1


        public Texture2D currentSelection;
        public TimeSpan betweenLevels = TimeSpan.Zero;
        public static bool gateOpen = false;
        public static InputState globalInput;
        public static bool script = false;

        public static List<string> scriptMessage;
    


        public static bool addDialogue = false;
        public static int storyIndex = 2; 

        public static bool battle = false;

        public static Quaternion battleRota = new Quaternion();
        public static Vector3 battleTrans = new Vector3();
        #region formations
        public static Vector3[] CFormation = new Vector3[9];
        public static Vector3[] NFormation = new Vector3[9];
        public static Vector3[] SFormation = new Vector3[9];
        public static Vector3[] EFormation = new Vector3[9];
        public static Vector3[] WFormation = new Vector3[9];
        #endregion
        #region mino boneindices
        public static int mhead;
        public static int mneck;
        public static int mspine2;
        public static int mspine1;
        public static int mspine;
        public static int mhips;
        public static int mlULeg;
        public static int mlLLeg;
        public static int mrULeg;
        public static int mrLLeg;
        public static int mrArm;
        public static int mrFArm;
        public static int mlArm;
        public static int mlFArm;
        public static int mlHand;
        public static int mrHand;
        public static int mrFoot;
        public static int mlFoot;
        #endregion
        #region mino boneindices
        public static int head;
        public static int neck;
        public static int spine2;
        public static int spine1;
        public static int spine;
        public static int hips;
        public static int lULeg;
        public static int lLLeg;
        public static int rULeg;
        public static int rLLeg;
        public static int rArm;
        public static int rFArm;
        public static int lArm;
        public static int lFArm;
        public static int lHand;
        public static int rHand;
        public static int rFoot;
        public static int lFoot;
        #endregion

        public ParticleSystem fireParticles;
        public ParticleSystem slashParticles;

        TimeSpan switchTime = TimeSpan.Zero;

        public int player = 2; 

        public  List<boundingSphere> creteSpheres;

        const int shadowMapWidthHeight = 2048;

        const int windowWidth = 800;
        const int windowHeight = 480;
        public Vector3 lightDir = new Vector3(-0.3333333f, 0.6666667f, 0.6666667f);

        public RenderTarget2D shadowRenderTarget;
        public Matrix lightViewProjection;


        public static List<Vector3> flags; 
        public static float[,] heightData;
        public static float[,] colData;
        int terrainWidth, terrainHeight;
        public static List<Vector3> towers;

        public static List<Tower> Towers;

        public static Model FinalProjectiles;
        public Texture2D hexGrid;
        public Texture2D gradient;
        public Texture2D fire;
        public Texture2D slashTga;
        public Texture2D smoke;
        public Texture2D gold1;
        public Texture2D satin;
        public Texture2D minoTex;
        Texture2D heightMap;
        Texture2D colMap;
        public Texture2D grass;
        public Texture2D humanTex;
        public static InputManager inputManager;
        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> screensToUpdate = new List<GameScreen>();

        InputState input = new InputState();

        SpriteBatch spriteBatch;
        SpriteFont font;
        SpriteFont menuFont;
        SpriteFont gameFont;
        public Texture2D blankTexture;
        Game aGame;

        

        protected bool isInitialized;

        bool traceEnabled;

        public static PrimitiveBatch primitiveBatch;
        
        public static Camera camera;


        public static MoveList moveList;
        public List<Move> theMoves;

        // Stores each players' most recent move and when they pressed it.
        Move[] playerMoves;
        TimeSpan[] playerMoveTimes;

        public static float arrowScale;
        public static float throwingAxeScale;
        public static Model minotaur;
        public static Model gorgon;
        public static Model angel;
        public static Model aosBoard;
        public static Model spearMan;
        public static Model juneModel;

        public Model humanProjectiles;
        public Model humanFormation;

        public static Model minoSphere;
        public static Model spearSphere;

        public Model minoBoard1;
        public Model board;
        public Model flag;
        public Model tower;
        public static Gorgon medusa;
        public static Minotaur asterion;
        public static Angel michael;

       
        public static List<MinotaurAI> minotaurs;
        public static List<GorgonAI> gorgons;
        public static int playerCharSelected = 1;

        public Texture2D white;
        public Texture2D aosCollision;

        public static Gorgon p1Gorgon;
        public static Minotaur p1Mino;
        public static Angel p1Angel;



        public static JuneXnaModel p1June;
        public static JuneAIModel eJune;

        public static JuneXnaModel Theseus;
        public static JuneXnaModel Hercules;
        public static JuneXnaModel Perseus;
        


        #endregion

        #region 
        public int mRHand;
        public int mLHand;

        #endregion
        #region Properties


        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public Game AGame
        {
            get { return aGame; }
        }
        /// <summary>
        /// A default font shared by all the screens. This saves
        /// each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
        }

        public SpriteFont GameFont
        {
            get
            {
                return gameFont;
            }
        }
        public SpriteFont MenuFont
        {
            get
            {
                return menuFont;
            }
        }

        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }


        #endregion


        #region Initialization

        public bool IsInitialized
    {
        get{
        return this.isInitialized;
    }
    set{
    this.isInitialized = value;
}

    }
        protected bool compSet;
        public bool CompSet
        {
            get
            {
                return compSet;
            }
            set
            {
                compSet = value;
            }
        }

        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        public ScreenManager(Game game)
            : base(game)
        {
            aGame = game;
            globalInput = new InputState();
            
        }


        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }

        public static void RemapModel(Model model, Effect effect)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = effect;
                }
            }

        }

        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load content belonging to the screen manager.
            ContentManager content = Game.Content;

            currentSelection = content.Load<Texture2D>("currentSelection");
            gradient = content.Load<Texture2D>("gradient");
            minoBoard1 = content.Load<Model>("minoBoard1");

            aosBoard = content.Load<Model>("finalBoard");
            fireParticles = new FireParticleSystem(Game, content);
            slashParticles = new SlashParticleSystem(Game, content);
            Game.Components.Add(slashParticles);
            Game.Components.Add(fireParticles);

            shadowRenderTarget = new RenderTarget2D(GraphicsDevice, shadowMapWidthHeight, shadowMapWidthHeight, false, SurfaceFormat.Single, DepthFormat.Depth24);

            for (int i = 0; i < 11; i++)
            {
                open[i] = new bool[11];
                for (int j = 0; j < 11; j++)
                    open[i][j] = true;

            }
            for(int i = 0; i<11; i++)
            {
                openBoxes[i] = new BoundingBox[11];
                for (int j = 0; j < 11; j++)
                    openBoxes[i][j] = new BoundingBox(new Vector3(i * 60, 0.0f, j * 60), new Vector3((i + 1) * 60, 50.0f, (j + 1) * 60));

            }


            for (int i = 0; i < 30; i++)
            {
                bigOpen[i] = new bool[30];
                for (int j = 0; j < 30; j++)
                    bigOpen[i][j] = true;
            }
            for (int i = 0; i < 30; i++)
            {
                bigOpenBoxes[i] = new BoundingBox[30];
                for (int j = 0; j < 30; j++)
                    bigOpenBoxes[i][j] = new BoundingBox(new Vector3(i * 70, 0.0f, j * 70), new Vector3((i + 1) * 70, 50.0f, (j + 1) * 70));


            }
            Vector3 scale = Vector3.Zero;
            Vector3 trans = Vector3.Zero;
            Quaternion rota = Quaternion.Identity;

            white = content.Load<Texture2D>("ice");



            hexGrid = content.Load<Texture2D>("hexGrid");
            fire = content.Load<Texture2D>("fire");
            smoke = content.Load<Texture2D>("smoke");
            humanTex = content.Load<Texture2D>("tex");
            gold1 = content.Load<Texture2D>("gold1");
            satin = content.Load<Texture2D>("satin");
            minoTex = content.Load<Texture2D>("minoTex");


            colMap = content.Load<Texture2D>("aosCollision");


            slashTga = content.Load<Texture2D>("slash");


            Towers = new List<Tower>();
            flags = new List<Vector3>();
            towers = new List<Vector3>();
            theMoves = new List<Move>();
            theMoves.Add(new Move("A", Buttons.A) { IsSubMove = true });
            theMoves.Add(new Move("B", Buttons.B));
            theMoves.Add(new Move("X", Buttons.X));
            theMoves.Add(new Move("Y", Buttons.Y));
            theMoves.Add(new Move("RT", Buttons.RightTrigger));
            theMoves.Add(new Move("LT", Buttons.LeftTrigger));
            theMoves.Add(new Move("AA", Buttons.A, Buttons.A) { IsSubMove = true });
            theMoves.Add(new Move("AAA", Buttons.A, Buttons.A, Buttons.A) );
            theMoves.Add(new Move("AAY", Buttons.A, Buttons.A, Buttons.Y) );
            theMoves.Add(new Move("RS", Buttons.RightShoulder));
            theMoves.Add(new Move("LS", Buttons.LeftShoulder));
            theMoves.Add(new Move("Start", Buttons.DPadUp));
            moveList = new MoveList(theMoves);

            inputManager = new InputManager((PlayerIndex)0, moveList.LongestMoveLength);

            primitiveBatch = new PrimitiveBatch(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("Font");
            gameFont = content.Load<SpriteFont>("gamefont");
            menuFont = content.Load<SpriteFont>("menufont");
            blankTexture = content.Load<Texture2D>("blank");

            FinalProjectiles = content.Load<Model>("projNoSpells");
            flag = content.Load<Model>("flag");
            tower = content.Load<Model>("tower");
           // aosBoard = content.Load<Model>("aosBoard");
            minotaur = content.Load<Model>("JuneXnaMino");
            gorgon = content.Load<Model>("DusaXnaModel");
            angel = content.Load<Model>("AngelXnaModel");
            board = content.Load<Model>("JBoard");
            minoSphere = content.Load<Model>("MinoXnaSpheres");
            spearMan = content.Load<Model>("PerseusXnaModel");
            humanFormation = content.Load<Model>("formationSpheres");
            spearSphere = content.Load<Model>("perseusSpheres");

            juneModel = content.Load<Model>("FinalSmellXna");
            humanProjectiles = content.Load<Model>("humanProjectiles");

            
            creteSpheres = new List<boundingSphere>();

            //tomb parthenon cave theseus crucible
            Matrix[] transforms = new Matrix[aosBoard.Bones.Count];
            aosBoard.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in aosBoard.Meshes)
            {
                transforms[mesh.ParentBone.Index].Decompose(out scale, out rota, out trans);
                if(mesh.Name == "tombSphere" || mesh.Name == "parthenonSphere" || mesh.Name == "caveSphere" ||mesh.Name == "theseusSphere" ||mesh.Name == "crucibleSphere" )
                    creteSpheres.Add(new boundingSphere(mesh.Name, new BoundingSphere( trans, mesh.BoundingSphere.Radius * scale.X))); 
               
            }


            p1June = new JuneXnaModel(new Vector3(1410.0f, 0.0f, 6100.7f), Vector3.Backward);
            p1June.SkinningData = juneModel.Tag as SkinningData;
            p1June.setAnimationPlayers();

            eJune = new JuneAIModel(new Vector3(100.0f, 0.0f, 100.0f), Vector3.Backward);
            eJune.SkinningData = juneModel.Tag as SkinningData;
            eJune.setAnimationPlayers();

            transforms = new Matrix[spearSphere.Bones.Count];
            spearSphere.CopyAbsoluteBoneTransformsTo(transforms);
            transforms[0].Decompose(out scale, out rota, out trans);


            Hercules = new JuneXnaModel(new Vector3(100.0f, 0.0f, 0.0f), Vector3.Forward);
            Hercules.SkinningData = juneModel.Tag as SkinningData;
            Hercules.setAnimationPlayers();

            Perseus = new JuneXnaModel(new Vector3(300.0f, 0.0f, 0.0f), Vector3.Forward);
            Perseus.SkinningData = juneModel.Tag as SkinningData;
            Perseus.setAnimationPlayers();

            Theseus = new JuneXnaModel(new Vector3(500.0f, 0.0f, 0.0f), Vector3.Forward);
            Theseus.SkinningData = juneModel.Tag as SkinningData;
            Theseus.setAnimationPlayers();



    

            loadSpheresJuneModel(Hercules);
            loadSpheresJuneModel(Perseus);
            loadSpheresJuneModel(Theseus);




      

            

            minotaurs = new List<MinotaurAI>();


            #region boneIndices
            //mhead = p1Mino.SkinningData.BoneIndices["Head"];
            //mneck = p1Mino.SkinningData.BoneIndices["Neck"];
            //mspine1 = p1Mino.SkinningData.BoneIndices["Spine1"];
            //mspine = p1Mino.SkinningData.BoneIndices["Spine"];
            //mrArm = p1Mino.SkinningData.BoneIndices["RightArm"];
            //mrFArm = p1Mino.SkinningData.BoneIndices["RightForeArm"];
            //mrHand = p1Mino.SkinningData.BoneIndices["RightHand"];
            //mlULeg = p1Mino.SkinningData.BoneIndices["LeftUpLeg"];
            //mlLLeg = p1Mino.SkinningData.BoneIndices["LeftLeg"];
            //mlFoot = p1Mino.SkinningData.BoneIndices["LeftFoot"];
            //mlArm = p1Mino.SkinningData.BoneIndices["LeftArm"];
            //mlFArm = p1Mino.SkinningData.BoneIndices["LeftForeArm"];
            //mlHand = p1Mino.SkinningData.BoneIndices["LeftHand"];
            //mhips = p1Mino.SkinningData.BoneIndices["Hips"];
            //mrULeg = p1Mino.SkinningData.BoneIndices["RightUpLeg"];
            //mrLLeg = p1Mino.SkinningData.BoneIndices["RightLeg"];
            //mrFoot = p1Mino.SkinningData.BoneIndices["RightFoot"];
            #endregion
            #region boneIndices
            head = p1June.SkinningData.BoneIndices["Head"];
            neck = p1June.SkinningData.BoneIndices["Neck"];
            spine1 = p1June.SkinningData.BoneIndices["Spine1"];
            spine = p1June.SkinningData.BoneIndices["Spine"];
            rArm = p1June.SkinningData.BoneIndices["RightArm"];
            rFArm = p1June.SkinningData.BoneIndices["RightForeArm"];
            rHand = p1June.SkinningData.BoneIndices["RightHand"];
            lULeg = p1June.SkinningData.BoneIndices["LeftUpLeg"];
            lLLeg = p1June.SkinningData.BoneIndices["LeftLeg"];
            lFoot = p1June.SkinningData.BoneIndices["LeftFoot"];
            lArm = p1June.SkinningData.BoneIndices["LeftArm"];
            lFArm = p1June.SkinningData.BoneIndices["LeftForeArm"];
            lHand = p1June.SkinningData.BoneIndices["LeftHand"];
            hips = p1June.SkinningData.BoneIndices["Hips"];
            rULeg = p1June.SkinningData.BoneIndices["RightUpLeg"];
            rLLeg = p1June.SkinningData.BoneIndices["RightLeg"];
            rFoot = p1June.SkinningData.BoneIndices["RightFoot"];
            #endregion

            for (int i = 0; i < 1; i++)
            {
                minotaurs.Add(new MinotaurAI(new Vector3(0.0f, i * 20.0f, i * 40.0f), Vector3.Backward));
                minotaurs[i].SkinningData = minotaur.Tag as SkinningData;
                minotaurs[i].setAnimationPlayers();

            }

            gorgons = new List<GorgonAI>();
            for (int i = 0; i < 1; i++)
            {
                gorgons.Add(new GorgonAI(new Vector3(6400.0f, 0.0f, 1280.0f), Vector3.Backward));
                gorgons[i].SkinningData = gorgon.Tag as SkinningData;
                gorgons[i].setAnimationPlayers();

            }
 transforms = new Matrix[tower.Bones.Count];
            tower.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in tower.Meshes)
            {
                
                if (mesh.Name == "Arrow")
                {
                    transforms[mesh.ParentBone.Index].Decompose(out scale, out rota, out trans);
                    arrowScale = scale.X;
                }
                if (mesh.Name == "ThrowingAxe")
                {
                    transforms[mesh.ParentBone.Index].Decompose(out scale, out rota, out trans);
                    throwingAxeScale = scale.X;

                }

            }
            camera = new Camera();
            camera.LookAtOffset = new Vector3(0.0f, 100.0f, 0.0f);
            camera.DesiredPositionOffset = new Vector3(0.0f, 100.0f, -300.0f);
            camera.NearPlaneDistance = 1.0f;
            camera.FarPlaneDistance = 10000.0f;
            camera.CameraFrustum = new BoundingFrustum(Matrix.Identity);
            camera.FakeFarPlaneDistance = 1000.0f;
            camera.FakeNearPlaneDistance = 1.0f;
            camera.State = 1;


            //camera.LookAtOffset = new Vector3(0.0f, 100.0f, 100.0f);
            //camera.DesiredPositionOffset = new Vector3(0.0f, 100.0f, -100.0f);
         

            // Tell each of the screens to load their content.
            foreach (GameScreen screen in screens)
            {
                screen.LoadContent();
            }

           // SkinnedEffect skinnedEffect = new SkinnedEffect(GraphicsDevice);
           // RemapModel(minotaur, skinnedEffect);
        }
        public void loadSpheresJuneModel(JuneXnaModel juney)
        {
            Vector3 scale, trans;
            Quaternion rota;
            Matrix[] transforms = new Matrix[spearSphere.Bones.Count];
            spearSphere.CopyAbsoluteBoneTransformsTo(transforms);
            transforms[0].Decompose(out scale, out rota, out trans);

            foreach (ModelMesh mesh in spearSphere.Meshes)
            {
                //chestS torsoS hipS lShoulderS rShoulderS lUArmS rUArmS lFArmS rFArmS
                //rHandS lULegS lLLegS rLLegS rULegS headS1
                if (mesh.Name == "forwardSpell")
                    juney.spellSpheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "rSpearS1" || mesh.Name == "rSpearS2" || mesh.Name == "rSpearS3")
                    juney.rSpear.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "lSpearS1" || mesh.Name == "lSpearS2" || mesh.Name == "lSpearS3")
                    juney.lSpear.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "shieldS1" || mesh.Name == "shieldS2" || mesh.Name == "shieldS3" || mesh.Name == "shieldS4" || mesh.Name == "shieldS5")
                    juney.roundShield.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "axeS1" || mesh.Name == "axeS2" || mesh.Name == "axeS3")
                    juney.axe.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "rSwordS1" || mesh.Name == "rSwordS2" || mesh.Name == "rSwordS3")
                    juney.rSword.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));

                if (mesh.Name == "lSwordS1" || mesh.Name == "lSwordS2" || mesh.Name == "lSwordS3")
                    juney.lSword.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "lTAxeS1")
                    juney.lTAxe.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));

                if (mesh.Name == "rTAxeS1")
                    juney.rTAxe.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "bowS1" || mesh.Name == "bowS2" || mesh.Name == "bowS3" || mesh.Name == "bowS4" || mesh.Name == "bowS5" || mesh.Name == "bowS6")
                    juney.bow.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "arrowS1" || mesh.Name == "arrowS2" || mesh.Name == "arrowS3")
                    juney.arrow.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                else
                    juney.spheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));


            }





        }
        public void loadSpheresJuneModel(JuneAIModel juney)
        {
            Vector3 scale, trans;
            Quaternion rota;
           Matrix[] transforms = new Matrix[spearSphere.Bones.Count];
            spearSphere.CopyAbsoluteBoneTransformsTo(transforms);
            transforms[0].Decompose(out scale, out rota, out trans);

            foreach (ModelMesh mesh in spearSphere.Meshes)
            {
                //chestS torsoS hipS lShoulderS rShoulderS lUArmS rUArmS lFArmS rFArmS
                //rHandS lULegS lLLegS rLLegS rULegS headS1
                if (mesh.Name == "forwardSpell")
                    juney.spellSpheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "rSpearS1" || mesh.Name == "rSpearS2" || mesh.Name == "rSpearS3")
                    juney.rSpear.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "lSpearS1" || mesh.Name == "lSpearS2" || mesh.Name == "lSpearS3")
                    juney.lSpear.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "shieldS1" || mesh.Name == "shieldS2" || mesh.Name == "shieldS3" || mesh.Name == "shieldS4" || mesh.Name == "shieldS5")
                    juney.roundShield.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "axeS1" || mesh.Name == "axeS2" || mesh.Name == "axeS3")
                    juney.axe.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "rSwordS1" || mesh.Name == "rSwordS2" || mesh.Name == "rSwordS3")
                    juney.rSword.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));

                if (mesh.Name == "lSwordS1" || mesh.Name == "lSwordS2" || mesh.Name == "lSwordS3")
                    juney.lSword.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "lTAxeS1")
                    juney.lTAxe.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));

                if (mesh.Name == "rTAxeS1")
                    juney.rTAxe.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "bowS1" || mesh.Name == "bowS2" || mesh.Name == "bowS3" || mesh.Name == "bowS4" || mesh.Name == "bowS5" || mesh.Name == "bowS6")
                    juney.bow.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "arrowS1" || mesh.Name == "arrowS2" || mesh.Name == "arrowS3")
                    juney.arrow.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                else
                    juney.spheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));


            }





        }

        public Matrix CreateLightViewProjectionMatrix()
        {
            // Matrix with that will rotate in points the direction of the light
            Matrix lightRotation = Matrix.CreateLookAt(Vector3.Zero,
                                                       -lightDir,
                                                       Vector3.Up);

            // Get the corners of the frustum
            Vector3[] frustumCorners = camera.FakeCameraFrustum.GetCorners();

            // Transform the positions of the corners into the direction of the light
            for (int i = 0; i < frustumCorners.Length; i++)
            {
                frustumCorners[i] = Vector3.Transform(frustumCorners[i], lightRotation);
            }

            // Find the smallest box around the points
            BoundingBox lightBox = BoundingBox.CreateFromPoints(frustumCorners);

            Vector3 boxSize = lightBox.Max - lightBox.Min;
            Vector3 halfBoxSize = boxSize * 0.5f;

            // The position of the light should be in the center of the back
            // pannel of the box. 
            Vector3 lightPosition = lightBox.Min + halfBoxSize;
            lightPosition.Z = lightBox.Min.Z;

            // We need the position back in world coordinates so we transform 
            // the light position by the inverse of the lights rotation
            lightPosition = Vector3.Transform(lightPosition,
                                              Matrix.Invert(lightRotation));

            // Create the view matrix for the light
            Matrix lightView = Matrix.CreateLookAt(lightPosition,
                                                   lightPosition - lightDir,
                                                   Vector3.Up);

            // Create the projection matrix for the light
            // The projection is orthographic since we are using a directional light
            Matrix lightProjection = Matrix.CreateOrthographic(boxSize.X, boxSize.Y,
                                                               -boxSize.Z, boxSize.Z);

            return lightView * lightProjection;
        }

        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (GameScreen screen in screens)
            {
                screen.UnloadContent();
            }
        }


        #endregion

        #region Update and Draw


        public static void HandleInput()
        {
            

        }
        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        public override void Update(GameTime gameTime)
        {



            primitiveBatch.Update(camera);
            // Read the keyboard and gamepad.
          //  input.Update();
            inputManager.Update(gameTime);


            camera.State = 0;
            camera.ChaseDirection = Vector3.Forward;
            camera.ChasePosition = Hercules.World.Translation;



            camera.LookAtOffset = new Vector3(0.0f, 50.0f, 200.0f);
            camera.DesiredPositionOffset = new Vector3(0.0f, 1000.0f, -250.0f);


            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            screensToUpdate.Clear();

            foreach (GameScreen screen in screens)
                screensToUpdate.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(input);

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            // Print debug trace?
            if (traceEnabled)
                TraceScreens();
            camera.Reset();

        }


        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));

        }


        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }


        #endregion



        #region Public Methods


        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.LoadContent();
            }

            screens.Add(screen);

#if WINDOWS_PHONE
            // update the TouchPanel to respond to gestures this screen is interested in
            TouchPanel.EnabledGestures = screen.EnabledGestures;
#endif
        }


        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToUpdate.Remove(screen);
#if WINDOWS_PHONE

            // if there is a screen still in the manager, update TouchPanel
            // to respond to gestures that screen is interested in.
            if (screens.Count > 0)
            {
                TouchPanel.EnabledGestures = screens[screens.Count - 1].EnabledGestures;
            }
#endif
        }


        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }


        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBufferToBlack(int alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(blankTexture,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             new Color(0, 0, 0, (byte)alpha));

            spriteBatch.End();
        }

        /// <summary>
        /// Informs the screen manager to serialize its state to disk.
        /// </summary>
        public void SerializeState()
        {
            // open up isolated storage
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // if our screen manager directory already exists, delete the contents
                if (storage.DirectoryExists("ScreenManager"))
                {
                    DeleteState(storage);
                }

                // otherwise just create the directory
                else
                {
                    storage.CreateDirectory("ScreenManager");
                }

                // create a file we'll use to store the list of screens in the stack
                using (IsolatedStorageFileStream stream = storage.CreateFile("ScreenManager\\ScreenList.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        // write out the full name of all the types in our stack so we can
                        // recreate them if needed.
                        foreach (GameScreen screen in screens)
                        {
                            if (screen.IsSerializable)
                            {
                                writer.Write(screen.GetType().AssemblyQualifiedName);
                            }
                        }
                    }
                }

                // now we create a new file stream for each screen so it can save its state
                // if it needs to. we name each file "ScreenX.dat" where X is the index of
                // the screen in the stack, to ensure the files are uniquely named
                int screenIndex = 0;
                foreach (GameScreen screen in screens)
                {
                    if (screen.IsSerializable)
                    {
                        string fileName = string.Format("ScreenManager\\Screen{0}.dat", screenIndex);

                        // open up the stream and let the screen serialize whatever state it wants
                        using (IsolatedStorageFileStream stream = storage.CreateFile(fileName))
                        {
                            screen.Serialize(stream);
                        }

                        screenIndex++;
                    }
                }
            }
        }

        public bool DeserializeState()
        {
            // open up isolated storage
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // see if our saved state directory exists
                if (storage.DirectoryExists("ScreenManager"))
                {
                    try
                    {
                        // see if we have a screen list
                        if (storage.FileExists("ScreenManager\\ScreenList.dat"))
                        {
                            // load the list of screen types
                            using (IsolatedStorageFileStream stream =
                                storage.OpenFile("ScreenManager\\ScreenList.dat", FileMode.Open,
                                FileAccess.Read))
                            {
                                using (BinaryReader reader = new BinaryReader(stream))
                                {
                                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                                    {
                                        // read a line from our file
                                        string line = reader.ReadString();

                                        // if it isn't blank, we can create a screen from it
                                        if (!string.IsNullOrEmpty(line))
                                        {
                                            Type screenType = Type.GetType(line);
                                            GameScreen screen = Activator.CreateInstance(screenType) as GameScreen;
                                            AddScreen(screen, PlayerIndex.One);
                                        }
                                    }
                                }
                            }
                        }

                        // next we give each screen a chance to deserialize from the disk
                        for (int i = 0; i < screens.Count; i++)
                        {
                            string filename = string.Format("ScreenManager\\Screen{0}.dat", i);
                            using (IsolatedStorageFileStream stream = storage.OpenFile(filename,
                                FileMode.Open, FileAccess.Read))
                            {
                                screens[i].Deserialize(stream);
                            }
                        }

                        return true;
                    }
                    catch (Exception)
                    {
                        // if an exception was thrown while reading, odds are we cannot recover
                        // from the saved state, so we will delete it so the game can correctly
                        // launch.
                        DeleteState(storage);
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Deletes the saved state files from isolated storage.
        /// </summary>
        private void DeleteState(IsolatedStorageFile storage)
        {
            // get all of the files in the directory and delete them
            string[] files = storage.GetFileNames("ScreenManager\\*");
            foreach (string file in files)
            {
                storage.DeleteFile(Path.Combine("ScreenManager", file));
            }
        }


        #endregion
    }
}
