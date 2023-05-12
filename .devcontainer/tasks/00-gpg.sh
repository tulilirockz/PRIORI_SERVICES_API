#!/bin/sh
echo 'export GPG_TTY=$(tty)' | tee -a "$HOME/.bashrc" "$HOME/.bash_profile" "$HOME/.profile"
