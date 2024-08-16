#include <malloc.h>
#include <mpi.h>
#include <stdio.h>
#include <unistd.h>

#include "util.h"

#define FILESIZE (1024 * 1024)

int main(int argc, char **argv) {
    MPI_Init(&argc, &argv);

    int world_rank, world_size;
    MPI_Comm_rank(MPI_COMM_WORLD, &world_rank);
    MPI_Comm_size(MPI_COMM_WORLD, &world_size);

    int bufsize = FILESIZE / world_size, numints = bufsize / sizeof(int);
    int *buf = (int *)malloc(bufsize);
    double *percents_read = (double *)malloc(world_size * sizeof(double));

    for (int i = 0; i < numints; ++i)
        buf[i] = world_rank;

    MPI_File fh;
    MPI_File_open(MPI_COMM_WORLD, "dat.dat",
                  MPI_MODE_CREATE | MPI_MODE_RDWR | MPI_MODE_DELETE_ON_CLOSE,
                  MPI_INFO_NULL, &fh);

    // 1
    MPI_File_write_at(fh, (world_size - 1 - world_rank) * bufsize, buf, numints,
                      MPI_INT, MPI_STATUS_IGNORE);

    // 2
    MPI_File_seek(fh, 0, MPI_SEEK_SET);
    MPI_Barrier(MPI_COMM_WORLD);

    MPI_File_read_shared(fh, buf, numints, MPI_INT, MPI_STATUS_IGNORE);

    for (int i = 0; i < numints; ++i)
        percents_read[buf[i]]++;

    for (int i = 0; i < world_size; ++i)
        percents_read[i] /= numints;

    char *results = join_doubles(percents_read, world_size, ", ");
    printf("Process[%d] results: %s\n", world_rank, results);

    if (results != NULL)
        free(results);
    free(percents_read);
    free(buf);

    MPI_File_close(&fh);
    MPI_Finalize();

    return 0;
}