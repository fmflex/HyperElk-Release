﻿using System.Linq;

namespace HyperElk.Core
{

public class HolyPally : CombatRoutine
    {

        //Spell Strings
        private string DivineShield = "Divine Shield";
        private string DivineProtection = "Divine Protection";
        private string HolyShock = "Holy Shock";
        private string HolyLight = "Holy Light";
        private string FoL = "Flash of Light";
        private string WoG = "Word of Glory";
        private string BoS = "Blessing of Sacrifice";
        private string LoH = "Lay on Hands";
        private string BF = "Bestow Faith";
        private string LoD = "Light of Dawn";
        private string HoLI = "Holy Light /w Infusion of Light";
        private string FoLI = "Flash of Light /w Infusion of Light";
        private string BoV = "Beacon of Virtue";
        private string CrusaderStrike = "Crusader Strike";
        private string Judgement = "Judgement";
        private string AvengingWrath = "Avenging Wrath";
        private string HolyAvenger = "Holy Avenger";
        private string AuraMastery = "Aura Mastery";
        private string HammerofJustice = "Hammer of Justice";
        private string Cons = "Consecration";
        private string Seraphim = "Seraphim";
        private string HammerofWrath = "Hammer of Wrath";
        private string HolyPrism = "Holy Prism";
        private string LightsHammer = "Light's Hammer";
        private string Forbearance = "Forbearance";
        private string Infusion = "Infusion of Light";
        private string AvengingCrusader = "Avenging Crusader";


        //Talents
        bool CrusadersMight => API.PlayerIsTalentSelected(1, 1);
        bool BestowFaith => API.PlayerIsTalentSelected(1, 2);
        bool LightsHammerT => API.PlayerIsTalentSelected(1, 3);
        bool Savedbythelight => API.PlayerIsTalentSelected(2, 1);
        bool JudgementofLight => API.PlayerIsTalentSelected(2, 2);
        bool HolyPrismT => API.PlayerIsTalentSelected(2, 3);
        bool FistofJustice => API.PlayerIsTalentSelected(3, 1);
        bool Repentance => API.PlayerIsTalentSelected(3, 2);
        bool BlindingLight => API.PlayerIsTalentSelected(3, 3);
        bool UnbreakableSpirit => API.PlayerIsTalentSelected(4, 1);
        bool Calvalier => API.PlayerIsTalentSelected(4, 2);
        bool RuleofLaw => API.PlayerIsTalentSelected(4, 3);
        bool DivinePurpose => API.PlayerIsTalentSelected(5, 1);
        bool HolyAvengerT => API.PlayerIsTalentSelected(5, 2);
        bool SeraphimT => API.PlayerIsTalentSelected(5, 3);
        bool SancifiedWrath => API.PlayerIsTalentSelected(6, 1);
        bool AvengingCrusaderT => API.PlayerIsTalentSelected(6, 2);
        bool Awakening => API.PlayerIsTalentSelected(6, 3);
        bool GlimmerofLight => API.PlayerIsTalentSelected(7, 1);
        bool BeaconofFaith => API.PlayerIsTalentSelected(7, 2);
        bool BeaconofVirtue => API.PlayerIsTalentSelected(7, 3);

        //CBProperties
        int[] numbList = new int[] { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100 };
        private string[] units = { "player", "party1", "party2", "party3", "party4" };
        private string[] raidunits = { "raid1", "raid2", "raid3", "raid4", "raid5", "raid6", "raid7", "raid8", "raid9", "raid8", "raid9", "raid10", "raid11", "raid12", "raid13", "raid14", "raid16", "raid17", "raid18", "raid19", "raid20", "raid21", "raid22", "raid23", "raid24", "raid25", "raid26", "raid27", "raid28", "raid29", "raid30", "raid31", "raid32", "raid33", "raid34", "raid35", "raid36", "raid37", "raid38", "raid39", "raid40",};
        private int UnitBelowHealthPercentRaid(int HealthPercent) => raidunits.Count(p => API.UnitHealthPercent(p) <= HealthPercent);
        private int UnitBelowHealthPercentParty(int HealthPercent) => units.Count(p => API.UnitHealthPercent(p) <= HealthPercent);
        private int UnitBelowHealthPercent(int HealthPercent) => API.PlayerIsInRaid ? UnitBelowHealthPercentRaid(HealthPercent) : UnitBelowHealthPercentParty(HealthPercent);
        private bool LodParty(int i)
        {

            return API.UnitHealthPercent(units[i]) <= LoDLifePercent && units.Count(p => API.UnitHealthPercent(p) <= LoDLifePercent) >= 3;
        }

        private bool LoDParty1 => LodParty(1);
        private bool LodRaid(int i)
        {

            return API.UnitHealthPercent(units[i]) <= LoDLifePercent && raidunits.Count(p => API.UnitHealthPercent(p) <= LoDLifePercent) >= 3;
        }

        private bool LoDRaid1 => LodRaid(1);
        private bool BoVParty(int i)
        {

            return API.UnitHealthPercent(units[i]) <= BoVLifePercent && units.Count(p => API.UnitHealthPercent(p) <= BoVLifePercent) >= 3;
        }

        private bool BoVParty1 => BoVParty(1);
        private bool BoVRaid(int i)
        {

            return API.UnitHealthPercent(units[i]) <= BoVLifePercent && raidunits.Count(p => API.UnitHealthPercent(p) <= BoVLifePercent) >= 3;
        }

        private bool BoVRaid1 => BoVRaid(1);
        private bool HPParty(int i)
        {

            return API.UnitHealthPercent(units[i]) <= HPLifePercent && units.Count(p => API.UnitHealthPercent(p) <= HPLifePercent) >= 3;
        }

        private bool HPParty1 => HPParty(1);
        private bool HPRaid(int i)
        {

            return API.UnitHealthPercent(units[i]) <= HPLifePercent && raidunits.Count(p => API.UnitHealthPercent(p) <= HPLifePercent) >= 3;
        }

        private bool HPRaid1 => HPRaid(1);
        public bool isMouseoverInCombat => CombatRoutine.GetPropertyBool("MouseoverInCombat");
        private int DivineShieldLifePercent => numbList[CombatRoutine.GetPropertyInt(DivineShield)];
        private int DivineProtectionLifePercent => numbList[CombatRoutine.GetPropertyInt(DivineProtection)];
        private int HolyShockLifePercent => numbList[CombatRoutine.GetPropertyInt(HolyShock)];
        private int HolyLightLifePercent => numbList[CombatRoutine.GetPropertyInt(HolyLight)];
        private int FoLLifePercent => numbList[CombatRoutine.GetPropertyInt(FoL)];
        private int WoGLifePercent => numbList[CombatRoutine.GetPropertyInt(WoG)];
        private int BoSLifePercent => numbList[CombatRoutine.GetPropertyInt(BoS)];
        private int FolILifePercent => numbList[CombatRoutine.GetPropertyInt(FoLI)];
        private int HoLILifePercent => numbList[CombatRoutine.GetPropertyInt(HoLI)];
        private int LoHLifePercent => numbList[CombatRoutine.GetPropertyInt(LoH)];
        private int BFLifePercent => numbList[CombatRoutine.GetPropertyInt(BF)];
        private int LoDLifePercent => numbList[CombatRoutine.GetPropertyInt(LoD)];
        private int BoVLifePercent => numbList[CombatRoutine.GetPropertyInt(BoV)];
        private int HPLifePercent => numbList[CombatRoutine.GetPropertyInt(HolyPrism)];
        //General
        private int Level => API.PlayerLevel;
        private bool InRange => API.TargetRange <= 40;
        private bool IsMelee => API.TargetRange < 6;
        private bool IsMoMelee = API.MouseoverRange < 6;
        private bool NotCasting => !API.PlayerIsCasting;
        private bool NotChanneling => !API.PlayerIsChanneling;
        private bool IsMouseover => API.ToggleIsEnabled("Mouseover");


    //  public bool isInterrupt => CombatRoutine.GetPropertyBool("KICK") && API.TargetCanInterrupted && API.TargetIsCasting && (API.TargetIsChanneling ? API.TargetElapsedCastTime >= interruptDelay : API.TargetCurrentCastTimeRemaining <= interruptDelay);
    //  public int interruptDelay => random.Next((int)(CombatRoutine.GetPropertyInt("KICKTime") * 0.9), (int)(CombatRoutine.GetPropertyInt("KICKTime") * 1.1));

    public override void Initialize()
        {
            CombatRoutine.Name = "Holy Pally by Ryu";
            API.WriteLog("Welcome to Holy Pally by Ryu");
            API.WriteLog("All Talents expect PVP Talents and Row 3 talents are supported. All Cooldowns are associated with Cooldown toggle.");
            API.WriteLog("Will add settings for CDs soon, however all CDs will be used when CD on one. If you wish to contorl when to use them, please use the toggle off fuction and use /break marco to cast them when needed");
            API.WriteLog("If you use Lights Hammer Macro, you need to use an /addonname break to stop the rotation to be able to cast it.");
            API.WriteLog("You need to create /cast [@mouseover] xxxx where xxx is each of the spells that have MO in the bindings in order for Mouseover to work");
            //Buff
            CombatRoutine.AddBuff(Infusion);
            CombatRoutine.AddBuff(AvengingWrath);
            CombatRoutine.AddBuff(AvengingCrusader);

            //Debuff
            CombatRoutine.AddDebuff(Forbearance);
            CombatRoutine.AddDebuff(Cons);

            //Spell
            CombatRoutine.AddSpell(HolyShock, "D2");
            CombatRoutine.AddSpell(FoL, "D4");
            CombatRoutine.AddSpell(HolyLight, "D3");
            CombatRoutine.AddSpell(CrusaderStrike, "D6");
            CombatRoutine.AddSpell(Judgement, "D5");
            CombatRoutine.AddSpell(LoD, "R");
            CombatRoutine.AddSpell(AvengingWrath, "F2");
            CombatRoutine.AddSpell(HolyAvenger, "F8");
            CombatRoutine.AddSpell(AuraMastery, "T");
            CombatRoutine.AddSpell(BoS, "F1");
            CombatRoutine.AddSpell(LoH, "D8");
            CombatRoutine.AddSpell(BoV, "None");
            CombatRoutine.AddSpell(DivineShield, "D0");
            CombatRoutine.AddSpell(BF, "None");
            CombatRoutine.AddSpell(HammerofJustice, "F");
            CombatRoutine.AddSpell(Cons, "None");
            CombatRoutine.AddSpell(DivineProtection, "None");
            CombatRoutine.AddSpell(HammerofWrath, "None");
            CombatRoutine.AddSpell(Seraphim, "None");
            CombatRoutine.AddSpell(AvengingCrusader, "F2");
            CombatRoutine.AddSpell(HolyPrism, "None");
            CombatRoutine.AddSpell(WoG, "None");

            //Mouseover
            CombatRoutine.AddSpell(HolyLight + "MO", "None");
            CombatRoutine.AddSpell(FoL + "MO", "None");
            CombatRoutine.AddSpell(HolyShock + "MO", "None");
            CombatRoutine.AddSpell(BoS + "MO", "None");
            CombatRoutine.AddSpell(LoH + "MO", "None");
            CombatRoutine.AddSpell(BoV + "MO", "None");
            CombatRoutine.AddSpell(HolyPrism + "MO", "None");
            CombatRoutine.AddSpell(LoD + "MO", "R");
            CombatRoutine.AddSpell(CrusaderStrike + "MO", "D6");
            CombatRoutine.AddSpell(Judgement + "MO", "D5");
            CombatRoutine.AddSpell(HammerofWrath + "MO");
            CombatRoutine.AddToggle("Mouseover");


            //Prop
            CombatRoutine.AddProp(DivineShield, DivineShield + " Life Percent", numbList, "Life percent at which" + DivineShield + "is used, set to 0 to disable", "Defense", 6);
            CombatRoutine.AddProp(DivineProtection, DivineProtection + " Life Percent", numbList, "Life percent at which" + DivineProtection + "is used, set to 0 to disable", "Defense", 8);

            CombatRoutine.AddProp(HolyShock, HolyShock + " Life Percent", numbList, "Life percent at which" + HolyShock + "is used, set to 0 to disable", "Healing", 10);
            CombatRoutine.AddProp(HolyLight, HolyLight + " Life Percent", numbList, "Life percent at which" + HolyLight + "is used, set to 0 to disable", "Healing", 11);
            CombatRoutine.AddProp(HoLI, HoLI + " Life Percent", numbList, "Life percent at which" + HoLI + "is used, set to 0 to disable", "Healing", 12);
            CombatRoutine.AddProp(FoL, FoL + " Life Percent", numbList, "Life percent at which" + FoL + "is used, set to 0 to disable", "Healing", 13);
            CombatRoutine.AddProp(FoLI, FoLI + " Life Percent", numbList, "Life percent at which" + FoLI + "is used, set to 0 to disable", "Healing", 14);
            CombatRoutine.AddProp(WoG, WoG + " Life Percent", numbList, "Life percent at which" + WoG + "is used, set to 0 to disable", "Healing", 15);
            CombatRoutine.AddProp(BoS, BoS + " Life Percent", numbList, "Life percent at which" + BoS + "is used, set to 0 to disable", "Healing", 16);
            CombatRoutine.AddProp(LoH, LoH + " Life Percent", numbList, "Life percent at which" + LoH + "is used, set to 0 to disable", "Healing", 17);
            CombatRoutine.AddProp(BF, BF + " Life Percent", numbList, "Life percent at which" + BF + "is used, set to 0 to disable", "Healing", 18);
            CombatRoutine.AddProp(LoD, LoD + " Life Percent", numbList, "Life percent at which" + LoD + "is used when three members are at life percent, set to 0 to disable", "Healing", 19);
            CombatRoutine.AddProp(BoV, BoV + " Life Percent", numbList, "Life percent at which" + BoV + "is used when three members are at life percent, set to 0 to disable", "Healing", 20);
            CombatRoutine.AddProp(HolyPrism, HolyPrism + " Life Percent", numbList, "Life percent at which" + HolyPrism + "is used when three members are at life percent, set to 0 to disable", "Healing", 21);


        }

        public override void Pulse()
        {
            if (!API.PlayerIsMounted && API.PlayerIsInCombat)
            {
                if (API.CanCast(AvengingWrath) && !API.PlayerHasBuff(AvengingWrath) && InRange && IsCooldowns)
                {
                    API.CastSpell(AvengingWrath);
                    return;
                }
                if (API.CanCast(AvengingCrusader) && AvengingCrusaderT && !API.PlayerHasBuff(AvengingCrusader) && InRange && IsCooldowns)
                {
                    API.CastSpell(AvengingCrusader);
                    return;
                }
                if (API.CanCast(HolyAvenger) && HolyAvengerT && InRange && IsCooldowns)
                {
                    API.CastSpell(HolyAvenger);
                    return;
                }
                if (API.CanCast(Seraphim) && SeraphimT && InRange && IsCooldowns && API.PlayerCurrentHolyPower >= 3)
                {
                    API.CastSpell(Seraphim);
                    return;
                }
                if (API.CanCast(DivineShield) && API.PlayerHealthPercent <= DivineShieldLifePercent && API.PlayerHealthPercent != 0 && !API.PlayerHasDebuff(Forbearance))
                {
                    API.CastSpell(DivineShield);
                    return;
                }
                if (API.CanCast(DivineProtection) && API.PlayerHealthPercent <= DivineProtectionLifePercent && API.PlayerHealthPercent != 0)
                {
                    API.CastSpell(DivineProtection);
                    return;
                }
                if (API.CanCast(BoV) && BeaconofVirtue && InRange && (BoVParty1 || BoVRaid1 && API.PlayerIsInRaid) && API.TargetHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(BoV);
                    return;
                }
                if (API.CanCast(BoV) && BeaconofVirtue && InRange && IsMouseover && (BoVParty1 || BoVRaid1 && API.PlayerIsInRaid) && API.MouseoverHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(BoV + "MO");
                    return;
                }
                if (API.CanCast(HolyPrism) && HolyPrismT && InRange && (HPParty1 || HPRaid1 && API.PlayerIsInRaid) && API.MouseoverHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(HolyPrism);
                    return;
                }
                if (API.CanCast(HolyPrism) && HolyPrismT && InRange && IsMouseover && (HPParty1 || HPRaid1 && API.PlayerIsInRaid) && API.MouseoverHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(HolyPrism + "MO");
                    return;
                }
                if (API.CanCast(LoD) && API.PlayerCurrentHolyPower >= 3 && InRange && (LoDParty1 || LoDRaid1 && API.PlayerIsInRaid) && API.TargetHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(LoD);
                    return;
                }
                if (API.CanCast(LoD)  && InRange && IsMouseover && (LoDParty1 || LoDRaid1 && API.PlayerIsInRaid) && API.MouseoverHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(LoD + "MO");
                    return;
                }
                if (API.CanCast(LoH) && InRange && API.TargetHealthPercent <= LoHLifePercent && API.TargetHealthPercent != 0 && !API.TargetHasBuff(Forbearance) && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(LoH);
                    return;
                }
                if (API.CanCast(LoH) && InRange && IsMouseover && API.MouseoverHealthPercent <= LoHLifePercent && API.MouseoverHealthPercent != 0 && !API.TargetHasBuff(Forbearance) && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(LoH + "MO");
                    return;
                }
                if (API.CanCast(BoS) && InRange && API.TargetHealthPercent <= BoSLifePercent && API.TargetHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(BoS);
                    return;
                }
                if (API.CanCast(BoS) && InRange && IsMouseover && API.MouseoverHealthPercent <= BoSLifePercent && API.MouseoverHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(BoS + "MO");
                    return;
                }
                if (API.CanCast(HolyShock) && InRange && API.TargetHealthPercent <= HolyShockLifePercent && API.TargetHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(HolyShock);
                    return;
                }
                if (API.CanCast(HolyShock) && InRange && IsMouseover && API.MouseoverHealthPercent <= HolyShockLifePercent && API.MouseoverHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(HolyShock + "MO");
                    return;
                }
                if (API.CanCast(WoG) && InRange && API.PlayerCurrentHolyPower >= 3 && API.TargetHealthPercent <= WoGLifePercent && API.TargetHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(WoG);
                    return;
                }
                if (API.CanCast(WoG) && InRange && IsMouseover && API.PlayerCurrentHolyPower >= 3 && API.MouseoverHealthPercent <= WoGLifePercent && API.MouseoverHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(WoG + "MO");
                    return;
                }
                if (API.CanCast(FoL) && InRange && API.TargetHealthPercent <= FoLLifePercent && API.TargetHealthPercent != 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(FoL);
                    return;
                }
                if (API.CanCast(FoL) && InRange && IsMouseover && API.MouseoverHealthPercent <= FoLLifePercent && API.MouseoverHealthPercent != 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(FoL + "MO");
                    return;
                }
                if (API.CanCast(HolyLight) && InRange && API.TargetHealthPercent <= HolyLightLifePercent && API.TargetHealthPercent != 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(HolyLight);
                    return;
                }
                if (API.CanCast(HolyLight) && InRange && IsMouseover && API.MouseoverHealthPercent <= HolyLightLifePercent && API.MouseoverHealthPercent != 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(HolyLight + "MO");
                    return;
                }
                if (API.CanCast(FoL) && API.PlayerHasBuff(Infusion) && InRange && API.TargetHealthPercent <= FolILifePercent && API.TargetHealthPercent != 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(FoL);
                    return;
                }
                if (API.CanCast(FoL) && API.PlayerHasBuff(Infusion) && IsMouseover && InRange && API.MouseoverHealthPercent <= FolILifePercent && API.MouseoverHealthPercent != 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(FoL + "MO");
                    return;
                }
                if (API.CanCast(HolyLight) && API.PlayerHasBuff(Infusion) && InRange && API.TargetHealthPercent <= HoLILifePercent && API.TargetHealthPercent != 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(HolyLight);
                    return;
                }
                if (API.CanCast(HolyLight) && API.PlayerHasBuff(Infusion) && IsMouseover && InRange && API.MouseoverHealthPercent <= HoLILifePercent && API.MouseoverHealthPercent != 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(HolyLight + "MO");
                    return;
                }
                if (API.CanCast(BF) && BestowFaith && InRange && API.TargetHealthPercent <= BFLifePercent && API.TargetHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(BF);
                    return;
                }
                if (API.CanCast(BF) && BestowFaith && InRange && IsMouseover && API.MouseoverHealthPercent <= BFLifePercent && API.MouseoverHealthPercent != 0 && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(BF + "MO");
                    return;
                }
            }
        }
        public override void CombatPulse()
        {
            if (API.CanCast(CrusaderStrike) && CrusadersMight && API.SpellISOnCooldown(HolyShock) && IsMelee && API.PlayerCanAttackTarget)
            {
                API.CastSpell(CrusaderStrike);
                return;
            }
            if (API.CanCast(CrusaderStrike) && IsMouseover && CrusadersMight && API.SpellISOnCooldown(HolyShock) && IsMoMelee && API.PlayerCanAttackTarget)
            {
                API.CastSpell(CrusaderStrike + "MO");
                return;
            }
            if (API.CanCast(CrusaderStrike) && Level <= 25 && !CrusadersMight && IsMelee && API.PlayerCanAttackTarget)
            {
                API.CastSpell(CrusaderStrike);
                return;
            }
            if (API.CanCast(CrusaderStrike) && IsMouseover && Level <= 25 && !CrusadersMight && IsMoMelee && API.PlayerCanAttackTarget)
            {
                API.CastSpell(CrusaderStrike + "MO");
                return;
            }
            if (API.CanCast(HolyShock) && InRange && API.PlayerCanAttackTarget)
            {
                API.CastSpell(HolyShock);
                return;
            }
            if (API.CanCast(HolyShock) && IsMouseover && InRange && API.PlayerCanAttackTarget)
            {
                API.CastSpell(HolyShock + "MO");
                return;
            }
            if (API.CanCast(Judgement) && InRange && API.PlayerCanAttackTarget)
            {
                API.CastSpell(Judgement);
                return;
            }
            if (API.CanCast(Judgement) && IsMouseover && InRange && API.PlayerCanAttackTarget)
            {
                API.CastSpell(Judgement + "MO");
                return;
            }
            if (API.CanCast(HammerofWrath) && InRange && API.TargetHealthPercent <= 20 && Level >= 46 && API.PlayerCanAttackTarget || (Level >= 58 && API.PlayerHasBuff(AvengingWrath) && API.PlayerCanAttackTarget && InRange))
            {
                API.CastSpell(HammerofWrath);
                return;
            }
            if (API.CanCast(HammerofWrath) && InRange && IsMouseover && API.MouseoverHealthPercent <= 20 && Level >= 46 && API.PlayerCanAttackTarget || (Level >= 58 && API.PlayerHasBuff(AvengingWrath) && API.PlayerCanAttackTarget && InRange))
            {
                API.CastSpell(HammerofWrath + "MO");
                return;
            }
            if (API.CanCast(Cons) && API.TargetUnitInRangeCount > 2 && IsMelee && !API.TargetHasDebuff(Cons) && !API.PlayerIsMoving && API.PlayerCanAttackTarget)
            {
                API.CastSpell(Cons);
                return;
            }
            if (API.CanCast(Cons) && API.TargetUnitInRangeCount > 2 && IsMoMelee && !API.MouseoverHasDebuff(Cons) && !API.PlayerIsMoving && API.PlayerCanAttackTarget)
            {
                API.CastSpell(Cons);
                return;
            }
            //  if (API.CanCast("Hammer of Justice") && API.IsSpellInRange("Hammer of Justice") && ConfigFile.ReadValue<bool>("Holy-Pally", "HJCheck") && API.TargetHealthPercent <= ConfigFile.ReadValue<int>("Holy-Pally", "HJUpDown") && API.TargetHealthPercent != 0 && API.PlayerCanAttackTarget)
            //  {
            //      API.CastSpell("Hammer of Justice");
            //      return;
            // }

        }

        public override void OutOfCombatPulse()
        {

        }

    }
}


