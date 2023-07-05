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
           
          
          

            //Skeleton
            SkeletonRun,
            SkeletonAttack,
            SkeletonImpact,
            SkeletonDeath,

            //Bear
            BearRun,
            BearAttack,
            BearImpact,
            BearDeath,

            //Metalon
            MetalonRun,
            Metalonttack,
            MetalonImpact,
            MetalonDeath,


            //Dragon
            DragonWalk,
            DragonRun,
            DragonAttack,
            DragonImpact,
            DragonFly,
            DragonDeath,
            DragonRoar,


            //DungeonOne
            DungeonOneCutTree,
            DungeonOneEnemyRespawn,
            DungeonOneTreeRespawn,
            DungeonOneBGMusic,

            //DungeonTwo
            DungeonTwoTrapTrigger,
            DungeonTwoPressurePlate,
            DungeonTwoLanternsFlicker,
            DungeonTwoBGMusic,

            //DungeonThree
            DungeonThreeTrapTrigger,
            DungeonThreePressurePlate,
            DungeonThreeLanternsFlicker,
            DungeonThreeBGMusic,


            Collect,
            Win,
            Jump,
            HitEnemy,
            HitPlayer,
            Alert,
            Miss,
            Bump,
            Doorbutton
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

               

                //collect = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-33");
                //win = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-18");
                //jump = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-41");
                //hitEnemy = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-21");
                //hitPlayer = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-10");
                //alert = Resources.Load<AudioClip>("ENGALRT");
                //miss = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-25");
                //bump = Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-03");
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
