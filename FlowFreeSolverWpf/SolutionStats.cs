﻿using System;

namespace FlowFreeSolverWpf
{
    public class SolutionStats
    {
        public SolutionStats(int numMatrixRows, int numMatrixCols, TimeSpan? matrixBuildingDuration, TimeSpan? matrixSolvingDuration)
        {
            NumMatrixRows = numMatrixRows;
            NumMatrixCols = numMatrixCols;
            MatrixBuildingDuration = matrixBuildingDuration;
            MatrixSolvingDuration = matrixSolvingDuration;
        }

        public int NumMatrixRows { get; set; }
        public int NumMatrixCols { get; set; }
        public TimeSpan? MatrixBuildingDuration { get; set; }
        public TimeSpan? MatrixSolvingDuration { get; set; }
    }
}
