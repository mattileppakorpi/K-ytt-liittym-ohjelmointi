```plantuml

Tarkastaja--- (Jarjestelman avaaminen)
Tarkastaja--- (Kohteen valinta listalta)
Tarkastaja--- (Kohteen lisaaminen)
rectangle toinen lista {
Tarkastaja--- (Kohteen tietojen tarkastelu)
Tarkastaja--- (Valineen valinta listalta)
Tarkastaja--- (Kohteen tietojen muokkaaminen)
}
rectangle kolmas lista {
Tarkastaja--- (Valineen tietojen tarkastelu)
Tarkastaja--- (Vian valinta listalta)
Tarkastaja---(Valineen tietojen muokkaaminen)
}
rectangle neljas lista {
Tarkastaja--- (vian tietojen tarkastelu)
Tarkastaja--- (vian tietojen muokkaaminen)
}

```