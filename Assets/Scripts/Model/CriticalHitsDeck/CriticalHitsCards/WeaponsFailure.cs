﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CriticalHitCard
{

    public class WeaponsFailure : GenericCriticalHit
    {
        public WeaponsFailure()
        {
            Name = "Weapons Failure";
            Type = CriticalCardType.Ship;
            CancelDiceResults.Add(DieSide.Success);
            CancelDiceResults.Add(DieSide.Crit);
        }

        public override void ApplyEffect(object sender, EventArgs e)
        {
            Host.AfterGotNumberOfPrimaryWeaponAttackDice += ReduceNumberOfAttackDice;
            Host.AfterGenerateAvailableActionsList += AddCancelCritAction;

            Host.AssignToken(new Tokens.WeaponsFailureCritToken(), Triggers.FinishTrigger);
        }

        public override void DiscardEffect(Ship.GenericShip host)
        {
            Messages.ShowInfo("Number of attack dice is restored");

            host.RemoveToken(typeof(Tokens.WeaponsFailureCritToken));
            host.AfterGotNumberOfPrimaryWeaponAttackDice -= ReduceNumberOfAttackDice;
            host.AfterGenerateAvailableActionsList -= AddCancelCritAction;
        }

        private void ReduceNumberOfAttackDice(ref int value)
        {
            Messages.ShowInfo("Weapons Failure: Number of attack dice is reduced");

            value--;
        }

    }

}