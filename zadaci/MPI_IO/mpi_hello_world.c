#include <mpi.h>
#include <stdio.h>

int main(int argc, char** argv) {
    MPI_Init(&argc, &argv);

    int world_size;
    MPI_Comm_size(MPI_COMM_WORLD, &world_size);

    int world_rank;
    MPI_Comm_rank(MPI_COMM_WORLD, &world_rank);

    char processor_name[MPI_MAX_PROCESSOR_NAME];
    int name_len;
    MPI_Get_processor_name(processor_name, &name_len);

    printf("Hello world from processor '%s', rank %d out of %d processors\n",
           processor_name, world_rank, world_size);

    MPI_Finalize();

    return 0;
}

// $ mpiexec -n 5 ./mpi_hello_world
// Hello world from processor jlo, rank 0 out of 5 processors
// Hello world from processor jlo, rank 1 out of 5 processors
// Hello world from processor jlo, rank 3 out of 5 processors
// Hello world from processor jlo, rank 4 out of 5 processors
// Hello world from processor jlo, rank 2 out of 5 processors