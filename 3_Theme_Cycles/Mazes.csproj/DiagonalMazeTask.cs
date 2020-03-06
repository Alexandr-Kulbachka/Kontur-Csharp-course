namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (true)
            {
                if (width < height) Move(robot, (height - width) / 6, Direction.Down);
                if (robot.Finished) break;
                Move(robot, width / height, Direction.Right);
                if (robot.Finished) break;
                if (width > height) Move(robot, height / width, Direction.Down);
            }
        }

        public static void Move(Robot robot, int counterOfSteps, Direction direction)
        {
            for (int i = 0; i <= counterOfSteps; i++)
                robot.MoveTo(direction);
        }
    }
}