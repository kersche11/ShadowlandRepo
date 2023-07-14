using System.Collections.Generic;
using UnityEngine;
using static CUAS.MMT.SoundManager;


namespace CUAS.MMT
{
    //Singleton: Nur einmal verwenden
    public class SoundManager : Singleton<SoundManager>
    {

        //https://docs.unity3d.com/2019.3/Documentation/Manual/class-AudioSource.html

        //AudioClip PlayerHieb;
        //AudioClip collect; //ok
        //AudioClip win;
        //AudioClip jump;
        //AudioClip hitEnemy;
        //AudioClip hitPlayer;
        //AudioClip alert;
        //AudioClip miss;
        //AudioClip bump;
        //AudioClip doorbutton;


        GameObject soundGameObject = null;
        AudioSource audioSource = null;

       // GameObject soundGameObject2 = null;
       // AudioSource audioSource2 = null;
        public AudioSource GetAudioSource()
        {
        
           
            return audioSource;

        }



        //new Soundlist
        private List<AudioClip> soundList = new List<AudioClip>();



        public enum Sound
        {

            //Player
            Player_Breath_Run,
            Player_Landing_Jump,
            Player_Dodge,
            Player_HeartBeat,
            Player_Run_Sand,
            Player_Run_Snow,
            Player_Run_Stone,
            Player_Run_Meadow,
            Player_Run_Wood,
            Player_GetDamage,
            Player_SwordSlash,
            Player_SwordSlash2,
            Player_CarryStone,
            Player_PickUpStone,         
            Player_Death,
           
            //Dungeon1
            Dungeon_One_FAIL,
            
            //Truhe
            Kiste_erscheinen,
            Kiste_öffnen,

            //Skeleton
            SkeletonImpact,
            SkeletonAttack,
            SkeletonDeath,

            //Bear           
            BearAttack,
            BearBrull,
            BearImpact,
            BearDeath,

            //Metalon       
            MetalonAttack,
            MetalonImpact,
            MetalonDeath,


            //Dragon
            DragonAufkommen,
            DragonImpact,
            DragonRoar,         
            DragonDeath,
           

            //DungeonTwo          
            DungeonTwoPressurePlate,
            DungeonTwoFalle


        }


      


        public void Init()
        {

            if (soundGameObject == null)
            {

                soundGameObject = new GameObject("Sound");
                audioSource = soundGameObject.AddComponent<AudioSource>();

               // soundGameObject2 = new GameObject("Sound2");
               // audioSource2 = soundGameObject2.AddComponent<AudioSource>();
               // audioSource2.loop = true;
                //add PlayerSounds
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_,Atmen_Run"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Aufkommen_Sprung"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Ausweichen"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Herzschlag"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Run_Sand"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Run_Schnee"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Run_tein"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Run_Wiese"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Run_Wood"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Schaden_bekommen"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Schwert_hieb"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Schwert_hieb_2"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Stein_gehen"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Stein_heben"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/PlayerSounds/_Player_Tod"));

                //Dungeon 1
                soundList.Add(Resources.Load<AudioClip>("Sounds/EnvironmentSounds/Dun1_Lose_Sound"));

                //Truhe
                soundList.Add(Resources.Load<AudioClip>("Sounds/TruheSounds/_Allgemein_Kiste_erscheint"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/TruheSounds/_Allgemein_Kiste_öffnen"));

                //Skeleton
                soundList.Add(Resources.Load<AudioClip>("Sounds/SkeletonSounds/_Skelett_Hit"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/SkeletonSounds/_Skelett_Schwert_hieb"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/SkeletonSounds/_Skelett_Tod"));


                //Bär
                soundList.Add(Resources.Load<AudioClip>("Sounds/BearSounds/_Bär_Attack"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/BearSounds/_Bär_Brüllen"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/BearSounds/_Bär_Schaden_bekommen"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/BearSounds/_Bär_Tod"));



                //Metalon
                soundList.Add(Resources.Load<AudioClip>("Sounds/MetalonSounds/_Metalon_Angriff"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/MetalonSounds/_Metalon_Hit"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/MetalonSounds/_Metalon_Tod"));

                //Dragon
                soundList.Add(Resources.Load<AudioClip>("Sounds/DragonSounds/_Dragon_Aufkommen"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/DragonSounds/_Dragon_Schaden"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/DragonSounds/_Dragon_Schrei"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/DragonSounds/_Dragon_Tod"));

                //Dungeon 2
                soundList.Add(Resources.Load<AudioClip>("Sounds/DungeonZwei/_Dun2_Druckplatte"));
                soundList.Add(Resources.Load<AudioClip>("Sounds/DungeonZwei/_Dun2_Falle"));







            }


        }


        public AudioClip GetClip(Sound sound)
        {
            AudioClip clip = soundList[(int)sound];
            return clip;
        }
        public void StopSound()
        {
          
            audioSource.Stop();
        }

        public void StopLoop()
        {

            //audioSource.Stop();
        }

      



            public void PlaySound(Sound sound, bool check = false)
        {
            if (check)
            {
               
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(soundList[(int)sound]);
                }
            }
            else
            {
                
                audioSource.PlayOneShot(soundList[(int)sound]);
            }

        

        }


    }

}
