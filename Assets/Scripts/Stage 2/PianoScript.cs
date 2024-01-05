using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PianoScript : MonoBehaviour
{   
    //Audio Source and clips
    private AudioSource audioSrc;
    public AudioClip Aclip;
    public AudioClip Gclip;
    public AudioClip Cclip;
    public AudioClip Dclip;
    //Player position
    public Transform player;

    private float lastPlayTime;

    public int interval = 8;
    public int pairInterval = 2;
    private float pairPause = 1f;

    
    private AudioClip[] audioClips;
    //random pair chosen from pianoPairs
    private Tuple<Note, Note> randomPair;
    //array of pairs to choose from
    private Tuple<Note, Note>[] pianoPairs;

    //Pair played by player
    private Note[] platePair;

    private int note_counter =0;

    //number of notes played by piano
    private int counter = 0;

    private bool found = false;
    private bool played = false;
    private bool play = true;
    private bool turnOffCubes = false;
    private Note note;

    //cubes next to piano
    public GameObject cube1;
    public GameObject cube2;
    //active cube material
    public Material cubeMaterial;
    //default cube material
    public Material oldMaterial;
    public Material wrongMaterial;
    public Material rightMaterial;
    private float startTime;

    //number of correct pairs found
    int correctCounter = 0;

    //coloured buttons
    public GameObject[] plates = new GameObject[4];
    public GameObject door;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = Cclip;
        lastPlayTime = Time.time;


        pianoPairs = new Tuple<Note, Note>[]
        {
            new Tuple<Note, Note>(new Note("A", Aclip),new Note("C", Cclip)),
            new Tuple<Note, Note>(new Note("A", Aclip),new Note("G", Gclip)),
            new Tuple<Note, Note>(new Note("G", Gclip),new Note("D", Dclip)),
            new Tuple<Note, Note>(new Note("C", Cclip),new Note("G", Gclip)),
            new Tuple<Note, Note>(new Note("D", Dclip),new Note("A", Aclip)),

        };

        audioClips = new AudioClip[4]
        {
            Aclip, Gclip,Cclip, Dclip,
        };

        platePair = new Note[2];
        randomPair = ChooseRandomPair();//choose initial random pair

        
    }

    
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if(correctCounter >= 3)//end of game, the door opens
        {
            play = false;
            
            Door doorScript = door.GetComponent<Door>();
            doorScript.enabled = true;
            doorScript.stageDoor = true;
            enabled = false;
        }
        else if (distanceToPlayer > 15) //if the player is far enough, play only one note
            {
            if ((Time.time - lastPlayTime >= interval))
            {
                IdlePlay();
                lastPlayTime = Time.time;
            }
            if(counter != 0)
            {
                counter = 0;
            }

           
        }
        else if (play)//if the player is close, play a pair of notes
        {
            if (found)
            {
                randomPair = ChooseRandomPair();
                found = false;
            }

            if ((Time.time - lastPlayTime >= pairInterval))

                if (counter == 0)
                {
                    playNote(randomPair.Item1);
                    counter++;
                    lastPlayTime = Time.time;
                }
                else
                {
                    playNote(randomPair.Item2);
                    counter = 0;
                    lastPlayTime = Time.time;
                    play = false;
                }
        }

        if (played && (correctCounter<3))
        {
            
            found =((randomPair.Item1.getId() == platePair[0].getId()) && (randomPair.Item2.getId() == platePair[1].getId()));//if pairs match
            if (found)
            {
                Debug.Log("Found");
                play = true;
                lastPlayTime = Time.time;
                correctCounter++;
            }
            else
            {
                //set cubes to wrong (red)
                cube1.GetComponent<Renderer>().material = wrongMaterial;
                cube2.GetComponent<Renderer>().material = wrongMaterial;
            }
            played = false;
            startTime = Time.time;
            turnOffCubes = true;
        }

        if ((Time.time - startTime >= 3f) && turnOffCubes && (correctCounter < 3))
        {
            if(note_counter ==0)
            {
                cube1.GetComponent<Renderer>().material = oldMaterial;
            }
           
            cube2.GetComponent<Renderer>().material = oldMaterial;
            

            
            played = false;
            turnOffCubes = false;
        }

    }

    void IdlePlay()
    {
        //Debug.Log("play");
        audioSrc.Play();
    }

    void playNote(Note note)
    {
        audioSrc.clip = note.getClip();
        audioSrc.Play();

    }

    public void repeat()
    {
        play = true;
    }

   

   

    Tuple<Note, Note> ChooseRandomPair()
    {
        //returns a random pair of notes, from pianoPairs
        if (pianoPairs != null && pianoPairs.Length > 0)
        {
            
            int randomIndex = UnityEngine.Random.Range(0, pianoPairs.Length);

            
            return pianoPairs[randomIndex];
        }
        else
        {
          
            return null;
        }
    }

    public void PressPlate(string noteName)
    {
        //add a Note object to the pair played by player
        if (noteName == "A")
        {
            
            platePair[note_counter] = new Note(noteName,audioClips[0]);
        }
        if (noteName == "G")
        {
            platePair[note_counter] = new Note(noteName, audioClips[1]);
        }
        if (noteName == "C")
        {
            platePair[note_counter] = new Note(2, noteName, audioClips[2]);
        }
        if (noteName == "D")
        {
            platePair[note_counter] = new Note(3, noteName, audioClips[3]);
        }

        note_counter++;

        if (note_counter == 1)
        {
            //set first cube active
            cube1.GetComponent<Renderer>().material = cubeMaterial;
            
        }

        if(note_counter == 2)
        {
            //set second cube active
            cube2.GetComponent<Renderer>().material = cubeMaterial;
            note_counter = 0;
            played = true;
            
                
        }
        
    }

    public class Note
    {
        //Class for music notes
        private int id;
        private string name;
        private AudioClip clip;


        public Note(int id, string name, AudioClip clip)
        {
            this.id = id;
            this.name = name;
            this.clip = clip;
        }

        public Note(string name, AudioClip clip)
        {
            if ( name == "A")
            {

                this.id = 0;
            }
            if (name == "G")
            {
                this.id = 1;
            }
            if (name == "C")
            {
                this.id = 2;
            }
            if (name == "D")
            {
                this.id = 3;
            }

            this.name = name;
            this.clip = clip;
        }

        
        public AudioClip getClip()
        {
            return clip;
        }

        public int getId()
        {
            return id;
        }

        public string getName()
        {
            return name;
        }




    }
}
