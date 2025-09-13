# Styleguide 
 Version 0.1



## Inhaltsverzeuichnis

1. [Einleitung](#1-einleitung)
2. [Sturkturen](#2-strukturen)
3. [Dateitypen und Präfixe](#3-dateitypen-und-präfixe)
4. [Code-Stil](#4-code-stil)
5. [Dokumentation](#5-dokumentation)



# 1. Einleitung

Die Nutzung des Styleguides wird verwendet um eine klare Stuktur und eine Lesbarkeit sicher zustellen.
Auch dient er dazu um Schreibfehler zu verhindern


# 2. Strukturen

Variablen werden in CamelCase geschrieben


## Klassen
    Class CL_CamelCase

Der Klassen-Funktionsblock wird mit einem 'CL_' im Namen betitelt.

## Methode
    M_Camelcase
Methoden werden mit einem 'M_' im Namen betitelt 

## Variablen
    float fCamelCase
Variablen nutzen den dafür vorgesehenen Präfix
      

# 3. Dateitypen und Präfixe
|Dateityp|Päfix|Beispiel|
|--------|-----|--------|
| Ganze Zhalen               | n  | nNumber|
| Fließkommazahlen           | f  | fNumber2|
| Boolean                    | b  | bVariable|
| String                     | s  | sName|
| Konstanten                 | c  | cKonstanteZahl|
| Klassen                    | cl | clAuto|
| Interface                  | i  | iKlasse|
| Strukturen                 | st | stStruktur|
| Enumeration                | e  | eEinheit|
| Liste                      | li | liNamen|
| Delegate                   | dcl| dclNachricht|


# 4. Code-Stil

## Code - Beispiel
      class CL_Beispiel{
        float    fVariable1
        Int      nVariable2
        bool     bVariable3

        CL_Beispiel(){
                if (fVariable1 = nVariable2){
                    bVariable3 = true;
                } 
                else{
                    bVariable3 = false;
                }
            }
          };

# 5. Dokumentation
