# Beleske sa prezentacija

## Prva prezentacija

### Osnovni C interfejs za rad sa fajlovima

- `fopen` i `fclose` - otvaranje/zatvaranje;
- `fscanf` i `fprintf` - formatirani upis/citanje (tekstualni fajlovi);
- `fgets` i `fputs` - neformatirani upis/citanje (tekstualni fajlovi);
- `fread` i `fwrite` - formatirani upis/citanje (binarni fajlovi);
- `fseek` - pozicioniranje u okviru fajla;

### Osnovni MPI I/O intefejs za rad sa fajlovima

MPI I/O ima funkcije za rad sa **neformatiranim binarnim fajlovima**;

- `MPI_File_open(MPI_Comm comm, const char *filename, int amode, MPI_Info info, MPI_file *fh)` - otvaranje;
  - `amode` - `RDONLY` | `RDWR` | `WRONLY` | `CREATE` (prefiks `MPI_MODE_`, mogu da se kombinuju sa `|`);
  - `info` - `MPI_INFO_NULL`;
- `MPI_File_close(MPI_File *fh)` - zatvaranje;
- `MPI_File_read(MPI_File fh, void *buf, int count, MPI_Datatype datatype, MPI_Status *status)` - citanje;
  - `status` - `MPI_STATUS_IGNORE`;
- `MPI_File_write(MPI_File fh, const void *buf, int count, MPI_Datatype datatype, MPI_status *status)` - upis;
- `MPI_file_seek(MPI_File fh, MPI_Offset offset, int whence)` - pozicioniranje;
  - `offset` - pomeraj u **bajtovima**;
  - `whence` - `SEEK` | `SET` | `CUR` (prefiks `MPI_SEEK_`);
- `MPI_File_read_at(MPI_File fh, MPI_Offset offset, void *buf, int count, MPI_Datatype, MPI_Status *status)` - citanje sa eksplicitnim pomerajem;
- `MPI_File_write_at(MPI_File fh, MPI_Offset offset, void *buf, int count, MPI_Datatype, MPI_Status *status)` - upis sa eksplicitnim pomerajem;

> [!IMPORTANT]
> Sve read/write funkcije nabrojane iznad spadaju u grupu MPI operacija koje se koriste za _eng. Indepentent I/O_, odnosno svaki proces vrsi I/O bez bilo kakve sinhronizacije ili znanja o drugim procesima. Kasnije ce biti obradjen _eng. Collective I/O_ pristup. Vise na [linku 1 (Ilinois)](http://wgropp.cs.illinois.edu/courses/cs598-s16/lectures/lecture32.pdf) i [linku 2 (Kornel)](https://cvw.cac.cornell.edu/parallel-io/mpi-io/index).

### Nekontinualni pristup

#### Kratko podsecanje - MPI izvedeni tipovi

- `MPI_Type_contiguous(int count, MPI_datatype oldtype, MPI_Datatype *newtype)` - blok kontinualne memorije;
- `MPI_Type_vector(int count, int blocklength, int stride, MPI_Datatype oldtype, MPI_Datatype *newtype)` - blokovi kontinualne memorije uniformne velicine, koji se nalaze na uniformnim rastojanjima;
  - `blocklenght` - broj elemenata starog tipa unutar svakog bloka (efektivno odredjuje velicinu bloka);
  - `stride` - broj elemenata starog tipa izmedju dva bloka, mereno od pocetka jednog do pocetka drugog (efektivno odredjuje rastojanje medju blokovima);
- `MPI_Type_indexed(int count, int *array_of_blocklengths, int *array_of_displacements, MPI_Datatype oldtype, MPI_Datatype *newtype)` - blokovi kontinualne memorije promenljive velicine, koji se nalaze na promenljivim rastojanjima;
  - `array_of_displacements` - prvi element predstavlja nesto nalik na pocetni _offset_;
  - `array_of_blocklengths` i `array_of_displacements` - treba da budu nizovi sa istim brojem elemenata;
- `MPI_Type_commit(MPI_Datatype *datatype)` - potvrda kreiranja izvedenog tipa (vrsi se pre koriscenja tipa u drugim funkcijama);
- `MPI_Type_create_resized(MPI_Datatype oldtype, MPI_Aint lb, MPI_Aint extent, MPI_Datatype *newtype)` - pomera adresu prvog sledeceg podatka u nizu izvedenih;
  - `lb` - nova donja granica tipa, uglavnom setovano na `0`;
  - `extent` - nova velicina tipa u **bajtovima** (pazi na ovo, cesta je greska); utice na to odakle ce krenuti slanje sledece jedinice;

#### File view

- **File view** - nekontinualni pristup bazira se na ideji definisanja koji deo fajla je 'vidljiv' procesu; pogled se kreira na osnovu izvedenih tipova podataka;
- `MPI_File_set_view(MPI_File fh, MPI_Offset disp, MPI_Datatype etype, MPI_Datatype filetype, const char *datarep, MPI_Info info)` - setovanje pogleda;
  - `disp` - offset od pocetka fajla, u bajtovima;
  - `etype` - tip koji definise osnovnu jedinicu pristupa;
  - `filetype` - istog tipa kao `etype` ili izvedeni koji se sastoji iz vise `etype` jedinica;
  - `datarep` - uglavnom navodimo vrednost `"native"`;
  - `info` - uglavnom navodimo vrednost `MPI_INFO_NULL`;

### Kolektivne I/O operacije

- `MPI_File_read_all(MPI_File fh, void *buf, int count, MPI_Datatype datatype, MPI_Status *status)` - operacija za kolektivno citanje;
- `MPI_File_write_all(MPI_File fh, void *buf, int count, MPI_Datatype datatype, MPI_Status *status)` - operacija za kolektivni upis;

> [!WARNING]
> Iz nekog razloga, u primerima koje sam radio (pogledati benchmark-ove na dnu svakog fajla, postoji vreme izvrsenja koda), operacije `MPI_File_write/read`, a posebno `MPI_File_write_at/read_at`, rade mnogo brze nego `MPI_File_write_all/read_all`, iako su funkcije sa sufiksom `all` kreirane za paralelni/kolektivni rad sa fajlovima; ne znam koji je razlog, ali siguran sam da je u pitanju moja ne-ekspertiza postavci primera (cak i kod, po meni, visokog stepena interleaving-a, nema povecanja brzine). Videti [link](https://stackoverflow.com/questions/38615856/how-does-mpi-file-write-differ-from-mpi-file-write-all) za bolje objasnjenje zasto bi kolektivne operacije trebalo da budu brze.

### Neblokirajuce I/O operacije

Neblokirajuce verzije operacija.

- `MPI_File_iread(MPI_File fh, void *buf, int count, MPI_Datatype datatype, MPI_Request *request)`;
- `MPI_File_iwrite(MPI_File fh, void *buf, int count, MPI_Datatype datatype, MPI_Request *request)`;
- `MPI_File_iread_at(MPI_File fh, MPI_Offset offset, void *buf, int count, MPI_Datatype datatype, MPI_Request *request)`;
- `MPI_File_iread(MPI_File fh, MPI_Offset offset, void *buf, int count, MPI_Datatype datatype, MPI_Request *request)`;

Koriste se u sprezi sa operacijama:

- `MPI_Wait(MPI_Request *request, MPI_Status *status)` - blokirajuca, cekanje na zavrsetak operacije (analogija sa [POSIX `wait`](https://www.man7.org/linux/man-pages/man2/wait.2.html));
- `MPI_Test(MPI_Request *request, int *flag, MPI_Status *status)` - neblokirajuca, testira trenutno stanje operacije; `flag` ima vrednost `1` (True) ukoliko je operacije zavrsena, odnosno `0` u suprotnom;

#### Split collective (podeljeni kolektivni) I/O

U praksi predstavlja verziju kolektivnog neblokirajuceg I/O-a.

- `MPI_File_write_all_begin(MPI_File fh, const void *buf, int count, MPI_Datatype datatype)` - neblokirajuca, pocetak kolektivnog upisa;
- `MPI_File_write_all_end(MPI_File fh, const void *buf, MPI_Status *status)` - blokirajuca, cekanje na zavrsetak kolektivnog upisa;
- `MPI_File_read_all_begin(MPI_File fh, const void *buf, int count, MPI_Datatype datatype)` - neblokirajuca, pocetak kolektivnog citanja;
- `MPI_File_read_all_end(MPI_File fh, const void *buf, MPI_Status *status)` - blokirajuca, cekanje na zavrsetak kolektivnog citanja;

### Deljeni pokazivaci na fajlove

- `MPI_File_write_shared(MPI_File fh, const void *buf, int count, MPI_Datatype datatype, MPI_Status *status)` - upis sa deljenim pokazivacem;
- `MPI_File_read_shared(MPI_File fh, const void *buf, int count, MPI_Datatype datatype, MPI_Status *status)` - citanje sa deljenim pokazivacem;
- `MPI_File_seek_shared(MPI_File fh, MPI_Offset offset, int whence)` - pozicioniranje deljenog pokazivaca;

> [!IMPORTANT] Neblokirajuce verzije
> Postoje i neblokirajuce verzije ovih operacija.

#### Kolektivne (uredjene) operacije sa deljenim pokazivacem

Uredjenost po redosledu ranga procesa.

- `MPI_File_write_ordered(MPI_File fh, const void *buf, int count, MPI_Datatype datatype, MPI_Status *status)` - kolektivni uredjeni upis sa deljenim pokazivacem;
- `MPI_File_read_ordered(MPI_File fh, const void *buf, int count, MPI_Datatype datatype, MPI_Status *status)` - kolektivno uredjeno citanje sa deljenim pokazivacem;

## Reference

- [Illinois uni.](https://wgropp.cs.illinois.edu/courses/cs598-s16/lectures/lecture32.pdf);
- [Cornell uni.](https://cvw.cac.cornell.edu/parallel-io/mpi-io/index);
- [Stack Overflow - write vs write_all](https://stackoverflow.com/questions/38615856/how-does-mpi-file-write-differ-from-mpi-file-write-all);

[Povratak na pocetni README](../../README.md)