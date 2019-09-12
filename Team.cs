using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    class Team
    {
        int id;
        int wins;
        int ties;
        int rains;
        int points;

        public Team(int id)
        {
            this.id = id;
            wins = 0;
            ties = 0;
            rains = 0;
            points = 0;
        }

        public Team(Team t)
        {
            this.id = t.getId();
            this.wins = t.getWins();
            this.ties = t.getTies();
            this.rains = t.getRains();
            this.points = t.getPoints();
        }

        public int getId()
        {
            return id;
        }

        public int getWins()
        {
            return wins;
        }

        public int getTies()
        {
            return ties;

        }

        public int getRains()
        {
            return rains;
        }

        public int getPoints()
        {
            return points;
        }

        public void setPoints(int points)
        {
            this.points = points;
        }

        public void teamsWins()
        {
            wins++;
        }

        public void matchTied()
        {
            ties++;
        }

        public void rainStoppedPlay()
        {
            rains++;
        }

        public void calcPoints(int winPoints, int rainPoints, int drawPoints)
        {
            this.points = wins * winPoints + (rains * rainPoints) + ties * drawPoints;
        }

        public string getTeamString()
        {
            return id + " " + wins + " " + rains + " " + ties + " " + points; 
        }
    }
}
