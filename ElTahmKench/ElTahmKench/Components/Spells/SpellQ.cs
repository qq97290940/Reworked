﻿namespace ElTahmKench.Components.Spells
{
    using System;
    using System.Linq;

    using ElTahmKench.Enumerations;
    using ElTahmKench.Utils;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    /// <summary>
    ///     The spell Q.
    /// </summary>
    internal class SpellQ : ISpell
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the damage type.
        /// </summary>
        internal override TargetSelector.DamageType DamageType => TargetSelector.DamageType.Magical;

        /// <summary>
        ///     Gets the delay.
        /// </summary>
        internal override float Delay => 0.25f;

        /// <summary>
        ///     Spell has collision.
        /// </summary>
        internal override bool Collision => true;

        /// <summary>
        ///     Gets the range.
        /// </summary>
        internal override float Range => 950f;

        /// <summary>
        ///     Gets or sets the skillshot type.
        /// </summary>
        internal override SkillshotType SkillshotType => SkillshotType.SkillshotLine;

        /// <summary>
        ///     Gets the speed.
        /// </summary>
        internal override float Speed => 2800f;

        /// <summary>
        ///     Gets the spell slot.
        /// </summary>
        internal override SpellSlot SpellSlot => SpellSlot.Q;

        /// <summary>
        ///     Gets the width.
        /// </summary>
        internal override float Width => 75f; // evade says 90f

        #endregion

        #region Methods

        /// <summary>
        ///     The on combo callback.
        /// </summary>
        internal override void OnCombo()
        {
            try
            {
                var target = Misc.GetTarget(this.Range + this.Width, this.DamageType);
                if (target != null)
                {
                    if (target.IsValidTarget(this.Range + this.Width))
                    {
                        this.SpellObject.Cast(target);
                    }
                }
            }
            catch (Exception e)
            {
                Logging.AddEntry(LoggingEntryType.Error, "@SpellQ.cs: Can not run OnCombo - {0}", e);
                throw;
            }
        }

        /// <summary>
        ///     The on mixed callback.
        /// </summary>
        internal override void OnMixed()
        {
            this.OnCombo();
        }

        /// <summary>
        ///     The on last hit callback.
        /// </summary>
        internal override void OnLastHit()
        {
            var minion =
               MinionManager.GetMinions(this.Range)
                   .Where(obj => this.SpellObject.IsKillable(obj))
                   .MinOrDefault(obj => obj.Health);

            if (minion != null)
            {
                this.SpellObject.Cast(minion);
            }
        }

        #endregion
    }
}