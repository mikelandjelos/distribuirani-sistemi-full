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

    MPI_Datatype interleaved_integers_vector;
    MPI_Type_vector(1, 2, 6, MPI_INT, &interleaved_integers_vector);
    MPI_Type_commit(&interleaved_integers_vector);

    MPI_File fh;
    MPI_File_open(MPI_COMM_WORLD, "file.dat",
                  MPI_MODE_CREATE | MPI_MODE_RDWR | MPI_MODE_DELETE_ON_CLOSE,
                  MPI_INFO_NULL, &fh);

    int bufsize = FILESIZE / world_size, numints = bufsize / sizeof(int);
    int *buf = (int *)malloc(numints * sizeof(int));

    for (int i = 0; i < numints; ++i)
        buf[i] = world_rank;

    MPI_File_set_view(fh, world_rank * sizeof(int), MPI_INT,
                      interleaved_integers_vector, "native", MPI_INFO_NULL);

    double start = MPI_Wtime();

    MPI_File_write_all(fh, buf, numints, MPI_INT, MPI_STATUS_IGNORE);

    MPI_File_set_view(fh, (world_size - 1 - world_rank) * sizeof(int), MPI_INT,
                      interleaved_integers_vector, "native", MPI_INFO_NULL);

    MPI_File_read_at_all(fh, 0, buf, numints, MPI_INT, MPI_STATUS_IGNORE);

    MPI_Barrier(MPI_COMM_WORLD);
    double end = MPI_Wtime();

    double sum = 0.;
    for (int i = 0; i < numints; ++i)
        sum += buf[i];

    free(buf);

    MPI_Type_free(&interleaved_integers_vector);
    MPI_File_close(&fh);
    MPI_Finalize();

    printf("Process[%d] - calculated average - %f\n", world_rank,
           sum / numints);

    if (world_rank == MASTER_RANK)
        printf("Calculated time of execution - %.6f\n", end - start);

    return 0;
}

// world_size = 16
// Process[2] - calculated average - 0.009583
// Process[1] - calculated average - 0.010498
// Process[3] - calculated average - 0.008789
// Process[4] - calculated average - 0.007996
// Process[0] - calculated average - 0.011414
// Calculated time of execution - 0.015844
// Process[6] - calculated average - 0.006409
// Process[5] - calculated average - 0.007202
// Process[7] - calculated average - 0.005615
// Process[10] - calculated average - 0.003235
// Process[11] - calculated average - 0.002441
// Process[12] - calculated average - 0.001648
// Process[9] - calculated average - 0.004028
// Process[13] - calculated average - 0.000854
// Process[15] - calculated average - 0.000000
// Process[8] - calculated average - 0.004822
// Process[14] - calculated average - 0.000061
