
```plantuml

Tarkastaja -left-- (Jarjestelman avaaminen)
Tarkastaja -left- (Tietojen haku)
Tarkastaja -left- (Kohteen valinta listalta)
Tarkastaja -left- (Kohteen poistaminen)
Tarkastaja -left-- (Kohteen lisaaminen)
rectangle toinenlista {
Tarkastaja--- (Kohteen tietojen tarkastelu)
Tarkastaja-- (Valineen valinta listalta)
Tarkastaja-- (Kohteen tietojen muokkaaminen)
}
rectangle kolmaslista {
Tarkastaja -right-- (Valineen lisaaminen)
Tarkastaja -right-- (Valineen poistaminen)
Tarkastaja -right-- (Valineen tietojen tarkastelu)
Tarkastaja -right-- (Vian valinta listalta)
}
rectangle neljaslista {
Tarkastaja -up-- (vian lisaaminen)
Tarkastaja -up- (vian tietojen tarkastelu)
Tarkastaja -up-- (vian poistaminen)
}

```