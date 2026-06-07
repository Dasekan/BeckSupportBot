# bc3fdb25 – Supplement: Indtast score / registrering af bordresultater

Denne fil beskriver resultatregistrering i BridgeCentral ud fra UI-observationer fra skærmbilledet **Indtast score**.

Filen supplerer den generelle vidensbase om `bc3fdb25` og fokuserer på:
- registrering af resultater pr. board/spil
- aktuelle spillere ved bordet
- kontrakt, declarer, stik og udspil
- beregnede scorer, procenter og matchpoint
- justeringer og afvigelser i afviklingen

Mappingen mellem UI og database er delvist baseret på observationer og bør derfor læses som sandsynlig, hvor relationen ikke er teknisk bekræftet.

---

# Indtast score

Resultatindtastning foregår i en konkret afviklingskontekst.

Et skærmbillede kan vise:
- turneringsdag
- spilletidspunkt
- række
- klubturnering
- skifteplan
- runde
- bord
- spil/board
- NS-par
- ØV-par
- aktuelle spillere ved bordet

Eksempel på kontekst:
- turneringsdag: `30-03-2026`
- spilletidspunkt: `19:00`
- række: `B-rækken`
- klubturnering: `Test par turnering i Mandagsklub`
- skifteplan: `Uendelig Howell, 10 par`

Det viser, at scoreindtastning ikke sker på turneringsniveau alene, men i en konkret sektion, runde og bordopstilling.

---

## Registreringsfelter

Ved scoreindtastning registreres typisk:

- bord
- spil / boardnummer
- NS-par
- ØV-par
- declarer
- kontrakt
- dobling/redobling
- antal stik
- udspil
- sårbarhed / zone
- aktuelle spillere ved bordet

Eksempel:

- bord: `1`
- spil: `2`
- NS: `10`
- ØV: `1`
- declarer: `V`
- kontrakt: `3 SP`
- stik: `9`
- udspil: `HJ`

---

## Betydning af felterne

### Bord

Bord angiver det konkrete bord i den aktuelle runde.

### Spil

Spil angiver det konkrete boardnummer, som resultatet registreres for.

### NS og ØV

NS og ØV angiver de parnumre, der sidder North-South og East-West ved bordet.

### Declarer

Declarer angiver den spillerretning, der spiller kontrakten.

Eksempel:
- `V` = Vest

### Kontrakt

Kontrakt angiver den meldte kontrakt.

Eksempel:
- `3 SP`

Dette svarer sandsynligvis til:
- niveau 3
- spar som trumf

### D/R

D/R angiver dobling eller redobling.

### Stik

Stik angiver antal vundne stik.

### Udspil

Udspil angiver åbningsudspillet.

### Zone

Zone angiver sårbarhed for det aktuelle spil.

---

# Resultatgrid

Resultatgridet viser registrerede resultater for spil/boards.

Typiske kolonner:
- spilnummer
- NS
- ØV
- spilfører
- kontrakt
- D/R
- stik
- udspil
- score NS
- score ØV
- procent NS
- procent ØV
- matchpoint NS
- matchpoint ØV
- bemærkning

Gridet viser, at BridgeCentral registrerer ét resultat pr.:
- bord
- spil/board
- NS-par
- ØV-par

Systemet beregner derefter:
- rå score
- procentscore
- matchpoint
- eventuelle bemærkninger eller afvigelser

---

# RESULT

`RESULT` er den centrale tabel for registrerede boardresultater.

### Relevante felter

- `FKMATCHID`
- `FKBOARDID`
- `BOARDNO`
- `BOARDGROUP`
- `BIDDINGSEQUENCE`
- `CONTRACT`
- `LEAD`
- `RESULT`
- `DECLARER`
- `DOUBLING`
- `TRICKS`
- `CALCULATEDSCORENS`
- `CALCULATEDSCORENSPCT`
- `CALCULATEDSCOREEW`
- `CALCULATEDSCOREEWPCT`
- `RESULTCOMPLETED`
- `EXCLUDEGAME`
- `BOARDCOMPARED`

### Mapping fra UI til database

- Kontrakt -> `RESULT.CONTRACT`
- Udspil -> `RESULT.LEAD`
- Declarer -> `RESULT.DECLARER`
- Dobling/redobling -> `RESULT.DOUBLING`
- Stik -> `RESULT.TRICKS`
- Score NS -> `RESULT.CALCULATEDSCORENS`
- Score ØV -> `RESULT.CALCULATEDSCOREEW`
- Procent NS -> `RESULT.CALCULATEDSCORENSPCT`
- Procent ØV -> `RESULT.CALCULATEDSCOREEWPCT`

---

## Fortolkning af `RESULT.RESULT`

Databasen indeholder både:
- `TRICKS`
- `RESULT`

`TRICKS` ser ud til at være antal faktiske stik.

`RESULT` ser ud til at være en intern eller afledt resultatværdi, som beskriver resultatet relativt til kontrakten.

Eksempel:
- kontrakt: 3 spar
- declarer: Vest
- stik: 9

Dette kan internt svare til:
- kontrakten er gået hjem
- resultatet er lig kontrakten
- en intern numerisk resultatværdi

Den præcise betydning af `RESULT.RESULT` bør behandles forsigtigt, hvis den ikke er verificeret direkte i databasen eller applikationslogikken.

---

# Relation til ROUNDMATCH

`ROUNDMATCH` beskriver bordopstillingen i den aktuelle runde.

Resultatindtastningen bruger sandsynligvis `ROUNDMATCH` til at finde:
- aktuelt bord
- NS-par
- ØV-par
- spillere ved bordet
- board-sæt
- relation til runden

### Relevante felter

- `FKROUNDID`
- `TABLENO`
- `BOARDSET`
- `BOARDSPEC`
- `NORTHTEAMNO`
- `NORTHPAIRNO`
- `FKNORTHSECTIONTEAMID`
- `FKNORTHSECTIONPLAYERID`
- `SOUTHTEAMNO`
- `SOUTHPAIRNO`
- `FKSOUTHSECTIONTEAMID`
- `FKSOUTHSECTIONPLAYERID`
- `EASTTEAMNO`
- `EASTPAIRNO`
- `FKEASTSECTIONTEAMID`
- `FKEASTSECTIONPLAYERID`
- `WESTTEAMNO`
- `WESTPAIRNO`
- `FKWESTSECTIONTEAMID`
- `FKWESTSECTIONPLAYERID`

### Proces

1. Systemet finder den aktuelle runde.
2. Systemet finder bordet i `ROUNDMATCH`.
3. Systemet finder de par og spillere, der sidder NS/ØV.
4. Det konkrete bordresultat gemmes i `RESULT`.

---

# Relation til ROUND

`ROUND` organiserer afviklingen pr. runde.

### Relevante felter

- `FKSECTIONID`
- `ROUNDNO`
- `HALFNO`
- `STARTTIME`
- `ENDTIME`
- `STARTBOARD`

`ROUND` er den strukturelle rundeenhed.  
`ROUNDMATCH` er den konkrete bordopstilling inden for runden.

---

# Relation til BOARD og BOARDSPEC

Resultatindtastningen er koblet til konkrete boards.

### Relevante tabeller

- `BOARDSPEC`
- `BOARD`
- `RESULT`

### Relationer

- `BOARDSPEC.FKSECTIONID -> SECTION.ID`
- `BOARD.FKBOARDSPECID -> BOARDSPEC.ID`
- `RESULT.FKBOARDID -> BOARD.ID`

`BOARDSPEC` beskriver board-sættet for sektionen.  
`BOARD` beskriver det konkrete board/spil.  
`RESULT` gemmer resultatet for boardet.

---

# Justering af resultater

UI indeholder funktionen **Justering**.

Det betyder, at et resultat kan behandles som:
- justeret resultat
- kunstigt resultat
- korrigeret resultat
- afvigelse fra normal scoreindtastning

### Relevante felter i `ROUNDMATCH`

- `OVERRULEDIMPSNS`
- `OVERRULEDIMPSEW`
- `OVERRULEDVPNS`
- `OVERRULEDVPEW`
- `ADJUSTEDIMPSNS`
- `ADJUSTEDIMPSEW`

### Relevante felter i `RESULT`

- `EXCLUDEGAME`
- `RESULTCOMPLETED`

Justering er en del af resultatbehandlingen og hører til den konkrete afvikling.

---

# Skift spillerplacering og afvigelser

UI indeholder funktionen **Skift spiller placering**.

Der kan også forekomme markeringer for afvigelser som:
- forkert led / retning
- forkert kortfordeling
- forkerte spillere ved bordet
- forkert spilnummer

Det viser, at BridgeCentral kan håndtere situationer, hvor den faktiske afvikling afviger fra den planlagte bordopstilling.

### Relevante tabeller

- `ROUNDMATCH`
- `RESULT`
- `SECTIONPLAYER`

---

# Matchpoint og procenter

I parturnering kan `RESULT` indeholde både rå kontraktdata og beregnede scorer.

### Relevante felter

- `CALCULATEDSCORENS`
- `CALCULATEDSCOREEW`
- `CALCULATEDSCORENSPCT`
- `CALCULATEDSCOREEWPCT`

Disse felter bruges til:
- score for NS
- score for ØV
- procent for NS
- procent for ØV
- matchpointberegning
- resultatvisning

---

# Spillere og parniveau

Scoreindtastningen viser både:
- parnummer/teamnummer
- konkrete spillernavne

Det passer med modellen:

`SECTIONTEAM`
→ par eller hold i sektionen

`SECTIONPLAYER`
→ spillere i sektionen

`ROUNDMATCH`
→ placering af par/spillere ved bordet

---

# Samlet model for scoreindtastning

Scoreindtastning hænger sammen med følgende tabeller:

## Turneringskontekst

- `MAINTOURNAMENT`
- `GROUPTOURNAMENT`
- `SECTION`

## Startliste og deltagere

- `SECTIONTEAM`
- `SECTIONPLAYER`

## Runde og bordopstilling

- `ROUND`
- `ROUNDMATCH`

## Boards og kortfordelinger

- `BOARDSPEC`
- `BOARD`

## Registrerede resultater

- `RESULT`

## Handicap og beregninger

- `HACROUND`
- `HACROUNDPLAYER`

---

# Samlet flow

`MAINTOURNAMENT`
→ `GROUPTOURNAMENT`
→ `SECTION`
→ `ROUND`
→ `ROUNDMATCH`
→ `RESULT`

Samtidig:

`SECTION`
→ `BOARDSPEC`
→ `BOARD`
→ `RESULT`

Deltagerdata:

`SECTIONTEAM`
→ `SECTIONPLAYER`
→ `ROUNDMATCH`

---

# Konklusion

Skærmbilledet **Indtast score** viser, at BridgeCentral registrerer resultater på et konkret afviklingsniveau.

Et resultat hører typisk til:
- en bestemt turnering
- en bestemt række
- en bestemt sektion
- en bestemt runde
- et bestemt bord
- et bestemt board/spil
- konkrete NS- og ØV-par
- konkrete spillere ved bordet

Den vigtigste tabelkobling er:

- `ROUNDMATCH` = bordopstilling og deltagere
- `BOARD` = konkret spil/board
- `RESULT` = registreret bordresultat

