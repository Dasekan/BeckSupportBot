# BridgeCentral og Bridgemate – Drift, scoring og turneringsafvikling

## Formål

Dette dokument beskriver samspillet mellem:

- BridgeCentral / BC3
- Bridgemate II (BMII)
- Bridgemate III (BMIII)
- turneringsafvikling
- elektronisk resultatregistrering
- justeret score
- fejlhåndtering under turnering

Dokumentet bruges som vidensgrundlag for supportbotten ved spørgsmål om:

- Bridgemate-opsætning
- elektronisk scoreindtastning
- fejl under turnering
- forbindelse mellem Bridgemate og BridgeCentral
- justering af resultater
- reset og fejlfinding på Bridgemate-enheder

---

# Bridgemate i BridgeCentral

BridgeCentral kan anvende Bridgemate-enheder til elektronisk registrering af resultater ved bordene.

Bridgemate bruges til:

- indtastning af boardresultater
- overførsel af resultater til turneringssystemet
- hurtigere scoring
- reduktion af tastefejl
- live opdatering af resultater

Resultater registreres ved bordet og sendes til turneringssoftwaren via en serverforbindelse.

---

# Systemkomponenter

## Bridgemate-enheder

Bridgemate-enheder står ved bordene og bruges af spillerne til:

- indtastning af boardnummer
- indtastning af kontrakt
- indtastning af resultat/stik
- godkendelse af resultat

Der findes flere generationer:

- Bridgemate II (BMII)
- Bridgemate III (BMIII)

---

## Bridgemate-server

Serveren forbinder Bridgemate-enhederne med computeren, der kører BridgeCentral eller tilhørende turneringssoftware.

Serveren kommunikerer trådløst med enhederne.

Typisk forbindes serveren via USB til turnerings-PC’en.

---

## Turneringssoftware

Turneringssoftwaren bruges til:

- opsætning af turnering
- håndtering af runder og borde
- modtagelse af resultater
- beregning af score
- visning af resultater

I BridgeCentral hænger dette tæt sammen med:

- `GROUPTOURNAMENT`
- `ROUND`
- `ROUNDMATCH`
- `RESULT`

---

# Sammenhæng med bc3fdb25-datamodellen

Bridgemate-data ender typisk som konkrete resultater i:

## `RESULT`

Denne tabel indeholder selve bordresultatet.

Typiske felter:

- `CONTRACT`
- `DECLARER`
- `DOUBLING`
- `TRICKS`
- `LEAD`
- `CALCULATEDSCORENS`
- `CALCULATEDSCOREEW`
- `CALCULATEDSCORENSPCT`
- `CALCULATEDSCOREEWPCT`

Bridgemate-indtastning matcher derfor direkte den scoreindtastning, som tidligere er observeret i BridgeCentral.

---

## `ROUNDMATCH`

`ROUNDMATCH` beskriver:

- hvilket bord der spilles ved
- hvilke par/spillere der sidder NS/ØV
- hvilken runde der er aktiv

Bridgemate bruger i praksis denne struktur til at validere:

- bordnummer
- boardnummer
- parnumre
- placeringer

---

## `ROUND`

`ROUND` beskriver den aktuelle runde i turneringen.

Bridgemate-afviklingen følger typisk den aktive rundeopsætning.

---

# Indtastning af resultater

Ved bordet indtastes typisk:

- boardnummer
- NS-par
- ØV-par
- kontrakt
- declarer
- antal stik
- eventuel dobling/redobling

Derefter:

- modspiller godkender resultatet
- resultatet sendes til serveren
- resultatet registreres i systemet
- score beregnes automatisk

---

# Parturneringer

I parturneringer beregnes typisk:

- matchpoint
- procentscore

Resultater kobles til:

- skifteplan
- bord
- runde
- boardnummer

BridgeCentral anvender her typisk:

- `MOVEMENTPLAN`
- `ROUND`
- `ROUNDMATCH`
- `RESULT`

---

# Holdturneringer

I holdturneringer sammenlignes resultater mellem to borde.

Systemet beregner typisk:

- IMP
- VP

Holdturneringer kræver derfor:

- korrekt synkronisering af boards
- korrekt bordplacering
- korrekt hold/par-identifikation

---

# Bridgemate II (BMII)

Bridgemate II er en ældre generation af Bridgemate-enheder.

BMII bruges stadig mange steder til:

- parturneringer
- holdturneringer
- klubturneringer

---

## BMII opsætning – par

Ved parturneringer skal systemet typisk opsættes med:

- antal borde
- turneringsform
- skifteplan
- runder
- boards pr. runde

Typiske turneringsformer:

- Mitchell
- Howell
- Uendelig Howell
- Barometer

---

## BMII opsætning – hold

Ved holdturneringer opsættes typisk:

- antal hold
- antal kampe/runder
- boards pr. kamp
- IMP/VP-beregning

---

# Bridgemate III (BMIII)

Bridgemate III er nyere hardware med forbedret brugeroplevelse og nyere serverløsning.

Grundfunktionen er den samme:

- indtastning af resultater
- godkendelse ved bordet
- trådløs overførsel
- integration med turneringssoftware

---

# Reset af Bridgemate

Reset bruges ved:

- tekniske problemer
- fejl i opsætning
- klargøring til ny turnering
- genbrug af enheder

---

## Blød reset

Blød reset er typisk en genstart.

Data og opsætning bevares normalt.

Bruges ved mindre problemer.

---

## Fuld reset

Fuld reset gendanner fabriksindstillinger.

Kan slette:

- konfiguration
- forbindelsesoplysninger
- gemte data

Bør ikke udføres midt i en aktiv turnering.

---

## Efter reset

Efter reset skal man typisk kontrollere:

- bordnummer
- forbindelse til server
- sprog/opsætning
- at enheden kan kommunikere korrekt

---

# Typiske problemer

## Ingen forbindelse

Mulige årsager:

- server ikke tilsluttet
- svag forbindelse
- batteriproblem
- forkert opsætning

Typiske løsninger:

- genstart enhed
- kontrollér server
- kontrollér USB-forbindelse
- flyt enhed tættere på server

---

## Forkert resultat

Mulige årsager:

- tastefejl
- forkert boardnummer
- forkert parnummer
- forkert kontrakt/stik

Løsning:

- ret straks på enheden
- eller få turneringsleder til at rette i systemet

---

## Manglende board

Mulige årsager:

- board ikke registreret
- forkert runde
- forkert boardnummer

Turneringsleder bør kontrollere:

- `ROUND`
- `ROUNDMATCH`
- boardopsætning
- aktiv skifteplan

---

# Justeret score

Justeret score bruges når et spil ikke kan scores normalt.

Typiske årsager:

- tekniske problemer
- forkert board
- regelbrud
- fejl i kortfordeling
- manglende resultat

---

## Typer af justeret score

### Kunstig justeret score

Typisk:

- 60/40
- 50/50

Bruges uden analyse af det sandsynlige resultat.

---

### Vægtet justeret score

Turneringsleder vurderer sandsynlige udfald og fordeler resultatet derefter.

---

## Relation til databasen

Justeringer hænger sandsynligvis sammen med:

### `RESULT`

Felter som:

- `RESULTCOMPLETED`
- `EXCLUDEGAME`

---

### `ROUNDMATCH`

Felter som:

- `OVERRULEDIMPSNS`
- `OVERRULEDIMPSEW`
- `OVERRULEDVPNS`
- `OVERRULEDVPEW`
- `ADJUSTEDIMPSNS`
- `ADJUSTEDIMPSEW`

Det tyder på, at systemet understøtter både:

- manuelle justeringer
- overstyrede scorer
- kunstige resultater

---

# Turneringslederens rolle

Turneringslederen kan typisk:

- overvåge indkomne resultater
- rette fejl
- genåbne boards
- sende beskeder til borde
- håndtere justerede scorer
- kontrollere manglende resultater

---

# Bedste praksis

## Før turneringen

Kontrollér:

- serverforbindelse
- batterier
- skifteplan
- runder
- boardopsætning
- at Bridgemate-systemet virker

---

## Under turneringen

- ret fejl hurtigt
- kontrollér manglende boards
- overvåg forbindelsen
- sørg for korrekt bordplacering

---

## Efter turneringen

Kontrollér:

- alle boards er registreret
- resultater er beregnet korrekt
- eventuelle justeringer er håndteret

---

# Hvad supportbotten bør forstå

Supportbotten bør forstå at:

- Bridgemate er tæt integreret med turneringsafviklingen
- resultatindtastning hænger direkte sammen med `RESULT`
- bordplaceringer kommer fra `ROUNDMATCH`
- skifteplaner styrer runder og bordplaceringer
- parturnering og holdturnering bruger forskellige scoringsmodeller
- justeret score er en del af den normale turneringsadministration

Botten bør også kunne hjælpe med:

- Bridgemate-fejl
- serverproblemer
- manglende resultater
- forkert scoreindtastning
- reset/genstart
- forståelse af parturnering vs. holdturnering

---

# Kort opsummering

BridgeCentral og Bridgemate arbejder sammen om:

- elektronisk resultatregistrering
- turneringsafvikling
- scoring
- live resultater
- håndtering af fejl og justeringer

De vigtigste tabeller i bc3fdb25 ser ud til at være:

- `MOVEMENTPLAN`
- `ROUND`
- `ROUNDMATCH`
- `RESULT`

Bridgemate fungerer som inputenhed til den konkrete registrering af bordresultater.
