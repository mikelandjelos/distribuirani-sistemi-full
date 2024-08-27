#include <mpi.h>
#include <stdio.h>

#define NUM_INTS 105
#define COUNT 14

int main(int argc, char **argv) {
    MPI_Init(&argc, &argv);

    int world_rank, world_size;

    MPI_Comm_rank(MPI_COMM_WORLD, &world_rank);
    MPI_Comm_size(MPI_COMM_WORLD, &world_size);

    // (a)

    MPI_File fh;

    int integers[NUM_INTS];

    for (int i = 0; i < NUM_INTS; ++i)
        integers[i] = world_rank;

    const char *file1dat = "file1.dat";

    MPI_File_open(MPI_COMM_WORLD, file1dat, MPI_MODE_CREATE | MPI_MODE_WRONLY,
                  MPI_INFO_NULL, &fh);

    MPI_File_seek(fh, (world_size - 1 - world_rank) * NUM_INTS * sizeof(int),
                  MPI_SEEK_SET);

    MPI_File_write_all(fh, integers, NUM_INTS, MPI_INT, MPI_STATUS_IGNORE);

    MPI_File_close(&fh);

    // (b)

    MPI_File_open(MPI_COMM_WORLD, file1dat,
                  MPI_MODE_RDONLY | MPI_MODE_DELETE_ON_CLOSE, MPI_INFO_NULL,
                  &fh);

    MPI_File_read_at(fh, world_rank * NUM_INTS * sizeof(int), integers,
                     NUM_INTS, MPI_INT, MPI_STATUS_IGNORE);

    MPI_File_close(&fh);

    double sum = 0.;
    for (int i = 0; i < NUM_INTS; ++i)
        sum += integers[i];

    printf("Process `%d` calculated average %f\n", world_rank, sum / NUM_INTS);

    // (c) - Look at calculation.png to understand where COUNT = 14 comes from

    MPI_Datatype indexed_type;
    int array_of_blocklengths[COUNT] = {1, 2, 3,  4,  5,  6,  7,
                                        8, 9, 10, 11, 12, 13, 14};
    int array_of_displacements[COUNT] = {
        1 * world_size,  2 * world_size,  3 * world_size,  4 * world_size,
        5 * world_size,  6 * world_size,  7 * world_size,  8 * world_size,
        9 * world_size,  10 * world_size, 11 * world_size, 12 * world_size,
        13 * world_size, 14 * world_size};

    MPI_Type_indexed(COUNT, array_of_blocklengths, array_of_displacements,
                     MPI_INT, &indexed_type);
    MPI_Type_commit(&indexed_type);

    MPI_File_open(MPI_COMM_WORLD, "file_new.dat",
                  MPI_MODE_CREATE | MPI_MODE_RDWR | MPI_MODE_DELETE_ON_CLOSE,
                  MPI_INFO_NULL, &fh);

    MPI_File_set_view(fh, world_rank, MPI_INT, indexed_type, "native",
                      MPI_INFO_NULL);

    MPI_File_write_all(fh, integers, NUM_INTS, MPI_INT, MPI_STATUS_IGNORE);

    MPI_File_close(&fh);

    MPI_Type_free(&indexed_type);

    MPI_Finalize();

    return 0;
}