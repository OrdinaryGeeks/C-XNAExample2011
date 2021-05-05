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
using Microsoft.Xna.Framework.Audio;
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
        public static float lookAtY = 0;
        public static float desiredY = 0;
        public int totalDummies = 0;
        public static Factory factory1;
        public static List<Item> items;
        public static List<JuneXnaModel> deadRunners;
        public static List<JuneXnaModel> deadMonsters;
        public static List<JuneXnaModel> deadFighters;
        public static bool firstRun = true;
        public static List<List<int>> collisionGroups;
        public List<MoveMouse> theMouseMoves;
        public static List<int> targetedJune = new List<int>();
        public static List<int> targetedTowers = new List<int>();
        public static List<JuneXnaModel> ghosts = new List<JuneXnaModel>();
        public static BoundingSphere mindGate; //300, 0, 300
        public static BoundingSphere soulGate; //600, 0, 300
        public static BoundingSphere spiritGate; //450, 0, 450
        public static BoundingSphere bodyGate; // 300, 0, 600
        public static BoundingSphere limbo; // 450, 0, 750
       // public static BoundingSphere godGate; //600,0,600
        public static List<BoundingSphere> gates = new List<BoundingSphere>();
        public static bool stage1 = false;
        //public static List<Vector3> towerVecs;
        //public static int AresTimer = 360;
        public static BoundingBox[][] mainSearch;
        public static bool[][] mainOpen;
        public static List<Resource> resources;
        public static Resource fire1;
        public static Resource earth1;
        public static Resource water1;
        public static Resource wind1;
        public static List<string> runes;
        public static string rune1;
        public static string rune2;
        public static string rune3;
        public static string rune4;
        public static string rune5;
        public static string rune6;
        public static string rune7;
        public static string rune8;
        public static bool paused = false;
        public static int creationTime;
        public static JuneXnaModel fighter;
        public static JuneXnaModel rogue;
        public static JuneXnaModel runner;
        public static JuneXnaModel god;

        public static List<JuneXnaModel> archers;
        public static List<JuneXnaModel> dummies;

        public static List<JuneXnaModel> runners;

        public static List<JuneXnaModel> fighters;

        public static Tower RinnaAl;
        public static Tower ArraOn;
        public static Tower CarraNo;
        public static Tower Tierra;
        public static JuneXnaModel Wizard;
        public static GraphicsDevice gd;
        public static GraphicsAdapter ga;
        public static Texture2D orbGui;
        public static BoundingBox [][] bbs = new BoundingBox[5][];
        public static MouseState oldMouse;
        public static MouseState mouseState;
        public static Texture2D cursor;
        public static List<JuneXnaModel> allySwords;
        public static List<Pylon> pylons;
        public static Model FinalBuild2;
        public static Model grid;
        public static Texture2D collision;
        public static Color[] collisionColors;
        public static JuneXnaModel Jailer;
        public static Texture2D rock;
        public static int stage = 1; 
        public static JuneXnaModel fakeStatue;
        public static Model arena;
        public static JuneXnaModel LSRunner;
        public static temple cupidTemple;
        public static bool buildStage = true;
        #region Fields
        /// <summary>
        /// 0 is board, 1 is wall, 2 is tower
        /// </summary>
        public static SoundEffect song;
        public static SoundEffectInstance songInstance;
        public static string [][] lvl1 = new string[10][];
        public static int[][] boardPanel = new int[10][];
        public static bool [][] pathBoard = new bool[24][];
        public static List<Point> projectedPts = new List<Point>();
        public static List<Vector3> enemyVecs = new List<Vector3>();
        public static Random rand = new Random();
        //Captain Asterion the mino
        //Lt Bender the Rock
        //Capt Paris 

        public static JuneXnaModel Cupid; 
        public static Model finalBuild;
        public static SpearMan oTSpear;
        public static SwordAndShield oTSword;


        public static JuneXnaModel SmartMovement1;
        public static JuneXnaModel SmartMovement2;
        public static JuneXnaModel SmartMovement3;

        public static JuneXnaModel runner1;
        public static JuneXnaModel Alecto;


        public static JuneXnaModel etLvl1Sword;
        public static JuneXnaModel etLvl1Player;

        

        public static List<JuneXnaModel> enemyRunners;
        public static List<JuneXnaModel> playerTowers;

        public static TimeSpan timer;

        public static Engineer eng1;
        public static SpearMan es1;
        public static SpearMan es2;
        public static SpearMan es3; 
        public static SpearMan ps1;
        public static SwordAndShield ess1;
        public static Archer a1;
        public static Thor thor1;

        public static Viewport vp1;
        public static Viewport vp2;


        public static List<SpearMan> enemySpears;
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
        
        public Texture2D lilMan;
        public Texture2D whiteG;
        public Texture2D blackG;
        public Texture2D yellowG;
        public Texture2D redG;
        public Texture2D whiteSq;
        public Texture2D lightning1, lightning2, lightning3;
        public Texture2D A, B, X, Y, LT, RT, LS, RS;
        public Texture2D road;
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
        #region human boneindices
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
        public Texture2D shieldTex;
        public Texture2D elvenTex;
        public Texture2D frostTex;
        public Texture2D deadTex;
        public static Model vegFloor;
        public static Model FinalProjectiles;
        public static Model newBoard;

        public Texture2D hexGrid;
        public Texture2D gradient;
        public Texture2D fire;
        public Texture2D earth;
        public Texture2D water;
        public Texture2D wind;

        public Texture2D slashTga;
        public Texture2D smoke;
        public Texture2D gold1;
        public Texture2D satin;
        public Texture2D minoTex;
        public Texture2D humanTex;
        public Texture2D black;
        public Texture2D chain;
        Texture2D heightMap;
        Texture2D colMap;
        public Texture2D grass;
        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> screensToUpdate = new List<GameScreen>();
        InputState input = new InputState();
        SpriteBatch spriteBatch;
        SpriteFont font;
        SpriteFont menuFont;
        SpriteFont gameFont;
        public Texture2D blankTexture;
        public static Game aGame;
        protected bool isInitialized;
        bool traceEnabled;
        public static PrimitiveBatch primitiveBatch;
        public static Camera camera;
        public static Camera camera2;

        public static MoveListMouse mouseMoveList;
        public static InputManagerMouse mouseInputManager;
        public static MoveList moveList;
        public List<Move> theMoves;

        public static InputManager inputManager;
        // Stores each players' most recent move and when they pressed it.
        Move[] playerMoves;
        TimeSpan[] playerMoveTimes;
        public static float arrowScale;
        public static float throwingAxeScale;
        public static Model grassSquare;
        public static Model grassModel;
        public static Model build;
        public static Model square;
        public static Model tdMap;
        public static Model minotaur;
        public static Model gorgon;
        public static Model angel;
        public static Model aosBoard;
        public static Model spearMan;
        public static Model juneModel;
        public Model humanProjectiles;
        public  Model humanFormation;
        public Model HumanFormation{

            set
            {
                humanFormation = value;
            }
            get
            {
                return humanFormation;
            }
    }
        public static Model minoSphere;
        public static Model spearSphere;
        public Model minoBoard1;
        public Model board;
        public Model flag;
        public static Model tower;
        public Model dargahe;
        public static Gorgon medusa;
        public static Minotaur asterion;
        public static Angel michael;
        public static List<MinotaurAI> minotaurs;
        public static List<GorgonAI> gorgons;
        public static int playerCharSelected = 1;
        public Texture2D white;
        public Texture2D aosCollision;
        public Texture2D rage;
        public static Gorgon p1Gorgon;
        public static Minotaur p1Mino;
        public static Angel p1Angel;
        public static JuneXnaModel p1June;

        
        public static JuneXnaModel Theseus;
        public static JuneXnaModel Hercules;
        public static JuneXnaModel Perseus;
        public static JuneXnaModel HercTVH;
        public static JuneXnaModel ThorTVH;
        public static JuneXnaModel TheseusTS;
        public static JuneXnaModel AchillesRage1;
        public static JuneXnaModel AchillesRage2;
        public static JuneXnaModel AchillesRage3;
        public static JuneXnaModel AchillesRage4;
        public static JuneXnaModel UndeadLeader;
        public static JuneXnaModel UndeadFollower1;
        public static JuneXnaModel UndeadFollower2;
        public static JuneXnaModel  UndeadFollower3;
        public static Minotaur MinoTS;
        public static Minotaur MinoAi1TS;
        public static Minotaur MinoAi2TS;

        public static Minotaur LSMino;
        public static JuneXnaModel Thor;

        public static JuneXnaModel ParisTS;
        public static JuneXnaModel SpearManTS;
        public static JuneXnaModel SwordsManTS;

        public static Gorgon GorgonTS;


        public static JuneXnaModel Wisp;
        public static JuneXnaModel Loki;

        //0
        public static JuneXnaModel Dargahe;

        public static JuneXnaModel Michael;

        //1 Cpt

        public static JuneXnaModel CaptThor;

        public static JuneXnaModel CaptWisp;

        //2 Lt

        public static JuneXnaModel LtHercules;

        public static JuneXnaModel LtOgthul; //blue hercules frost giant

        //3 LT

        public static JuneXnaModel LtParis;
        public static JuneXnaModel LtLoki;

        public static JuneXnaModel jSp1;



        



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
            ga = GraphicsAdapter.DefaultAdapter;
            
        }


        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            gd = GraphicsDevice;

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

        public static JuneXnaModel lastDummy()
        {

            return dummies[dummies.Count - 1];
        }

        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            factory1 = new Factory(new Vector3(900, 0, 900));
            items = new List<Item>();
            deadFighters = new List<JuneXnaModel>();
            deadMonsters = new List<JuneXnaModel>();
            deadRunners = new List<JuneXnaModel>();
            collisionGroups = new List<List<int>>();
            mindGate = new BoundingSphere(new Vector3(300, 0, 300), 40);
            soulGate = new BoundingSphere(new Vector3(600, 0, 300), 40);
            spiritGate = new BoundingSphere(new Vector3(300, 0, 600), 40);
            bodyGate = new BoundingSphere(new Vector3(450, 0, 750), 40);
            limbo = new BoundingSphere(new Vector3(600, 0, 600), 40);
           // godGate = new BoundingSphere(new Vector3(600, 0, 600), 40);
            gates.Add(mindGate);
            gates.Add(soulGate);
            gates.Add(spiritGate);
            gates.Add(bodyGate);
            gates.Add(limbo);
          //  gates.Add(godGate);
            

            fire1 = new Resource(200, 1, 100, 1, new Vector3(500, 0.0f, 500.0f));
            earth1 = new Resource(200, 1, 100, 0, new Vector3(700, 0.0f, 500.0f));
            water1 = new Resource(200, 1, 100, 3, new Vector3(900, 0.0f, 500.0f));
            wind1 = new Resource(200, 1, 100, 2, new Vector3(900, 0.0f, 600.0f));
            resources = new List<Resource>();
            resources.Add(fire1);
            resources.Add(earth1);
            resources.Add(water1);
            resources.Add(wind1);
            runes = new List<string>();
            for (int i = 0; i < 8; i++)
                runes.Add("Fire 1");
            rune1 = "Fire";
            rune2 = "Fire";
            rune3 = "Fire";
            rune4 = "Fire";
            rune5 = "Fire";
            rune6 = "Fire";
            rune7 = "Fire";
            rune8 = "Fire";
            archers = new List<JuneXnaModel>();
            dummies = new List<JuneXnaModel>();
            fighters = new List<JuneXnaModel>();
            runners = new List<JuneXnaModel>();

            mainSearch =new BoundingBox[1200/60][];
            mainOpen = new bool[1200 / 60][];
            for(int i = 0; i<1200/60; i++)
            {
                mainSearch[i] = new BoundingBox[1200/60];
                mainOpen[i] = new bool[1200/60];
                for(int j =0; j<1200/60; j++)
                {
                    mainSearch[i][j] = new BoundingBox(new Vector3(i * 60, 0, j * 60), new Vector3((i + 1) * 60, 10, (j + 1) * 60));
                    mainOpen[i][j] = true;
                }
            }
            int height2 = 0; 
            int width2 = 0; 

          //  for (int i = 0; i < collisionColors.Length; i++)
            {
            //    width2 = i % 128;
           //     height2 = i / 128;
               // if(collisionColors[i].R < 15)



            }
            
            for(int i = 0; i<5; i++)
            {bbs[i]= new BoundingBox[5];
                for(int j = 0; j<5; j++)
                    bbs[i][j] = new BoundingBox(new Vector3(i*100, 0, j*100), new Vector3((i+1)*100, 1, (j+1)*100));
                }
            mouseState = new MouseState();
            oldMouse = new MouseState();
            // Load content belonging to the screen manager.
            ContentManager content = Game.Content;

            pylons = new List<Pylon>();

            pylons.Add(new Pylon(new Vector3(500.0f, 0.0f, 500.0f)));
            pylons.Add(new Pylon(new Vector3(3000.0f, 0.0f, 500.0f)));
            vp1 = new Viewport(0, 0, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height);
            vp2 = new Viewport(GraphicsDevice.Viewport.Width / 2, 0, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height);
            //song = content.Load<SoundEffect>("Background");
           // songInstance = song.CreateInstance();
           // songInstance.IsLooped = true;

            lilMan = content.Load<Texture2D>("lilMan");
                
            enemyRunners = new List<JuneXnaModel>();
            playerTowers = new List<JuneXnaModel>();
            timer = new TimeSpan();
            

            for (int i = 0; i < 24; i++)
            {
                pathBoard[i] = new bool[24];
               // boardPanel[i] = new int[6];
                for (int j = 0; j < 24; j++)
                {
                    pathBoard[i][j] = true;
                   // boardPanel[i][j] = 0;
                }

            }


            cupidTemple = new temple(new Vector3(0.0f, 0.0f, 300.0f), Vector3.Backward);


            lvl1[0]= new string[6];
            lvl1[0][1] = "M";
            lvl1[0][2] = "R";
            lvl1[0][3] = "P";
            lvl1[0][4] = "O";
            lvl1[0][5] = "P";
            lvl1[0][0] = "P";
            orbGui = content.Load<Texture2D>("orbGui");
            cursor = content.Load<Texture2D>("cursor");
            rock = content.Load<Texture2D>("rock");
            arena = content.Load<Model>("arena");
            FinalBuild2 = content.Load<Model>("FinalBuild2");
            finalBuild = content.Load<Model>("FinalBuild");
            vegFloor = content.Load<Model>("grassFloor");
            newBoard = content.Load<Model>("newBoard");
            grassSquare = content.Load<Model>("grassSquare");
            grass = content.Load<Texture2D>("grass");
            grassModel = content.Load<Model>("grassSquares");
            grid = content.Load<Model>("128Grid");

            build = content.Load<Model>("build");
            square = content.Load<Model>("square");
            collision = content.Load<Texture2D>("collision");
            int width = collision.Width;
            int height = collision.Height;
            collisionColors = new Color[width * height];
            collision.GetData(collisionColors);
            shieldTex = content.Load<Texture2D>("shield");
            whiteG = content.Load<Texture2D>("whiteTex");
            redG = content.Load<Texture2D>("redG");
            blackG = content.Load<Texture2D>("blackG");
            yellowG = content.Load<Texture2D>("yellowG");
            whiteSq = content.Load<Texture2D>("whitesq");
            black = content.Load<Texture2D>("black");
            chain = content.Load<Texture2D>("chain");
            lightning1 = content.Load<Texture2D>("lightning1");
            lightning2 = content.Load<Texture2D>("lightning3");
            lightning3 = content.Load<Texture2D>("lightning4");
            currentSelection = content.Load<Texture2D>("currentSelection");
            gradient = content.Load<Texture2D>("gradient");
            tdMap = content.Load<Model>("tdMap");
            minoBoard1 = content.Load<Model>("minoBoard1");
            dargahe = content.Load<Model>("Dargahe");
            deadTex = content.Load<Texture2D>("deadTexture");
            frostTex = content.Load<Texture2D>("iceTexture");
            elvenTex = content.Load<Texture2D>("elvenTexture");

            rage = content.Load<Texture2D>("Rage");
            A = content.Load<Texture2D>("xboxControllerButtonA");
            B = content.Load<Texture2D>("xboxControllerButtonB");
            X = content.Load<Texture2D>("xboxControllerButtonX");
            Y = content.Load<Texture2D>("xboxControllerButtonY");
           LT = content.Load<Texture2D>("xboxControllerLeftTrigger");
           LS = content.Load<Texture2D>("xboxControllerLeftShoulder");
           RT = content.Load<Texture2D>("xboxControllerRightTrigger");
           RS = content.Load<Texture2D>("xboxControllerRightShoulder");
            road = content.Load<Texture2D>("roads");
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

            white = content.Load<Texture2D>("white");



            hexGrid = content.Load<Texture2D>("hexGrid");
            fire = content.Load<Texture2D>("fire");
            rock = content.Load<Texture2D>("rock");
            wind = content.Load<Texture2D>("wind");
            water = content.Load<Texture2D>("ice");
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

            theMouseMoves = new List<MoveMouse>();
            theMouseMoves.Add(new MoveMouse("LC", MiceInput.LeftClick) { IsSubMove = true });
            theMouseMoves.Add(new MoveMouse("RC", MiceInput.RightClick) { IsSubMove = true});
            theMouseMoves.Add(new MoveMouse("LC,LC", MiceInput.LeftClick, MiceInput.LeftClick) { IsSubMove = true });

            mouseMoveList = new MoveListMouse(theMouseMoves);
            mouseInputManager = new InputManagerMouse((PlayerIndex)0, mouseMoveList.LongestMoveLength);

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
        //    theMoves.Add(new Move("LT+RT", Buttons.LeftTrigger | Buttons.RightTrigger));

            moveList = new MoveList(theMoves);

            inputManager = new InputManager((PlayerIndex)0, moveList.LongestMoveLength);



            primitiveBatch = new PrimitiveBatch(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("Font");
            gameFont = content.Load<SpriteFont>("gamefont");
            menuFont = content.Load<SpriteFont>("menufont");
            blankTexture = content.Load<Texture2D>("blank");



            //FinalProjectiles = content.Load<Model>("augProjectiles");
            FinalProjectiles = content.Load<Model>("AugProj2");
            flag = content.Load<Model>("flag");
            tower = content.Load<Model>("tower");
            minotaur = content.Load<Model>("JuneXnaMino");
            gorgon = content.Load<Model>("DusaXnaModel");
            angel = content.Load<Model>("AngelXnaModel");
            board = content.Load<Model>("JBoard");
            minoSphere = content.Load<Model>("MinoXnaSpheres");
            spearMan = content.Load<Model>("PerseusXnaModel");
            humanFormation = content.Load<Model>("formationSpheres");
            spearSphere = content.Load<Model>("perseusSpheres");

            //needs a dif name finalbuild means buildings
            //juneModel = content.Load<Model>("males4");
            juneModel = content.Load<Model>("DecXnaModel");
            humanProjectiles = content.Load<Model>("humanProjectiles");

            Theseus = new JuneXnaModel(new Vector3(750.0f, 0.0f, 750.0f), Vector3.Forward);
            Theseus.SkinningData = juneModel.Tag as SkinningData;
            Theseus.setAnimationPlayers2();
            Theseus.fighterIndex = 99;
            loadSpheresJuneModel(Theseus);
            for (int i = 0; i < 1; i++)
            {
                fighters.Add(new JuneXnaModel(new Vector3(2000.0f, 0.0f, 2000.0f), Vector3.Forward));
                fighters[i].SkinningData = juneModel.Tag as SkinningData;
                fighters[i].setAnimationPlayers2();
                loadSpheresJuneModel(fighters[i]);
                fighters[i].creationTime = i * 5;
                fighters[i].fighterIndex = i;
                fighters[i].runnerIndex = i;
            }

            for (int i = 0; i < 5; i++)
            {

                const float radius = 500;
                double angle = (double)i / 10 * Math.PI * 2;
                float x = (float)Math.Cos(angle);
                float y = (float)Math.Sin(angle);
                runners.Add(new JuneXnaModel(new Vector3(600.0f + x * radius, 0, 600.0f + y * radius), Vector3.Forward));
                runners[i].SkinningData = juneModel.Tag as SkinningData;
                runners[i].setAnimationPlayers2();
                loadSpheresJuneModel(runners[i]);
                runners[i].fighterIndex = i + 100;
                runners[i].isRunner = true;
                runners[i].runnerIndex = i;
                

            }
            for (int i = 0; i < -1; i++)
            {
                const float radius = 500;
                double angle = (double)i / 3 * Math.PI * 2;

                float x = (float)Math.Cos(angle);
                float y = (float)Math.Sin(angle);
                archers.Add(new JuneXnaModel(new Vector3(750.0f + x * radius, 0, 750.0f + y * radius), Vector3.Forward));
                archers[i].SkinningData = juneModel.Tag as SkinningData;
                archers[i].setAnimationPlayers2();
                loadSpheresJuneModel(archers[i]);
                archers[i].fighterIndex = i + 200;
                archers[i].isMonster = true;
            }
            double dummyCount = 5;
            for (int i = 0; i < dummyCount; i ++)
            {
                const float radius = 300;
                double angle = (double)i / dummyCount * Math.PI * 2;
                float x = (float)Math.Cos(angle);
                float y = (float)Math.Sin(angle);
                dummies.Add(new JuneXnaModel(new Vector3(750.0f + x * radius, 0, 750.0f + y * radius), Vector3.Forward));
                dummies[i].SkinningData = juneModel.Tag as SkinningData;
                dummies[i].setAnimationPlayers2();
                loadSpheresJuneModel(dummies[i]);
                dummies[i].fighterIndex = totalDummies++;
                dummies[i].isMonster = true;
            }
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

            transforms = new Matrix[tower.Bones.Count];
            tower.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in tower.Meshes)
            {
                if (mesh.Name == "tower")
                    RinnaAl = new Tower(transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(1000.0f, 0.0f, 1000.0f));


            }
           // RinnaAl = new Tower();


            fighter = new JuneXnaModel(new Vector3(2000.0f, 0.0f, 2000.0f), Vector3.Forward);
            fighter.SkinningData = juneModel.Tag as SkinningData;
            fighter.setAnimationPlayers();
            loadSpheresJuneModel(fighter);
            rogue = new JuneXnaModel(new Vector3(0.0f, 0.0f, 2000.0f), Vector3.Forward);
            rogue.SkinningData = juneModel.Tag as SkinningData;
            rogue.setAnimationPlayers();
            loadSpheresJuneModel(rogue);
            god = new JuneXnaModel(new Vector3(500.0f, 0.0f, 0.0f), Vector3.Forward);
            god.SkinningData = juneModel.Tag as SkinningData;
            god.setAnimationPlayers();
            loadSpheresJuneModel(god);
         //rogue;
        // runner;
        //god;
            Wizard = new JuneXnaModel(new Vector3(550.0f, 0.0f, 500.0f), Vector3.Forward);
            Wizard.SkinningData = juneModel.Tag as SkinningData;
            Wizard.setAnimationPlayers();
            loadSpheresJuneModel(Wizard);




           // SmartMovement1 = new JuneXnaModel(new Vector3(550.0f, 0.0f, 30.0f), Vector3.Forward);
           // SmartMovement1.SkinningData = juneModel.Tag as SkinningData;
           // SmartMovement1.setAnimationPlayers();


           // SmartMovement2 = new JuneXnaModel(new Vector3(570.0f, 0.0f, 30.0f), Vector3.Forward);
           // SmartMovement2.SkinningData = juneModel.Tag as SkinningData;
           // SmartMovement2.setAnimationPlayers();
           // SmartMovement3 = new JuneXnaModel(new Vector3(590.0f, 0.0f, 30.0f), Vector3.Forward);
           // SmartMovement3.SkinningData = juneModel.Tag as SkinningData;
           // SmartMovement3.setAnimationPlayers();

           // loadSpheresJuneModel(SmartMovement1);
           // loadSpheresJuneModel(SmartMovement2);
           // loadSpheresJuneModel(SmartMovement3);

           // p1June = new JuneXnaModel(new Vector3(1410.0f, 0.0f, 6100.7f), Vector3.Backward);
           // p1June.SkinningData = juneModel.Tag as SkinningData;
           // p1June.setAnimationPlayers();



           // transforms = new Matrix[spearSphere.Bones.Count];
           // spearSphere.CopyAbsoluteBoneTransformsTo(transforms);
           // transforms[0].Decompose(out scale, out rota, out trans);


           // Hercules = new JuneXnaModel(new Vector3(100.0f, 0.0f, 0.0f), Vector3.Forward);
           // Hercules.SkinningData = juneModel.Tag as SkinningData;
           // Hercules.setAnimationPlayers();

           // Perseus = new JuneXnaModel(new Vector3(300.0f, 0.0f, 0.0f), Vector3.Forward);
           // Perseus.SkinningData = juneModel.Tag as SkinningData;
           // Perseus.setAnimationPlayers();



           // Theseus.runes.Add(new Rune("split", 1));
           // Theseus.runes.Add(new Rune("power", 1));
           // Theseus.runes.Add(new Rune("refresh", 1));


           // Cupid = new JuneXnaModel(new Vector3(500.0f, 0.0f, 0.0f), Vector3.Forward);
           // Cupid.SkinningData = juneModel.Tag as SkinningData;
           // Cupid.setAnimationPlayers();

           // HercTVH = new JuneXnaModel(new Vector3(100.0f, 0.0f, 0.0f), Vector3.Forward);
           // HercTVH.SkinningData = juneModel.Tag as SkinningData;
           // HercTVH.setAnimationPlayers();
           // loadSpheresJuneModel(HercTVH);

           // ThorTVH =  new JuneXnaModel(new Vector3(140.0f, 0.0f, 120.0f), Vector3.Forward);
           //             ThorTVH.SkinningData = juneModel.Tag as SkinningData;
           //ThorTVH.setAnimationPlayers();
           //loadSpheresJuneModel(ThorTVH);

           // //13.49 -27.692
           // TheseusTS = new JuneXnaModel(new Vector3(1349.0f, 0.0f, 2770.0f), Vector3.Backward);
           // TheseusTS.SkinningData = juneModel.Tag as SkinningData;
           //TheseusTS.setAnimationPlayers();

           //ParisTS = new JuneXnaModel(new Vector3(1350.0f, 0.0f, 2900.0f), Vector3.Backward);
           //ParisTS.SkinningData = juneModel.Tag as SkinningData;
           //ParisTS.setAnimationPlayers();

           //MinoTS = new Minotaur(new Vector3(1375.0f, 0.0f, 2900.0f), Vector3.Backward);
           //MinoTS.SkinningData = minotaur.Tag as SkinningData;
           //MinoTS.setAnimationPlayers();

           //GorgonTS = new Gorgon(new Vector3(1350.0f, 0.0f, 3100.0f), Vector3.Forward);
           //GorgonTS.SkinningData = gorgon.Tag as SkinningData;
           //GorgonTS.setAnimationPlayers();

           // LSMino = new Minotaur(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Forward);
           // LSMino.SkinningData = minotaur.Tag as SkinningData;
           // LSMino.setAnimationPlayers();


           // LSRunner = new JuneXnaModel(Vector3.Zero, Vector3.Forward);
           // LSRunner.SkinningData = juneModel.Tag as SkinningData;
           // loadSpheresJuneModel(LSRunner);
           // LSRunner.setAnimationPlayers();

           //AchillesRage1 = new JuneXnaModel(new Vector3(100.0f, 0.0f, 200.0f), Vector3.Forward);
           //AchillesRage1.SkinningData = juneModel.Tag as SkinningData;
           //AchillesRage1.setAnimationPlayers();


           //AchillesRage2 = new JuneXnaModel(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Forward);
           //AchillesRage2.SkinningData = juneModel.Tag as SkinningData;
           //AchillesRage2.setAnimationPlayers();
           //AchillesRage3 = new JuneXnaModel(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Forward);
           //AchillesRage3.SkinningData = juneModel.Tag as SkinningData;
           //AchillesRage3.setAnimationPlayers();
           //AchillesRage4 = new JuneXnaModel(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Forward);
           //AchillesRage4.SkinningData = juneModel.Tag as SkinningData;
           //AchillesRage4.setAnimationPlayers();

           //UndeadLeader = new JuneXnaModel(new Vector3(100.0f, 0.0f, 0.0f), Vector3.Forward);
           //UndeadLeader.SkinningData = juneModel.Tag as SkinningData;
           //UndeadLeader.setAnimationPlayers();
           //UndeadFollower1 = new JuneXnaModel(new Vector3(100.0f, 0.0f, 200.0f), Vector3.Forward);
           //UndeadFollower1.SkinningData = juneModel.Tag as SkinningData;
           //UndeadFollower1.setAnimationPlayers();
           // UndeadFollower2 = new JuneXnaModel(new Vector3(100.0f, 0.0f, 300.0f), Vector3.Forward);
           // UndeadFollower2.SkinningData = juneModel.Tag as SkinningData;
           // UndeadFollower2.setAnimationPlayers();
           // UndeadFollower3 = new JuneXnaModel(new Vector3(100.0f, 0.0f, 400.0f), Vector3.Forward);
           // UndeadFollower3.SkinningData = juneModel.Tag as SkinningData;
           // UndeadFollower3.setAnimationPlayers();


           // Michael = new JuneXnaModel(new Vector3(100.0f, 0.0f, 0.0f), Vector3.Forward);
           // Michael.SkinningData = juneModel.Tag as SkinningData;
           // Michael.setAnimationPlayers();


           // Dargahe = new JuneXnaModel(new Vector3(300.0f, 0.0f, 180.0f), Vector3.Forward);
           // Dargahe.SkinningData = dargahe.Tag as SkinningData;
           // Dargahe.setAnimationPlayers();


           // Wisp = new JuneXnaModel(new Vector3(360.0f, 0.0f, 180.0f), Vector3.Forward);
           // Wisp.SkinningData = juneModel.Tag as SkinningData;
           // Wisp.setAnimationPlayers();

           // loadSpheresJuneModel(Wisp);

           // Loki = new JuneXnaModel(new Vector3(270.0f, 0.0f, 180.0f), Vector3.Forward);
           // Loki.SkinningData = juneModel.Tag as SkinningData;
           // Loki.setAnimationPlayers();

            //a1 = new Archer(new Vector3(200.0f, 0.0f, 500.0f), Vector3.Backward);
            //a1.SkinningData = juneModel.Tag as SkinningData;
            //a1.setAnimationPlayers();
            //loadArcherModel(a1);

            //ps1 = new SpearMan(new Vector3(10.0f, 0.0f, 10.0f), Vector3.Backward);
            //ps1.SkinningData = juneModel.Tag as SkinningData;
            //ps1.setAnimationPlayers();

            //eng1 = new Engineer(new Vector3(10.0f, 0.0f, 10.0f), Vector3.Backward);
            //eng1.SkinningData = juneModel.Tag as SkinningData;
            //eng1.setAnimationPlayers();

            //loadSpearmanModel(ps1);

            //thor1 = new Thor(new Vector3(400.0f, 0.0f, 400.0f), Vector3.Forward);
            //thor1.SkinningData = juneModel.Tag as SkinningData;
            //thor1.setAnimationPlayers();

            //jSp1 = new JuneXnaModel(new Vector3(000.0f, 0.0f, 180.0f), Vector3.Forward);
            //jSp1.SkinningData = juneModel.Tag as SkinningData;
            //jSp1.setAnimationPlayers();
            //loadSpheresJuneModel(jSp1);


            //es1 = new SpearMan(new Vector3(400.0f, 0.0f, 180.0f), Vector3.Forward);
            //es1.SkinningData = juneModel.Tag as SkinningData;
            //es1.setAnimationPlayers();
            //es1.charNum = 0;
            //es1.formVec = ps1.CFormation[0];
            //es2 = new SpearMan(new Vector3(440.0f, 0.0f, 180.0f), Vector3.Forward);
            //es2.SkinningData = juneModel.Tag as SkinningData;
            //es2.setAnimationPlayers();
            //es2.charNum = 0;
            ////es2.formVec = ps1.CFormation[1];
            //es3 = new SpearMan(new Vector3(420.0f, 0.0f, 180.0f), Vector3.Forward);
            //es3.SkinningData = juneModel.Tag as SkinningData;
            //es3.setAnimationPlayers();
            //es3.charNum = 0;
            ////es3.formVec = ps1.CFormation[2];
            //loadSpearmanModel(es2);
            //loadSpearmanModel(es3);

            //ess1 = new SwordAndShield(new Vector3(200.0f, 0.0f, 300.0f), Vector3.Forward);
            //ess1.SkinningData = juneModel.Tag as SkinningData;
            //ess1.setAnimationPlayers();
            //ess1.charNum = 1;
            //a1.formVec = ps1.NFormation[3];
            //ess1.formVec = ps1.CFormation[3];

            //a1.brace.CopyTo(a1.previousAnimation, 0);



            //runner1 = new JuneXnaModel(new Vector3(0.0f, 0.0f, 500.0f), Vector3.Forward);
            //runner1.SkinningData = juneModel.Tag as SkinningData;
            //runner1.setAnimationPlayers();

            //Alecto = new JuneXnaModel(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Forward);
            //Alecto.SkinningData = juneModel.Tag as SkinningData;
            //Alecto.setAnimationPlayers();


            //CaptThor = new JuneXnaModel(new Vector3(120.0f, 0.0f, 0.0f), Vector3.Forward);
            //CaptThor.SkinningData = juneModel.Tag as SkinningData;
            //CaptThor.setAnimationPlayers();

            //CaptWisp = new JuneXnaModel(new Vector3(320.0f, 0.0f, 400.0f), Vector3.Forward);
            //CaptWisp.SkinningData = juneModel.Tag as SkinningData;
            //CaptWisp.setAnimationPlayers();

            //LtHercules = new JuneXnaModel(new Vector3(180.0f, 0.0f, 0.0f), Vector3.Forward);
            //LtHercules.SkinningData = juneModel.Tag as SkinningData;
            //LtHercules.setAnimationPlayers();

            //LtOgthul = new JuneXnaModel(new Vector3(340.0f, 0.0f, 400.0f), Vector3.Forward);
            //LtOgthul.SkinningData = juneModel.Tag as SkinningData;
            //LtOgthul.setAnimationPlayers();

            //LtParis = new JuneXnaModel(new Vector3(160.0f, 0.0f, 0.0f), Vector3.Forward);
            //LtParis.SkinningData = juneModel.Tag as SkinningData;
            //LtParis.setAnimationPlayers();

            //LtLoki = new JuneXnaModel(new Vector3(360.0f, 0.0f, 400.0f), Vector3.Forward);
            //LtLoki.SkinningData = juneModel.Tag as SkinningData;
            //LtLoki.setAnimationPlayers();

            //loadSpheresJuneModel(runner1);
            //loadSpheresJuneModel(Alecto);
            //loadThorModel(thor1);
            //loadSpearmanModel(es1);


            //etLvl1Player = new JuneXnaModel(new Vector3(0.0f, 0.0f, 300.0f), Vector3.Left);
            //etLvl1Player.SkinningData = juneModel.Tag as SkinningData;
            //etLvl1Player.setAnimationPlayers();
            //etLvl1Sword = new JuneXnaModel(new Vector3(300.0f, 0.0f, 300.0f), Vector3.Right);
            //etLvl1Sword.SkinningData = juneModel.Tag as SkinningData;
            //etLvl1Sword.setAnimationPlayers();

            //fakeStatue = new JuneXnaModel(Vector3.Zero, Vector3.Forward);
            //fakeStatue.SkinningData = juneModel.Tag as SkinningData;
            //fakeStatue.setAnimationPlayers();


            //Jailer = new JuneXnaModel(new Vector3(1300, 0.0f, 2660), Vector3.Forward);
            //Jailer.SkinningData = juneModel.Tag as SkinningData;
            //Jailer.setAnimationPlayers();
            //loadSpheresJuneModel(Jailer);
            //Jailer.patrol1 = new Vector3(1300, 0.0f, 2660);
            //Jailer.patrol2 = new Vector3(1632, 0.0f, 2318);


            //loadSpheresJuneModel(fakeStatue);
            //loadSpheresJuneModel(etLvl1Player);
            //loadSpheresJuneModel(etLvl1Sword);

            //loadSSmanModel(ess1);
            //loadSpheresJuneModel(Michael);
            //loadSpheresJuneModel(Hercules);
            //loadSpheresJuneModel(Perseus);
            //loadSpheresJuneModel(Theseus);
            //loadSpheresJuneModel(Cupid);
            //loadSpheresJuneModel(Wisp);
            //loadSpheresJuneModel(Loki);
            //loadSpheresJuneModel(CaptThor);
            //loadSpheresJuneModel(LtHercules);
            
            //loadSpheresJuneModel(



      

            

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
            head = Theseus.SkinningData.BoneIndices["Head"];
            neck = Theseus.SkinningData.BoneIndices["Neck"];
            spine1 = Theseus.SkinningData.BoneIndices["Spine1"];
            spine = Theseus.SkinningData.BoneIndices["Spine"];
            rArm = Theseus.SkinningData.BoneIndices["RightArm"];
            rFArm = Theseus.SkinningData.BoneIndices["RightForeArm"];
            rHand = Theseus.SkinningData.BoneIndices["RightHand"];
            lULeg = Theseus.SkinningData.BoneIndices["LeftUpLeg"];
            lLLeg = Theseus.SkinningData.BoneIndices["LeftLeg"];
            lFoot = Theseus.SkinningData.BoneIndices["LeftFoot"];
            lArm = Theseus.SkinningData.BoneIndices["LeftArm"];
            lFArm = Theseus.SkinningData.BoneIndices["LeftForeArm"];
            lHand = Theseus.SkinningData.BoneIndices["LeftHand"];
            hips = Theseus.SkinningData.BoneIndices["Hips"];
            rULeg = Theseus.SkinningData.BoneIndices["RightUpLeg"];
            rLLeg = Theseus.SkinningData.BoneIndices["RightLeg"];
            rFoot = Theseus.SkinningData.BoneIndices["RightFoot"];
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


            camera2 = new Camera();
            camera2.LookAtOffset = new Vector3(0.0f, 100.0f, 0.0f);
            camera2.DesiredPositionOffset = new Vector3(0.0f, 100.0f, -300.0f);
            camera2.NearPlaneDistance = 1.0f;
            camera2.FarPlaneDistance = 10000.0f;
            camera2.CameraFrustum = new BoundingFrustum(Matrix.Identity);
            camera2.FakeFarPlaneDistance = 1000.0f;
            camera2.FakeNearPlaneDistance = 1.0f;
            camera2.State = 1;

            camera = new Camera();
            camera.LookAtOffset = new Vector3(0.0f, 100.0f, 0.0f);
            camera.DesiredPositionOffset = new Vector3(0.0f, 300.0f, -300.0f);
            camera.NearPlaneDistance = 1.0f;
            camera.FarPlaneDistance = 10000.0f;
            camera.CameraFrustum = new BoundingFrustum(Matrix.Identity);
            camera.FakeFarPlaneDistance = 1000.0f;
            camera.FakeNearPlaneDistance = 1.0f;
            camera.State = 1;

           // songInstance.Play();

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

        public void newJuneModel(JuneXnaModel actor, Vector3 pos, Vector3 dir)
        {
            actor = new JuneXnaModel(pos, dir);
            actor.SkinningData = juneModel.Tag as SkinningData;
            actor.setAnimationPlayers();
        }
        public void loadSSmanModel(SwordAndShield actor)
        {
            Vector3 scale, trans;
            Quaternion rota;
            Matrix[] transforms = new Matrix[spearSphere.Bones.Count];
            spearSphere.CopyAbsoluteBoneTransformsTo(transforms);
            transforms[0].Decompose(out scale, out rota, out trans);
            foreach (ModelMesh mesh in spearSphere.Meshes)
            {
                if (mesh.Name == "KnockBackCheck")
                    actor.knockBackSphere.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "forwardSpell")
                    actor.spellSpheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "rSwordS1" || mesh.Name == "rSwordS2" || mesh.Name == "rSwordS3")
                    actor.rSword.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "shieldS1" || mesh.Name == "shieldS2" || mesh.Name == "shieldS3" || mesh.Name == "shieldS4" || mesh.Name == "shieldS5")
                    actor.shieldS.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                   
                if (mesh.Name == "headS1" || mesh.Name == "chestS" || mesh.Name == "hipS" || mesh.Name == "lULegS" || mesh.Name == "lLLegS" || mesh.Name == "rULegS" || mesh.Name == "rLLegS")
                    actor.spheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "chestS")
                {

                    actor.collisionS.Add(new boundingSphere("collisionS", new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X * 2.0f)));
                }
            }


        }
        public void loadArcherModel(Archer actor)
        {
            Vector3 scale, trans;
            Quaternion rota;
            Matrix[] transforms = new Matrix[spearSphere.Bones.Count];
            spearSphere.CopyAbsoluteBoneTransformsTo(transforms);
            transforms[0].Decompose(out scale, out rota, out trans);
            foreach (ModelMesh mesh in spearSphere.Meshes)
            {
                if (mesh.Name == "KnockBackCheck")
                    actor.knockBackSphere.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "forwardSpell")
                    actor.spellSpheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "bowS1" || mesh.Name == "bowS2" || mesh.Name == "bowS3"
                    || mesh.Name == "bowS4" || mesh.Name == "bowS5" || mesh.Name == "bowS6" )
                    actor.rBow.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "headS1" || mesh.Name == "chestS" || mesh.Name == "hipS" || mesh.Name == "lULegS" || mesh.Name == "lLLegS" || mesh.Name == "rULegS" || mesh.Name == "rLLegS")
                    actor.spheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "chestS")
                {
                    actor.collisionS.Add(new boundingSphere("collisionS", new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X * 2.0f)));
                }
            }


        }
        public void loadSpearmanModel(SpearMan actor)
        {
            Vector3 scale, trans;
            Quaternion rota;
            Matrix[] transforms = new Matrix[spearSphere.Bones.Count];
            spearSphere.CopyAbsoluteBoneTransformsTo(transforms);
            transforms[0].Decompose(out scale, out rota, out trans);
            foreach (ModelMesh mesh in spearSphere.Meshes)
            {
                if (mesh.Name == "KnockBackCheck")
                    actor.knockBackSphere.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "forwardSpell")
                    actor.spellSpheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "rSpearS1" || mesh.Name == "rSpearS2" || mesh.Name == "rSpearS3")
                    actor.rSpear.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if(mesh.Name == "headS1" || mesh.Name == "chestS" || mesh.Name == "hipS" || mesh.Name == "lULegS" || mesh.Name == "lLLegS" || mesh.Name == "rULegS" || mesh.Name == "rLLegS")
                    actor.spheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if(mesh.Name == "chestS")
                {
                    actor.collisionS.Add(new boundingSphere("collisionS", new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X * 2.0f)));
                    actor.collisionS.Add(new boundingSphere("collisionS", new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X )));
                }
            }


        }
        public  static void loadSpheresJuneModel(JuneXnaModel juney)
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
                if(mesh.Name == "KnockBackCheck")
                    juney.knockBackSphere.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
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
                    if(mesh.Name == "lFootS" || mesh.Name == "lPunchS" || mesh.Name == "rPunchS")
                        juney.physicalSphere.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                 if(mesh.Name == "chestS" || mesh.Name == "headS1" || mesh.Name == "hipS" || mesh.Name == "lULegS" || mesh.Name == "lLLegS" || mesh.Name == "rULegS"
                        || mesh.Name == "rLLegS")
                    juney.spheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));

                    if (mesh.Name == "chestS")
                    {
                        juney.collisionS.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X * 2.0f)));
                        juney.collisionS.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                    }

            }
            
            //juney.World.Translation

            Matrix world = new Matrix();


            //juney.World.Decompose(out scale, out rota, out trans);
            //transforms = new Matrix[humanFormation.Bones.Count];
            //humanFormation.CopyAbsoluteBoneTransformsTo(transforms);

            //foreach (ModelMesh mesh in humanFormation.Meshes)
            //{
            //    world = Matrix.Transform(transforms[mesh.ParentBone.Index], rota) * Matrix.CreateTranslation(trans);

            //    if (mesh.Name == "0,0")
            //        CFormation[0] = world.Translation;
            //    if (mesh.Name == "0,1")
            //         CFormation[3] = world.Translation;
            //    if (mesh.Name == "0,2")
            //         CFormation[6] = world.Translation;
            //    if (mesh.Name == "1,0")
            //         CFormation[1] = world.Translation;
            //    if (mesh.Name == "1,1")
            //         CFormation[4] = world.Translation;
            //    if (mesh.Name == "1,2")
            //         CFormation[7] = world.Translation;
            //    if (mesh.Name == "2,0")
            //         CFormation[2] = world.Translation;
            //    if (mesh.Name == "2,1")
            //         CFormation[5] = world.Translation;
            //    if (mesh.Name == "2,2")
            //         CFormation[8] = world.Translation;
            //    if (mesh.Name == "E0,0")
            //         EFormation[0] = world.Translation;
            //    if (mesh.Name == "E0,1")
            //         EFormation[3] = world.Translation;
            //    if (mesh.Name == "E0,2")
            //         EFormation[6] = world.Translation;
            //    if (mesh.Name == "E1,0")
            //         EFormation[1] = world.Translation;
            //    if (mesh.Name == "E1,1")
            //         EFormation[4] = world.Translation;
            //    if (mesh.Name == "E1,2")
            //         EFormation[7] = world.Translation;
            //    if (mesh.Name == "E2,0")
            //         EFormation[2] = world.Translation;
            //    if (mesh.Name == "E2,1")
            //         EFormation[5] = world.Translation;
            //    if (mesh.Name == "E2,2")
            //         EFormation[8] = world.Translation;
            //    if (mesh.Name == "W0,0")
            //         WFormation[0] = world.Translation;
            //    if (mesh.Name == "W0,1")
            //         WFormation[3] = world.Translation;
            //    if (mesh.Name == "W0,2")
            //         WFormation[6] = world.Translation;
            //    if (mesh.Name == "W1,0")
            //         WFormation[1] = world.Translation;
            //    if (mesh.Name == "W1,1")
            //         WFormation[4] = world.Translation;
            //    if (mesh.Name == "W1,2")
            //         WFormation[7] = world.Translation;
            //    if (mesh.Name == "W2,0")
            //         WFormation[2] = world.Translation;
            //    if (mesh.Name == "W2,1")
            //         WFormation[5] = world.Translation;
            //    if (mesh.Name == "W2,2")
            //         WFormation[8] = world.Translation;
            //    if (mesh.Name == "N0,0")
            //         NFormation[0] = world.Translation;
            //    if (mesh.Name == "N0,1")
            //         NFormation[3] = world.Translation;
            //    if (mesh.Name == "N0,2")
            //         NFormation[6] = world.Translation;
            //    if (mesh.Name == "N1,0")
            //         NFormation[1] = world.Translation;
            //    if (mesh.Name == "N1,1")
            //         NFormation[4] = world.Translation;
            //    if (mesh.Name == "N1,2")
            //         NFormation[7] = world.Translation;
            //    if (mesh.Name == "N2,0")
            //         NFormation[2] = world.Translation;
            //    if (mesh.Name == "N2,1")
            //         NFormation[5] = world.Translation;
            //    if (mesh.Name == "N2,2")
            //         NFormation[8] = world.Translation;

            //    if (mesh.Name == "S0,0")
            //         SFormation[0] = world.Translation;
            //    if (mesh.Name == "S0,1")
            //         SFormation[3] = world.Translation;
            //    if (mesh.Name == "S0,2")
            //         SFormation[6] = world.Translation;
            //    if (mesh.Name == "S1,0")
            //         SFormation[1] = world.Translation;
            //    if (mesh.Name == "S1,1")
            //         SFormation[4] = world.Translation;
            //    if (mesh.Name == "S1,2")
            //         SFormation[7] = world.Translation;
            //    if (mesh.Name == "S2,0")
            //         SFormation[2] = world.Translation;
            //    if (mesh.Name == "S2,1")
            //         SFormation[5] = world.Translation;
            //    if (mesh.Name == "S2,2")
            //         SFormation[8] = world.Translation;
            //}


        }
        public void loadThorModel(Thor actor)
        {
            Vector3 scale, trans;
            Quaternion rota;
            Matrix[] transforms = new Matrix[spearSphere.Bones.Count];
            spearSphere.CopyAbsoluteBoneTransformsTo(transforms);
            transforms[0].Decompose(out scale, out rota, out trans);
            foreach (ModelMesh mesh in spearSphere.Meshes)
            {
                if (mesh.Name == "KnockBackCheck")
                    actor.knockBackSphere.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "forwardSpell")
                    actor.spellSpheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "rSwordS1" || mesh.Name == "rSwordS2" || mesh.Name == "rSwordS3")
                    actor.rSword.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));

                if (mesh.Name == "headS1" || mesh.Name == "chestS" || mesh.Name == "hipS" || mesh.Name == "lULegS" || mesh.Name == "lLLegS" || mesh.Name == "rULegS" || mesh.Name == "rLLegS")
                    actor.spheres.Add(new boundingSphere(mesh.Name, new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X)));
                if (mesh.Name == "chestS")
                {

                    actor.collisionS.Add(new boundingSphere("collisionS", new BoundingSphere(mesh.BoundingSphere.Center, mesh.BoundingSphere.Radius * scale.X * 2.0f)));
                }
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

            //Camera thisCam = camera;

            //thisCam.DesiredPosition = Vector3.Transform(thisCam.DesiredPosition, Matrix.Invert(lightRotation));
            //lightView = Matrix.CreateLookAt(thisCam.DesiredPosition, thisCam.DesiredPosition - lightDir, Vector3.Up);
            //return camera.View * camera.Projection;
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
            oldMouse = mouseState;
            mouseState = Mouse.GetState();
            //timer += gameTime.ElapsedGameTime;
            //if (timer.TotalMilliseconds > 10000)
            //{
            //    JuneXnaModel newRunner = new JuneXnaModel(new Vector3(200.0f, 0.0f, 300.0f), Vector3.Backward);
            //    newRunner.SkinningData = juneModel.Tag as SkinningData;
            //    newRunner.setAnimationPlayers();
            //    loadSpheresJuneModel(newRunner);
            //    enemyRunners.Add(newRunner);


            //    timer -= TimeSpan.FromMilliseconds(10000);

            //}
            primitiveBatch.Update(camera);
            // Read the keyboard and gamepad.
          //  input.Update();

            //for orderly tactics
                  //  camera.State = 0;

                  //camera.ChasePosition = ps1.World.Translation;
                  //camera.ChaseDirection = new Vector3(ps1.World.Forward.X, ps1.World.Forward.Y, ps1.World.Forward.Z);
                  //camera.LookAtOffset = new Vector3(0.0f, 50.0f, 200.0f);
            // camera.DesiredPositionOffset = new Vector3(-200.0f, 300.0f, -250.0f);
            #region oldcamera
            /*
            if (EternalStruggle.AosRun)
            {
                camera.State = 0;
                camera.ChaseDirection = Vector3.Forward;

                if (EternalStruggle.aosCurrentTeam == 0)
                    if (EternalStruggle.aosCurrentSelection == 0)

                        camera.ChasePosition = ScreenManager.ps1.World.Translation;



                camera.LookAtOffset = new Vector3(0.0f, 50.0f, 200.0f);
                camera.DesiredPositionOffset = new Vector3(-200.0f, 300.0f, -250.0f);



            }

            if (EternalStruggle.RageOfTheMachine)
            {


                camera.State = 0;

                camera.ChaseDirection = Vector3.Forward;
                camera.ChasePosition = AchillesRage1.World.Translation;



                camera.LookAtOffset = new Vector3(0.0f, 50.0f, 200.0f);
                camera.DesiredPositionOffset = new Vector3(0.0f, 200.0f, -250.0f);

            }
            if (EternalStruggle.TheseusStandRun)
            {

                camera.State = 0;

                camera.ChaseDirection = Vector3.Forward;
                camera.ChasePosition = Theseus.World.Translation;



                camera.LookAtOffset = new Vector3(0.0f, 50.0f, 200.0f);
                camera.DesiredPositionOffset = new Vector3(0.0f, 200.0f, -250.0f);


            }
            if (EternalStruggle.EternalStruggleRun)
            {
                camera.State = 0;
                camera2.State = 0;
                camera.ChaseDirection = Vector3.Forward;
                if (EternalStruggle.currentIndex == 0)
                    camera.ChasePosition = Hercules.World.Translation;
                if (EternalStruggle.currentIndex == 1)
                    camera.ChasePosition = Cupid.World.Translation;
                if (EternalStruggle.currentIndex == 2)
                    camera.ChasePosition = Perseus.World.Translation;


                camera2.ChasePosition = LSMino.World.Translation;
                camera2.ChaseDirection = LSMino.World.Forward;
                camera2.LookAtOffset = new Vector3(50.0f * 0, 50.0f * 1, 200.0f * 1);
                camera2.DesiredPositionOffset = new Vector3(-50.0f * 0, 300.0f * 1, -250.0f * 1);
                int sign = 1;
                if (EternalStruggle.currentIndex == 1)
                    if (Theseus.World.Forward.Z > 0)
                        sign = -1;
                //camera.State = 1; 
                //camera.ChasePosition = new Vector3(-100.0f, 300.0f, 0.0f);
                //camera.NewLookAt =  Vector3.Zero;
                //camera.ChaseDirection = Cupid.World.Forward;
                //camera.LookAtOffset = new Vector3(50.0f * 0 , 50.0f *1,  200.0f  *1);
                //camera.DesiredPositionOffset = new Vector3(-50.0f * 0 , 300.0f * 1,  -250.0f * 1);

                camera.State = 1;
                camera.ChasePosition = Cupid.Position + new Vector3(0.0f, 700.0f, 0.0f);
                camera.NewLookAt = Cupid.Position;
                
            }
            if (EternalStruggle.HerculesVsThorRun)
            {
                camera.State = 0;

                camera.ChaseDirection = Vector3.Forward;
                camera.ChasePosition = HercTVH.World.Translation;



                camera.LookAtOffset = new Vector3(0.0f, 50.0f, 200.0f);
                camera.DesiredPositionOffset = new Vector3(0.0f, 200.0f, -250.0f);

                if (EternalStruggle.HercFinishThor)
                {
                    camera.State = 1;

                    camera.NewLookAt = ThorTVH.World.Translation;



                }





            }*/
#endregion
            if (stage == 1)
            {
                //camera.State = 1; 
                ////camera.ChasePosition = Theseus.World.Translation;
                ////camera.ChaseDirection = Theseus.World.Forward;
                ////camera.LookAtOffset = new Vector3(50.0f * 0, 50.0f * 1, 200.0f * 1);
                ////camera.DesiredPositionOffset = new Vector3(-50.0f * 0, 300.0f * 1, -250.0f * 1);

                ////camera.LookAtOffset = new Vector3(50.0f * 0, 50.0f * 1, 200.0f * 0);
                ////camera.DesiredPositionOffset = new Vector3(-50.0f * 0, 300.0f * 1, -250.0f * 0);
                //camera.NewLookAt = Theseus.World.Translation;
                //camera.ChasePosition = Theseus.World.Translation + new Vector3(0.0f, 200.0f, 0.0f);
                //camera.ChaseDirection = Theseus.World.Forward;

                //camera.State = 0;
                //camera.ChasePosition = Theseus.World.Translation;
                //camera.ChaseDirection = Theseus.World.Forward;
                //camera.LookAtOffset = new Vector3(50.0f * 0, 50.0f * 1, 200.0f * 1);
                //camera.DesiredPositionOffset = new Vector3(-50.0f * 0, 200.0f * 1, -250.0f * 1);

                //camera.State = 0;
                //camera.ChasePosition = Theseus.World.Translation;
                //camera.ChaseDirection = Vector3.Forward;
                //camera.LookAtOffset = new Vector3(50.0f * 0, 50.0f * 1, 200.0f * 1);
                //camera.DesiredPositionOffset = new Vector3(-50.0f * 0, 500.0f, -250.0f * 0);

                //3rd person close
                camera.ChaseDirection = Vector3.Forward;
                camera.LookAtOffset = new Vector3(50.0f * 0, -100.0f * 1, 250.0f * 3);
                camera.State = 1;
                camera.ChasePosition = Theseus.World.Translation;
                camera.NewLookAt = Theseus.World.Translation;
                camera.DesiredPositionOffset = new Vector3(-50.0f * 0, 100.0f * 2, -250.0f * 1);

                //3rd person far
                camera.ChaseDirection = Vector3.Forward;
                camera.LookAtOffset = new Vector3(50.0f * 0, -100.0f * 1, 250.0f * 30);
                camera.State = 1;
                camera.ChasePosition = Theseus.World.Translation;
                camera.NewLookAt = Theseus.World.Translation;
                camera.DesiredPositionOffset = new Vector3(-50.0f * 0, 100.0f * 5, -250.0f * 3);


                //new Orthographic camera
                camera.LookAtOffset = new Vector3(0.0f, -100 + lookAtY, 250.0f);
                camera.DesiredPositionOffset = new Vector3(-50.0f * 0, 300.0f * 1 + desiredY, -200);

                //for ninjaneer
                camera.ChaseDirection = ScreenManager.Theseus.Facing;

                camera.ChaseDirection = Vector3.Forward;
                //Interesting top down below this cmt
                //camera.LookAtOffset = new Vector3(50.0f * 0, -100.0f * 1f, -50);
                //camera.DesiredPositionOffset = new Vector3(-50.0f * 0, 100.0f * 1, -80);

#region otrhographic
  //              camera.LookAtOffset = new Vector3(-50.0f * 0, -100.0f * 0f, 0);
    //            camera.DesiredPositionOffset = new Vector3(-50.0f * 0, 100.0f * 1, -80);
#endregion
             //   camera.LookAtOffset = new Vector3(0.0f, 100.0f, 100.0f);
               // camera.DesiredPositionOffset = new Vector3(0.0f, 1000.0f, -300.0f);
               // camera.AspectRatio = 1.0f;
            }
            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            screensToUpdate.Clear();

            foreach (GameScreen screen in screens)
                screensToUpdate.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            input.Update();
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

            inputManager.Update(gameTime);
            mouseInputManager.Update(gameTime);
            // Print debug trace?
            if (traceEnabled)
                TraceScreens();
            //used to be camera.rEset gonna try
            //camera.Update(gameTime);
            camera.Reset();
            fireParticles.SetCamera(camera.View, camera.Projection);
            camera2.Reset();

        }
        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Trace.WriteLine(string.Join(", ", screenNames.ToArray()));

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
