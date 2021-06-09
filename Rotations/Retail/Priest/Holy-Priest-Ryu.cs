﻿using System.Linq;
using System.Diagnostics;

namespace HyperElk.Core
{
    public class HolyPriest : CombatRoutine
    {
        //Spell Strings
        private string HolyWordSerenity = "Holy Word: Serenity";
        private string HolyWordSanctify = "Holy Word: Sancity";
        private string CoH = "Circle of Healing";
        private string Halo = "Halo";
        private string FlashHeal = "Flash Heal";
        private string PoH = "Prayer of Healing";
        private string GuardianSpirit = "Guardian Spirit";
        private string HolyWordSalvation = "Holy Word: Salvation";
        private string DivineHymn = "Divine Hymn";
        private string Heal = "Heal";
        private string Renew = "Renew";
        private string HolyWordChastise = "Holy Word: Chastise";
        private string HolyFire = "Holy Fire";
        private string ShadowWordPain = "Shadow Word: Pain";
        private string Smite = "Smite";
        private string HolyNova = "Holy Nova";
        private string PrayerofMending = "Prayer of Mending";
        private string PowerInfusion = "Power Infusion";
        private string ShadowWordDeath = "Shadow Word: Death";
        private string PowerWordShield = "Power Word: Shield";
        private string LeapOfFaith = "Leap of Faith";
        private string MindControl = "Mind Control";
        private string MassDispel = "Mass Dispel";
        private string ShackleUndead = "Shackle Undead";
        private string MindSoothe = "Mind Soothe";
        private string PowerWordFortitude = "Power Word: Fortitude";
        private string SymbolOfHope = "Symbol of Hope";
        private string Fade = "Fade";
        private string DispelMagic = "Dispel Magic";
        private string PsychicScream = "Psychic Scream";
        private string SpiritOfRedemption = "Spirit of Redemption";
        private string BindingHeal = "Binding Heal";
        private string Apotheosis = "Apotheosis";
        private string BoonoftheAscended = "Boon of the Ascended";
        private string AscendedNova = "Ascended Nova";
        private string AscendedBlast = "Ascended Blast";
        private string Mindgames = "Mindgames";
        private string UnholyNova = "Unholy Nova";
        private string FaeGuardians = "Fae Guardians";
        private string Fleshcraft = "Fleshcraft";
        private string WeakenedSoul = "Weakened Soul";
        private string SurgeofLight = "Surge of Light";
        private string PrayerCircle = "Prayer Circle";
        private string AoE = "AOE";
        private string AoERaid = "AOE Healing Raid";
        private string AoEDPS = "AOEDPS";
        private string AoEDPSRaid = "AOEDPS Raid";
        private string AoEDPSH = "AOEDPS Health";
        private string AoEDPSHRaid = "AOEDPS Health Raid";
        private string PhialofSerenity = "Phial of Serenity";
        private string SpiritualHealingPotion = "Spiritual Healing Potion";
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string TargetChange = "Target Change";
        private string Party1 = "party1";
        private string Party2 = "party2";
        private string Party3 = "party3";
        private string Party4 = "party4";
        private string Player = "player";
        private string PartySwap = "Target Swap";
        private string Quake = "Quake";
        private string Purify = "Purify";
        private string SwapSpeed = "Target Swap Speed";
        private string DivineStar = "Divine Star";
        private string AngelicFeather = "Angelic Feather";
        private string EatingBuff = "Food Buff";
        private string MageEatingBuff = "Mage Food Buff";



        //Talents
        bool EnlightenmentTalent => API.PlayerIsTalentSelected(1, 1);
        bool TrailofLightTalent => API.PlayerIsTalentSelected(1, 2);
        bool RenewedFaithTalent => API.PlayerIsTalentSelected(1, 3);
        bool AgenlsMercyTalent => API.PlayerIsTalentSelected(2, 1);
        bool BodyandSoulTalent => API.PlayerIsTalentSelected(2, 2);
        bool AngelicFeatherTalent => API.PlayerIsTalentSelected(2, 3);
        bool ComiscRippleTalent => API.PlayerIsTalentSelected(3, 1);
        bool GuardianAngelTalent => API.PlayerIsTalentSelected(3, 2);
        bool AfterlifeTalent => API.PlayerIsTalentSelected(3, 3);
        bool PsychicVoiceTalent => API.PlayerIsTalentSelected(4, 1);
        bool CensureTalent => API.PlayerIsTalentSelected(4, 2);
        bool ShiningForceTalent => API.PlayerIsTalentSelected(4, 3);
        bool SurgeofLightTalent => API.PlayerIsTalentSelected(5, 1);
        bool BindingHealTalent => API.PlayerIsTalentSelected(5, 2);
        bool PrayerCircleTalent => API.PlayerIsTalentSelected(5, 3);
        bool BenedictionTalent => API.PlayerIsTalentSelected(6, 1);
        bool DivineStarTalent => API.PlayerIsTalentSelected(6, 2);
        bool HaloTalent => API.PlayerIsTalentSelected(6, 3);
        bool LightofTheNaaruTalent => API.PlayerIsTalentSelected(7, 1);
        bool ApotheosisTalent => API.PlayerIsTalentSelected(7, 2);
        bool HolyWordSalavationTalent => API.PlayerIsTalentSelected(7, 3);

        //CBProperties
       

        int PlayerHealth => API.TargetHealthPercent;

        private static readonly Stopwatch SwapWatch = new Stopwatch();
        private static readonly Stopwatch DispelWatch = new Stopwatch();
        string[] DispellList = {"Chilled" ,"Frozen Binds" ,"Clinging Darkness" ,"Rasping Scream" ,"Heaving Retch" ,"Goresplatter" ,"Slime Injection" ,"Gripping Infection" ,"Repulsive Visage" ,"Soul Split" ,"Anima Injection" ,"Bewildering Pollen" ,"Bramblethorn Entanglement" ,"Sinlight Visions" ,"Siphon Life" ,"Turn to Stone" ,"Stony Veins" ,"Cosmic Artifice" ,"Wailing Grief" ,"Shadow Word:  Pain" ,"Anguished Cries" ,"Wrack Soul" ,"Dark Lance" ,"Insidious Venom" ,"Charged Anima" ,"Lost Confidence" ,"Burden of Knowledge" ,"Internal Strife" ,"Forced Confession" ,"Insidious Venom 2","Soul Corruption",
        "Debilitating Plague", "Burning Strain", "Blightbeak", "Corroded Claws", "Wasting Blight", "Hurl Spores", "Corrosive Gunk", "Genetic Alteration" , "Withering Blight" , "Decaying Blight" , "Burst" };

        // bool ChannelingCov => API.CurrentCastSpellID("player") == 323764;
        bool ChannelingDivine => API.CurrentCastSpellID("player") == 64843;
        private bool QuakingHelper => CombatRoutine.GetPropertyBool("QuakingHelper");
        private bool CoHRange => API.UnitRangeCount(30) >= AoENumber;
        private bool CoHAoE => API.UnitBelowHealthPercent(CoHLifePercent) >= AoENumber && !API.PlayerCanAttackTarget && NotChanneling && CoHRange;
        private bool PoHAoE => API.UnitBelowHealthPercent(PoHLifePercent) >= AoENumber && !API.PlayerCanAttackTarget && NotChanneling;
        private bool HaloAoE => API.UnitBelowHealthPercent(HaloLifePercent) >= AoENumber && !API.PlayerCanAttackTarget;
        private bool BoonAoE => API.UnitBelowHealthPercent(BoonLifePercent) >= AoENumber && !API.PlayerCanAttackTarget && NotChanneling;
        private bool UnholyAoE => API.UnitBelowHealthPercent(UnholyNovaLifePercent) >= AoENumber && !API.PlayerCanAttackTarget && NotChanneling;
        private bool HWSancitfyAoE => API.UnitBelowHealthPercent(HolyWordSanctifyLifePercent) >= AoENumber;
        private bool DivineHymnAoE => API.UnitBelowHealthPercent(DivineHymnLifePercent) >= AoENumber;
        private bool DivineStarAoE => API.UnitBelowHealthPercent(DivineStarLifePercent) >= AoENumber && API.PlayerIsInRaid ? API.UnitRangeCountRaid(24) >= AoERaidNumber : API.UnitRangeCountParty(24) >= AoENumber;
        private bool HWSalAoE => API.UnitBelowHealthPercent(HolyWordSalvationLifePercent) >= AoENumber;
        private bool HWSCheck => API.CanCast(HolyWordSalvation) && HolyWordSalavationTalent && HWSalAoE && Mana >= 6 && !ChannelingDivine && !API.PlayerIsMoving;
        private bool CoHCheck => API.CanCast(CoH) && CoHAoE && Mana >= 4 && (!API.PlayerIsMoving || API.PlayerIsMoving) && !ChannelingDivine;
        private bool DHCheck => API.CanCast(DivineHymn) && DivineHymnAoE && Mana >= 5 && !API.PlayerCanAttackTarget && !API.PlayerIsMoving;
        private bool PoHCheck => API.CanCast(PoH) && PoHAoE && Mana >= 5 && !API.PlayerIsMoving && !ChannelingDivine;
        private bool GSCheck => API.CanCast(GuardianSpirit) && API.TargetHealthPercent <= GuardianSpirtLifePercent && Mana >= 1 && !API.PlayerCanAttackTarget && NotChanneling && (!API.PlayerIsMoving || API.PlayerIsMoving) && !ChannelingDivine;
        private bool HealCheck => API.CanCast(Heal) && API.TargetHealthPercent <= HealLifePercent && Mana >= 3 && !API.PlayerCanAttackTarget && NotChanneling && !API.PlayerIsMoving && !ChannelingDivine;
        private bool FlashHealCheck => API.CanCast(FlashHeal) && (API.TargetHealthPercent <= FlashHealLifePercent || SurgeofLightTalent && API.PlayerHasBuff(SurgeofLight)) && Mana >=4 && !API.PlayerCanAttackTarget && NotChanneling && (SurgeofLightTalent && API.PlayerHasBuff(SurgeofLight)  || !API.PlayerIsMoving) && !ChannelingDivine;
        private bool RenewCheck => API.CanCast(Renew) && API.TargetHealthPercent <= RenewLifePercent && Mana >= 2 && !API.PlayerCanAttackTarget && !API.TargetHasBuff(Renew) && NotChanneling && (!API.PlayerIsMoving || API.PlayerIsMoving) && !ChannelingDivine;
        private bool HolyWordSerenityCheck => API.CanCast(HolyWordSerenity) && API.TargetHealthPercent <= HolyWordSerenityLifePercent && Mana >= 4 && !API.PlayerCanAttackTarget && API.SpellCharges(HolyWordSerenity) > 0 && (!API.PlayerIsMoving || API.PlayerIsMoving) && !ChannelingDivine;
        private bool HaloCheck => API.CanCast(Halo) && HaloAoE && !ChannelingDivine && Mana >= 3 && HaloTalent && !API.PlayerIsMoving;

        private bool HWSancitfyCheck => API.CanCast(HolyWordSanctify) && HWSancitfyAoE && !API.PlayerCanAttackTarget && NotChanneling && (!API.PlayerIsMoving || API.PlayerIsMoving);
        private bool PoMCheck => API.CanCast(PrayerofMending) && API.TargetHealthPercent <= PoMLifePercent && !API.PlayerCanAttackTarget && Mana >= 2 && !API.PlayerIsMoving && !ChannelingDivine && API.TargetRoleSpec == RoleSpec && !API.TargetHasBuff(PrayerofMending);
        private bool KyrianCheck => API.CanCast(BoonoftheAscended) && PlayerCovenantSettings == "Kyrian" && BoonAoE && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && !API.PlayerCanAttackTarget && !API.PlayerIsMoving && !ChannelingDivine;
        private bool NightFaeCheck => API.CanCast(FaeGuardians) && PlayerCovenantSettings == "Night Fae" && Mana >= 2 && API.TargetHealthPercent >= FaeLifePercent && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && !ChannelingDivine;
        private bool NecrolordCheck => API.CanCast(UnholyNova) && PlayerCovenantSettings == "Necrolord" && UnholyAoE && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && !API.PlayerCanAttackTarget && (!API.PlayerIsMoving || API.PlayerIsMoving) && !ChannelingDivine;
        private bool VenthyrCheck => API.CanCast(Mindgames) && PlayerCovenantSettings == "Venthyr" && Mana >= 2 && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && (!API.PlayerIsMoving || API.PlayerIsMoving) && !ChannelingDivine;
        


        public bool isMouseoverInCombat => CombatRoutine.GetPropertyBool("MouseoverInCombat");
        private bool IsAutoSwap => API.ToggleIsEnabled("Auto Target");
        private bool IsOOC => API.ToggleIsEnabled("OOC");
        private bool IsDPS => API.ToggleIsEnabled("DPS Auto Target");
        private bool UseAngel => CombatRoutine.GetPropertyBool(AngelicFeather);
        private int TankHealth => API.numbPercentListLong[CombatRoutine.GetPropertyInt("Tank Health")];
        private int HealLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(Heal)];
        private int FlashHealLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(FlashHeal)];
        private int RenewLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(Renew)];
        private int HolyWordSerenityLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyWordSerenity)];
        private int HolyWordSanctifyLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyWordSanctify)];
        private int PoHLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(PoH)];
        private int PoMLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(PrayerofMending)];
        private int DivineHymnLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineHymn)];
        private int DivineStarLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineStar)];
        private int CoHLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(CoH)];
        private int HaloLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(Halo)];
        private int HolyWordSalvationLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyWordSalvation)];
        private int BoonLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(BoonoftheAscended)];
      //  private int MindgamesLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(Mindgames)];
        private int UnholyNovaLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(UnholyNova)];
        private int FaeLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(FaeGuardians)];
        private int GuardianSpirtLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(GuardianSpirit)];
        private string UseCovenant => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Use Covenant")];
        private int AoENumber => API.numbPartyList[CombatRoutine.GetPropertyInt(AoE)];
        private int AoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(AoERaid)];
        private int AoEDPSNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(AoEDPS)];
        private int AoEDPSRaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(AoEDPSRaid)];
        private int AoEDPSHLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AoEDPSH)];
        private int AoEDPSHRaidLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AoEDPSHRaid)];
        private int FleshcraftPercentProc => API.numbPercentListLong[CombatRoutine.GetPropertyInt(Fleshcraft)];
        private int PhialofSerenityLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(PhialofSerenity)];
        private int SpiritualHealingPotionLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(SpiritualHealingPotion)];
        private int PartySwapPercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(PartySwap)];
        private int TargetChangePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(TargetChange)];
        private int UnitHealth => API.numbPercentListLong[CombatRoutine.GetPropertyInt("Other Members Health")];
        private int PlayerHP => API.numbPercentListLong[CombatRoutine.GetPropertyInt("Player Health")];

        private string UseTrinket1 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket1")];
        private string UseTrinket2 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket2")];
        //private int AoERaidNumber => numbRaidList[CombatRoutine.GetPropertyInt(AoER)];

        //General

        private int Level => API.PlayerLevel;
        private bool InRange => API.TargetRange <= 40;
        private bool IsMelee => API.TargetRange < 12;
       // private bool NotCasting => !API.PlayerIsCasting;
        private bool NotChanneling => !API.PlayerIsChanneling;
        private bool IsMouseover => API.ToggleIsEnabled("Mouseover");
        bool IsTrinkets1 => (UseTrinket1 == "With Cooldowns" && IsCooldowns || UseTrinket1 == "On Cooldown" || UseTrinket1 == "on AOE" && API.TargetUnitInRangeCount >= 2 && IsAOE);
        bool IsTrinkets2 => (UseTrinket2 == "With Cooldowns" && IsCooldowns || UseTrinket2 == "On Cooldown" || UseTrinket2 == "on AOE" && API.TargetUnitInRangeCount >= 2 && IsAOE);
        private int Mana => API.PlayerMana;
        private bool Quaking => (API.PlayerIsCasting(false) || API.PlayerIsChanneling) && API.PlayerDebuffRemainingTime(Quake) < 110 && PlayerHasDebuff(Quake);
        private bool SaveQuake => (PlayerHasDebuff(Quake) && API.PlayerDebuffRemainingTime(Quake) > 200 && QuakingHelper || !PlayerHasDebuff(Quake) || !QuakingHelper);
        private bool QuakingPoM => (API.PlayerDebuffRemainingTime(Quake) > PoMCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingHalo => (API.PlayerDebuffRemainingTime(Quake) > HaloCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingFlash => (API.PlayerDebuffRemainingTime(Quake) > FlashCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingMind => (API.PlayerDebuffRemainingTime(Quake) > MindCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingPoH => (API.PlayerDebuffRemainingTime(Quake) > PoHCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingHWSalv => (API.PlayerDebuffRemainingTime(Quake) > HWSalvCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingDivine => (API.PlayerDebuffRemainingTime(Quake) > DivineHymnCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingHeal => (API.PlayerDebuffRemainingTime(Quake) > HealCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingHolyFire => (API.PlayerDebuffRemainingTime(Quake) > HolyFireCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingSmite => (API.PlayerDebuffRemainingTime(Quake) > SmiteCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingBoon => (API.PlayerDebuffRemainingTime(Quake) > BoonCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingSymbol => (API.PlayerDebuffRemainingTime(Quake) > SymbolCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool IsNotEating => (!API.PlayerHasBuff(MageEatingBuff) || !API.PlayerHasBuff(EatingBuff));

        float PoMCastTime => 150f / (1f + API.PlayerGetHaste);
        float HaloCastTime => 150f / (1f + API.PlayerGetHaste);
        float FlashCastTime => 150f / (1f + API.PlayerGetHaste);
        float MindCastTime => 150f / (1f + API.PlayerGetHaste);
        float PoHCastTime => 200f / (1f + API.PlayerGetHaste);
        float HWSalvCastTime => 250f / (1f + API.PlayerGetHaste);
        float DivineHymnCastTime => 800f / (1f + API.PlayerGetHaste);
        float HealCastTime => 250f / (1f + API.PlayerGetHaste);
        float HolyFireCastTime => 150f / (1f + API.PlayerGetHaste);
        float SmiteCastTime => 150f / (1f + API.PlayerGetHaste);
        float BoonCastTime => 150f / (1f + API.PlayerGetHaste);
        float SymbolCastTime => 500f / (1f + API.PlayerGetHaste);
        private bool IsDispell => API.ToggleIsEnabled("Dispel");
        private static bool TargetHasDispellAble(string debuff)
        {
            return API.TargetHasDebuff(debuff, false, true);
        }
        private static bool MouseouverHasDispellAble(string debuff)
        {
            return API.MouseoverHasDebuff(debuff, false, true);
        }
        private static bool UnitHasDispellAble(string debuff, string unit)
        {
            return API.UnitHasDebuff(debuff, unit, false, true);
        }
        private static bool UnitHasBuff(string buff, string unit)
        {
            return API.UnitHasBuff(buff, unit, true, true);
        }
        private static bool UnitHasDebuff(string buff, string unit)
        {
            return API.UnitHasDebuff(buff, unit, false, true);
        }
        private static bool PlayerHasDebuff(string buff)
        {
            return API.PlayerHasDebuff(buff, false, false);
        }
        private static bool TargetHasBuff(string buff)
        {
            return API.TargetHasBuff(buff, true, true);
        }
        private static bool MouseoverHasBuff(string buff)
        {
            return API.MouseoverHasBuff(buff, true, false);
        }
        private static bool TargetHasDebuff(string buff)
        {
            return API.TargetHasDebuff(buff, false, true);
        }
        private static bool MouseoverHasDebuff(string buff)
        {
            return API.MouseoverHasDebuff(buff, false, false);
        }
        public string[] PoMTarget = new string[] { "Tank", "DPS", "Healer" };
        private string UsePoM => PoMTarget[CombatRoutine.GetPropertyInt("Use PoM")];
        private int RoleSpec
        {
            get
            {
                if (UsePoM == "Tank")
                    return 999;
                else if (UsePoM == "DPS")
                    return 997;
                else if (UsePoM == "Healer")
                    return 998;
                return 999;
            }
        }

        //  public bool isInterrupt => CombatRoutine.GetPropertyBool("KICK") && API.TargetCanInterrupted && API.TargetIsCasting && (API.TargetIsChanneling ? API.TargetElapsedCastTime >= interruptDelay : API.TargetCurrentCastTimeRemaining <= interruptDelay);
        //  public int interruptDelay => random.Next((int)(CombatRoutine.GetPropertyInt("KICKTime") * 0.9), (int)(CombatRoutine.GetPropertyInt("KICKTime") * 1.1));

        public override void Initialize()
        {
            CombatRoutine.Name = "Holy Priest by Ryu";
            API.WriteLog("Welcome to Holy Priest by Ryu");
            API.WriteLog("BETA ROTATION : Things may be missing or not work correctly yet. Please post feedback in Priest channel. Cov expect Unholy Nova is supported via Cooldown toggle or break marcos");
           // API.WriteLog("Mouseover Support is added. Please create /cast [@mouseover] xx whereas xx is your spell and assign it the binds with MO on it in keybinds.");
            API.WriteLog("For all ground spells, either use @Cursor or when it is time to place it, the Bot will pause until you've placed it. If you'd perfer to use your own logic for them, please place them on ignore in the spellbook.");
            API.WriteLog("For the Quaking helper you just need to create an ingame macro with /stopcasting and bind it under the Macros Tab in Elk :-)");
            API.WriteLog("If you use Angelic Feather, please create a @player marco, it will use it while moving. If you want to use it, change the setting to True. It is false by default.");
            API.WriteLog("If you wish to use Auto Target, please set your WoW keybinds in the keybinds => Targeting for Self, Party, and Assist Target and then match them to the Macro's's in the spell book. Enable it the Toggles. You must at least have a target for it to swap, friendly or enemy. UNDER TESTING : It can swap back to an enemy, but YOU WILL NEED TO ASSIGN YOUR ASSIST TARGET KEY IT WILL NOT WORK IF YOU DONT DO THIS. If you DO NOT want it to do target enemy swapping, please IGNORE Assist Macro in the Spellbook. This works for both raid and party, however, you must set up the binds. Please watch video in the Discord");

            //Buff
            CombatRoutine.AddBuff(PowerWordFortitude, 21562);
            CombatRoutine.AddBuff(PowerWordShield, 17);
            CombatRoutine.AddBuff(SurgeofLight, 109186);
            CombatRoutine.AddBuff(PrayerCircle, 321377);
            CombatRoutine.AddBuff(Apotheosis, 200183);
            CombatRoutine.AddBuff(SpiritOfRedemption, 20711);
            CombatRoutine.AddBuff(Renew, 139);
            CombatRoutine.AddBuff(PrayerofMending, 41635);
            CombatRoutine.AddBuff(Quake, 240447);
            CombatRoutine.AddBuff("Gluttonous Miasma", 329298);
            CombatRoutine.AddBuff(EatingBuff, 327786);
            CombatRoutine.AddBuff(MageEatingBuff, 167152);

            //Debuff
            CombatRoutine.AddDebuff(WeakenedSoul, 6788);
            CombatRoutine.AddDebuff(ShadowWordPain, 589);
            CombatRoutine.AddDebuff(Quake, 240447);
            //Debuff Dispell
            CombatRoutine.AddDebuff("Chilled", 328664);
            CombatRoutine.AddDebuff("Frozen Binds", 320788);
            CombatRoutine.AddDebuff("Clinging Darkness", 323347);
            CombatRoutine.AddDebuff("Rasping Scream", 324293);
            CombatRoutine.AddDebuff("Heaving Retch", 320596);
            CombatRoutine.AddDebuff("Goresplatter", 338353);
            CombatRoutine.AddDebuff("Slime Injection", 329110);
            CombatRoutine.AddDebuff("Gripping Infection", 328180);
            CombatRoutine.AddDebuff("Repulsive Visage", 328756);
            CombatRoutine.AddDebuff("Soul Split", 322557);
            CombatRoutine.AddDebuff("Anima Injection", 325224);
            CombatRoutine.AddDebuff("Bewildering Pollen", 321968);
            CombatRoutine.AddDebuff("Bramblethorn Entanglement", 324859);
            CombatRoutine.AddDebuff("Sinlight Visions", 339237);
            CombatRoutine.AddDebuff("Siphon Life", 325701);
            CombatRoutine.AddDebuff("Turn to Stone", 326607);
            CombatRoutine.AddDebuff("Stony Veins", 326632);
            CombatRoutine.AddDebuff("Cosmic Artifice", 325725);
            CombatRoutine.AddDebuff("Wailing Grief", 340026);
            CombatRoutine.AddDebuff("Shadow Word:  Pain", 332707);
            CombatRoutine.AddDebuff("Anguished Cries", 325885);
            CombatRoutine.AddDebuff("Wrack Soul", 321038);
            CombatRoutine.AddDebuff("Dark Lance", 327481);
            CombatRoutine.AddDebuff("Insidious Venom", 323636);
            CombatRoutine.AddDebuff("Charged Anima", 338731);
            CombatRoutine.AddDebuff("Lost Confidence", 322818);
            CombatRoutine.AddDebuff("Burden of Knowledge", 317963);
            CombatRoutine.AddDebuff("Internal Strife", 327648);
            CombatRoutine.AddDebuff("Forced Confession", 328331);
            CombatRoutine.AddDebuff("Insidious Venom 2", 317661);
            CombatRoutine.AddDebuff("Soul Corruption", 333708);
            CombatRoutine.AddDebuff("Debilitating Plague", 324652);
            CombatRoutine.AddDebuff("Burning Strain", 322358);
            CombatRoutine.AddDebuff("Blightbeak", 327882);
            CombatRoutine.AddDebuff("Corroded Claws", 320512);
            CombatRoutine.AddDebuff("Wasting Blight", 320542);
            CombatRoutine.AddDebuff("Hurl Spores", 328002);
            CombatRoutine.AddDebuff("Corrosive Gunk", 319070);
            CombatRoutine.AddDebuff("Genetic Alteration", 320248);
            CombatRoutine.AddDebuff("Withering Blight", 341949);
            CombatRoutine.AddDebuff("Decaying Blight", 330700);
            CombatRoutine.AddDebuff("Gluttonous Miasma", 329298);
            CombatRoutine.AddDebuff("Burst", 240443);


            //Spell
            CombatRoutine.AddSpell(Heal, 2060);
            CombatRoutine.AddSpell(FlashHeal, 2061);
            CombatRoutine.AddSpell(Renew, 139);
            CombatRoutine.AddSpell(HolyWordSerenity, 2050);
            CombatRoutine.AddSpell(HolyWordSanctify, 34861);
            CombatRoutine.AddSpell(PoH, 596);
            CombatRoutine.AddSpell(CoH, 204883);
            CombatRoutine.AddSpell(HolyWordChastise, 88625);
            CombatRoutine.AddSpell(Smite, 585);
            CombatRoutine.AddSpell(HolyNova, 132157);
            CombatRoutine.AddSpell(HolyFire, 14914);
            CombatRoutine.AddSpell(SymbolOfHope, 64901);
            CombatRoutine.AddSpell(Fade, 586);
            CombatRoutine.AddSpell(DispelMagic, 528);
            CombatRoutine.AddSpell(GuardianSpirit, 47788);
            CombatRoutine.AddSpell(LeapOfFaith, 73325);
            CombatRoutine.AddSpell(MindControl, 136287);
            CombatRoutine.AddSpell(MassDispel, 32375);
            CombatRoutine.AddSpell(ShackleUndead, 9484);
            CombatRoutine.AddSpell(Fleshcraft, 324631);
            CombatRoutine.AddSpell(MindSoothe, 453);
            CombatRoutine.AddSpell(PowerInfusion, 10060);
            CombatRoutine.AddSpell(PsychicScream, 8122);
            CombatRoutine.AddSpell(BoonoftheAscended, 325013);
            CombatRoutine.AddSpell(BindingHeal, 32546);
            CombatRoutine.AddSpell(AscendedBlast, 325315);
            CombatRoutine.AddSpell(AscendedNova, 325020);
            CombatRoutine.AddSpell(Mindgames, 323673);
            CombatRoutine.AddSpell(UnholyNova, 347788);
            CombatRoutine.AddSpell(FaeGuardians, 327661);
            CombatRoutine.AddSpell(HolyWordSalvation, 265202);
            CombatRoutine.AddSpell(Halo, 120517);
            CombatRoutine.AddSpell(Apotheosis, 200183);
            CombatRoutine.AddSpell(PrayerofMending, 33076);
            CombatRoutine.AddSpell(DivineHymn, 64843);
            CombatRoutine.AddSpell(ShadowWordPain, 589);
            CombatRoutine.AddSpell(PowerWordFortitude, 21562);
            CombatRoutine.AddSpell(Purify, 527);
            CombatRoutine.AddSpell(DivineStar, 110744);
            CombatRoutine.AddSpell(AngelicFeather, 121536);
            //Item
            CombatRoutine.AddItem(PhialofSerenity, 177278);
            CombatRoutine.AddItem(SpiritualHealingPotion, 171267);

            //Toggle
            CombatRoutine.AddToggle("Auto Target");
            CombatRoutine.AddToggle("DPS Auto Target");
            CombatRoutine.AddToggle("OOC");
            CombatRoutine.AddToggle("Dispel");
            CombatRoutine.AddToggle("Mouseover");
            //Macro
            CombatRoutine.AddMacro(Trinket1);
            CombatRoutine.AddMacro(Trinket2);
            CombatRoutine.AddMacro(Purify + "MO");
            CombatRoutine.AddMacro("Stopcast", "F10");
            CombatRoutine.AddMacro(Player);
            CombatRoutine.AddMacro(Party1);
            CombatRoutine.AddMacro(Party2);
            CombatRoutine.AddMacro(Party3);
            CombatRoutine.AddMacro(Party4);
            CombatRoutine.AddMacro("Assist");
            CombatRoutine.AddMacro("raid1");
            CombatRoutine.AddMacro("raid2");
            CombatRoutine.AddMacro("raid3");
            CombatRoutine.AddMacro("raid4");
            CombatRoutine.AddMacro("raid5");
            CombatRoutine.AddMacro("raid6");
            CombatRoutine.AddMacro("raid7");
            CombatRoutine.AddMacro("raid8");
            CombatRoutine.AddMacro("raid9");
            CombatRoutine.AddMacro("raid10");
            CombatRoutine.AddMacro("raid11");
            CombatRoutine.AddMacro("raid12");
            CombatRoutine.AddMacro("raid13");
            CombatRoutine.AddMacro("raid14");
            CombatRoutine.AddMacro("raid15");
            CombatRoutine.AddMacro("raid16");
            CombatRoutine.AddMacro("raid17");
            CombatRoutine.AddMacro("raid18");
            CombatRoutine.AddMacro("raid19");
            CombatRoutine.AddMacro("raid20");
            CombatRoutine.AddMacro("raid21");
            CombatRoutine.AddMacro("raid22");
            CombatRoutine.AddMacro("raid23");
            CombatRoutine.AddMacro("raid24");
            CombatRoutine.AddMacro("raid25");
            CombatRoutine.AddMacro("raid26");
            CombatRoutine.AddMacro("raid27");
            CombatRoutine.AddMacro("raid28");
            CombatRoutine.AddMacro("raid29");
            CombatRoutine.AddMacro("raid30");
            CombatRoutine.AddMacro("raid31");
            CombatRoutine.AddMacro("raid32");
            CombatRoutine.AddMacro("raid33");
            CombatRoutine.AddMacro("raid34");
            CombatRoutine.AddMacro("raid35");
            CombatRoutine.AddMacro("raid36");
            CombatRoutine.AddMacro("raid37");
            CombatRoutine.AddMacro("raid38");
            CombatRoutine.AddMacro("raid39");
            CombatRoutine.AddMacro("raid40");

            //Prop
          //  CombatRoutine.AddProp(SwapSpeed, SwapSpeed + "Speed ", SwapSpeedList, "Speed at which to change targets, it is in Milliseconds, to convert to seconds please divide by 1000. If you don't understand, please leave at at default setting", "Targeting", 1250);
            CombatRoutine.AddProp(Fleshcraft, "Fleshcraft", API.numbPercentListLong, "Life percent at which " + Fleshcraft + " is used, set to 0 to disable set 100 to use it everytime", "Defense", 0);
            CombatRoutine.AddProp(PhialofSerenity, PhialofSerenity + " Life Percent", API.numbPercentListLong, " Life percent at which" + PhialofSerenity + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(SpiritualHealingPotion, SpiritualHealingPotion + " Life Percent", API.numbPercentListLong, " Life percent at which" + SpiritualHealingPotion + " is used, set to 0 to disable", "Defense", 40);

            CombatRoutine.AddProp("Use Covenant", "Use " + "Covenant Ability", CDUsageWithAOE, "Use " + "Covenant" + "On Cooldown, with Cooldowns, On AOE, Not Used and meets the below healing perecents", "Cooldowns", 1);
            CombatRoutine.AddProp("QuakingHelper", "Quaking Helper", false, "Will cancel casts on Quaking", "Generic");

            CombatRoutine.AddProp(AngelicFeather, AngelicFeather, false, "Use Angelic Feather if talented", "Movement");

            CombatRoutine.AddProp("Tank Health", "Tank Health", API.numbPercentListLong, "Life percent at which " + "Tank Health" + "needs to be at to target during DPS Targeting", "Targeting", 75);
            CombatRoutine.AddProp("Other Members Health", "Other Members Health", API.numbPercentListLong, "Life percent at which " + "Other Members Health" + "needs to be at to targeted during DPS Targeting", "Targeting", 35);
            CombatRoutine.AddProp("Player Health", "Player Health", API.numbPercentListLong, "Life percent at which " + "Player Health" + "needs to be at to targeted above all else", "Targeting", 35);
            CombatRoutine.AddProp(AoEDPS, "Number of units needed to be above DPS Health Percent to DPS in party ", API.numbPartyList, " Units above for DPS ", "Targeting", 2);
            CombatRoutine.AddProp(AoEDPSRaid, "Number of units needed to be above DPS Health Percent to DPS in Raid ", API.numbRaidList, " Units above for DPS ", "Targeting", 7);
            CombatRoutine.AddProp(AoEDPSH, "Life Percent for units to be above for DPS and below to return back to Healing", API.numbPercentListLong, "Health percent at which DPS in party" + "is used,", "Targeting", 75);
            CombatRoutine.AddProp(AoEDPSHRaid, "Life Percent for units to be above for DPS and below to return back to Healing in raid", API.numbPercentListLong, "Health percent at which DPS" + "is used,", "Targeting", 75);

            CombatRoutine.AddProp("Use PoM", "Select your PoM Target Role", PoMTarget, "Select Your PoM Target Role", "PoM", 1);

            CombatRoutine.AddProp(Heal, Heal + " Life Percent", API.numbPercentListLong, "Life percent at which " + Heal + " is used, set to 0 to disable", "Healing", 80);
            CombatRoutine.AddProp(FlashHeal, FlashHeal + " Life Percent", API.numbPercentListLong, "Life percent at which " + FlashHeal + " is used, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(PrayerofMending, PrayerofMending + " Life Percent", API.numbPercentListLong, "Life percent at which " + PrayerofMending + " is used, set to 0 to disable", "Healing", 100);
            CombatRoutine.AddProp(HolyWordSerenity, HolyWordSerenity + " Life Percent", API.numbPercentListLong, "Life percent at which " + HolyWordSerenity + " is used, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(Renew, Renew + " Life Percent", API.numbPercentListLong, "Life percent at which " + Renew + " is used, set to 0 to disable", "Healing", 85);
            CombatRoutine.AddProp(FaeGuardians, FaeGuardians + " Life Percent", API.numbPercentListLong, "Life percent at which " + FaeGuardians + " is used, set to 0 to disable", "Healing", 10);
            CombatRoutine.AddProp(GuardianSpirit, GuardianSpirit + " Life Percent", API.numbPercentListLong, "Life percent at which " + GuardianSpirit + " is used, set to 0 to disable", "Healing", 15);
            CombatRoutine.AddProp(Halo, Halo + " Life Percent", API.numbPercentListLong, "Life percent at which " + Halo + " is used when AoE Number of members are at, set to 0 to disable", "Healing", 50);
            CombatRoutine.AddProp(HolyWordSanctify, HolyWordSanctify + " Life Percent", API.numbPercentListLong, "Life percent at which " + HolyWordSanctify + " is used when AoE Number of members are at, set to 0 to disable", "Healing", 55);
            CombatRoutine.AddProp(HolyWordSalvation, HolyWordSalvation + " Life Percent", API.numbPercentListLong, "Life percent at which " + HolyWordSalvation + " is used when AoE Number of members are at life percent, if talented, set to 0 to disable", "Healing", 40);
            CombatRoutine.AddProp(BoonoftheAscended, BoonoftheAscended + " Life Percent", API.numbPercentListLong, "Life percent at which " + BoonoftheAscended + " is used when AoE Number of members are at life percent, if is your Cov, set to 0 to disable", "Healing", 60);
            CombatRoutine.AddProp(CoH, CoH + " Life Percent", API.numbPercentListLong, "Life percent at which " + CoH + " is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 75);
            CombatRoutine.AddProp(PoH, PoH + " Life Percent", API.numbPercentListLong, "Life percent at which " + PoH + " is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 60);
            CombatRoutine.AddProp(DivineHymn , DivineHymn + " Life Percent", API.numbPercentListLong, "Life percent at which " + DivineHymn + " is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 45);
            CombatRoutine.AddProp(DivineStar, DivineStar + " Life Percent", API.numbPercentListLong, "Life percent at which " + DivineStar + " is used when AoE Number of members are at life percent, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(AoE, "Number of units for AoE Healing ", API.numbPartyList, " Units for AoE Healing", "Healing", 3);
            CombatRoutine.AddProp(AoERaid, "Number of units for AoE Healing in raid ", API.numbRaidList, " Units for AoE Healing in raid", "Healing", 7);

            CombatRoutine.AddProp("Trinket1", "Trinket1 usage", CDUsageWithAOE, "When should trinket1 be used", "Trinket", 1);
            CombatRoutine.AddProp("Trinket2", "Trinket2 usage", CDUsageWithAOE, "When should trinket1 be used", "Trinket", 1);
        }


        public override void Pulse()
        {
            var UnitLowestParty = API.UnitLowestParty();
            var UnitLowestRaid = API.UnitLowestRaid();
            var LowestTankUnitParty = API.UnitLowestParty(out string lowestTankParty);
            var LowestTankUnitRaid = API.UnitLowestRaid(out string lowestTankRaid);

            if (!API.PlayerIsMounted && !API.PlayerSpellonCursor && (IsOOC || API.PlayerIsInCombat) && IsNotEating && (!TargetHasDebuff("Gluttonous Miasma") || IsMouseover && !MouseoverHasDebuff("Gluttonous Miasma")))
            {
                if (API.PlayerCurrentCastTimeRemaining > 40 && QuakingHelper && Quaking)
                {
                    API.CastSpell("Stopcast");
                    API.WriteLog("Debuff Time Remaining for Quake : " + API.PlayerDebuffRemainingTime(Quake));
                    return;
                }
                if (API.CanCast(AngelicFeather) && AngelicFeatherTalent && API.PlayerIsMoving && UseAngel)
                {
                    API.CastSpell(AngelicFeather);
                    return;
                }
                #region Dispell
                if (IsDispell)
                {
                    if (API.CanCast(Purify) && !ChannelingDivine && NotChanneling)
                    {
                        for (int i = 0; i < DispellList.Length; i++)
                        {
                            if (TargetHasDispellAble(DispellList[i]) && (!TargetHasDispellAble("Frozen Binds") || TargetHasDispellAble("Frozen Binds") && API.TargetDebuffRemainingTime("Frozen Binds", false) <= 1000 || !TargetHasDispellAble("Bursting") || TargetHasDispellAble("Bursting") && API.TargetDebuffStacks("Bursting") >= 2))
                            {
                                API.CastSpell(Purify);
                                return;
                            }
                        }
                    }
                    if (API.CanCast(Purify) && IsMouseover && !ChannelingDivine && NotChanneling)
                    {
                        for (int i = 0; i < DispellList.Length; i++)
                        {
                            if (MouseouverHasDispellAble(DispellList[i]))
                            {
                                API.CastSpell(Purify + "MO");
                                return;
                            }
                        }
                    }
                }
                #endregion
                if (GSCheck && InRange)
                {
                    API.CastSpell(GuardianSpirit);
                    return;
                }
                if (HWSCheck && InRange && (!QuakingHelper || QuakingHWSalv && QuakingHelper))
                {
                    API.CastSpell(HolyWordSalvation);
                    return;
                }
                if (DHCheck && InRange && (!QuakingHelper || QuakingDivine && QuakingHelper))
                {
                    API.CastSpell(DivineHymn);
                    return;
                }
                if (HWSancitfyCheck && InRange)
                {
                    API.CastSpell(HolyWordSanctify);
                    return;
                }
                if (HolyWordSerenityCheck && InRange)
                {
                    API.CastSpell(HolyWordSerenity);
                    return;
                }
                if (CoHCheck & InRange)
                {
                    API.CastSpell(CoH);
                    return;
                }
                if (PoMCheck && InRange && (!QuakingHelper || QuakingPoM && QuakingHelper))
                {
                    API.CastSpell(PrayerofMending);
                    return;
                }
                if (API.CanCast(DivineStar) && DivineStarTalent && DivineStarAoE && InRange && (API.PlayerCanAttackTarget || !API.PlayerCanAttackTarget))
                {
                    API.CastSpell(DivineStar);
                    return;
                }
                if (PoHCheck && InRange && (!QuakingHelper || QuakingPoH && QuakingHelper))
                {
                    API.CastSpell(PoH);
                    return;
                }
                if (NecrolordCheck && InRange)
                {
                    API.CastSpell(UnholyNova);
                    return;
                }
                if (RenewCheck && InRange)
                {
                    API.CastSpell(Renew);
                    return;
                }
                if (HaloCheck && InRange && (!QuakingHelper || QuakingHalo && QuakingHelper))
                {
                    API.CastSpell(Halo);
                    return;
                }
                if (FlashHealCheck && InRange && (!QuakingHelper || QuakingFlash && QuakingHelper))
                {
                    API.CastSpell(FlashHeal);
                    return;
                }
                if (HealCheck && InRange && (!QuakingHelper || QuakingHeal && QuakingHelper))
                {
                    API.CastSpell(Heal);
                    return;
                } 
                if (KyrianCheck && InRange && (!QuakingHelper || QuakingBoon && QuakingHelper))
                {
                    API.CastSpell(BoonoftheAscended);
                    return;
                }
                if (NightFaeCheck && InRange)
                {
                    API.CastSpell(FaeGuardians);
                    return;
                }
                //DPS
                if (API.PlayerIsInCombat)
                {
                    if (VenthyrCheck && InRange && API.TargetHealthPercent > 0 && API.PlayerCanAttackTarget && (!QuakingHelper || QuakingMind && QuakingHelper))
                    {
                        API.CastSpell(Mindgames);
                        return;
                    }
                    if (API.CanCast(HolyWordChastise) && InRange && Mana >= 2 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !ChannelingDivine && API.TargetHealthPercent > 0 && API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(HolyWordChastise);
                        return;
                    }
                    if (API.CanCast(HolyFire) && InRange && Mana >= 1 && !ChannelingDivine && !API.PlayerIsMoving && API.TargetHealthPercent > 0 && API.PlayerCanAttackTarget && (!QuakingHelper || QuakingHolyFire && QuakingHelper))
                    {
                        API.CastSpell(HolyFire);
                        return;
                    }
                    if (API.CanCast(ShadowWordPain) && InRange && Mana >= 1 && !API.TargetHasDebuff(ShadowWordPain) && (API.PlayerIsMoving || !API.PlayerIsMoving) && !ChannelingDivine && API.TargetHealthPercent > 0 && API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(ShadowWordPain);
                        return;
                    }
                    if (API.CanCast(HolyNova) && API.TargetUnitInRangeCount >= 3 && API.TargetRange <= 12 && !ChannelingDivine && Mana >= 2 && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.TargetHealthPercent > 0 && API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(HolyNova);
                        return;
                    }
                    if (API.CanCast(AscendedNova) && PlayerCovenantSettings == "Kyrian" && API.TargetRange <= 8 && !ChannelingDivine && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(AscendedNova);
                        return;
                    }
                    if (API.CanCast(AscendedBlast) && PlayerCovenantSettings == "Kyrian" && !ChannelingDivine && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.TargetHealthPercent > 0 && API.PlayerCanAttackTarget && (!QuakingHelper || QuakingBoon && QuakingHelper))
                    {
                        API.CastSpell(AscendedBlast);
                        return;
                    }
                    if (API.CanCast(Smite) && !ChannelingDivine && Mana >= 1 && !ChannelingDivine && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.TargetHealthPercent > 0 && API.PlayerCanAttackTarget && (!QuakingHelper || QuakingSmite && QuakingHelper))
                    {
                        API.CastSpell(Smite);
                        return;
                    }
                }
                // Auto Target
                if (IsAutoSwap && (IsOOC || API.PlayerIsInCombat))
                {
                    if (API.PlayerHealthPercent <= PlayerHP && API.TargetIsUnit() != "player")
                    {
                        API.CastSpell(Player);
                        return;
                    }
                    if (!API.PlayerIsInGroup && !API.PlayerIsInRaid)
                    {
                        if (API.PlayerHealthPercent <= PlayerHP && API.TargetIsUnit() != "player")
                        {
                            API.CastSpell(Player);
                            return;
                        }
                    }
                    if (API.PlayerIsInGroup && !API.PlayerIsInRaid)
                    {
                        for (int j = 0; j < DispellList.Length; j++)
                            for (int i = 0; i < API.partyunits.Length; i++)
                            {

                                if (UnitHasDispellAble(DispellList[j], API.partyunits[i]) && IsDispell && !API.SpellISOnCooldown(Purify) && API.TargetIsUnit() != API.partyunits[i])
                                {
                                    API.CastSpell(API.partyunits[i]);
                                    return;
                                }
                                if (UnitLowestParty != "none" && API.UnitHealthPercent(UnitLowestParty) <= 10 && API.TargetIsUnit() != UnitLowestParty)
                                {
                                    API.CastSpell(UnitLowestParty);
                                    return;
                                }
                                if (API.UnitRoleSpec(API.partyunits[i]) == 999 && !API.UnitHasBuff(PrayerofMending, API.partyunits[i]) && !API.SpellISOnCooldown(PrayerofMending) && API.UnitHealthPercent(API.partyunits[i]) > 0 && API.TargetIsUnit() != API.partyunits[i])
                                {
                                    API.CastSpell(API.partyunits[i]);
                                    return;
                                }
                                if (LowestTankUnitParty != "none" && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(LowestTankUnitParty) <= TankHealth && API.TargetIsUnit() != LowestTankUnitParty)
                                {
                                    API.CastSpell(LowestTankUnitParty);
                                    SwapWatch.Restart();
                                    return;
                                }
                                if (UnitLowestParty != "none" && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(UnitLowestParty) <= UnitHealth && API.TargetIsUnit() != UnitLowestParty)
                                {
                                    API.CastSpell(UnitLowestParty);
                                    SwapWatch.Restart();
                                    return;
                                }
                                if (IsDPS && !API.PlayerCanAttackTarget && API.UnitRoleSpec(API.partyunits[i]) == API.TankRole && !API.MacroIsIgnored("Assist") && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber && API.UnitRange(API.partyunits[i]) <= 40 && API.UnitHealthPercent(API.partyunits[i]) > 0 && API.PlayerIsInCombat && API.TargetIsUnit() != API.partyunits[i])
                                {
                                    API.CastSpell(API.partyunits[i]);
                                    API.CastSpell("Assist");
                                    SwapWatch.Restart();
                                    return;
                                }
                            }
                    }
                    if (API.PlayerIsInRaid)
                    {
                        for (int t = 0; t < API.raidunits.Length; t++)
                        {
                            if (UnitLowestRaid != "none" && API.UnitHealthPercent(UnitLowestRaid) <= 10 && API.TargetIsUnit() != UnitLowestRaid && !UnitHasDebuff("Gluttonous Miasma", UnitLowestRaid))
                            {
                                API.CastSpell(UnitLowestRaid);
                                return;
                            }
                            if (API.UnitRoleSpec(API.raidunits[t]) == 999 && !API.UnitHasBuff(PrayerofMending, API.raidunits[t]) && !API.SpellISOnCooldown(PrayerofMending) && API.UnitHealthPercent(API.raidunits[t]) > 0 && API.TargetIsUnit() != API.raidunits[t])
                            {
                                API.CastSpell(API.raidunits[t]);
                                return;
                            }
                            if (LowestTankUnitRaid != "none" && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && !UnitHasDebuff("Gluttonous Miasma", LowestTankUnitRaid) && API.UnitHealthPercent(LowestTankUnitRaid) <= TankHealth && API.TargetIsUnit() != LowestTankUnitRaid)
                            {
                                API.CastSpell(LowestTankUnitRaid);
                                SwapWatch.Restart();
                                return;
                            }
                            if (UnitLowestRaid != "none" && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && !UnitHasDebuff("Gluttonous Miasma", UnitLowestRaid) && API.UnitHealthPercent(UnitLowestRaid) <= UnitHealth && API.TargetIsUnit() != UnitLowestRaid)
                            {
                                API.CastSpell(UnitLowestRaid);
                                SwapWatch.Restart();
                                return;
                            }
                            if (IsDPS && !API.PlayerCanAttackTarget && API.UnitRange(API.raidunits[t]) <= 40 && API.UnitRoleSpec(API.raidunits[t]) == API.TankRole && !API.MacroIsIgnored("Assist") && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber && API.UnitHealthPercent(API.raidunits[t]) > 0 && API.PlayerIsInCombat && API.TargetIsUnit() != API.raidunits[t])
                            {
                                API.CastSpell(API.raidunits[t]);
                                SwapWatch.Restart();
                                API.CastSpell("Assist");
                                return;
                            }
                        }
                    }
                }

            }
        }
        public override void CombatPulse()
        {
            if (API.CanCast(Fleshcraft) && PlayerCovenantSettings == "Necrolord" && API.PlayerHealthPercent <= FleshcraftPercentProc && NotChanneling && !ChannelingDivine && !API.PlayerIsMoving)
            {
                API.CastSpell(Fleshcraft);
                return;
            }
            if (API.PlayerItemCanUse(PhialofSerenity) && API.PlayerItemRemainingCD(PhialofSerenity) == 0 && API.PlayerHealthPercent <= PhialofSerenityLifePercent)
            {
                API.CastSpell(PhialofSerenity);
                return;
            }
            if (API.PlayerItemCanUse(SpiritualHealingPotion) && API.PlayerItemRemainingCD(SpiritualHealingPotion) == 0 && API.PlayerHealthPercent <= SpiritualHealingPotionLifePercent)
            {
                API.CastSpell(SpiritualHealingPotion);
                return;
            }
            if (API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && IsTrinkets1)
            {
                API.CastSpell("Trinket1");
            }
            if (API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && IsTrinkets2)
            {
                API.CastSpell("Trinket2");
            }
        }

        public override void OutOfCombatPulse()
        {
            if (!API.PlayerHasBuff(PowerWordFortitude) && API.CanCast(PowerWordFortitude))
            {
                API.CastSpell(PowerWordFortitude);
                return;
            }
        }

    }
}


