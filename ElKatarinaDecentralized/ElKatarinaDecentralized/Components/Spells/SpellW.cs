﻿namespace ElKatarinaDecentralized.Components.Spells
{
    using System;
    using System.Linq;

    using ElKatarinaDecentralized.Enumerations;
    using ElKatarinaDecentralized.Utils;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    /// <summary>
    ///     The spell W.
    /// </summary>
    internal class SpellW : ISpell
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the damage type.
        /// </summary>
        internal override TargetSelector.DamageType DamageType => TargetSelector.DamageType.Magical;

        /// <summary>
        ///     Gets the range.
        /// </summary>
        internal override float Range => Misc.KatarinaAutoAttackRange + 150f;

        /// <summary>
        ///     Gets the spell slot.
        /// </summary>
        internal override SpellSlot SpellSlot => SpellSlot.W;


        #endregion

        #region Methods

        /// <summary>
        ///     The on combo callback.
        /// </summary>
        internal override void OnCombo()
        {
            try
            {
                if (this.SpellObject == null)
                {
                    return;
                }

                if (ObjectManager.Player.IsChannelingImportantSpell())
                {
                    return;
                }

                var target = Misc.GetTarget(this.Range, this.DamageType);
                if (target != null)
                {
                    this.SpellObject.Cast();
                }
            }
            catch (Exception e)
            {
                Logging.AddEntry(LoggingEntryTrype.Error, "@SpellE.cs: Can not run OnCombo - {0}", e);
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
        }

        /// <summary>
        ///     The on lane clear callback.
        /// </summary>
        internal override void OnLaneClear()
        {
        }

        /// <summary>
        ///     The on jungle clear callback.
        /// </summary>
        internal override void OnJungleClear()
        {
        }

        #endregion
    }
}