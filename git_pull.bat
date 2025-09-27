@echo off
REM Git Auto-Pull Script

REM Ins gewünschte Verzeichnis wechseln
cd /d %CD%

REM Prüfen ob wir im richtigen Verzeichnis sind
if not exist ".git" (
    echo FEHLER: Kein Git-Repository gefunden!
    echo Aktuelles Verzeichnis: %CD%
    pause
    exit /b 1
)

echo Aktuelles Verzeichnis: %CD%
echo.

REM Git Status anzeigen
echo === Git Status ===
git status
echo.

REM Alle Änderungen hinzufügen
echo === Git Pull ===
git pull

