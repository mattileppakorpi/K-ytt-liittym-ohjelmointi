Leikkipaikkatietokanta

[Koodi](https://gitlab.labranet.jamk.fi/N3998/klohjelmointi_harjoitust./-/tree/master/Leikkipaikat)

[Dokumentit](https://gitlab.labranet.jamk.fi/N3998/klohjelmointi_harjoitust./-/tree/master/Dokut)

1. Asennus:
Nugetissa Install-Package LiteDB. http://www.litedb.org/docs/getting-started/

2. Tietoa ohjelmasta:
Käyttöliittymän kautta voidaan lisätä, muokata ja poistaa kohteita leikkipaikkatietokannasta. Tietokannan tallennuspaikan 
käyttäjä voi itse määritellä. Kohteille voidaan lisätä ja poistaa välineitä ja välineille lisätä tai poistaa vikoja. 
-Toteutetut toiminnalliset vaatimukset:
	-Voidaan lisätä ja poistaa kohteita, välineitä ja vikoja, ja lisäksi muokata kohteiden tietoa.
-Toteuttamtta jääneet:
	Olioiden päivittämiselle tai muokkaamiselle olisi ollut hyvä tehdä varmistus käyttäjältä, eli paina Y/N ennen toimenpidettä.
-Yli/ohi vaatimusten tehdyt toiminnallisuudet
-Ei-toiminnalliset vaatimukset:

3. [Kuvat]()
	Hae leikkipaikat-napilla haetaan leikkipaikkatietokanta jos sellainen on olemassa. Jos ei ole, sellainen luodaan ensim-
mäisen kohteen talletuksen yhteydessä. Kohteelle kirjoitetaan uniikki osoite ja mahdollinen info, ja painetaan lisää leikkikenttä.
Poistaa kohteen voi painamalla poista-nappia kun kohde on valittu. Kohteen tietoja voi myös muokata muuttamalla niitä teksikentissä
ja painamalla muokkaa.
	Välineen lisäys toimii samalla logiikalla, nimi pitää olla ja se on uniikki, valmistaja laitetaan jos tiedossa. Ensin
tiedot kenttiin ja sitten lisää-nappia. Poistaminen kun väine on valittuna.
	Ylläolevalla periaatteella toimii myös vian lisäys, eli väline pitää olla valittuna, ja sitten laitetaan uniikki vian nimi,
myös luokka pitää laittaa, ja painetaan lisää vika. Poistaminen kun vika valittuna.

4. Testidata repon tietokanta-kansiossa, josta sen voi ladata minne haluaa ja muuttaa polun oikein käyttöliittymään.
Oletuksena C:\Temp\Mydata. 

5. Ongelmat, bugit ja jatkokehitys:
	Jatkokehitysmahdollisuuksia leikkipaikkatarkastustietokannalle olisi vaikka kuinka. Kohteisiin, välineisiin ja vikoihin
olisi ensinnäkin hyvä liittää kuvat, ja vikoihin voisi lisätä korjausehdotus-kentän. Lisäksi voisi tehdä kentän jonne voitaisiin 
laittaa tarjoushinta vian korjaamisesta ja button josta asiakas voi tilata korjauksen, olettaen että hänellä olisi järjestelmä
myös käytössä.
Toisekseen koko järjestelmä olisi hyvä olla käytettävissä puhelimessa/tabletilla jos sitä oikeasti joskus meinaisi käyttää. 
Ja eri käyttäjätilien mahdollistaminen olisi tottakai tarpeen, mutta ne eivät enää taida olla 
tämän kurssin asioita.

6. Mitä opittu, suurimmat haasteet, mitä kannattaisi opiskella lisää:
	-Olioiden tallentaminen tietokantaan tuli tutuksi, ja muutosten tekeminen olion ominaisuuksiin käyttöliittymän kautta. 
Myös käyttöliittymän ominaisuuksiin ja niiden muokkaamiseen sai hieman perehtyä. Lisäksi joutui miettimään eri mahdollisuuksia
kuinka ohjleman saisi kaatumaan ja joutui niitä koittaa estämään.
	-Haasteita oli matkan varrella useita, mutta niistä pienen googlailun jälkeen ylennsä pääsi etenemään. Oikeanlaisten 
keinojen käyttö olioiden läpikäymiseksi muutoksia tehtäessä tuotti hieman vaikeuksia aluksi. Myös käyttöliittymän 
datagridien päivittyminen tuotti haasteita, jouduin korvaamaan InotifyPropertyChangedin metodilla joka päivittää tiedot. 
Jouduin myös päivittämään olioita, playground-luokalle piti antaa LiteDB:n id-ominaisus,ja tein aluksi viat pelkkinä
stringeinä mutta päivitin myöhemmin fault-luokaksi että sain myös categoryn mukaan.
	-Asioita joita olisi hyvä opiskella lisää on ainakin käyttöliittymän lukemattomat eri ominaisuudet, joista nyt tuli tutuksi
hyvin pieni osa. Lisäksi eri tietokantoihin, kuten MySQL:n tallentamista ja muokkaamista olisi hyvä opetella enemmän. 
Varmasti opettelemisen arvoinen asia joka tullee vielä usein vastaan olisi Entity Frameworkin tms. ORM:n käyttö. Ja 
INotifyPropertyChanged-ominaisuus jäi nyt hieman arvoitukseksi, mutta se olisi varmasti hyvä opetella.

7. Matti Leppäkorpi N3998 TTV19S3

8. Koska arvosteluperusteissa ei lue yksiselitteisesti mitä ohjelman kuuluisi sisältää on arvioiminen vaikeaa, mutta 
toteutus oli mielestäni kohtalaisen haasteellinen ja laaja, se sisältää olioita ja niiden välisiä suhteita, poikkeukset
ja väärät syötteet/painallukset on pyritty huomioimaan. Käytetty tietokanta on melko helppokäyttöinen, mutta valinta on
perusteltu. Luokat ja tietokantayhteydet on tehty omiin tiedostoihinsa erilleen käyttöliittymän koodista.
Työssä on käytetty paljon kurssilla opittuja asioita. Dokumentoinnin olen mielestäni tehnyt hyvin. Näillä perusteilla 
arvioisin harjoitustyön arvosanaksi 4 tai 5, riippuen siitä kuinka vaativaksi ja toisaalta onnistuneeksi työ nähdään.




