# Používateľská dokumentácia

## Inštalácia a spustenie

Pre spustenie prejdite do zložky `Tetris` a spustite príkaz `dotnet run`. Odporúčam spustiť program v konzole, ktorá je maximalizovaná na celú obrazovku a následene veľkosť okna nemeniť.

## Pravidlá hry
Smerom nadol padajú bloky (tetrominá), ktoré po spadnutí zostanú na mieste. Po zaplnení celého riadku bloky z tohto riadku zmiznú a riadky nad týmto riadkom sa posunú smerom dolu.

Koniec hry nastáva v momente, kedy sa blok poleží mimo hracej plochy.

Hra sa pri každom vymazaní riadkov o trochu zrýchli.

Cieľ je získať čo najviac bodov.


## Bodovanie
Body sa získavajú za:
- Vymazanie riadku (za vymazanie viacerých riadkov naraz sú bonusové body)
- Zrýchlenie padania padajúceho bloku
- Okamžité spadnutie padajúceho bloku na najnižšiu možnú pozíciu


## Ovládanie
### V menu
Šípkami hore a dole vyberáte akciu a pomocou medzerníka/Enteru ju zvolíte.

### V hre
- Šípky vpravo a vľavo posúvajú padajúci blok doprava a doľava.
- Šípka hore otočí blokom.
- Šípka dole zrýchli pád.
- Medzerník okamžite spustí blok najnižšie, ako sa dá rovno v smere pádu.
