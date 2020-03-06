namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (true)
            {
                if (robot.Finished) break;
                Move(robot, width - 3, Direction.Right);
                Move(robot, 2, Direction.Down);
                Move(robot, width - 3, Direction.Left);
                if (robot.Finished) break;
                Move(robot, 2, Direction.Down);
            }
        }

        public static void Move(Robot robot, int counterOfSteps, Direction direction)
        {
            for (int i = 0; i < counterOfSteps; i++)
                robot.MoveTo(direction);
        }
    }
}