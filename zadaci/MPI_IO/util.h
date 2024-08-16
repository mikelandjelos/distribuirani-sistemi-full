#include <stdio.h>
#include <stdlib.h>
#include <string.h>

char* join_doubles(double* array, size_t length, const char* delimiter) {
    size_t estimated_length = length * (10 + strlen(delimiter));

    char* result = (char*)malloc(estimated_length);
    if (!result) {
        return NULL;
    }

    result[0] = '\0';

    char buffer[50];

    for (size_t i = 0; i < length; ++i) {
        snprintf(buffer, sizeof(buffer), "%f", array[i]);
        strcat(result, buffer);

        if (i < length - 1) {
            strcat(result, delimiter);
        }
    }

    return result;
}
