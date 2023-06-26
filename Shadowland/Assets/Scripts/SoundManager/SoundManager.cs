using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CUAS.MMT
{
    //Singleton: Nur einmal verwenden
    public class SoundManager : Singleton<SoundManager>
    {

        //https://docs.unity3d.com/2019.3/Documentation/Manual/class-AudioSource.html

        AudioClip collect; //ok
        AudioClip win;
        AudioClip jump;
        AudioClip hitEnemy;
        AudioClip hitPlayer;
        AudioClip alert;
        AudioClip miss;
        AudioClip bump;
        AudioClip doorbutton;
        

        GameObject soundGameObject = null;
        AudioSource audioSource = null;
        public AudioSource GetAudioSource()
        {
            return audioSource;
        }



        //new Soundlist
        private List<AudioClip> soundList = new List<AudioClip>();



        public enum Sound
        {

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

            //Player
            PlayerRun,
            PlayerAttack,
            PlayerImpact,
            PlayerDeath,
            PlayerDodge,
            PlayerPickUp,
            PlayerJump,

            //Dragon
            DragonWalk,
            DragonRun,
            DragonAttack,
            DragonImpact,
            DragonFly,
            DragonDeath,
            DragonRoar,
            //
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


             public enum Skeleton
        {
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

                //add Sounds to List
                soundList.Add(Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-33"));
                soundList.Add(Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-18"));
                soundList.Add(Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-41"));
                soundList.Add(Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-21"));
                soundList.Add(Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-10"));
                soundList.Add(Resources.Load<AudioClip>("ENGALRT"));
                soundList.Add(Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-25"));
                soundList.Add(Resources.Load<AudioClip>("CasualGameSounds/DM-CGS-03"));
                soundList.Add(Resources.Load<AudioClip>("zelda"));
                

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
        
        public void PlaySound(Sound sound, bool check = false)
        {
            if(check)
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

            //Play sounds
            

            //if (sound == Sound.Collect)
            //{
            //    audioSource.PlayOneShot(collect);
            //}
            //else if (sound == Sound.Win)
            //{
            //    audioSource.PlayOneShot(win);
            //}
            //else if (sound == Sound.Jump)
            //{
            //    audioSource.PlayOneShot(jump);
            //}
            //else if (sound == Sound.HitEnemy)
            //{
            //    audioSource.PlayOneShot(hitEnemy);
            //}
            //else if (sound == Sound.HitPlayer)
            //{
            //    audioSource.PlayOneShot(hitPlayer);
            //}
            //else if (sound == Sound.Alert)
            //{
            //    audioSource.PlayOneShot(alert);
            //}
            //else if (sound == Sound.Miss)
            //{
            //    audioSource.PlayOneShot(miss);
            //}
            //else if (sound == Sound.Bump)
            //{
            //    audioSource.PlayOneShot(bump);
            //}
            //else
            //{
            //    Debug.Log("SoundManager: unknown sound");
            //}

        }


    }

}
