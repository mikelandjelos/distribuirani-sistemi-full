#include <malloc.h>
#include <mpi.h>
#include <stdio.h>

#define FILESIZE (1024 * 1024)
#define MASTER_RANK 0

int main(int argc, char **argv) {
    MPI_Init(&argc, &argv);

    int world_rank, world_size;
    MPI_Comm_rank(MPI_COMM_WORLD, &world_rank);
    MPI_Comm_size(MPI_COMM_WORLD, &world_size);

    int bufsize = FILESIZE / world_size, numints = bufsize / sizeof(int);
    int *buf = (int *)malloc(numints * sizeof(int));

    for (int i = 0; i < numints; ++i)
        buf[i] = world_rank;

    MPI_File fh;

    MPI_File_open(MPI_COMM_WORLD, "file.dat",
                  MPI_MODE_CREATE | MPI_MODE_RDWR | MPI_MODE_DELETE_ON_CLOSE,
                  MPI_INFO_NULL, &fh);

    double start = MPI_Wtime();

    MPI_File_write_at(fh, world_rank * bufsize, buf, numints, MPI_INT,
                      MPI_STATUS_IGNORE);
    MPI_File_read_at(fh, (world_size - 1 - world_rank) * bufsize, buf, numints,
                     MPI_INT, MPI_STATUS_IGNORE);

    MPI_Barrier(MPI_COMM_WORLD);
    double end = MPI_Wtime();

    double sum = 0.;
    for (int i = 0; i < numints; ++i)
        sum += buf[i];

    free(buf);
    MPI_File_close(&fh);
    MPI_Finalize();

    printf("Process[%d] - calculated average - %f\n", world_rank,
           sum / numints);

    if (world_rank == MASTER_RANK)
        printf("Calculated time of execution - %.6f\n", end - start);

    return 0;
}

// Process[2] - calculated average - 1.000000
// Process[3] - calculated average - 0.000000
// Process[0] - calculated average - 3.000000
// Calculated time of execution - 0.000663
// Process[1] - calculated average - 2.000000