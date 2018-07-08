﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using Vocalization.Framework;

namespace Vocalization
{
    /// <summary>
    /// TODO:
    /// 
    /// Make a directory where all of the wav files will be stored. (Done?)
    /// Load in said wav files.(Done?)
    /// 
    /// Find way to add in supported dialogue via some sort of file system. (Done?)
    ///     -Make each character folder have a .json that has....
    ///         -Character Name(Done?)
    ///         -Dictionary of supported dialogue lines and values for .wav files. (Done?)
    ///         -*Note* The value for the dialogue dictionaries is the name of the file excluding the .wav extension.
    /// 
    /// Find way to play said wave files. (Done?)
    /// 
    /// Sanitize input to remove variables such as pet names, farm names, farmer name.
    /// 
    /// Add in dialogue for npcs into their respective VoiceCue.json files.
    /// 
    /// Add support for different kinds of menus. TV, shops, etc.
    /// </summary>
    public class Vocalization : Mod
    {
        public static IModHelper ModHelper;
        public static IMonitor ModMonitor;

        /// <summary>
        /// A string that keeps track of the previous dialogue said to ensure that dialogue isn't constantly repeated while the text box is open.
        /// </summary>
        public static string previousDialogue;

        /// <summary>
        /// Simple Sound Manager class that handles playing and stoping dialogue.
        /// </summary>
        public static SimpleSoundManager.Framework.SoundManager soundManager;

        /// <summary>
        /// The path to the folder where all of the NPC folders for dialogue .wav files are kept.
        /// </summary>
        public static string VoicePath = "";

        
        /// <summary>
        /// A dictionary that keeps track of all of the npcs whom have voice acting for their dialogue.
        /// </summary>
        public static Dictionary<string, CharacterVoiceCue> DialogueCues;

        public override void Entry(IModHelper helper)
        {
            StardewModdingAPI.Events.SaveEvents.AfterLoad += SaveEvents_AfterLoad;
            DialogueCues = new Dictionary<string, CharacterVoiceCue>();

            StardewModdingAPI.Events.GameEvents.UpdateTick += GameEvents_UpdateTick;
            StardewModdingAPI.Events.MenuEvents.MenuClosed += MenuEvents_MenuClosed;


            previousDialogue = "";

            ModMonitor = Monitor;
            ModHelper = Helper;

            soundManager = new SimpleSoundManager.Framework.SoundManager();
           
        }

        /// <summary>
        /// Runs whenever any onscreen menu is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuEvents_MenuClosed(object sender, StardewModdingAPI.Events.EventArgsClickableMenuClosed e)
        {
            //Clean out my previous dialogue when I close any sort of menu.
            previousDialogue = "";
        }

        /// <summary>
        /// Runs after the game is loaded to initialize all of the mod's files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveEvents_AfterLoad(object sender, EventArgs e)
        {
            initialzeDirectories();
            loadAllVoiceFiles();
        }

        /// <summary>
        /// Runs every game tick to check if the player is talking to an npc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameEvents_UpdateTick(object sender, EventArgs e)
        {
            if (Game1.currentSpeaker != null)
            {
                string speakerName = Game1.currentSpeaker.Name;
                if (Game1.activeClickableMenu.GetType() == typeof(StardewValley.Menus.DialogueBox))
                {
                    StardewValley.Menus.DialogueBox dialogueBox =(DialogueBox)Game1.activeClickableMenu;
                    string currentDialogue = dialogueBox.getCurrentString();
                    if (previousDialogue != currentDialogue)
                    {
                        ModMonitor.Log(speakerName);
                        previousDialogue = currentDialogue; //Update my previously read dialogue so that I only read the new string once when it appears.
                        ModMonitor.Log(currentDialogue); //Print out my dialogue.


                        //Do logic here to figure out what audio clips to play.
                        //Sanitize input here!
                        //Load all game dialogue files and then sanitize input for that???
                    }
                }
            }
        }

        /// <summary>
        /// Runs after loading.
        /// </summary>
        private void initialzeDirectories()
        {
            string basePath = ModHelper.DirectoryPath;
            string contentPath = Path.Combine(basePath, "Content");
            string audioPath = Path.Combine(contentPath, "Audio");
            string voicePath = Path.Combine(audioPath, "VoiceFiles");
            VoicePath = voicePath; //Set a static reference to my voice files directory.

            List<string> characterDialoguePaths = new List<string>();

            //Get a list of all characters in the game and make voice directories for them.
            foreach (var loc in Game1.locations)
            {
                foreach(var NPC in loc.characters)
                {
                    string characterPath = Path.Combine(voicePath, NPC.Name);
                    characterDialoguePaths.Add(characterPath);
                }
            }

            //Create a list of new directories if the corresponding character directory doesn't exist.
            //Note: A modder could also manually add in their own character directory for voice lines instead of having to add it via code.
            foreach(var dir in characterDialoguePaths)
            {
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            }
        }


        /// <summary>
        /// Loads in all of the .wav files associated with voice acting clips.
        /// </summary>
        public void loadAllVoiceFiles()
        {
            List<string> directories = Directory.GetDirectories(VoicePath).ToList();
            foreach(var dir in directories)
            {
                List<string> audioClips = Directory.GetFiles(dir, ".wav").ToList();
                //For every .wav file in every character voice clip directory load in the voice clip.
                foreach(var file in audioClips)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    soundManager.loadWavFile(ModHelper, fileName, file);
                    ModMonitor.Log("Loaded sound file: " + fileName+ " from: "+ file);
                }

                //Get the character dialogue cues (aka when the character should "speak") from the .json file.
                string voiceCueFile=Path.Combine(dir,"VoiceCues.json");
                string characterName = Path.GetFileName(dir);

                //If a file was not found, create one and add it to the list of character voice cues.
                if (!File.Exists(voiceCueFile))
                {
                    CharacterVoiceCue cue= new CharacterVoiceCue(characterName);
                    ModHelper.WriteJsonFile<CharacterVoiceCue>(Path.Combine(dir, "VoiceCues.json"), cue);
                    DialogueCues.Add(characterName, cue);
                }
                else
                {
                    CharacterVoiceCue cue=ModHelper.ReadJsonFile<CharacterVoiceCue>(voiceCueFile);
                    DialogueCues.Add(characterName,cue);
                }
            }
        }
    }
}
