#region File Description
//-----------------------------------------------------------------------------
// FireParticleSystem.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SmellOfRevenge2011
{
    /// <summary>
    /// Custom particle system for creating a flame effect.
    /// </summary>
    class SlashParticleSystem : ParticleSystem
    {
        public SlashParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "white";

            settings.MaxParticles = 2400;

            settings.Duration = TimeSpan.FromSeconds(5);

            settings.DurationRandomness = 1;

            settings.MinHorizontalVelocity = 0;
            settings.MaxHorizontalVelocity = 0;

            settings.MinVerticalVelocity = 0;
            settings.MaxVerticalVelocity = 0;

            // Set gravity upside down, so the flames will 'fall' upward.
            settings.Gravity = new Vector3(0, 0, 0);

            settings.MinColor = new Color(255, 255, 255, 10);
            settings.MaxColor = new Color(255, 255, 255, 255);

            settings.MinStartSize = 5;
            settings.MaxStartSize = 10;

            settings.MinEndSize = 10;
            settings.MaxEndSize = 40;


            settings.MinStartSize = 20;
            settings.MaxStartSize = 20;

            settings.MinEndSize = 5;
            settings.MaxEndSize = 20;

            // Use additive blending.
            settings.BlendState = BlendState.Additive;
        }
    }
}

