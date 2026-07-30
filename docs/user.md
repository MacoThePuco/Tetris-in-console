# Užívatelská dokumentácia

*Sem napište, jak se váš program používá: jak ho spustit (můžete zkopírovat to, co je v README a případně rozšířit), jak ho ovládat (např. pokud programujete hru, tak jaké klávesy slouží k čemu), případně formát vstupních souborů, ...*

## Inštalácia a Spustenie

Pre spustenie prejdite do zložky `Tetris` a spustite príkaz `dotnet run`. Odporúčam začať program v konzole, ktorá je maximalizovaná na obrazovku a následene veľkosť okna nemeniť.

## Pravidlá hry
Smerom dole padajú bloky (tetraminá), ktoré po spadnutí zostanú na mieste. Po zaplnení celého riadku bloky z tohto riadku zmiznú a riadky nad týmto riadkom sa posunú smerom dolu.

Koniec hry nastáva v moment, kedy je blok poležený mimo plochy.

Hra sa pri každom vymazaní riadkov o trochu zrýchli.

Cieľ je získať čo najviac bodov.


## Bodovanie
Body sa získavajú za:
- Vymazanie riadku (za vymazanie viacerých riadkov naraz sú bonusové body)
- Zrýchlenie padania padajúceho bloku
- Instanté spadnutie padajúceho bloku


## Ovládanie
### V menu
Šípkami hore a dole vyberáte voľbu akcie a pomocou medzerníka/Enteru ju zvolíte.

### V hre
- Šípky vpravo a vľavo posúvajú padajúci blok doprava a doľava.
- Šípka hore otočí blokom.
- Šípka dole zrýchli pád.
- Medzerník instantne položí blok najviac ako sa dá rovno v smere pádu.