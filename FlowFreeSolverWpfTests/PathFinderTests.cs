﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FlowFreeSolverWpf.Model;
using NUnit.Framework;

namespace FlowFreeSolverWpfTests
{
    [TestFixture]
    internal class PathFinderTests
    {
        [Test]
        public void Grid2X2WithOneColourPair()
        {
            // Arrange
            var startCoords = CoordsFactory.GetCoords(0, 0);
            var endCoords = CoordsFactory.GetCoords(1, 1);

            // " B"
            // "B "
            var colourPairBlue = new ColourPair(startCoords, endCoords, DotColours.Blue);
            var grid = new Grid(2, colourPairBlue);

            var expectedPath1 = new Path();
            expectedPath1.AddCoords(startCoords);
            expectedPath1.AddCoords(CoordsFactory.GetCoords(1, 0));
            expectedPath1.AddCoords(endCoords);

            var expectedPath2 = new Path();
            expectedPath2.AddCoords(startCoords);
            expectedPath2.AddCoords(CoordsFactory.GetCoords(0, 1));
            expectedPath2.AddCoords(endCoords);

            // Act
            var pathFinder = new PathFinder(CancellationToken.None);
            var initialPathsBlue = PathFinder.InitialPaths(colourPairBlue);
            var paths = pathFinder.FindAllPaths(grid, endCoords, initialPathsBlue, 10);

            // Assert
            Assert.That(paths.PathList.Count(), Is.EqualTo(2));
            Assert.That(paths.PathList, Has.All.Matches<Path>(p => p.IsActive));
            Assert.That(paths.PathList, Has.Exactly(1).Matches<Path>(p => p.CoordsList.SequenceEqual(expectedPath1.CoordsList)));
            Assert.That(paths.PathList, Has.Exactly(1).Matches<Path>(p => p.CoordsList.SequenceEqual(expectedPath2.CoordsList)));
        }

        [Test]
        public void Grid2X2WithOneColourPairAndMaxDirectionChangesSetToOne()
        {
            // Arrange
            var startCoords = CoordsFactory.GetCoords(0, 0);
            var endCoords = CoordsFactory.GetCoords(1, 1);

            // " B"
            // "B "
            var colourPairBlue = new ColourPair(startCoords, endCoords, DotColours.Blue);
            var grid = new Grid(2, colourPairBlue);

            var expectedPath1 = new Path();
            expectedPath1.AddCoords(startCoords);
            expectedPath1.AddCoords(CoordsFactory.GetCoords(1, 0));
            expectedPath1.AddCoords(endCoords);

            var expectedPath2 = new Path();
            expectedPath2.AddCoords(startCoords);
            expectedPath2.AddCoords(CoordsFactory.GetCoords(0, 1));
            expectedPath2.AddCoords(endCoords);

            // Act
            var pathFinder = new PathFinder(CancellationToken.None);
            var initialPathsBlue = PathFinder.InitialPaths(colourPairBlue);
            var paths = pathFinder.FindAllPaths(grid, endCoords, initialPathsBlue, 1);

            // Assert
            Assert.That(paths.PathList.Count(), Is.EqualTo(2));
            Assert.That(paths.PathList, Has.All.Matches<Path>(p => p.IsActive));
            Assert.That(paths.PathList, Has.Exactly(1).Matches<Path>(p => p.CoordsList.SequenceEqual(expectedPath1.CoordsList)));
            Assert.That(paths.PathList, Has.Exactly(1).Matches<Path>(p => p.CoordsList.SequenceEqual(expectedPath2.CoordsList)));
        }

        [Test]
        public void Grid4X4WithFourColourPairs()
        {
            // Arrange
            var startCoords = CoordsFactory.GetCoords(0, 3);
            var endCoords = CoordsFactory.GetCoords(3, 3);

            // "BOOB"
            // " RR "
            // " GG "
            // "    "
            var colourPairBlue = new ColourPair(startCoords, endCoords, DotColours.Blue);
            var grid = new Grid(4,
                colourPairBlue,
                new ColourPair(CoordsFactory.GetCoords(1, 3), CoordsFactory.GetCoords(2, 3), DotColours.Orange),
                new ColourPair(CoordsFactory.GetCoords(1, 2), CoordsFactory.GetCoords(2, 2), DotColours.Red),
                new ColourPair(CoordsFactory.GetCoords(1, 1), CoordsFactory.GetCoords(2, 1), DotColours.Green));

            var expectedPath = new Path();
            expectedPath.AddCoords(startCoords);
            expectedPath.AddCoords(CoordsFactory.GetCoords(0, 2));
            expectedPath.AddCoords(CoordsFactory.GetCoords(0, 1));
            expectedPath.AddCoords(CoordsFactory.GetCoords(0, 0));
            expectedPath.AddCoords(CoordsFactory.GetCoords(1, 0));
            expectedPath.AddCoords(CoordsFactory.GetCoords(2, 0));
            expectedPath.AddCoords(CoordsFactory.GetCoords(3, 0));
            expectedPath.AddCoords(CoordsFactory.GetCoords(3, 1));
            expectedPath.AddCoords(CoordsFactory.GetCoords(3, 2));
            expectedPath.AddCoords(endCoords);

            // Act
            var pathFinder = new PathFinder(CancellationToken.None);
            var initialPathsBlue = PathFinder.InitialPaths(colourPairBlue);
            var paths = pathFinder.FindAllPaths(grid, endCoords, initialPathsBlue, 10);

            // Assert
            Assert.That(paths.PathList.Count(), Is.EqualTo(1));
            Assert.That(paths.PathList, Has.All.Matches<Path>(p => p.IsActive));
            Assert.That(paths.PathList, Has.Exactly(1).Matches<Path>(p => p.CoordsList.SequenceEqual(expectedPath.CoordsList)));
        }

        [Test]
        public void Grid4X4WithFourColourPairsAndMaxDirectionChangesSetToOneReturnsThreeInactivePaths()
        {
            // Arrange
            var startCoords = CoordsFactory.GetCoords(0, 3);
            var endCoords = CoordsFactory.GetCoords(3, 3);

            // "BOOB"
            // " RR "
            // " GG "
            // "    "
            var colourPairBlue = new ColourPair(startCoords, endCoords, DotColours.Blue);
            var grid = new Grid(4,
                colourPairBlue,
                new ColourPair(CoordsFactory.GetCoords(1, 3), CoordsFactory.GetCoords(2, 3), DotColours.Orange),
                new ColourPair(CoordsFactory.GetCoords(1, 2), CoordsFactory.GetCoords(2, 2), DotColours.Red),
                new ColourPair(CoordsFactory.GetCoords(1, 1), CoordsFactory.GetCoords(2, 1), DotColours.Green));

            // Act
            var pathFinder = new PathFinder(CancellationToken.None);
            var initialPathsBlue = PathFinder.InitialPaths(colourPairBlue);
            var paths = pathFinder.FindAllPaths(grid, endCoords, initialPathsBlue, 1);

            // Assert
            Assert.That(paths.PathList.Count(), Is.EqualTo(3));
            Assert.That(paths.PathList, Has.All.Matches<Path>(p => !p.IsActive));
        }

        [Test]
        public void SecondCallToFindAllPathsGivenInactivePathsReturnsACompletedPath()
        {
            // Arrange
            var startCoords = CoordsFactory.GetCoords(0, 3);
            var endCoords = CoordsFactory.GetCoords(3, 3);

            // "BOOB"
            // " RR "
            // " GG "
            // "    "
            var colourPairBlue = new ColourPair(startCoords, endCoords, DotColours.Blue);
            var grid = new Grid(4,
                colourPairBlue,
                new ColourPair(CoordsFactory.GetCoords(1, 3), CoordsFactory.GetCoords(2, 3), DotColours.Orange),
                new ColourPair(CoordsFactory.GetCoords(1, 2), CoordsFactory.GetCoords(2, 2), DotColours.Red),
                new ColourPair(CoordsFactory.GetCoords(1, 1), CoordsFactory.GetCoords(2, 1), DotColours.Green));

            var expectedPath = new Path();
            expectedPath.AddCoords(startCoords);
            expectedPath.AddCoords(CoordsFactory.GetCoords(0, 2));
            expectedPath.AddCoords(CoordsFactory.GetCoords(0, 1));
            expectedPath.AddCoords(CoordsFactory.GetCoords(0, 0));
            expectedPath.AddCoords(CoordsFactory.GetCoords(1, 0));
            expectedPath.AddCoords(CoordsFactory.GetCoords(2, 0));
            expectedPath.AddCoords(CoordsFactory.GetCoords(3, 0));
            expectedPath.AddCoords(CoordsFactory.GetCoords(3, 1));
            expectedPath.AddCoords(CoordsFactory.GetCoords(3, 2));
            expectedPath.AddCoords(endCoords);

            // Act
            var pathFinder = new PathFinder(CancellationToken.None);
            var initialPathsBlue = PathFinder.InitialPaths(colourPairBlue);
            var paths1 = pathFinder.FindAllPaths(grid, endCoords, initialPathsBlue, 1);

            // Assert
            Assert.That(paths1.PathList.Count(), Is.EqualTo(3));
            Assert.That(paths1.PathList, Has.All.Matches<Path>(p => !p.IsActive));

            // Act 2
            var inactivePaths = paths1.PathList.ToList();
            var paths2 = pathFinder.FindAllPaths(grid, endCoords, inactivePaths, 2);

            // Assert 2
            Assert.That(paths2.PathList, Has.Exactly(1).Matches<Path>(p => p.CoordsList.SequenceEqual(expectedPath.CoordsList) && p.IsActive));
        }

        [Test]
        public void BugExploration()
        {
            // "BOOB"
            // " RR "
            // " GG "
            // "    "
            var grid = new Grid(4,
                new ColourPair(CoordsFactory.GetCoords(0, 3), CoordsFactory.GetCoords(3, 3), DotColours.Blue),
                new ColourPair(CoordsFactory.GetCoords(1, 3), CoordsFactory.GetCoords(2, 3), DotColours.Orange),
                new ColourPair(CoordsFactory.GetCoords(1, 2), CoordsFactory.GetCoords(2, 2), DotColours.Red),
                new ColourPair(CoordsFactory.GetCoords(1, 1), CoordsFactory.GetCoords(2, 1), DotColours.Green));

            var endCoords = CoordsFactory.GetCoords(3, 3);

            // Calling FindAllPaths in a loop with an increasing value of maxDirectionChanges.
            var pathFinder1 = new PathFinder(CancellationToken.None);
            var completedPaths = new List<Path>();
            var inactivePaths = new List<Path>();
            var firstCall = true;

            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (var maxDirectionChanges in Enumerable.Range(1, 5))
            {
                if (!firstCall && !inactivePaths.Any()) break;
                firstCall = false;
                var pathFinderResult = pathFinder1.FindAllPaths(grid, endCoords, inactivePaths, maxDirectionChanges);
                completedPaths.AddRange(pathFinderResult.PathList.Where(p => p.IsActive));
                inactivePaths = pathFinderResult.PathList.Where(p => !p.IsActive).ToList();
            }

            // Calling FindAllPaths once with a large value of maxDirectionChanges.
            var pathFinder2 = new PathFinder(CancellationToken.None);
            var pathFinderResult2 = pathFinder2.FindAllPaths(grid, endCoords, new List<Path>(), 5);

            Assert.That(completedPaths.Count, Is.EqualTo(pathFinderResult2.PathList.Count()));
        }

        [Test]
        public void BugExploration2()
        {
            // "   RG"
            // "  BG "
            // "R    "
            // "OB YO"
            // "    Y"
            var grid = new Grid(5,
                new ColourPair(CoordsFactory.GetCoords(1, 1), CoordsFactory.GetCoords(2, 3), DotColours.Blue),
                new ColourPair(CoordsFactory.GetCoords(0, 1), CoordsFactory.GetCoords(4, 1), DotColours.Orange),
                new ColourPair(CoordsFactory.GetCoords(0, 2), CoordsFactory.GetCoords(3, 4), DotColours.Red),
                new ColourPair(CoordsFactory.GetCoords(3, 3), CoordsFactory.GetCoords(4, 4), DotColours.Green),
                new ColourPair(CoordsFactory.GetCoords(3, 1), CoordsFactory.GetCoords(4, 0), DotColours.Yellow));

            var endCoords = grid.ColourPairs.First().EndCoords;

            const int maxDirectionChangeLimit = 5 * 5;

            // Calling FindAllPaths in a loop with an increasing value of maxDirectionChanges up to maxDirectionChangeLimit.
            var pathFinder1 = new PathFinder(CancellationToken.None);
            var completedPaths = new List<Path>();
            var inactivePaths = new List<Path>();
            var firstCall = true;

            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (var directionChangeLimit in Enumerable.Range(1, maxDirectionChangeLimit))
            {
                if (!firstCall && !inactivePaths.Any()) break;
                firstCall = false;
                var pathFinderResult1 = pathFinder1.FindAllPaths(grid, endCoords, inactivePaths, directionChangeLimit);
                completedPaths.AddRange(pathFinderResult1.PathList.Where(p => p.IsActive));
                inactivePaths = pathFinderResult1.PathList.Where(p => !p.IsActive).ToList();
            }

            // Calling FindAllPaths once with maxDirectionChangeLimit.
            var pathFinder2 = new PathFinder(CancellationToken.None);
            var pathFinderResult2 = pathFinder2.FindAllPaths(grid, endCoords, new List<Path>(), maxDirectionChangeLimit);

            Assert.That(completedPaths.Count, Is.EqualTo(pathFinderResult2.PathList.Count()));
        }
    }
}
