#!/bin/bash

# Git Auto-Pull Script

# Prüfen ob Git-Repository vorhanden
if [ ! -d ".git" ]; then
    echo "FEHLER: Kein Git-Repository gefunden!"
    echo "Aktuelles Verzeichnis: $(pwd)"
    exit 1
fi

echo "Aktuelles Verzeichnis: $(pwd)"
echo ""

# Git Status anzeigen
echo "=== Git Status ==="
git status
echo ""

# Pull vom Remote
echo "=== Git Pull ==="
git pull

echo ""
echo "Drücke Enter zum Beenden..."
read
