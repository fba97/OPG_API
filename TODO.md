# TODO OPG-APP

1 - Crea l'infrastruttura per poter avviare la partita.
        - partitaRepository: - CreaPartita() ok
                             - GetPartita(id) ok
                             - updatePartita(Partita) cosa updata? il nome? personaggi e oggetti son gia salvati.
                             - deletePartita ok
        - partitaController:    - CreaPartita ok
                                - LoadPartita 
                                - getAllPartite ok
                                
salavataggio e load: https://www.youtube.com/watch?v=f5GvfZfy3yk&ab_channel=DapperDino
loaddare una partita significa:
cancellare tutte le info che hai nelle tabelle attuali
 e riempirlo con tutte le informazioni CHE HAI RELATIVE A QUELLA PARTITA

 ci vuole quindi prima di tutto un metodo che riempie casualmente quelle tabelle 
 e poi uno che prendendo dalle tabelle salvataggio i dati li copia nelle tabelle attuali 

 per creare gli oggetti ha senso usare un factory oppure basta che faccio dei cicli guardando oggetti, personaggi, carte? 
 che vantaggi ho nel creare una factory?
 su l2-software-solution per creare gli item siccome hanno tutti una certa serie di parametri (id, subAreaId, code, description, subItems, type, communicationChannelId)
 estendono tutti item 

potrei creare un factory che estende un oggetto item (con id, nome, tipo, descrizione, posizionex, posizione y, idpartita,stato)



creazione degli oggetti, personaggi, creazione partita -> in base alle impostazioni che vengono inserite 



salvataggio e load della sessione:
in una tabella salvataggi: idSalvataggio, data, idPartita, json: 




da risistemare il personaggio_base e Personaggio_in_partita. i personaggi sono sempre gli stessi. sempre base. come gli oggetti.



## ULTIMO AGGIORNAMENTO DEL 26/11/2023
le tabelle sono tutte base.
Significa che non ci sono tabelle di oggetti_InPartita.
quando si caricano i personaggi li si carica da db nell'oggetto game.
quando devo creare la partita chiedo alcune informazioni all'utente. altre invece le crea l'init partita.
quando si deve salvare la partita la si salva come json nella tabella salvataggi. quando devo caricarla li riprendo e deserializzo.

azioni che devono fare i personaggi: movimenti, combattimenti, usare oggetti, raccogliere oggetti, scambiare oggetti



1.1 - ok - modifica le tabelle a db -> togli tutte quelle in partita, aggiunti i punti con la loro id, tessere, mappe, aree
1.2 - ok - modifica le primitives
1.3 - ok - modifica l'accesso ai dati per riempire gli oggetti
1.4 - modifica l'init della partita nella classe _game
1.5 - 

2.0 - crea il salva e il load della partita tramite serializzazione json



Mentre scrivevo game.sql ho notato che la repository sono un po merdose. il dto partita non è nel progetto primitives.
e mi sono chiesto a che cazzo servano le repo se tiro su tutto tramite game.sql
quindi sarebbe possibile toglierle. 
Per il momento le tengo lasciando il codice sporco perchè potrei doverle utilizzare.

Infatti ho chiesto a nico e mi dice che il modogiusto sono le repository e non game.sql

ma io me ne fotto e lo tiro su cosi al massimo farò un refactor.


mi sono accorto che in game stavo facendo query per prendere tutti i personaggi e non solo quelli in partita.
ho deciso che in game ho ALLpersonaggi, ALLOggetti, ALL mappe.. cosi se serve le faccio vedere al frontend oppure prendere oggetti a caso tra tutti gli oggetti..
e in game.PartitaAttuale ho dentro la partita che carico o nuova. 
e come faccio a prendere i personaggi nella partita? 
--> tiro su il json che ha solo gli id dei personaggi e oggetti. e poi l'oggetto personaggio/oggetto lo prendo da game che li ha gia tutti.
Anche se alcune proprietà vanno valorizzate come l'attacco gli oggetti assegnati. quindi per il momento serializziamo e poi vediamo come va

ce l'ho fatta a passare il singleton al progetto API. era piu semplice di quel che pensavo. bastava aggiungerlo alle dipendenze del program.cs (IServiceCollection) come singleton e poi chiamarlo dove mi serve dai _services.
