using System;
using System.Drawing;

namespace FOGocalypse
{
    public class ParticleGenerator
    {
        //define global variables
        Random r = new Random();

        //constructor
        public ParticleGenerator()
        {

        }

        //create blood particles
        public void CreateBloodEffect(int x, int y, int amount, Color c)
        {
            int total = r.Next(1, 5) + amount;

            for (int xOffset = 0; xOffset <= total; xOffset++)
            {
                for (int yOffset = 0; yOffset <= total; yOffset++)
                {
                    int number = r.Next(1, 10);
                    Game.bloodParticles.Add(new Particle(x - xOffset + number, y - yOffset + number, c, number));
                }
            }
        }
    }
}
