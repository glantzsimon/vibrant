﻿using K9.DataAccessLayer.Enums;
using K9.Globalisation;
using System.ComponentModel.DataAnnotations;

namespace K9.DataAccessLayer.Models
{
    public partial class HealthQuestionnaire
    {

        #region Cbs

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.WhiteSpotsOnNailsLabel)]
        public EYesNo? WhiteSpotsOnNails { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AnaemiaLabel)]
        public EYesNo? Anaemia { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.PostExertionalMalaiseLabel)]
        public EYesNo? PostExertionalMalaise { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CrampsTremorsTwitchesLabel)]
        public EYesNo? CrampsTremorsTwitches { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HistoryOfchronicFatigueOrFibromyalgiaLabel)]
        public EYesNo? HistoryOfchronicFatigueOrFibromyalgia { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.MigrainesLabel)]
        public EYesNo? Migraines { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.POTSLabel)]
        public EYesNo? POTS { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DepressionAnxietyLabel)]
        public EYesNo? DepressionAnxiety { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.BrainFogLabel)]
        public EYesNo? BrainFog { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.InsomniaLabel)]
        public EYesNo? Insomnia { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.NightSweatsLabel)]
        public EYesNo? NightSweats { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LowMorningEnergyLabel)]
        public EYesNo? LowMorningEnergy { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RacingThoughtsLabel)]
        public EYesNo? RacingThoughts { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.InnerTensionLabel)]
        public EYesNo? InnerTension { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LoudNoisesBrightLightsLabel)]
        public EYesNo? LoudNoisesBrightLights { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CoarseThinEyebrowsLabel)]
        public EYesNo? CoarseThinEyebrows { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.AmmoniaSmellLabel)]
        public EYesNo? AmmoniaSmell { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SugarCrashesLabel)]
        public EYesNo? SugarCrashes { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.OCDLabel)]
        public EYesNo? OCD { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ChronicViralInfectionsLabel)]
        public EYesNo? ChronicViralInfections { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SpiderVeinsLabel)]
        public EYesNo? SpiderVeins { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.StretchMarksLabel)]
        public EYesNo? StretchMarks { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.FrequentNighttimeUrinationLabel)]
        public EYesNo? FrequentNighttimeUrination { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.HerpesLabel)]
        public EYesNo? Herpes { get; set; }

        [UIHint("YesNo")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.IrritabilityLabel)]
        public EYesNo? Irritability { get; set; }

        public double GetCbsScore()
        {
            var totalScore = 30;
            var score = 0;

            if (ChronicViralInfections == EYesNo.Yes)
            {
                score++;
            }

            if (SpiderVeins == EYesNo.Yes)
            {
                score++;
            }

            if (StretchMarks == EYesNo.Yes)
            {
                score++;
            }

            if (FrequentNighttimeUrination == EYesNo.Yes)
            {
                score++;
            }

            if (Herpes == EYesNo.Yes)
            {
                score++;
            }

            if (Irritability == EYesNo.Yes)
            {
                score++;
            }

            if (OCD == EYesNo.Yes)
            {
                score++;
            }

            if (SugarCrashes == EYesNo.Yes)
            {
                score++;
            }

            if (AmmoniaSmell == EYesNo.Yes)
            {
                score++;
                score++;
                score++;
            }

            if (CoarseThinEyebrows == EYesNo.Yes)
            {
                score++;
                score++;
            }

            if (LoudNoisesBrightLights == EYesNo.Yes)
            {
                score++;
                score++;
            }

            if (RacingThoughts == EYesNo.Yes || InnerTension == EYesNo.Yes)
            {
                score++;
            }

            if (LowMorningEnergy == EYesNo.Yes)
            {
                score++;
            }

            if (NightSweats == EYesNo.Yes)
            {
                score++;
            }

            if (Insomnia == EYesNo.Yes)
            {
                score++;
            }

            if (BrainFog == EYesNo.Yes)
            {
                score++;
                score++;
            }

            if (DepressionAnxiety == EYesNo.Yes)
            {
                score++;
                score++;
            }

            if (POTS == EYesNo.Yes)
            {
                score++;
                score++;
            }

            if (Migraines == EYesNo.Yes)
            {
                score++;
            }

            if (HistoryOfchronicFatigueOrFibromyalgia == EYesNo.Yes)
            {
                score++;
                score++;
            }

            if (PostExertionalMalaise == EYesNo.Yes)
            {
                score++;
            }

            if (WhiteSpotsOnNails == EYesNo.Yes)
            {
                score++;
                score++;
            }

            if (Anaemia == EYesNo.Yes)
            {
                score++;
                score++;
            }

            if (CrampsTremorsTwitches == EYesNo.Yes)
            {
                score++;
            }

            return score / totalScore;
        }

        #endregion

        public bool IsCbsAndMethylationComplete() =>
            WhiteSpotsOnNails.HasValue &&
            Anaemia.HasValue &&
            PostExertionalMalaise.HasValue &&
            CrampsTremorsTwitches.HasValue &&
            HistoryOfchronicFatigueOrFibromyalgia.HasValue &&
            Migraines.HasValue &&
            POTS.HasValue &&
            DepressionAnxiety.HasValue &&
            BrainFog.HasValue &&
            Insomnia.HasValue &&
            NightSweats.HasValue &&
            LowMorningEnergy.HasValue &&
            RacingThoughts.HasValue &&
            InnerTension.HasValue &&
            LoudNoisesBrightLights.HasValue &&
            CoarseThinEyebrows.HasValue &&
            AmmoniaSmell.HasValue &&
            OCD.HasValue &&
            SugarCrashes.HasValue &&
            ChronicViralInfections.HasValue &&
            SpiderVeins.HasValue &&
            StretchMarks.HasValue &&
            FrequentNighttimeUrination.HasValue &&
            Herpes.HasValue &&
            Irritability.HasValue;
    }
}