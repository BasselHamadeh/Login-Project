using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class BackendGameState
    {
        public int Rows { get; }
        public int Columns { get; }
        public BackendGrid[,] Grid { get; private set; }
        public BackendDirection Direction { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        public readonly LinkedList<BackendDirection> directions = new LinkedList<BackendDirection>();

        public readonly LinkedList<BackendPosition> snakePosition = new LinkedList<BackendPosition>();

        public readonly Random random = new Random();

        public BackendGameState(int rows, int column)
        {
            Rows = rows;
            Columns = column;
            Grid = new BackendGrid[Rows, Columns];
            Direction = BackendDirection.Right;

            AddSnake();
            AddFood();
        }

        public IEnumerable<BackendPosition> EmptyPosition()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Grid[r, c] == BackendGrid.Empty)
                    {
                        yield return new BackendPosition(r, c);
                    }
                }
            }
        }

        public void AddSnake()
        {
            int r = Rows / 2;

            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = BackendGrid.Snake;
                snakePosition.AddFirst(new BackendPosition(r, c));
            }
        }

        public void AddFood()
        {
            List<BackendPosition> empty = new List<BackendPosition>(EmptyPosition());

            if (empty.Count == 0)
            {
                return;
            }

            BackendPosition pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Column] = BackendGrid.Food;
        }


        public BackendPosition PositionSnakeHead()
        {
            return snakePosition.First.Value;
        }

        public BackendPosition PositionSnakeTail()
        {
            return snakePosition.Last.Value;
        }

        public IEnumerable<BackendPosition> SnakePosition()
        {
            return snakePosition;
        }

        public void AddHead(BackendPosition pos)
        {
            snakePosition.AddFirst(pos);
            Grid[pos.Row, pos.Column] = BackendGrid.Snake;
        }

        public void RemoveTail()
        {
            BackendPosition tail = snakePosition.Last.Value;
            Grid[tail.Row, tail.Column] = BackendGrid.Empty;
            snakePosition.RemoveLast();
        }

        public void SnakeDirection(BackendDirection direction)
        {
            if (CanChangeDirection(direction))
            {
                directions.AddLast(direction);
            }
        }

        public bool CanChangeDirection(BackendDirection direction)
        {
            if (directions.Count == 2)
            {
                return false;
            }

            BackendDirection lasdirection = LastDirection();
            return direction != lasdirection && direction != lasdirection.Opposite();
        }

        public BackendDirection LastDirection()
        {
            if (directions.Count == 0)
            {
                return Direction;
            }

            return directions.Last.Value;
        }

        public bool GridOutside(BackendPosition pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns;
        }

        public BackendGrid Hit(BackendPosition pos)
        {
            if (GridOutside(pos))
            {
                return BackendGrid.Outside;
            }

            if (pos == PositionSnakeTail())
            {
                return BackendGrid.Empty;
            }

            return Grid[pos.Row, pos.Column];
        }

        public void SnakeMove()
        {
            if (directions.Count > 0)
            {
                Direction = directions.First.Value;
                directions.RemoveFirst();
            }

            BackendPosition newHeadPosition = PositionSnakeHead().Translate(Direction);
            BackendGrid hit = Hit(newHeadPosition);

            if (hit == BackendGrid.Outside || hit == BackendGrid.Snake)
            {
                GameOver = true;
            }
            else if (hit == BackendGrid.Empty)
            {
                RemoveTail();
                AddHead(newHeadPosition);
            }
            else if (hit == BackendGrid.Food)
            {
                AddHead(newHeadPosition);
                Score++;
                Grid[newHeadPosition.Row, newHeadPosition.Column] = BackendGrid.Empty;
                AddFood();
            }
        }
    }
}