namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void Move(Robot robot, int counterOfSteps, Direction direction)
        {
            for (int i = 0; i < counterOfSteps; i++)
                robot.MoveTo(direction);
        }

        public static void MoveOut(Robot robot, int width, int height)
        {
            Move(robot, width - 3, Direction.Right);
            Move(robot, height - 3, Direction.Down);
        }
    }
}