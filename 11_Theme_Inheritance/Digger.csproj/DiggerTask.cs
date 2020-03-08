using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        private void ScoreIncrease()
        {
            Game.Scores += 10;
        }

        public CreatureCommand Act(int x, int y)
        {
            var deltaX = 0;
            var deltaY = 0;
            switch (Game.KeyPressed)
            {
                case Keys.Up:
                    deltaY = y > 0 ? -1 : 0;
                    break;
                case Keys.Down:
                    deltaY = y < Game.MapHeight - 1 ? 1 : 0;
                    break;
                case Keys.Right:
                    deltaX = x < Game.MapWidth - 1 ? 1 : 0;
                    break;
                case Keys.Left:
                    deltaX = x > 0 ? -1 : 0;
                    break;
            }
            if (Game.Map[x + deltaX, y + deltaY] is Sack)
            {
                deltaX = 0;
                deltaY = 0;
            }
            if (Game.Map[x + deltaX, y + deltaY] is Gold)
                ScoreIncrease();
            return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Monster || conflictedObject is Sack;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Sack : ICreature
    {
        private int cellPassedCounter;

        private void TryToKillPlayerOrMonster(int x, int y)
        {
            if (cellPassedCounter > 0) Game.Map[x, y] = null;
        }

        private CreatureCommand TryToTurnIntoGold(CreatureCommand objOnNextStep)
        {
            if (cellPassedCounter > 1) objOnNextStep.TransformTo = new Gold();
            return objOnNextStep;
        }

        public CreatureCommand Act(int x, int y)
        {
            var nextY = y < Game.MapHeight - 1 ? y + 1 : y;
            if (Game.Map[x, nextY] is Player || Game.Map[x, nextY] is Monster)
                TryToKillPlayerOrMonster(x, nextY);
            var deltaY = Game.Map[x, nextY] == null ? 1 : 0;
            var objOnNextStep = new CreatureCommand() { DeltaX = 0, DeltaY = deltaY };
            if (deltaY == 1)
                cellPassedCounter++;
            else
            {
                objOnNextStep = TryToTurnIntoGold(objOnNextStep);
                cellPassedCounter = 0;
            }
            return objOnNextStep;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Monster : ICreature
    {
        Point playerPosition = new Point() { X = -1, Y = -1 };
        private void PlayerSearch()
        {
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                    if (Game.Map[i, j] is Player)
                    {
                        playerPosition.X = i;
                        playerPosition.Y = j;
                    }
        }

        private bool CanMove(int x, int y)
        {
            return x > -1 && y > -1 &&
                x < Game.MapWidth &&
                y < Game.MapHeight &&
                (Game.Map[x, y] is Gold ||
                Game.Map[x, y] is null ||
                Game.Map[x, y] is Player);
        }

        public CreatureCommand Act(int x, int y)
        {
            PlayerSearch();
            if (playerPosition.X > -1 && playerPosition.Y > -1)
            {
                var deltaX = playerPosition.X == x ? 0 : playerPosition.X > x ? 1 : -1;
                if (CanMove(x + deltaX, y))
                    return new CreatureCommand() { DeltaX = deltaX };
                var deltaY = playerPosition.Y == y ? 0 : playerPosition.Y > y ? 1 : -1;
                if (CanMove(x, y + deltaY))
                    return new CreatureCommand() { DeltaY = deltaY };
            }
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Monster || conflictedObject is Sack;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
    }
}
