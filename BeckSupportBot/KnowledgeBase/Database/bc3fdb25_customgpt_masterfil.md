# BC3 / bc3fdb25 – vidensbase

## Indhold

1. Startpakke
2. Medlem og klub
3. Turneringsopsætning
4. Movement plan
5. Sektion / live-afvikling

---

# Startpakke

Databasen `bc3fdb25` kan overordnet opdeles i disse domæner:

## Klub og medlemmer

Bruges til klubopsætning, medlemmer, underklubber/spillegrupper og medlemskaber.

Vigtige tabeller:
- `SYS_MAINCLUB`
- `MEM_MEMBER`
- `MEM_CLUB`
- `MEM_MEMBER_CLUB`
- `MEM_CONTACT`
- `SYS_DISTRICT`
- `SYS_MP_TITLES`

## Turneringsopsætning

Bruges til oprettelse af turneringer, hold/par og spillerregistrering.

Vigtige tabeller:
- `MAINTOURNAMENT`
- `MAINTOURNAMENTTEAM`
- `MAINTOURNAMENTPLAYER`
- `GROUPTOURNAMENT`

## Movement plans / turneringsstruktur

Bruges til bevægelsesplaner og teknisk struktur for turneringsafvikling.

Vigtige tabeller:
- `MOVEMENTPLAN`
- `MOVEMENTPLANDETAIL`
- `MOVEMENTPLANREFERENCE`

## Sektion og live-afvikling

Bruges til konkret afvikling af turneringer i sektioner, runder og bordopstillinger.

Vigtige tabeller:
- `SECTION`
- `SECTIONTEAM`
- `SECTIONPLAYER`
- `ROUND`
- `ROUNDMATCH`

## Kort og resultater

Bruges til board-sæt, konkrete boards og registrerede resultater.

Vigtige tabeller:
- `BOARDSPEC`
- `BOARD`
- `RESULT`
- `CONTRACT`

## Handicap / HAC

Bruges til handicapberegning på rundeniveau og spillerniveau.

Vigtige tabeller:
- `HACROUND`
- `HACROUNDPLAYER`

## Hjemmeside / CMS

Bruges til klubbens hjemmeside, sider, links og standardindhold.

Vigtige tabeller:
- `WEB_HTML_PAGE`
- `WEB_HTML_PARAGRAPH`
- `WEB_HTML_LINK`
- `LIST_HTML_PARAGRAPH_DEFAULT`

---

# Centrale relationer

## Tekniske relationer

- `GROUPTOURNAMENT.FKMAINTOURNAMENTID -> MAINTOURNAMENT.ID`
- `MAINTOURNAMENTTEAM.FKMAINTOURNAMENTID -> MAINTOURNAMENT.ID`
- `MAINTOURNAMENTPLAYER.FKMAINTOURNAMENTTEAMID -> MAINTOURNAMENTTEAM.ID`
- `SECTION.FKGROUPTOURNAMENTID -> GROUPTOURNAMENT.ID`
- `ROUND.FKSECTIONID -> SECTION.ID`
- `ROUNDMATCH.FKROUNDID -> ROUND.ID`
- `RESULT.FKMATCHID -> ROUNDMATCH.ID`
- `RESULT.FKBOARDID -> BOARD.ID`
- `BOARD.FKBOARDSPECID -> BOARDSPEC.ID`
- `MOVEMENTPLANDETAIL.FKMOVEMENTPLANID -> MOVEMENTPLAN.ID`
- `SECTIONTEAM.FKSECTIONID -> SECTION.ID`
- `SECTIONPLAYER.FKSECTIONID -> SECTION.ID`
- `SECTIONPLAYER.FKSECTIONTEAMID -> SECTIONTEAM.ID`

## Logiske relationer

- `GROUPTOURNAMENT.FKMOVEMENTPLANID -> MOVEMENTPLAN.ID`
- `BOARDSPEC.FKSECTIONID -> SECTION.ID`
- `SECTIONTEAM.FKMAINTOURNAMENTTEAMID -> MAINTOURNAMENTTEAM.ID`
- `SECTIONPLAYER.FKMAINTOURNAMENTPLAYERID -> MAINTOURNAMENTPLAYER.ID`
- `MEM_MEMBER_CLUB.MEMBER_ID -> MEM_MEMBER.MEMBER_ID`
- `MEM_MEMBER_CLUB.CLUB_ID -> MEM_CLUB.CLUB_ID`
- `HACROUND.FKGROUPTOURNAMENTID -> GROUPTOURNAMENT.ID`
- `HACROUND.FKSECTIONID -> SECTION.ID`
- `HACROUND.FKROUNDID -> ROUND.ID`
- `HACROUNDPLAYER.FKHACROUNDID -> HACROUND.ID`

---

# Overordnet dataflow

## Medlemsflow

`SYS_MAINCLUB`
→ `MEM_MEMBER`
→ `MEM_MEMBER_CLUB`
→ `MEM_CLUB`

## Turneringsflow

`MAINTOURNAMENT`
→ `MAINTOURNAMENTTEAM`
→ `MAINTOURNAMENTPLAYER`

## Afviklingsflow

`MAINTOURNAMENT`
→ `GROUPTOURNAMENT`
→ `SECTION`
→ `ROUND`
→ `ROUNDMATCH`
→ `RESULT`

## Board-flow

`SECTION`
→ `BOARDSPEC`
→ `BOARD`
→ `RESULT`

## Movement plan-flow

`MOVEMENTPLAN`
→ `MOVEMENTPLANDETAIL`

`GROUPTOURNAMENT`
→ `MOVEMENTPLAN`

## Hjemmeside-flow

`WEB_HTML_PAGE`
→ `WEB_HTML_PARAGRAPH`
→ `WEB_HTML_LINK`

---

# Medlem og klub

Medlem- og klubdelen håndterer:
- hovedklubbens opsætning
- medlemmer og stamdata
- underklubber og spillegrupper
- kobling mellem medlemmer og underklubber
- kontaktpersoner
- distrikter
- mesterpointtitler

---

## SYS_MAINCLUB

`SYS_MAINCLUB` beskriver hovedklubbens eller installationens konfiguration.

### Primærnøgle

- `MAINCLUB_ID`

### Vigtige felter

- `MAINCLUB_ID`
- `ORG_CLUB_NO`
- `LOCATION`
- `NAME`
- `ADDRESS_1`
- `ADDRESS_2`
- `ZIP_CODE`
- `CITY`
- `PHONE_1`
- `PHONE_2`
- `EMAIL`
- `HOMEPAGE`
- `DISTRICT_NO`
- `USES_MP`
- `USE_GROUPS`
- `USE_CLUB_NAME`
- `USE_BRIDGEMATE`
- `USE_FLEXIBLE`
- `LANGUAGE_CODE`
- `SYSTEM_START_DATE`
- `SYSTEM_LAST_LOGIN`

### Interne systemfelter

- `CLUB_HOST_LOGIN`
- `CLUB_HOST_PASSWORD`
- `CLUB_WEB_FTP_LOGIN_ORG`
- `CLUB_WEB_FTP_PASSWORD_ORG`
- `SENDER_SMTP_USER_ID`
- `SENDER_PASSWORD`
- `CURRENTUSER`

---

## MEM_MEMBER

`MEM_MEMBER` er den centrale medlemstabel.

### Primærnøgle

- `MEMBER_ID`

### Vigtige felter

- `MEMBER_ID`
- `ORG_MEMBER_ID`
- `MEMBER_NO`
- `STATUS`
- `TITLE`
- `NAME`
- `FIRST_NAME`
- `MIDDLE_NAME`
- `LAST_NAME`
- `ADDRESS_1`
- `ADDRESS_2`
- `ZIP_CODE`
- `CITY`
- `COUNTRY_CODE`
- `PHONE_1`
- `PHONE_2`
- `PHONE_3`
- `EMAIL`
- `CLUB_STATUS_ID`
- `CLUB_START`
- `CLUB_TITLE_LEVEL`
- `ORG_TITLE_LEVEL`
- `TOTAL_BRONZE`
- `TOTAL_SILVER`
- `TOTAL_GOLD`
- `TOTAL_MASTER`
- `CLUB_NAME`
- `IS_PRIMARY_CLUB`
- `BIRTHDAY`
- `HAC`
- `SIDST_RETTET`

### Følsomme felter

- `CPR`
- `NOTES`

---

## MEM_CLUB

`MEM_CLUB` beskriver underklubber, spillegrupper eller lokale klubenheder i hovedklubben.

### Primærnøgle

- `CLUB_ID`

### Vigtige felter

- `CLUB_ID`
- `ORG_CLUB_ID`
- `CLUB_NAME`
- `GAME_DAY`
- `GAME_TIME`
- `LEADER_ID`
- `IS_VISIBLE`
- `NO_SMOKING`

---

## MEM_MEMBER_CLUB

`MEM_MEMBER_CLUB` kobler medlemmer til underklubber.

### Primærnøgle

- `MEMBER_CLUB_ID`

### Vigtige felter

- `MEMBER_CLUB_ID`
- `MEMBER_ID`
- `CLUB_ID`
- `PARTNER_ID`
- `PARTNER_FIRST`
- `IS_SUBSTITUTE`
- `GROUP_INDEX`
- `LAST_CHANGED_BY`
- `LAST_CHANGED_DATE`

### Relationer

- `MEMBER_ID -> MEM_MEMBER.MEMBER_ID`
- `CLUB_ID -> MEM_CLUB.CLUB_ID`
- `PARTNER_ID -> MEM_MEMBER.MEMBER_ID`

---

## MEM_CONTACT

`MEM_CONTACT` indeholder kontaktpersoner eller kontaktoplysninger knyttet til medlemmer.

### Primærnøgle

- `CONTACT_NO`

### Vigtige felter

- `CONTACT_NO`
- `TITLE`
- `MEMBER_ID`
- `COMMENTS`

### Relation

- `MEMBER_ID -> MEM_MEMBER.MEMBER_ID`

---

## SYS_DISTRICT

`SYS_DISTRICT` er opslagstabel for distrikter.

### Primærnøgle

- `LANGUAGE_CODE`
- `DISTRICT_NO`

### Vigtige felter

- `LANGUAGE_CODE`
- `DISTRICT_NO`
- `DISTRICT_NAME`

---

## SYS_MP_TITLES

`SYS_MP_TITLES` er opslagstabel for mesterpointtitler.

### Primærnøgle

- `LANGUAGE_CODE`
- `MP_LEVEL`

### Vigtige felter

- `LANGUAGE_CODE`
- `MP_LEVEL`
- `NUM_MP`
- `MP_TITLE`

### Relationer

- `MEM_MEMBER.CLUB_TITLE_LEVEL -> SYS_MP_TITLES.MP_LEVEL`
- `MEM_MEMBER.ORG_TITLE_LEVEL -> SYS_MP_TITLES.MP_LEVEL`

---

# Turneringsopsætning

Turneringsopsætningen beskriver:
- den overordnede turnering
- hold/par i turneringen
- spillere i turneringen
- gruppeopsætning
- kobling til movement plan

Turneringsopsætningen ligger før live-afvikling.

Overordnet model:

`MAINTOURNAMENT`
→ `MAINTOURNAMENTTEAM`
→ `MAINTOURNAMENTPLAYER`

og:

`MAINTOURNAMENT`
→ `GROUPTOURNAMENT`
→ `SECTION`
→ `ROUND`
→ `ROUNDMATCH`
→ `RESULT`

---

## MAINTOURNAMENT

`MAINTOURNAMENT` er hovedtabellen for en turnering.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `NAME`
- `DESCRIPTION`
- `FKCLUBID`
- `TOURNAMENTFORM`
- `COMMONTOP`
- `INCLUDECLUBNAME`
- `USELEADS`
- `STRENGTHGROUPCOUNT`
- `NUMBEROFGROUPS`
- `NUMBEROFPLAYINGDAYS`
- `IS_VISIBLE`
- `DO_WEB_PUBLISH`
- `IS_FLEXIBLE`
- `FLEXIBLEPERCENT`

### Relationer

- `FKCLUBID -> MEM_CLUB.CLUB_ID`

---

## MAINTOURNAMENTTEAM

`MAINTOURNAMENTTEAM` beskriver hold eller par i hovedturneringen.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `FKMAINTOURNAMENTID`
- `TEAMNO`
- `TEAMNAME`
- `TOURNAMENTTYPE`

### Relation

- `FKMAINTOURNAMENTID -> MAINTOURNAMENT.ID`

---

## MAINTOURNAMENTPLAYER

`MAINTOURNAMENTPLAYER` beskriver spillere i et hold/par i hovedturneringen.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `FKMAINTOURNAMENTTEAMID`
- `FKPLAYERID`
- `PLAYERNAME`
- `STARTHAC`
- `TEAMNO`
- `PLAYERNO`
- `PAIRNO`
- `ISCAPTAIN`

### Relationer

- `FKMAINTOURNAMENTTEAMID -> MAINTOURNAMENTTEAM.ID`
- `FKPLAYERID -> MEM_MEMBER.MEMBER_ID`

`PLAYERNAME` fungerer som turneringsspecifikt visningsnavn.  
`STARTHAC` angiver spillerens handicap ved turneringsstart.

---

## GROUPTOURNAMENT

`GROUPTOURNAMENT` beskriver en gruppe- eller afviklingsopsætning under en hovedturnering.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `FKMAINTOURNAMENTID`
- `GROUPNO`
- `DESCRIPTION`
- `TOURNAMENTTYPE`
- `NUMBEROFTEAMS`
- `NUMBEROFSECTIONS`
- `NUMBEROFROUNDS`
- `NUMBEROFTABLES`
- `BOARDSPERROUND`
- `HALVESPERMATCH`
- `CALCULATIONMETHOD`
- `ISMITCHELL`
- `ISMONRAD`
- `REGISTERMP`
- `USEBOARDSPEC`
- `FKMOVEMENTPLANID`
- `CALCULATEHAC`
- `TOURNAMENTTEAMTYPE`
- `TOURNAMENTPAIRCALCTYPE`
- `TOURNAMENTMATCHPOINTTYPE`
- `STARTNOTES`
- `RESULTNOTES`
- `USELEADS`
- `SIMPLIFIEDHAC`

### Relationer

- `FKMAINTOURNAMENTID -> MAINTOURNAMENT.ID`
- `FKMOVEMENTPLANID -> MOVEMENTPLAN.ID`

`GROUPTOURNAMENT` er bindeled mellem den overordnede turnering og den konkrete afvikling.  
Antal runder, borde, sektioner og boards pr. runde findes typisk her.

---

# Movement plan

Movement plan-delen beskriver, hvordan en turnering afvikles teknisk:
- antal borde
- antal runder
- placeringer pr. runde og bord
- flytning af par/hold
- relation til gruppe-/turneringsopsætning

Vigtige tabeller:
- `MOVEMENTPLAN`
- `MOVEMENTPLANDETAIL`
- `MOVEMENTPLANREFERENCE`
- `GROUPTOURNAMENT`

---

## MOVEMENTPLAN

`MOVEMENTPLAN` er hovedtabellen for en movement plan.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `ISORIGINAL`
- `FKLANGUAGEID`
- `TOURNAMENTTYPE`
- `NOOFTEAMS`
- `NOOFTABLES`
- `NOOFROUNDS`
- `MOVEMENTPLANTYPE`
- `NAME`
- `DESCRIPTION`
- `REGISTRATION`
- `ISVISIBLE`
- `WEBLINK`
- `ISHANDICAP`
- `SUBMOVEMENTPLANTYPE`
- `NOOFCUTOFF`
- `SPLITROUNDS`

### Relationer

- `MOVEMENTPLAN.ID -> MOVEMENTPLANDETAIL.FKMOVEMENTPLANID`
- `GROUPTOURNAMENT.FKMOVEMENTPLANID -> MOVEMENTPLAN.ID`

---

## MOVEMENTPLANDETAIL

`MOVEMENTPLANDETAIL` indeholder detaljelinjer for en movement plan.

Tabellen beskriver movement-strukturen pr. runde og bord.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `FKMOVEMENTPLANID`
- `ROUNDNO`
- `TABLENO`
- `NORTH`
- `SOUTH`
- `EAST`
- `WEST`
- `BOARDSPECINDEX`
- `BOARDSETINDEX`
- `NSPAIR`
- `EWPAIR`
- `HALFNO`

### Relation

- `FKMOVEMENTPLANID -> MOVEMENTPLAN.ID`

Hver række beskriver typisk:
- movement plan
- runde
- bord
- placering af par/hold
- boardsæt eller boardspec

---

## MOVEMENTPLANREFERENCE

`MOVEMENTPLANREFERENCE` indeholder referenceoplysninger for movement plans.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `FKMOVEMENTPLANID`
- `TOURNAMENTFORM`
- `NOOFTABLES`
- `NOOFGAMES`

### Relation

- `FKMOVEMENTPLANID -> MOVEMENTPLAN.ID`

Tabellen bruges til at knytte movement plans til turneringsform, antal borde og antal spil.

---

# Sektion / live-afvikling

Sektion/live-afvikling beskriver den konkrete afvikling af en turnering efter oprettelse af hovedturnering og gruppeturnering.

Denne del omfatter:
- sektioner
- hold/par i sektionen
- spillere i sektionen
- runder
- bordopstillinger/matches

---

## SECTION

`SECTION` repræsenterer en konkret sektion under en gruppeturnering.

### Primærnøgle

- `ID`

### Vigtige felter

- `FKGROUPTOURNAMENTID`
- `SECTIONNO`
- `STARTTIME`
- `ENDTIME`
- `STARTROUNDNO`
- `ENDROUNDNO`
- `LASTSCORETRANSFERRED`
- `SECTIONSTARTNOTES`
- `SECTIONRESULTNOTES`
- `WAVESTARTINDEX`
- `WAVELENGTH`
- `WAVEEQWAVEINDEX`
- `PAYMENTSTATUS`
- `SECTIONSCOREVALID`
- `TOTALSCOREVALID`
- `BRIDGEMATEFILE`

### Relation

- `SECTION.FKGROUPTOURNAMENTID -> GROUPTOURNAMENT.ID`

---

## SECTIONTEAM

`SECTIONTEAM` repræsenterer et hold/par i en konkret sektion.

### Primærnøgle

- `ID`

### Vigtige felter

- `FKMAINTOURNAMENTTEAMID`
- `FKSECTIONID`
- `TEAMNO`
- `STARTSCORE`
- `ADJUSTMENTMISSING`
- `ADJUSTMENTOTHER`
- `REGULATIONSCORE`
- `TIEBREAKSECTION`
- `TIEBREAKTOURNAMENT`
- `TRANSFERSCORE`
- `TRANSFERBOARDS`
- `TRANSFERPERCENT`
- `TIEBREAKADD`

### Relationer

- `SECTIONTEAM.FKSECTIONID -> SECTION.ID`
- `SECTIONTEAM.FKMAINTOURNAMENTTEAMID -> MAINTOURNAMENTTEAM.ID`

---

## SECTIONPLAYER

`SECTIONPLAYER` repræsenterer en spiller i en konkret sektion.

### Primærnøgle

- `ID`

### Vigtige felter

- `FKSECTIONID`
- `FKSECTIONTEAMID`
- `FKPLAYERID`
- `PLAYERNAME`
- `STARTHAC`
- `PLAYERNO`
- `PAIRNO`
- `SUBSTITUTE`
- `ISCAPTAIN`
- `FKMAINTOURNAMENTPLAYERID`
- `ROUNDIDSTRING`

### Relationer

- `SECTIONPLAYER.FKSECTIONID -> SECTION.ID`
- `SECTIONPLAYER.FKSECTIONTEAMID -> SECTIONTEAM.ID`
- `SECTIONPLAYER.FKMAINTOURNAMENTPLAYERID -> MAINTOURNAMENTPLAYER.ID`

---

## ROUND

`ROUND` repræsenterer en runde i en sektion.

### Primærnøgle

- `ID`

### Vigtige felter

- `FKSECTIONID`
- `ROUNDNO`
- `HALFNO`
- `STARTTIME`
- `ENDTIME`
- `STARTBOARD`

### Relation

- `ROUND.FKSECTIONID -> SECTION.ID`

---

## ROUNDMATCH

`ROUNDMATCH` repræsenterer bordopstillingen og matchdata for et bestemt bord i en bestemt runde.

### Primærnøgle

- `ID`

### Vigtige felter

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
- `CALCULATEDSCORENS`
- `CALCULATEDSCOREEW`
- `CALCULATEDTEAMSCORENS`
- `CALCULATEDTEAMSCOREEW`
- `CALCULATEDTEAMVPNS`
- `CALCULATEDTEAMVPEW`
- `CALCULATEDBOARDS`
- `OVERRULEDIMPSNS`
- `OVERRULEDIMPSEW`
- `OVERRULEDVPNS`
- `OVERRULEDVPEW`
- `ADJUSTEDIMPSNS`
- `ADJUSTEDIMPSEW`
- `ABSENT`

### Relationer

- `ROUNDMATCH.FKROUNDID -> ROUND.ID`
- `ROUNDMATCH.FKNORTHSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKSOUTHSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKEASTSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKWESTSECTIONTEAMID -> SECTIONTEAM.ID`

---

# Live-afviklingsflow

`MAINTOURNAMENT`
→ `GROUPTOURNAMENT`
→ `SECTION`
→ `ROUND`
→ `ROUNDMATCH`
→ `RESULT`

Samtidig findes deltagerflowet:

`MAINTOURNAMENTTEAM`
→ `SECTIONTEAM`

`MAINTOURNAMENTPLAYER`
→ `SECTIONPLAYER`

`ROUNDMATCH` beskriver bordopstillingen.  
De konkrete boardresultater gemmes i `RESULT`.

---

# Boards og resultater

Dette domæne dækker den konkrete bridgeafvikling på bord- og boardniveau.

Det beskriver:
- hvilke boards der bruges i en sektion
- hvordan boards er grupperet i board-sæt
- hvilke bordopstillinger der spilles i en runde
- hvilke resultater der registreres pr. board
- hvilke kontraktoplysninger der knytter sig til det enkelte resultat

Domænet hænger sammen med live-afvikling via:

`SECTION`
→ `ROUND`
→ `ROUNDMATCH`
→ `RESULT`

og:

`SECTION`
→ `BOARDSPEC`
→ `BOARD`
→ `RESULT`

---

## BOARDSPEC

`BOARDSPEC` beskriver et board-sæt for en sektion.

Tabellen fungerer som mellemled mellem en sektion og de konkrete boards.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `FKSECTIONID`
- `FILENAME`
- `BOARDSPECNO`
- `NAME`

### Relationer

- `BOARDSPEC.FKSECTIONID -> SECTION.ID`
- `BOARD.FKBOARDSPECID -> BOARDSPEC.ID`

En sektion kan have et eller flere board-sæt.  
Board-sæt bruges til at gruppere de konkrete boards, som resultater registreres imod.

---

## BOARD

`BOARD` indeholder de konkrete boards i et board-sæt.

Tabellen bruges som reference ved registrering af resultater.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `FKBOARDSPECID`
- `BOARDNO`
- `BOARDGROUP`
- `DISTRIBUTION`

### Relation

- `BOARD.FKBOARDSPECID -> BOARDSPEC.ID`

`BOARDNO` er det menneskeligt læsbare boardnummer.  
`DISTRIBUTION` indeholder kortfordelingen som binære data.

---

## ROUND

`ROUND` beskriver en konkret runde i en sektion.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `FKSECTIONID`
- `ROUNDNO`
- `HALFNO`
- `STARTTIME`
- `ENDTIME`
- `STARTBOARD`

### Relation

- `ROUND.FKSECTIONID -> SECTION.ID`

En sektion kan have flere runder.  
Runder bruges som overordnet struktur for bordkampe i `ROUNDMATCH`.

---

## ROUNDMATCH

`ROUNDMATCH` beskriver en konkret kamp eller bordopstilling i en bestemt runde.

Tabellen indeholder bordnummer, board-sæt og placering af par/hold ved bordet.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `FKROUNDID`
- `TABLENO`
- `BOARDSET`
- `BOARDSPEC`
- `NORTHTEAMNO`
- `SOUTHTEAMNO`
- `EASTTEAMNO`
- `WESTTEAMNO`
- `NORTHPAIRNO`
- `SOUTHPAIRNO`
- `EASTPAIRNO`
- `WESTPAIRNO`
- `FKNORTHSECTIONTEAMID`
- `FKSOUTHSECTIONTEAMID`
- `FKEASTSECTIONTEAMID`
- `FKWESTSECTIONTEAMID`
- `FKNORTHSECTIONPLAYERID`
- `FKSOUTHSECTIONPLAYERID`
- `FKEASTSECTIONPLAYERID`
- `FKWESTSECTIONPLAYERID`
- `CALCULATEDSCORENS`
- `CALCULATEDSCOREEW`
- `CALCULATEDTEAMSCORENS`
- `CALCULATEDTEAMSCOREEW`
- `CALCULATEDTEAMVPNS`
- `CALCULATEDTEAMVPEW`

### Relationer

- `ROUNDMATCH.FKROUNDID -> ROUND.ID`
- `ROUNDMATCH.FKNORTHSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKSOUTHSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKEASTSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKWESTSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKNORTHSECTIONPLAYERID -> SECTIONPLAYER.ID`
- `ROUNDMATCH.FKSOUTHSECTIONPLAYERID -> SECTIONPLAYER.ID`
- `ROUNDMATCH.FKEASTSECTIONPLAYERID -> SECTIONPLAYER.ID`
- `ROUNDMATCH.FKWESTSECTIONPLAYERID -> SECTIONPLAYER.ID`

`ROUNDMATCH` er bindeleddet mellem runde, bordopstilling og resultat.  
Et `ROUNDMATCH` svarer typisk til ét bord i én bestemt runde.  
Resultater i `RESULT` peger på matchen via `FKMATCHID`.

---

## RESULT

`RESULT` registrerer konkrete resultater på boardniveau.

Tabellen er den centrale tabel for boardresultater.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
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

### Relationer

- `RESULT.FKMATCHID -> ROUNDMATCH.ID`
- `RESULT.FKBOARDID -> BOARD.ID`

Samme match kan have flere resultatrækker, typisk én pr. board.

Bridgeoplysninger som kontrakt, udspil, spilfører, dobling og antal stik findes i `RESULT`.

De beregnede scorefelter bruges til resultatvisning, procenter og matchpoint.

---

## CONTRACT

`CONTRACT` er en hjælpe- eller opslagstabel for kontraktrelaterede kombinationer.

### Vigtige felter

- `ID`
- `DECLARER`
- `DOUBLING`
- `TRICKS`
- `RESULT`
- `CONTRACT`

De faktiske registrerede resultater findes i `RESULT`.  
`CONTRACT` fungerer som støtte til kontraktfortolkning eller validering.

---

## Relationer i boards/resultater

### Tekniske relationer

- `ROUND.FKSECTIONID -> SECTION.ID`
- `ROUNDMATCH.FKROUNDID -> ROUND.ID`
- `RESULT.FKMATCHID -> ROUNDMATCH.ID`
- `RESULT.FKBOARDID -> BOARD.ID`
- `BOARD.FKBOARDSPECID -> BOARDSPEC.ID`

### Logiske relationer

- `BOARDSPEC.FKSECTIONID -> SECTION.ID`
- `ROUNDMATCH.FKNORTHSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKSOUTHSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKEASTSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKWESTSECTIONTEAMID -> SECTIONTEAM.ID`
- `ROUNDMATCH.FKNORTHSECTIONPLAYERID -> SECTIONPLAYER.ID`
- `ROUNDMATCH.FKSOUTHSECTIONPLAYERID -> SECTIONPLAYER.ID`
- `ROUNDMATCH.FKEASTSECTIONPLAYERID -> SECTIONPLAYER.ID`
- `ROUNDMATCH.FKWESTSECTIONPLAYERID -> SECTIONPLAYER.ID`

---

## Domæneflow

1. En `SECTION` repræsenterer en konkret sektion i en gruppeturnering.
2. Sektionen har runder i `ROUND`.
3. Sektionen har et eller flere board-sæt i `BOARDSPEC`.
4. Board-sæt indeholder konkrete boards i `BOARD`.
5. Runder har bordopstillinger i `ROUNDMATCH`.
6. Resultater registreres i `RESULT`, koblet både til match og board.

Forenklet:

`SECTION`
→ `ROUND`
→ `ROUNDMATCH`
→ `RESULT`

og:

`SECTION`
→ `BOARDSPEC`
→ `BOARD`
→ `RESULT`

---

# Hjemmeside / CMS

Hjemmeside- og CMS-delen dækker:
- klubbens web- og kontaktoplysninger
- sider på klubbens hjemmeside
- afsnit og indhold på siderne
- links og kontaktpunkter
- standard/default HTML-indhold

Denne del er relevant for webindhold, kontaktoplysninger og sideopbygning.

---

## SYS_MAINCLUB

`SYS_MAINCLUB` indeholder hovedklubbens overordnede opsætning og basisoplysninger.

### Primærnøgle

- `MAINCLUB_ID`

### Vigtige felter til visning

- `MAINCLUB_ID`
- `ORG_CLUB_NO`
- `NAME`
- `LOCATION`
- `ADDRESS_1`
- `ADDRESS_2`
- `ZIP_CODE`
- `CITY`
- `PHONE_1`
- `PHONE_2`
- `EMAIL`
- `HOMEPAGE`
- `DISTRICT_NO`
- `ORG_NAME`
- `ORG_ADDRESS_1`
- `ORG_ADDRESS_2`
- `ORG_PHONE`
- `ORG_EMAIL`

### Webrelaterede felter

- `USE_HOMEPAGE`
- `HTML_INDEX_FILE`
- `HTML_MODIFIED`
- `HTML_GENERATED`
- `HTML_UPLOADED`
- `USE_REMOVE_OLD_WEBPAGE`
- `REMOVE_WEBPAGES_MONTHS`

### Interne systemfelter

- `CLUB_HOST_LOGIN`
- `CLUB_HOST_PASSWORD`
- `CLUB_WEB_FTP_LOGIN_ORG`
- `CLUB_WEB_FTP_PASSWORD_ORG`
- `SENDER_SMTP_USER_ID`
- `SENDER_PASSWORD`
- `CURRENTUSER`

---

## WEB_HTML_PAGE

`WEB_HTML_PAGE` definerer de sider, der findes på klubbens hjemmeside.

Eksempler på sider:
- Forside
- Turneringer
- Bronzestilling
- Mesterpoint
- Klubben
- Kontakt os
- Links
- Rangliste

### Primærnøgle

- `HTML_PAGE_ID`

### Vigtige felter

- `HTML_PAGE_ID`
- `PAGE_NO`
- `PAGE_TYPE`
- `PAGE_HEADER`
- `IS_VISIBLE`
- `ALLOW_EDIT`

`PAGE_NO` bruges til sortering eller menu-rækkefølge.  
`PAGE_HEADER` er sidens titel.  
`IS_VISIBLE` angiver, om siden vises.  
`ALLOW_EDIT` angiver, om siden kan redigeres.  
`PAGE_TYPE` angiver sidetype.

---

## WEB_HTML_PARAGRAPH

`WEB_HTML_PARAGRAPH` indeholder afsnit eller indhold på hjemmesidesider.

### Primærnøgle

- `HTML_PAGE_ID`
- `PARAGRAPH_NO`

### Relation

- `WEB_HTML_PARAGRAPH.HTML_PAGE_ID -> WEB_HTML_PAGE.HTML_PAGE_ID`

En side kan bestå af flere nummererede afsnit.  
Afsnit vises typisk i rækkefølge efter `PARAGRAPH_NO`.

---

## WEB_HTML_LINK

`WEB_HTML_LINK` indeholder links, kontaktpunkter eller linkblokke til hjemmesiden.

### Vigtige felter

- `LINK_TYPE`
- `DESCRIPTION`
- `ADDRESS`
- `COMMENT`

`LINK_TYPE` angiver typen af link, fx email, telefon eller URL.  
`DESCRIPTION` er visningsteksten.  
`ADDRESS` er selve adressen, mailen, telefonnummeret eller URL’en.  
`COMMENT` er en note eller uddybning.

---

## LIST_HTML_PARAGRAPH_DEFAULT

`LIST_HTML_PARAGRAPH_DEFAULT` indeholder standard/default HTML-afsnit pr. sprog.

### Vigtige felter

- `LANGUAGE_CODE`
- `HTML_DATA`
- `RVF_DATA`

`HTML_DATA` indeholder standard HTML-indhold.  
`RVF_DATA` er et ældre eller internt rich text-format.  
`LANGUAGE_CODE` angiver sprogvariant.

---

## Webmodel

`SYS_MAINCLUB`
→ klub- og kontaktinfo

`WEB_HTML_PAGE`
→ hjemmesidens sider

`WEB_HTML_PARAGRAPH`
→ indhold eller afsnit på sider

`WEB_HTML_LINK`
→ links og kontaktoplysninger

`LIST_HTML_PARAGRAPH_DEFAULT`
→ standardindhold pr. sprog

---

# Handicap / HAC

HAC-delen understøtter handicapberegning på runde- og spillerniveau.

HAC-tabellerne fungerer som et beregnings- og analyselag oven på turneringsafviklingen.

Overordnet model:

`GROUPTOURNAMENT / SECTION / ROUND`
→ `HACROUND`
→ `HACROUNDPLAYER`

---

## HACROUND

`HACROUND` beskriver en handicaprunde eller handicapberegningskontekst for en bestemt turneringskontekst.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `BRIDGEORGID`
- `FKGROUPTOURNAMENTID`
- `FKSECTIONID`
- `FKROUNDID`
- `TEAMNOOFHALVES`
- `TEAMHALF`
- `TOURNAMENTTEAMTYPE`
- `PAIRCALCULATIONTYPE`
- `PAIRMATCHPOINTTYPE`
- `NOOFBOARDS`
- `NOOFTEAMS`
- `NOOFTABLES`
- `HACAVERAGE`
- `HACSTATUS`
- `SPECIFICOPPONENTS`

### Relationer

- `FKGROUPTOURNAMENTID -> GROUPTOURNAMENT.ID`
- `FKSECTIONID -> SECTION.ID`
- `FKROUNDID -> ROUND.ID`

`HACROUND` gemmer den turneringskontekst og de parametre, som handicapberegningen er udført med.

---

## HACROUNDPLAYER

`HACROUNDPLAYER` beskriver handicapdata for en bestemt spiller i en handicaprunde.

### Primærnøgle

- `ID`

### Vigtige felter

- `ID`
- `BRIDGEORGID`
- `FKHACROUNDID`
- `FKPLAYERID`
- `PAIRNO`
- `SEQUENCENO`
- `TEAMNO`
- `TEAMHALFNO`
- `OPPOSINGTEAMNO`
- `OPPOSINGPAIRNO`
- `PAIRDIRECTION`
- `HACDELTA`
- `HACSTART`
- `RESULT`
- `ROUNDRANK`
- `TOURNAMENTRANK`
- `HACDELTASUM`
- `EXPECTEDSCOREPCT`
- `ACHIEVEDSCOREPCT`
- `SPECIFICOPPONENTS`

### Relationer

- `FKHACROUNDID -> HACROUND.ID`
- `FKPLAYERID -> spiller-id`

`HACROUNDPLAYER` indeholder input og output for handicapberegning pr. spiller.

Centrale felter:
- `HACSTART`
- `HACDELTA`
- `HACDELTASUM`
- `EXPECTEDSCOREPCT`
- `ACHIEVEDSCOREPCT`

---

# Medlem / Ret medlem

Denne del beskriver medlemsdata, som de fremgår af BridgeCentral-skærmen **Spillere** og dialogen **Ret medlem**.

---

## Spillere

Skærmen viser en liste over spillere/medlemmer og detaljer for valgt medlem.

### Centrale UI-elementer

- søgning i spillerliste
- visning som “Fornavn efternavn”
- visning som “Efternavn, fornavn”
- medlemsliste
- Ny spiller
- Ret spiller
- Slet spiller

### Detaljer for valgt medlem

- navn
- adresse
- medlemsnr.
- telefon privat
- telefon arbejde
- telefon mobil
- email
- handicap
- kontingent

### Primær tabel

- `MEM_MEMBER`

### Relevante felter

- `NAME`
- `ADDRESS_1`
- `ZIP_CODE`
- `CITY`
- `MEMBER_NO`
- `PHONE_1`
- `PHONE_2`
- `PHONE_3`
- `EMAIL`
- `HAC`

---

## Medlemsfaner

Medlemsskærmen indeholder flere underområder:

- MP-oversigt
- MP-titel
- DBF-kontingent
- Klubforhold
- Klubturneringer
- Notater
- Historik

Medlemsdata består derfor både af stamdata og tilknyttede underdomæner.

---

## MP-oversigt

MP-oversigt viser:
- bronze
- sølv
- guld
- MP

Der findes valg for:
- alle MP-posteringer
- kun MP optjent i klubben

Gridet viser typisk:
- dato
- hovedklub
- klub
- klubturnering
- mesterpoint

### Relevante datakilder

- `MEM_MEMBER`
- `MASTERPOINTS`

### Relevante felter i `MEM_MEMBER`

- `TOTAL_BRONZE`
- `TOTAL_SILVER`
- `TOTAL_GOLD`
- `TOTAL_MASTER`

---

## Ret medlem – personlige oplysninger

Dialogen **Ret medlem** viser stamdata for medlemmet.

### Synlige felter

- navn
- gade/vej
- postnr./by
- landekode
- email
- handicap
- fødselsdato
- telefon privat
- telefon arbejde
- telefon mobil
- medlem 1. gang fra
- vedligeholder egne stamdata
- notater

### Mapping til `MEM_MEMBER`

- Navn -> `NAME`
- Gade/vej -> `ADDRESS_1`
- Postnr. -> `ZIP_CODE`
- By -> `CITY`
- Landekode -> `COUNTRY_CODE`
- Email -> `EMAIL`
- Handicap -> `HAC`
- Fødselsdato -> `BIRTHDAY`
- Privat -> `PHONE_1`
- Arbejde -> `PHONE_2`
- Mobil -> `PHONE_3`
- Notater -> `NOTES`

### Øvrige relevante felter

- `DBF_OPRETTET_DATO`
- `CLUB_START`
- `VEDLIGEHOLDER_STAMDATA`

---

## Ret medlem – DBF-kontingent

DBF-kontingentdelen viser:
- aktuel DBF-kontingentstatus
- fremtidig DBF-kontingentstatus
- mulighed for automatisk ændring på en fremtidig dato

### Relevante felter

- `STATUS`
- `CLUB_STATUS_ID`
- `FUTURE_STATUS`
- `FUTURE_STATUS_DATE`

---

# Underklubber og hovedklub

BridgeCentral arbejder med mindst to klubniveauer:
- hovedklub
- underklub/spillegruppe

Derudover findes en relation mellem spiller og underklub.

---

## Hovedklub

Hovedklubben er den samlede klubinstallation eller DBF-klub.

### Primær tabel

- `SYS_MAINCLUB`

### UI-felter

- navn
- spillested
- DBF nr
- sæsonstart
- telefon 1
- telefon 2
- bankkonto
- email
- hjemmeside
- rygebegrænsning
- undervisning
- MP-ordning

### Mapping til `SYS_MAINCLUB`

- Navn -> `NAME`
- DBF nr -> `ORG_CLUB_NO`
- Sæsonstart -> `SEASON_START`
- Telefon 1 -> `PHONE_1`
- Telefon 2 -> `PHONE_2`
- Bankkonto -> `BANK_ACCOUNT`
- Email -> `EMAIL`
- Hjemmeside -> `HOMEPAGE`
- Rygebegrænsning -> `NO_SMOKING`
- MP-ordning -> `USES_MP`
- Undervisning -> `USE_EDUCATION`

### Spillested

Spillested består af flere felter:
- `LOCATION`
- `ADDRESS_1`
- `ZIP_CODE`
- `CITY`

---

## Hovedklub – kontaktpersoner

Kontaktpersoner kan være roller som:
- varemodtager
- BridgeCentral-kontakt
- fakturamodtager
- underviser

### Relevante datakilder

- `MEM_MEMBER`
- `MEM_CONTACT`

Kontaktpersoner er et særligt rolletilknyttet lag, ikke bare en almindelig medlemsliste.

---

## Underklub

Underklubber er interne klubber, spillegrupper eller spilledage under hovedklubben.

### Primær tabel

- `MEM_CLUB`

### UI-felter

- klubnavn
- spilledag
- klubleder

### Mapping til `MEM_CLUB`

- Klubnavn -> `CLUB_NAME`
- Spilledag -> `GAME_DAY`
- Spilletid -> `GAME_TIME`
- Klubleder -> `LEADER_ID`
- Synlighed -> `IS_VISIBLE`

---

## Underklub – spillerliste

Spillerlisten under en underklub viser:
- navn
- kontingent
- makker

### Relevante tabeller

- `MEM_MEMBER`
- `MEM_MEMBER_CLUB`

### Mapping

- Navn -> `MEM_MEMBER.NAME`
- Kontingent -> `MEM_MEMBER.STATUS` eller `MEM_MEMBER.CLUB_STATUS_ID`
- Makker -> `MEM_MEMBER_CLUB.PARTNER_ID`
- Tilknytning mellem spiller og underklub -> `MEM_MEMBER_CLUB`

---

## Klubstruktur

`SYS_MAINCLUB`
→ hovedklub / installation

`MEM_CLUB`
→ underklub / spillegruppe

`MEM_MEMBER`
→ medlem/person

`MEM_MEMBER_CLUB`
→ relation mellem medlem og underklub

---

# Ret klub og tilknyt medlem

Denne del beskriver redigering af underklub og tilknytning af medlem til underklub.

---

## Ret klub

Dialogen **Ret klub** redigerer stamdata for en underklub.

### Synlige felter

- klubnavn
- spilleperiode
- ugedag
- klokkeslæt
- klubleder

### Primær tabel

- `MEM_CLUB`

### Mapping

- Klubnavn -> `MEM_CLUB.CLUB_NAME`
- Spilledag -> `MEM_CLUB.GAME_DAY`
- Spilletid -> `MEM_CLUB.GAME_TIME`
- Klubleder -> `MEM_CLUB.LEADER_ID`

---

## Tilknyt medlem

Dialogen **Tilknyt medlem** opretter en relation mellem medlem og underklub.

### Synlige felter

- spiller
- klub
- spilleren er substitut i klubben
- makker
- makker skal stå først på lister

### Primær tabel

- `MEM_MEMBER_CLUB`

### Mapping

- Spiller -> `MEM_MEMBER_CLUB.MEMBER_ID`
- Klub -> `MEM_MEMBER_CLUB.CLUB_ID`
- Substitut -> `MEM_MEMBER_CLUB.IS_SUBSTITUTE`
- Makker -> `MEM_MEMBER_CLUB.PARTNER_ID`
- Makker først -> `MEM_MEMBER_CLUB.PARTNER_FIRST`

---

## Find spiller

Dialogen **Find spiller** bruges til at søge efter medlemmer.

### Søgefelter

- fornavn
- efternavn
- adresse
- postnr
- telefon

### Primær tabel

- `MEM_MEMBER`

### Mapping

- Fornavn -> `FIRST_NAME`
- Efternavn -> `LAST_NAME`
- Adresse -> `ADDRESS_1`
- Postnr -> `ZIP_CODE`
- Telefon -> `PHONE_1`, `PHONE_2` eller `PHONE_3`
- Visningsnavn -> `NAME`
- Medlemsnr -> `MEMBER_NO`
- By -> `CITY`

---

## Makkerrelation

Makkerforholdet er en del af klubtilknytningen.

### Relevante felter

- `MEM_MEMBER_CLUB.PARTNER_ID`
- `MEM_MEMBER_CLUB.PARTNER_FIRST`

Makkerrelationen er knyttet til medlemskab i en underklub og kan derfor være klubspecifik.

---

## Substitut i klubben

Substitutstatus er knyttet til medlemskab i underklubben.

### Relevant felt

- `MEM_MEMBER_CLUB.IS_SUBSTITUTE`

---

# Ret / fjern tilknytning

Denne del beskriver redigering og fjernelse af eksisterende relationer mellem medlem og underklub.

---

## Ret tilknytning

Dialogen **Ret tilknytning** redigerer en eksisterende relation mellem medlem og underklub.

### Synlige felter

- spiller
- klub
- spilleren er substitut i klubben
- makker
- makker skal stå først på lister

### Primær tabel

- `MEM_MEMBER_CLUB`

### Mapping

- Spiller -> `MEM_MEMBER_CLUB.MEMBER_ID`
- Klub -> `MEM_MEMBER_CLUB.CLUB_ID`
- Substitut -> `MEM_MEMBER_CLUB.IS_SUBSTITUTE`
- Makker -> `MEM_MEMBER_CLUB.PARTNER_ID`
- Makker først -> `MEM_MEMBER_CLUB.PARTNER_FIRST`

---

## Fjern tilknytning

**Fjern tilknytning** fjerner relationen mellem et medlem og en underklub.

### Primær tabel

- `MEM_MEMBER_CLUB`

Fjernelse af tilknytning betyder, at medlemskabet i underklubben fjernes.  
Det betyder ikke, at selve medlemmet slettes fra `MEM_MEMBER`.

---

## Makkerrelation ved fjernelse

Makkerrelationen ligger på tilknytningsniveau via:
- `PARTNER_ID`
- `PARTNER_FIRST`

Hvis en klubtilknytning fjernes, kan relaterede makkerfelter skulle håndteres særskilt.

---

# Klubturnering og tilmeldinger

Denne del beskriver oprettelse af klubturneringer, rækker, spilletidspunkter og tilmeldinger.

---

## Overordnet klubturneringsflow

En klubturnering kan oprettes med:
- navn
- beskrivelse
- turneringsform
- antal rækker
- styrkeopdeling
- styrkeniveau pr. række
- spilletidspunkter
- tilmeldinger

Overordnet flow:

1. Turnering oprettes.
2. Rækker defineres.
3. Spilletidspunkter/sektioner oprettes.
4. Tilmeldinger registreres.
5. Konkrete deltagere vælges til den enkelte sektion/aften.

---

## Oprettelse af klubturnering

### UI-felter

- navn
- beskrivelse
- turneringsform
- anvend udspil fra Bridgemate

### Primær tabel

- `MAINTOURNAMENT`

### Mapping

- Navn -> `MAINTOURNAMENT.NAME`
- Beskrivelse -> `MAINTOURNAMENT.DESCRIPTION`
- Turneringsform -> `MAINTOURNAMENT.TOURNAMENTFORM`

---

## Rækker og styrkeopdeling

En klubturnering kan have:
- én eller flere rækker
- lige stærke rækker
- styrkeopdelte rækker
- fælles top på tværs af rækker

### Relevante tabeller

- `MAINTOURNAMENT`
- `GROUPTOURNAMENT`

### Relevante felter

- `MAINTOURNAMENT.STRENGTHGROUPCOUNT`
- `MAINTOURNAMENT.COMMONTOP`

---

## Styrkeniveau pr. række

Rækker kan have styrkeniveauer, fx:
- A-rækken -> styrkeniveau 1
- B-rækken -> styrkeniveau 2

Rækker og styrkeniveauer bruges til at opdele turneringen i passende grupper.

### Relevante tabeller

- `GROUPTOURNAMENT`
- `MAINTOURNAMENT`

---

## Spilletidspunkter / sektioner

Spilletidspunkter kaldes sektioner i BridgeCentral.

Et spilletidspunkt indeholder typisk:
- dato
- klokkeslæt
- række/gruppe
- konkret afvikling

### Primær tabel

- `SECTION`

### Mapping

- Dato/tid -> `SECTION.STARTTIME`
- Sluttid -> `SECTION.ENDTIME`
- Gruppe/række -> `SECTION.FKGROUPTOURNAMENTID`

Flere spilleaftener kan repræsenteres som flere sektioner eller afviklingsenheder under samme klubturnering.

---

## Tilmeldinger

Tilmeldinger ligger før den konkrete afvikling.

Der er forskel på:
- tilmeldte deltagere
- faktisk deltagende spillere i en bestemt sektion/aften

### Relevante tabeller

- `MAINTOURNAMENTTEAM`
- `MAINTOURNAMENTPLAYER`

I parturnering kan et par repræsenteres som et team i `MAINTOURNAMENTTEAM`, med to spillere i `MAINTOURNAMENTPLAYER`.

---

## Vedligehold tilmeldinger

Tilmeldinger kan oprettes fra:
- ledige makkerpar
- ledige klubmedlemmer
- søgning i hovedklubben
- manuel indtastning
- nyt medlem
- csv- eller txt-fil
- kopi fra anden klubturnering

Tilmeldingstyper kan være:
- par
- hold
- enkeltmands

---

## Valg af deltagere pr. aften

Ved den konkrete spilleaften/sektion vælges de faktisk deltagende spillere fra turneringens tilmeldinger.

### Tilmeldingsniveau

- `MAINTOURNAMENTTEAM`
- `MAINTOURNAMENTPLAYER`

### Afviklingsniveau

- `SECTIONTEAM`
- `SECTIONPLAYER`

---

## Flere aftener

En turnering over flere aftener kan modelleres som:

`MAINTOURNAMENT`
→ `GROUPTOURNAMENT`
→ `SECTION`
→ `SECTIONTEAM`
→ `SECTIONPLAYER`
→ `ROUND`
→ `ROUNDMATCH`
→ `RESULT`

---

# Turneringsdata og afvikling

Denne del beskriver opsætning af turneringsdata før konkret afvikling.

Den omfatter:
- valg af rækker pr. turneringsdag
- mesterpointopsætning
- turneringsform
- skifteplan
- antal spil pr. runde
- regnskabsform
- indtastningstype
- udregningsmetode
- handicapopsætning
- kortfordelinger
- overgang til turneringsafvikling

---

# Turneringsdata og afvikling

Denne del beskriver opsætning af turneringsdata og overgangen fra planlagt klubturnering til konkret turneringsafvikling.

Turneringsafviklingen omfatter:
- valg af rækker pr. turneringsdag
- mesterpointopsætning
- turneringsform
- skifteplan
- antal spil pr. runde
- regnskabsform
- resultatindtastning
- udregningsmetode
- handicapopsætning
- kortfordelinger
- konkret afviklingsskærm

---

## Rækker for turneringsdagen

En turneringsdag kan indeholde flere rækker, fx:
- A-rækken
- B-rækken

Hver række får sin egen konkrete afvikling under samme turneringsdag.

### Relevante tabeller

- `MAINTOURNAMENT`
- `GROUPTOURNAMENT`
- `SECTION`

### Model

`MAINTOURNAMENT`
→ overordnet klubturnering

`GROUPTOURNAMENT`
→ række / gruppe

`SECTION`
→ konkret afvikling / spilleaften pr. gruppe

---

## Mesterpointopsætning

Mesterpoint kan sættes pr. række.

Eksempel:
- A-rækken
- Række 1
- Bronzepoint

### Relevant felt

- `GROUPTOURNAMENT.MASTERPOINTSTYPE`

---

## Turneringsform

Turneringsformen kan angives på afviklings- eller rækkeniveau.

Mulige turneringsformer:
- parturnering
- holdturnering
- enkeltmandsturnering
- monrad

### Relevante felter

- `GROUPTOURNAMENT.TOURNAMENTTYPE`
- `GROUPTOURNAMENT.ISMONRAD`
- `GROUPTOURNAMENT.ISMITCHELL`
- `GROUPTOURNAMENT.TOURNAMENTTEAMTYPE`
- `GROUPTOURNAMENT.TOURNAMENTPAIRCALCTYPE`
- `GROUPTOURNAMENT.TOURNAMENTMATCHPOINTTYPE`

---

## Skifteplan

Skifteplanen vælges for den konkrete turnering eller række.

Eksempler på skifteplaner:
- Howell, 10 par, model B
- Howell, 10 par, model A
- Uendelig Howell, 10 par
- Howell afkortet, 10 par, 7 runder
- Howell afkortet, 10 par, 8 runder

### Relevante tabeller

- `GROUPTOURNAMENT`
- `MOVEMENTPLAN`
- `MOVEMENTPLANREFERENCE`
- `MOVEMENTPLANDETAIL`

### Mapping

- valgt skifteplan -> `GROUPTOURNAMENT.FKMOVEMENTPLANID`
- selve planen -> `MOVEMENTPLAN`
- reference mellem turneringsform, bordantal og antal spil -> `MOVEMENTPLANREFERENCE`
- detaljeret bord- og rundeplan -> `MOVEMENTPLANDETAIL`

---

## Antal spil pr. runde

Antal spil pr. runde fastlægger rundeopbygningen for rækken.

Eksempel:
- antal par: 10
- antal runder: 9
- antal spil pr. runde: 3

### Relevante felter

- `GROUPTOURNAMENT.BOARDSPERROUND`
- `GROUPTOURNAMENT.NUMBEROFROUNDS`
- `GROUPTOURNAMENT.NUMBEROFTABLES`
- `GROUPTOURNAMENT.NUMBEROFTEAMS`

---

## Regnskabsform og resultatindtastning

Regnskabsform og resultatindtastning er en del af turneringsopsætningen.

### Regnskabsform

Mulige former:
- vandreregnskaber
- spillerrækkefølge
- N/S-rækkefølge
- bordregnskaber
- barometer
- slipper

### Resultatindtastning

Mulige indstillinger:
- indtast score
- indtast kontrakt og antal stik
- husk indtastet score
- skift automatisk til næste bord/spil efter sidste indtastning

### Relevante felter

- `GROUPTOURNAMENT.TRAVELER`
- `GROUPTOURNAMENT.TRAVELERBYGAMENO`
- `GROUPTOURNAMENT.INPUTSCORE`
- `GROUPTOURNAMENT.INPUTSCOREBYTEN`
- `GROUPTOURNAMENT.SAVESCORE`
- `GROUPTOURNAMENT.AUTOMATICNEXTSCORE`

---

## Udregningsmetode

Udregningsmetoden fastlægger scoringslogikken for rækken eller turneringen.

Mulige udregningstyper:
- standardudregning
- 0 som bundscore
- 0 som middelscore
- IMP-par udregning

### Relevante felter

- `GROUPTOURNAMENT.CALCULATIONMETHOD`
- `GROUPTOURNAMENT.TOURNAMENTPAIRCALCTYPE`
- `GROUPTOURNAMENT.TOURNAMENTMATCHPOINTTYPE`

---

## Handicapopsætning

Handicapopsætning styrer, om og hvordan handicapdata bruges i turneringen.

Mulige valg:
- resultat indberettes til handicapsystemet
- handicapinfo gemmes ikke lokalt
- HAC-info gemmes lokalt
- handicapinformation vises på hjemmesider
- handicapinformation vises på papirudskrifter
- turneringen er en handicapturnering
- præmier uddeles baseret på handicapscore

### Relevante felter

- `GROUPTOURNAMENT.CALCULATEHAC`
- `GROUPTOURNAMENT.SHOWHACONHTML`
- `GROUPTOURNAMENT.SHOWHACONPAPER`
- `GROUPTOURNAMENT.GIVEHACPRIZES`
- `GROUPTOURNAMENT.SIMPLIFIEDHAC`

Der er forskel på almindelig handicapregistrering og en egentlig handicapturnering med handicapbaserede præmier.

---

## Kortfordelinger i turneringsopsætning

Kortfordelinger kan indlæses for hver sektion.

### Relevante tabeller

- `SECTION`
- `BOARDSPEC`
- `BOARD`

### Relevante felter

- `GROUPTOURNAMENT.USEBOARDSPEC`
- `BOARDSPEC.FKSECTIONID`
- `BOARDSPEC.FILENAME`
- `BOARD.FKBOARDSPECID`
- `BOARD.DISTRIBUTION`

Kortfordelinger er knyttet til den konkrete afvikling og bruges før turneringen starter.

---

## Bekræft turneringsdata

Bekræftelse af turneringsdata samler opsætningen for den konkrete række.

Typiske oplysninger:
- række
- turneringsform
- handicapindberetning
- handicapvisning på hjemmeside
- handicapvisning på papirudskrifter
- skifteplan
- antal par
- antal runder
- antal spil pr. runde
- regnskabsform
- indtastningstype
- udregningsmetode
- kortfordelinger pr. sektion
- Bridgemate-fil

### Relevante tabeller

- `GROUPTOURNAMENT`
- `MOVEMENTPLAN`
- `SECTION`
- `BOARDSPEC`
- `BOARD`
- `ROUND`
- `ROUNDMATCH`
- `RESULT`

---

## Turneringsafvikling

Efter opsætning vises den konkrete rækkeafvikling.

Eksempel på oplysninger i afviklingen:
- række
- dato
- sektion
- skifteplan
- turneringsform
- MP-tildeling
- styrkeniveau
- handicapudregning
- handicappræmier

### Model

`MAINTOURNAMENT`
→ overordnet klubturnering

`GROUPTOURNAMENT`
→ række / afviklingsopsætning

`SECTION`
→ konkret sektion / spilleaften

`SECTIONTEAM`
→ konkret startliste på hold-/parniveau

`SECTIONPLAYER`
→ konkrete spillere i sektionen

`ROUND`
→ runder

`ROUNDMATCH`
→ bordopstillinger

`RESULT`
→ resultater

---

## Turneringsafviklingens faser

### Før turneringen

- vedligehold startliste
- angiv startscore
- indlæs kortfordelinger
- udskriv kortfordelinger
- udskriv startdata
- startdata til hjemmeside
- start Bridgemate

### Under turneringen

- hent data fra Bridgemate
- indtast score
- udskriv rundestilling
- rundestilling til hjemmeside

### Efter turneringen

- hent data fra Bridgemate
- udskriv resultater
- resultater til hjemmeside

---

## Tabelkobling for afviklingsskærmen

### Tilmeldinger

- `MAINTOURNAMENTTEAM`
- `MAINTOURNAMENTPLAYER`

### Startliste for konkret sektion

- `SECTIONTEAM`
- `SECTIONPLAYER`

### Kortfordelinger

- `BOARDSPEC`
- `BOARD`

### Rundeopbygning

- `ROUND`
- `ROUNDMATCH`

### Resultatregistrering

- `RESULT`

### Handicap

- `HACROUND`
- `HACROUNDPLAYER`

---

# Vedligehold startliste

Startlisten er den konkrete, ordnede liste over de par eller spillere, der faktisk spiller i en bestemt række eller sektion.

Tilmeldinger til klubturneringen og startliste for en konkret række er to forskellige niveauer.

---

## Tilmeldingsniveau

Turneringens samlede tilmeldinger ligger på turneringsniveau.

### Relevante tabeller

- `MAINTOURNAMENTTEAM`
- `MAINTOURNAMENTPLAYER`

Tilmeldinger kan være:
- par
- hold
- enkeltspillere

---

## Startlisteniveau

Startlisten beskriver de deltagere, som faktisk sættes ind i en bestemt række eller sektion på en bestemt aften.

### Relevante tabeller

- `SECTIONTEAM`
- `SECTIONPLAYER`

---

## Vedligehold startliste

Startlisten vedligeholdes pr. række.

Typiske funktioner:
- vælge fra ledige tilmeldinger
- manuel indtastning
- flytte deltagere ind i startlisten
- flytte deltagere ud af startlisten
- flytte deltagere op/ned
- tilfældig opstilling
- angive oversidder
- angive substitut
- fjerne substitut

---

## Ledige tilmeldinger

Ledige tilmeldinger er deltagere, der er tilmeldt turneringen, men endnu ikke placeret i den aktuelle række.

Når et par flyttes fra ledige tilmeldinger til startlisten, bliver det en del af den konkrete rækkes startliste.

---

## Manuel indtastning

Manuel indtastning kan bruges til at tilføje spillere eller par direkte til en konkret række.

Typiske handlinger:
- indtast medlemsnummer
- find spiller
- søg på navn
- overfør til startliste

Manuel indtastning gør det muligt at supplere en startliste med spillere/par, som ikke allerede findes blandt turneringens tilmeldinger.

---

## Startlistens rækkefølge

Startlisten er rækkefølsom.

Rækkefølgen bruges til:
- placering i skifteplan
- parnummer
- bordplacering
- startposition

### Relevante felter

- `SECTIONTEAM.TEAMNO`
- `SECTIONPLAYER.PAIRNO`
- `SECTIONPLAYER.PLAYERNO`

---

## Oversidder

Oversidder bruges ved ulige deltagerantal eller skifteplaner med oversidder.

Oversidder indgår i den konkrete startliste og kan fremgå af startdata.

---

## Substitut

Substitut kan angives på den konkrete startliste.

### Relevante tabeller

- `SECTIONPLAYER`
- `MAINTOURNAMENTPLAYER`

Substitut i startlisten er et afviklingsniveau-begreb.

---

## Kobling mellem tilmelding og startliste

`SECTIONPLAYER.FKMAINTOURNAMENTPLAYERID` kobler den konkrete deltager i sektionen til turneringens samlede deltager-/tilmeldingsniveau.

### Model

`MAINTOURNAMENTTEAM`
→ `SECTIONTEAM`

`MAINTOURNAMENTPLAYER`
→ `SECTIONPLAYER`

---

# Kortfordelinger og startdata

Kortfordelinger indlæses før turneringen og bruges sammen med startliste, skifteplan og afviklingsdata.

---

## Indlæs kortfordelinger

BridgeCentral kan indlæse kortfordelinger fra filer.

### Understøttede filtyper

- `.bri`
- `.dup`
- `.pbn`

Kortfordelinger indlæses som et trin før turneringen starter.

---

## Samme kortfil for flere rækker

Den samme kortfil kan bruges for to rækker på én gang.

Dette bruges typisk, hvis flere rækker spiller samme aften og bruger samme kortfordelinger.

### Relevante tabeller

- `BOARDSPEC`
- `BOARD`

Kortfilen kan oprette eller opdatere board-sæt og boards for de relevante sektioner/rækker.

---

## BOARDSPEC ved kortfordelinger

`BOARDSPEC` beskriver kortspecifikationen eller board-sættet.

### Relevante felter

- `ID`
- `FKSECTIONID`
- `FILENAME`
- `BOARDSPECNO`
- `NAME`

`FILENAME` indeholder filnavn eller filreference.  
`FKSECTIONID` knytter board-sættet til en konkret sektion.

---

## BOARD ved kortfordelinger

`BOARD` indeholder de enkelte spil/boards fra kortfilen.

### Relevante felter

- `ID`
- `FKBOARDSPECID`
- `BOARDNO`
- `BOARDGROUP`
- `DISTRIBUTION`

`BOARDNO` er spilnummeret.  
`DISTRIBUTION` indeholder kortfordelingen i binær form.

---

## Udskriv startdata

Startdata genereres, når startliste og skifteplan er klar.

Startdata kan indeholde:
- startliste for runde 1
- alle rækker samtidigt
- sideskift mellem rækker
- substitutter
- stilling i turneringen
- bordregnskaber
- bordplancher
- checkliste
- storskærmsinformation

---

## Startliste-rapport

Den genererede startliste kan indeholde:
- klubnavn
- turneringsnavn
- dato/tid
- række
- sektion
- runde
- skifteplan
- sektionsperiode
- gennemsnitligt handicap
- par
- spillere
- handicap
- forventet score
- startplacering
- oversidder

---

## Startplacering

Startplacering viser, hvor et par starter.

Eksempler:
- `1 ØV`
- `2 ØV`
- `4 NS`
- `5 NS`

Startplacering består typisk af:
- startbord
- retning (`NS` eller `ØV`)

### Relevante tabeller

- `ROUND`
- `ROUNDMATCH`
- `SECTIONTEAM`
- `SECTIONPLAYER`
- `MOVEMENTPLAN`
- `MOVEMENTPLANDETAIL`

---

## Handicap i startdata

Startdata kan vise:
- handicap pr. spiller
- gennemsnitligt handicap for rækken
- forventet score i procent

### Relevante felter

- `GROUPTOURNAMENT.CALCULATEHAC`
- `GROUPTOURNAMENT.SHOWHACONHTML`
- `GROUPTOURNAMENT.SHOWHACONPAPER`
- `GROUPTOURNAMENT.GIVEHACPRIZES`

### Relevante tabeller

- `HACROUND`
- `HACROUNDPLAYER`

---

# Indtast score

Indtast score bruges til registrering af konkrete resultater pr. bord og board.

Resultatindtastning foregår i en konkret afviklingskontekst:
- turneringsdag
- spilletidspunkt
- række
- klubturnering
- skifteplan
- runde
- bord
- board

---

## Registreringsfelter

Ved scoreindtastning bruges typisk:

- bord
- spil / boardnummer
- NS-par
- ØV-par
- declarer
- kontrakt
- dobling/redobling
- antal stik
- udspil
- sårbarhed/zone
- aktuelle spillere ved bordet

---

## Eksempel på registrering

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

## Resultatgrid

Resultatgridet viser registrerede resultater og beregninger.

Typiske kolonner:
- spilnummer
- NS
- ØV
- spilfører
- kontrakt
- dobling/redobling
- stik
- udspil
- score NS
- score ØV
- procent NS
- procent ØV
- matchpoint NS
- matchpoint ØV
- bemærkning

---

## RESULT ved scoreindtastning

`RESULT` gemmer det konkrete bordresultat.

### Relevante felter

- `FKMATCHID`
- `FKBOARDID`
- `BOARDNO`
- `BOARDGROUP`
- `BIDDINGSEQUENCE`
- `CONTRACT`
- `LEAD`
- `RESULT`
- `CALCULATEDSCORENS`
- `CALCULATEDSCORENSPCT`
- `CALCULATEDSCOREEW`
- `CALCULATEDSCOREEWPCT`
- `DECLARER`
- `DOUBLING`
- `TRICKS`
- `RESULTCOMPLETED`
- `EXCLUDEGAME`
- `BOARDCOMPARED`

### Mapping

- Kontrakt -> `RESULT.CONTRACT`
- Udspil -> `RESULT.LEAD`
- Declarer -> `RESULT.DECLARER`
- Dobling/redobling -> `RESULT.DOUBLING`
- Stik -> `RESULT.TRICKS`
- Score NS -> `RESULT.CALCULATEDSCORENS`
- Score ØV -> `RESULT.CALCULATEDSCOREEW`
- Procent NS -> `RESULT.CALCULATEDSCORENSPCT`
- Procent ØV -> `RESULT.CALCULATEDSCOREEWPCT`

`RESULT.TRICKS` er antal stik.  
`RESULT.RESULT` er en intern resultatværdi relateret til kontrakten.

---

## Relation til ROUNDMATCH

`ROUNDMATCH` beskriver bordopstillingen i den aktuelle runde.

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
2. Systemet finder det aktuelle bord i `ROUNDMATCH`.
3. Systemet finder hvilke par/spillere der sidder NS/ØV.
4. Det konkrete bordresultat gemmes i `RESULT`.

---

## Relation til ROUND

`ROUND` organiserer resultatindtastning pr. runde.

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

## Relation til BOARD og BOARDSPEC

Resultatindtastning er koblet til det konkrete board.

### Relevante tabeller

- `BOARDSPEC`
- `BOARD`
- `RESULT`

### Relation

- `RESULT.FKBOARDID -> BOARD.ID`

`BOARDSPEC` er knyttet til sektionen.  
`BOARD` indeholder konkret boardnummer og kortfordeling.

---

## Justering

Resultater kan justeres.

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

Justering er en del af resultatbehandlingen.

---

## Afvigelser i afviklingen

Systemet kan markere afvigelser som:
- forkert led / retning
- forkert kortfordeling
- forkerte spillere ved bordet
- forkert spilnummer

### Relevante tabeller

- `ROUNDMATCH`
- `RESULT`
- `SECTIONPLAYER`

---

## Matchpoint og procenter

I parturnering kan `RESULT` indeholde både rå kontraktdata og beregnede scorer.

### Relevante felter

- `CALCULATEDSCORENS`
- `CALCULATEDSCOREEW`
- `CALCULATEDSCORENSPCT`
- `CALCULATEDSCOREEWPCT`

---

## Deltagerniveauer ved scoreindtastning

Scoreindtastning viser både:
- parnummer/teamnummer
- spillernavne

### Relevante tabeller

- `SECTIONTEAM`
- `SECTIONPLAYER`
- `ROUNDMATCH`

---

## Samlet model for scoreindtastning

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