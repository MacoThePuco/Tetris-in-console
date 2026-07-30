# Programátorská dokumentace

Beh programu beži na jednom z dvoch stavov: 
- Menu
- Hra

Vždy sa objekty odvolávajú na Tiskárnu, ktorá prepíše časť obrazovky

# Program.cs
Tu sa nachádza class Program, Utils a Position
## Class Program
Má v sebe funkciu Main(), kde sa začína program. V nej len nastavíme Tiskárnu a čo sa má stať v prípade, že uživateľ stlačí Ctrl-C. Následne nás dostane rovno do Hlavného menu.

Tatkiež má v sebe funkciu Exit(), ktorá vymaže hru a zmení anstavenia konzole na východzie.

## Class Utils
Má v sebe random a stopwatch, ktoré sa využívaju naprieč celým programom

## Class Positon
Pomáha s výpočtom pozícií naprieč programom. Má dva konštruktory, buď z inej už existujúcej pozície alebo z dvoch hodnôt

Taktiež má funkciu AddPosition() ktorá vráti novú pozíciu. Táto funkcia buď beria ako argumenty inú pozíciu alebo dve hodnoty y a x.

Ešte má funkciu IsOutOfBounds() ktorá vráti true alebo false podľa toho, či je mimo plochy.

# Constants.cs
Má v sebe len class Constants v ktorej sú zapísané konštanty používané v celom programe

# Menu.cs
Má v sebe len classu MenuManager. Tá sa stará o správne fungovanie menu.

Funkcia StartMenuFuncionality() Začne bežať hlavnú smyčky pre menu, kde číta stlačenia klávesov pre hýbanie kurzoru a vykonanie akcie.

Pri stlačení šípok spustí MovePointer() ktorá pohne kurzorom a pri stlačení Space/Enter vykoná akciu, na ktorú máme práve ukázanú.

## Credits()
Vyíše credits zadedinované v konštantách, ktoré postupne posúva obrazovkou hore, a reaguje na stlačenie Space/Enter na preskočenie credits. Po prejdení alebo preskočení credits sa vráti naspäť na hlavné menu

# Game.cs
Tu sa nachádza celá herná logika hry.

## Class Game
Je statická class.

Má v sebe funkciu StartGame() ktorá pripravý celú hru, taktiež ju vyresetuje ak hráme znova, a zapne ju. Na pripravenie používa rôzne funkcie Setup...() a ResetGameVariables(). Po pripravení zavolá funkciu Play().

### Play()
V tejto funkcii sa nachádza hlavný game-loop, ktorý sa skončí práve keď prehráme. V hlavnom loope len zistí, aká klávea je práve stlačená, skúsi pohnút s padajúcim blokom a následne počká, kým má vykonať ďaľší frame a potom to celé zopakuje. 

Po prehratí zavolá funkciu Loose(), ktorá nás presunie do LooseScreenMenu.

## Class Score
Je statická classa, ktorá si ukladá skóre. Má funkcie AddPoints(), ak chceme pridať body priamo alebo LinesCleared(), kde nechávame výpočet bodov na classu podľa počtu vymazaných riadkov.

## Class Board
Je statická classa, ktorá sa stará o hernú plochu.

V každom bode si pamätá padajúci blok a blok, ktorý najsleduje. Pri vytvorení vygeneruje tieto dva bloky pomocou BlockFactory.

Taktiež si pamätá ako vyzerá plocha dvojdimenzionálnym arrayom. Na mieste kde sa žiaden blok nenachádza je -1 a tam kde je je číslo jeho farby.

### SpawnBlock()
Nastaví padajúci blok na nasledujúci a vygeneruje nový nasledujúci. Taktiež zavolá Tiskárnu aby prekreslila panel s ďaľším blokom.

### StopFallingBlock()
Najprv skontroluje, či blok nepokladáme mimo plochy, a ak áno tak nastaví Game.lost na true, čím sa ukončí game-loop. Toto je jediné miesto kde sa môže ukončiť hra.

Ak ho pokladáme na validné miesto, zavoláme SpawnBlock() a CheckAndDeleteRows() a povieme Tiskárne aby vykreslila plochu znova.

### CheckAndDeleteRows()
Prejde cez všetky riadky a skontroluje, či sú plné. Ak áno, vymaže ich a na konci zavolá Score.LinesCleared. Tú volá aj v prípade, že sme nevymazali žiaden riadok, ale do nej pošle hodnotu 0, ktorá nepridá žiadne body do skóre.  

To, či sú riadky plné kontrolujeme odvrchu plochy, aby sa nám nestalo, že posunieme o jedno dole plný riadok a potom ho už neskontrolujeme.

### RowFull()
Skontroluje či je riadok na ploche plný, a podla toho vráti true alebo false.

### DeleteRow()
Vymaže riadok ktroý dostal v argumetoch a následne všetky riadky nad ním posunie o 1 dole a najvyšší nastaví na prázdny.

### BlockWillOverlap
Má dve možnosti pohybu:
- normálny pohyb
- rotácia

Do argumentu môžeme dať jeden z nich a funkcia zistí, či sa bude blok pretínať už s nejakými spadnutými blokmi, ak na ňom vykonáme tento pohyb.

### CheckOverlap
Pre určitú pozíciu len zistí, či sa na nej nachádza plné alebo voľné políčko na ploche. Vráti true ak políčko je prázdne

## Class BlockFactory
Je classa určená na generovanie nových blokov. 

Na začiatku si vytvorí zoznam všetkých konštruktorov rôznych blokov, a následne z neho náhodne vyberá. Keď už použila všetky konštruktory, do zoznamu naspäť všetky vráti a potom vyberá znova.

## Class Block
Od tejto classy sú odvodené classy jednotlivých blokov

Každý blok má svoje časti (4 kocky), pozíciu, rotáciu a farbu

Blok má konštruktor ktorú používa plocha, a vtedy sa vyutvorí v strede nad plochou. Alebo má konštruktor z iného bloku kde len okopíruje jeho vlastnosti.

### Fall() 
Táto funkcia sa volá až keď sme si istý, že môžeme blok posunúť dole. 

### ShouldStop()
Vráti true, ak by blok po posunutí o políčko smerom dole kolidoval s nejakým už spadnutým blokom.

### GetBlockPositions()
Má tri rôzne tvary:
- Ak do argumentov nedáme nič, vráti kde sa časti bloku nachádzajú aktuálne
- Ak do argumentov dáme pozíciu, vráti, kde by sa časti bloku nachádzali, ak by bol na tejto pozícii
- Ak do argumentov dáme rotáciu, vráti, kde by sa časti bloku nachádzali, ak by mal blok túto rotáciu
Na zistenie pozícií používaju GetBlockPositionsBase()

### GetBlockPositionsBase()
Pre daný blok, rotáciu a pozíciu zistí pozície častí bloku tak, že prejde cez všetky pozície častí v určitej rotácií a položí ich na novú pozíciu.

### MoveSideways()
Funkcia volaná z hlavného game-loopu.

Najprv zistíme do ktorej strany sa chceme pohnúť pomocou funkcie GetSidewaysMovement a potom ak týmto pohybom nedostaneme žiadnu časť bloku von z plochy alebo do iného už položeného bloku, tak ho pohneme do tejto strany.

Na toto využivame Board.BlockWillOverlap() a Position.IsOutOfBounds()

### GetSidewaysMovement()
Skontroluje, ktorá šípka do strany bola stlačená a podľa toho vráti 1 ak pravá, -1 ak ľavá a 0 ak žiadna.


### Rotate()
Funkcia volaná z hlavného game-loopu.

Znovu najprv zistíme do ktorej strany chceme rotovať a následne zistíme, či by po vykonaní rotácie žiadna časť bloku nevyskytla mimo obrazovky alebo v inom bloku, podovne ako pri MoveSideways().

Ak sa nám nič také nestalo, otočíme blok

### GetRotation()
Vráti 1 ak je šípka hore stlačená a 0 ak nie.

### MoveDownwards()
Funkcia volaná z hlavného game-loopu.

Ako prvé zistí, či je stlačený medzerník, V takom prípade instante spadne padajúci blok najviack ako sa dá. To spraví tak, že ním posúva dolu až pokým môže. Taktiež sa za instantný pád pridajú body.

Ak nebol medzerník stlačený, tak sa pozrie či má blok padať tento frame. To je vtedy ak časovač nameral od posledného spadnutia viac ako čas medzi spadnutiami daný v konštantách. Taktiež sa táto podmienka preskočí, ak sa drží šípka dolu.

Potom skontroluje či blok už nemá stáť. (Toto sa robí pred pohybom, aby sme sa blokom mohli hýbať do strán ak sa spodkom len dotýka iného bloku, ale ešte na neho nespadol)

Ak nemá stáť, posunie ho nižšie a ak sme nedržali šípku dolu, tak nastavíme nový čas od posledného padnutia.

Ak sme šípku dole držali, pripočítame za to skóre.

### GetGhostBlockPositions()
Vráti všetky pozície častí ghostblocku. To je block, ktorý sa zobrazí na miesto, kam má práve blok spadnúť. To zistíme tak že skúšame časti posúvať dole až dovtedy, dokedy môžeme. Keď už nemôžeme, vrátime ich pozície.

# Blockdefinitions.cs
V tomto súbore sú rozpísane rôzne typy blokov a ich možné rotácie relatívne ku stredovému bloku.

Ten sme vybrali tak, aby sa nám celý blok dobre zobrazoval na ploche pre ďaľší blok.

# LooseScreen.cs
V tomto súbore je len classa LooseScreem ktorá je odvodená od MenuManager, len definuje nové texty a akcie. 

# Tiskarna.cs
Je súbor v ktorom je classa Tiskarna. Táto slúži na vypisovanie všetkých informácií o hre na obrazovku.

Pred Spustením vykresľovania sa spustí Setup(), ktorý vymaže obrazovku, zistí jej stred a schvoá kurzor

Niektoré funkcie majú volitelný argunment scaleWithBlockSize, ktorý robí to, že objekty sa vykreslia toľkokráť širšie, ako je široký jeden blok.

### Clear()
Vymaže obrazovku

### ClearLine()
Vymaže riadok obrazovky o danej šírke.

### SetRelativeCursorPosition()
Nastaví kurzoru novú pozíciu relatívne ku nejakej začiatočnej pozícií

## Vykresľovanie Hry
Začína sa spustením SetupGame() ktorý nastaví hodnoty začiatkov, kde na obrazovke začínajú rôzne elementy.

### Draw()
Najprv vymaže dva riadky nad plochov (pretože tan sa spawnujú nové bloky) a potom najpr vykreslí všetky riadky plochý, potom padajúci blok a následne spod plochy.

### DrawBlock()
Najprv vykreslí jeho ghost block a potom prejde cez jeho pozície a vykreslí časti jeho blocku. Ghost block sa vykresľuje ako prvý preto, lebo časti reálneho bloku chceme mať vykreslené nad ghost blockom.

### DrawGhostBlock()
Vykreslí pre všetky časti ghost blocku textúru zadefinovanú v konštantách. Textúra vyzerá akoby tam znak bol len napoli.

### DrawGameRow()
Vykreslí jeden riadok z plochy, teda najprv bočné mantinely a následne prázdne miesto ak na tej pozícii nič nie je a farbu bloku ak je na tej pozícii nejaký je.

### DrawScore()
Vykreslí obdĺžnik pre skóre a potom do stredu vpíše skóre vyplnené nulami vpredu.

### DrawNextBlockPanel
Vykreslí obdĺžnik pre ďaľší blok a potom doneho vykreslí ďaľší blok.

### DrawRectangle
Vykreslí obdĺžnik s požadovanými dimenziami

## Vykresľovanie Menu
### DrawMenu()
Najprv zistí dĺžku najdlhšieho textu, aby potom vedel text správne centrovať na stred. Potom vykreslí ASCII nadplis a následne vykreslí riadky akcií. Ak je riadok vybratý, zafarbí ho farbou v určitých intervaloch. Tie kontroluje ale ten, čo funkciu volal, takže táto funkcia už len dostane hodnotu, či vybratý riadok má byť zafarbený alebo nie.

### DrawAsciiHeader()
Vykreslí ascii nadplis na x-ový stred obrazovky s nejakým posunutím y. 


## Vykresľovanie obrazovky prehry
Funguje rovnako ako vykresľovanie menu, no predtým sa ešte zavolá funkcia SetupLoseScreen()

### SetupLoseScreen()
Najprv vymaže obrazovku, a potom nastaví začiatky plochy a bočného panelu naľavo a napravo od stredu tak, aby sa medzi plochu a bočný panel zmestilo menu. Potom plochu a menu vykreslí a ďaľej sa už volá len funkcia ako pri menu.

## Vykresľovanie credits
### DrawCredits
Vždy najprv vymaže obrazovku a potom napíše na stred všetky riadky creditov.
