//Klassen skriven av Niklas Carlsson (9009072472)
//för projektarbete i Programmerings teknik i C# (dva105)
//Höstterminen 2012

using Microsoft.Xna.Framework.Audio;


namespace Tetris
{
    static class ljudklass
    {
        //Deklarering av ljudmotorn, ljudbanker samt ljudkategorier.
        private static AudioEngine audioEngine;
        private static WaveBank waveBank;
        private static SoundBank soundBank;
        private static AudioCategory musicCategory;

        //Variabel som används för att spela upp bakgrundsmusiken
        public static Cue CurrentSong;
        public static Cue slutljud;

        //Inläsning av ljudbiblioteken, startas från Game1.Initialize för att vara med från början.
        public static void LoadAudioContent()
        {
            audioEngine = new AudioEngine("Content/TetrisAudio.xgs");
            waveBank = new WaveBank(audioEngine, "Content/Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Content/Sound Bank.xsb");
        }

        //Ljud för start av programmet samt start av spelomgång
        public static void StartLjud()
        {
            Cue startljud = soundBank.GetCue("start");
            startljud.Play();
        }

        //Musiken i menyn, spelet och efter avslutat spel är tre olika tolkningar av Tetris. Alla spår är satta för att loopa,
        //och den inställningen har gjorts i XACT AAT.
        public static void MenyMusik()
        {
            CurrentSong = soundBank.GetCue("Meny");
            CurrentSong.Play();
        }

        public static void SpelMusik()
        {
            CurrentSong = soundBank.GetCue("melodispel");
            CurrentSong.Play();
        }

        public static void SpelSlutMusik()
        {
            CurrentSong = soundBank.GetCue("melodislut");
            CurrentSong.Play();
        }

        //Ljud för att markera att spelomgången är slut
        public static void SpelSlutLjud()
        {
            slutljud = soundBank.GetCue("slut");
            slutljud.Play();
        }

        //Ljud för nedsättning av block
        public static void Block()
        {
            Cue blockljud = soundBank.GetCue("block");
            blockljud.Play();
        }

        //Ljud som först användes vid menyval, men används nu som referensljud vid inställning av ljudvolymen på effektljuden.
        public static void MenyVal()
        {
            Cue menyval = soundBank.GetCue("menyval");
            menyval.Play();
        }

        //Ljud för när en rad tas bort
        public static void Rad()
        {
            Cue radljud = soundBank.GetCue("rad");
            radljud.Play();
        }


        //används för att ändra volymen på allting klassat som "Music" i XACT-filen
        //Metoden kallas inne i spelet från menyn, under undertiteln Audio, och tar input från Reglage-klassen.
        public static void MusikVolym(float volym)
        {
            musicCategory = audioEngine.GetCategory("Music");
            musicCategory.SetVolume(volym);
        }

        //Ställe rom ljudvolymen på allting under kategorin "Default" i XACT-filen samt spelar upp ett referensljud.
        //Metoden kallas inne i spelet från menyn, under undertiteln FX, och tar input från Reglage-klassen.
        public static void EffektVolym(float volym)
        {
            musicCategory = audioEngine.GetCategory("Default");
            musicCategory.SetVolume(volym);
            MenyVal();
        }
    }
}
