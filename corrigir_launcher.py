#!/usr/bin/env python3
"""Launcher: o retry (loadfile) usava rbxasset://, que o loadfile nao entende; passa a usar o caminho real do script.

Funciona com quebras de linha do Windows (CRLF) ou Linux (LF), e pode rodar mais de uma vez.
Uso (dentro da pasta do repositorio RobloxServerLauncher):   python corrigir_launcher.py
"""
import os, sys

E = []
E.append(('RobloxPlayerLauncher/GameStarter.cs', '            string retry = Regex.Replace(args, @"dofile\\(\'([^\']*)\'\\)", "loadfile(\'$1\')()");\n', '            // loadfile() takes a plain file path (not rbxasset://), and in a Lua string a backslash starts an\n            // escape: pass the real path of the generated script with forward slashes.\n            string scriptFile = scriptPath.Replace(\'\\\\\', \'/\');\n            string retry = Regex.Replace(args, @"dofile\\(\'([^\']*)\'\\)", m => "loadfile(\'" + scriptFile + "\')()");\n'))

root = sys.argv[1] if len(sys.argv) > 1 else '.'
cache, problemas, feitos = {}, [], 0
for path, old, new in E:
    full = os.path.join(root, path)
    if not os.path.isfile(full):
        problemas.append('arquivo nao encontrado: ' + path + ' (rode dentro da pasta do repositorio)')
        continue
    if full not in cache:
        raw = open(full, 'rb').read().decode('utf-8')
        cache[full] = [raw, '\r\n' if '\r\n' in raw else '\n', False]
    raw, eol, _ = cache[full]
    o, n = old.replace('\n', eol), new.replace('\n', eol)
    if n in raw:
        continue                      # ja aplicado
    if raw.count(o) != 1:
        problemas.append(f'nao achei o trecho em {path}: {old.strip().splitlines()[0][:70]}')
        continue
    cache[full][0] = raw.replace(o, n, 1)
    cache[full][2] = True
    feitos += 1
for full, (raw, eol, changed) in cache.items():
    if changed:
        open(full, 'wb').write(raw.encode('utf-8'))
print(f'{feitos} alteracoes aplicadas.')
if problemas:
    print('PROBLEMAS:')
    for p in problemas:
        print('  -', p)
    sys.exit(1)
print('Tudo certo.')
