#!/bin/bash

# Git Auto-Commit Script
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

# Alle Änderungen hinzufügen
echo "=== Git Add ==="
git add .

# Commit mit Nachricht
echo "=== Git Commit ==="
read -p "Commit-Nachricht eingeben: " commit_msg

if [ -z "$commit_msg" ]; then
    echo "Keine Commit-Nachricht eingegeben. Verwende Standard-Nachricht."
    git commit -m "Automatischer Commit vom $(date '+%Y-%m-%d %H:%M:%S')"
else
    git commit -m "$commit_msg"
fi

echo ""
echo "=== Fertig ==="
echo "Git add und commit abgeschlossen."

echo "=== Upload ==="
git push

echo ""
echo "Drücke Enter zum Beenden..."
read
