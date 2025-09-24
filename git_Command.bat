@echo off
REM Git Auto-Commit Script

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
echo === Git Add ===
git add .

REM Commit mit Nachricht
echo === Git Commit ===
set /p commit_msg="Commit-Nachricht eingeben: "

if "%commit_msg%"=="" (
    echo Keine Commit-Nachricht eingegeben. Verwende Standard-Nachricht.
    git commit -m "Automatischer Commit vom %date% %time%"
) else (
    git commit -m "%commit_msg%"
)

echo.
echo === Fertig ===
echo Git add und commit abgeschlossen.

echo == Upload ===
git push

pause