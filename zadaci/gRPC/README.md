# gRPC

## Sta je najbitnije da se zna

- kako se pise protobuf specifikacija, poruke (`message`), servisi (`service`) odnosno deklaracije poziva udaljenih metoda (`rpc`);
- sta je *unary* RPC, *client streaming* RPC, *bidirectional* (full duplex) streaming RPC (i pozeljno je da se razume _server streaming_) - kako da prepoznas sta kad treba da pises;
- kako se implementira servis u C#-u po datoj protobuf specifikaciji;
  - izvodi se iz `ImeDeklarisanogPaketa.ImeDeklarisanogServisaBase`;
  - `override`-uju se sve RPC metode deklarisane u protobuf specifikaciji;
  - za stream RPC-eve:
    - `IAsyncStreamReader` - kada klijent stream-uje;
    - `IServerStreamWriter` - kada server stream-uje;
    - `MoveNext` i `Current` - metode za citanje sledeceg podatka u stream-u;
    - `WriteAsync` za upisivanje podatka u stream;


> [!IMPORTANT] VAZNO
> Vodi racuna o konkurentnosti - implementiraj neki *mutual exclusion* ili koristi [konkurentne strukture](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/data-structures-for-parallel-programming)

## Primeri

### simple-ping

- Uvodni primer - prost gRPC ping servis;

Implementacija:

- Link do [proto fajla](./simple-ping/SimplePingServer/Protos/simple_ping.proto);
- Link do [implementacije servisa](./simple-ping/SimplePingServer/Services/SimplePingService.cs) po protobuf specifikaciji;
- Link do [klijenta](./simple-ping/SimplePingClient/Program.cs);

### simple-messaging

- Zadatak iz Junskog ispitnog roka (i prvog kolokvijuma);

Implementacija:

- Link do [proto fajla](./simple-messaging/SimpleMessagingServer/Protos/simple_messaging.proto);
- Link do [implementacije servisa](./simple-messaging/SimpleMessagingServer/Services/SimpleMessagingService.cs) po protobuf specifikaciji;
- Link do [klijenta](./simple-messaging/SimpleMessagingClient/Program.cs);

### accumulator-ops

Postavka:

Implementirati i testirati servis koji u sebi sadrzi prost celobrojni akumulator, nad kojim se moze vrsiti 3 operacije:

- INC - inkrementira vrednost akumulatora i vraca vrednost nakon inkrementiranja;
- DEC - dekrementira vrednost akumulatora i vraca vrednost nakon dekrementiranja;
- ADD op - dodaje operand `op` akumulatoru i vraca vrednost nakon dekrementiranja;
- NOP - dodatak, ne radi nista, vec samo vraca vrednost akumulatora (sluzi za naznacavanje kraja stream-ova);

> Napomena: na optimalnost lock-ova se nije mnogo obracalo paznje, oni su tu samo reprezentativno, da bi moglo vise klijenata istovremeno da pristupa servisu bez konflikata. Nisu bitni za sam koncept gRPC-a, vec su vise dobra praksa, zato sto je thread-safety nezaobilazna komponenta servera u realnim situacijama.

Implementacija:

- Link do [proto fajla](./accumulator-ops/AccumulatorOps/Protos/accumulator_ops.proto);
- Link do [implementacije servisa](./accumulator-ops/AccumulatorOps/Services/AccumulatorOps.cs) po protobuf specifikaciji;
- Link do [klijenta](./accumulator-ops/AccumulatorOpsTests/Program.cs);

### vlasnici-skladista

- TBA;

[Povratak na pocetni README](../../README.md)