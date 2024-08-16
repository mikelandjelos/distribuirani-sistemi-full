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

    MPI_Datatype interleaved_integers_contiguous;
    MPI_Type_contiguous(1, MPI_INT, &interleaved_integers_contiguous);
    MPI_Datatype interleaved_integers_resized;
    MPI_Type_create_resized(interleaved_integers_contiguous, 0,
                            world_size * sizeof(int),
                            &interleaved_integers_resized);
    MPI_Type_commit(&interleaved_integers_resized);

    MPI_File fh;
    MPI_File_open(MPI_COMM_WORLD, "file.dat",
                  MPI_MODE_CREATE | MPI_MODE_RDWR | MPI_MODE_DELETE_ON_CLOSE,
                  MPI_INFO_NULL, &fh);

    int bufsize = FILESIZE / world_size, numints = bufsize / sizeof(int);
    int *buf = (int *)malloc(numints * sizeof(int));

    for (int i = 0; i < numints; ++i)
        buf[i] = world_rank;

    MPI_File_set_view(fh, world_rank * sizeof(int), MPI_INT,
                      interleaved_integers_resized, "native", MPI_INFO_NULL);

    double start = MPI_Wtime();

    MPI_File_write(fh, buf, numints, MPI_INT, MPI_STATUS_IGNORE);

    MPI_File_set_view(fh, (world_size - 1 - world_rank) * sizeof(int), MPI_INT,
                      interleaved_integers_resized, "native", MPI_INFO_NULL);

    MPI_File_read_at(fh, 0, buf, numints, MPI_INT, MPI_STATUS_IGNORE);

    MPI_Barrier(MPI_COMM_WORLD);
    double end = MPI_Wtime();

    double sum = 0.;
    for (int i = 0; i < numints; ++i)
        sum += buf[i];

    free(buf);

    MPI_Type_free(&interleaved_integers_contiguous);
    MPI_Type_free(&interleaved_integers_resized);
    MPI_File_close(&fh);
    MPI_Finalize();

    printf("Process[%d] - calculated average - %f\n", world_rank,
           sum / numints);

    if (world_rank == MASTER_RANK)
        printf("Calculated time of execution - %.6f\n", end - start);

    return 0;
}

// world_size = 4
// Process[1] - calculated average - 2.000000
// Process[2] - calculated average - 1.000000
// Process[3] - calculated average - 0.000000
// Process[0] - calculated average - 3.000000
// Calculated time of execution - 0.008256

// world_size = 8
// Process[0] - calculated average - 7.000000
// Calculated time of execution - 0.013385
// Process[1] - calculated average - 6.000000
// Process[4] - calculated average - 3.000000
// Process[5] - calculated average - 2.000000
// Process[7] - calculated average - 0.000000
// Process[2] - calculated average - 5.000000
// Process[3] - calculated average - 4.000000
// Process[6] - calculated average - 1.000000

// world_size = 16
// Process[1] - calculated average - 14.000000
// Process[2] - calculated average - 13.000000
// Process[3] - calculated average - 12.000000
// Process[5] - calculated average - 10.000000
// Process[0] - calculated average - 15.000000
// Calculated time of execution - 0.025786
// Process[4] - calculated average - 11.000000
// Process[6] - calculated average - 9.000000
// Process[7] - calculated average - 8.000000
// Process[9] - calculated average - 6.000000
// Process[10] - calculated average - 5.000000
// Process[8] - calculated average - 7.000000
// Process[15] - calculated average - 0.000000
// Process[11] - calculated average - 4.000000
// Process[13] - calculated average - 2.000000
// Process[12] - calculated average - 3.000000
// Process[14] - calculated average - 1.000000