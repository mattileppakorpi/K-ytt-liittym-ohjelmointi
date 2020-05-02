
###Tekijä:
* Matti Leppäkorpi N3998, 1. vuoden IT-opiskelija Jyväskylän Ammattikorkeakoulusta, ryhmä TTV19S3

**Sovelluksen yleiskuvaus:**

* Sovellus on tarkoitettu leikkipaikkojen turvatarkastuksien tietojen hallintaan. Sovelluksella voi lisätä kohteita, kohteisiin
leikkivälineitä ja välineille vikoja. Tietoja pitää myös pystyä poistamaan ja muokkaamaan.

**Kenelle sovellus on suunnattu:**

* Sovellus on tarkoitettu leikkipaikkoja tarkastavien henkilöiden käyttöön tietojen hallinnoimiseksi.

**Käyttöympäristö ja käytetyt teknologiat:**
* Sovellusta käytetään Windowsissa.

**Toiminnot:**
* Kohteiden ja niiden tietojen tarkastelu, muokkaaminen, lisääminen ja poistaminen.
* Välineiden tarkastelu, lisääminen ja poistaminen.
* Vikojen tarkastelu, lisäämien ja poistaminen.

* Käsitteitä ovat kohde, eli siis leikkialue, sen sisältämät leikkivälineet, ja leikkivälineiden sisältämt viat. Yhdellä
kohteella voi olla yksi tai monta leikkivälinettä, ja joka leikkivälineessä ei yhtään, yksi tai useampi vika. 
Nämä esitetään listoina.

* Käyttötapauskaavio alla:

```plantuml

Tarkastaja -left-- (Jarjestelman avaaminen)
Tarkastaja -left- (Kohteen valinta listalta)
Tarkastaja -left-- (Kohteen lisaaminen)
rectangle toinenlista {
Tarkastaja--- (Kohteen tietojen tarkastelu)
Tarkastaja-- (Valineen valinta listalta)
Tarkastaja-- (Kohteen tietojen muokkaaminen)
}
rectangle kolmaslista {
Tarkastaja -right-- (Valineen lisaaminen)
Tarkastaja -right-- (Valineen tietojen tarkastelu)
Tarkastaja -right-- (Vian valinta listalta)
Tarkastaja -right-- (Valineen tietojen muokkaaminen)
}
rectangle neljaslista {
Tarkastaja--- (vian lisaaminen)
Tarkastaja--- (vian tietojen tarkastelu)
Tarkastaja--- (vian tietojen muokkaaminen)
}

```