""" Replaces project GUIDs and renames the solution

Tested on Python 3.9, should work on any recent 3.x

"""

import os
import re
import sys
import uuid

PT_PROJECT_NAME = r'^([A-Z][a-z_0-9]+)+$'
RX_PROJECT_NAME = re.compile(PT_PROJECT_NAME)

PROJECT_NAMES = (
    'ClientPlugin',
    'TorchPlugin',
    'DedicatedPlugin',
    'Shared',
)


def generate_guid():
    return str(uuid.uuid4())


def replace_text_in_file(replacements, path):
    is_project = path.endswith('.sln') or path.endswith('.csproj') or path.endswith('.shproj')
    encoding = 'utf-8-sig' if is_project else 'utf-8'

    with open(path, 'rt', encoding=encoding) as f:
        text = f.read()

    original = text

    for k, v in replacements.items():
        text = text.replace(k, v)

    if text == original:
        return

    with open(path, 'wt', encoding=encoding) as f:
        f.write(text)


def input_plugin_name():
    while 1:
        plugin_name = input('Name of the plugin (in CapitalizedWords format): ')
        if not plugin_name:
            break

        if RX_PROJECT_NAME.match(plugin_name):
            break

        print('Invalid plugin name, it must match regexp: ' + PT_PROJECT_NAME)

    return plugin_name


def main():
    if not os.path.isfile('PluginTemplate.sln'):
        print('Run this script only once from the working copy (solution) folder')
        sys.exit(-1)

    plugin_name = input_plugin_name()
    if not plugin_name:
        return

    torch_guid = generate_guid()
    replacements = {
        'PluginTemplate': 'PluginName',
        'E507FDD0-C983-44A3-BBEE-82856AC4AAE0': generate_guid().upper(),
        '21F45862-D7B3-4AFD-8056-099E713A7C25': generate_guid().upper(),
        '204234CA-79BF-42DE-BCE7-4737BBCC0290': generate_guid().upper(),
        'ba48180c-934c-484c-b502-44c1a855a37c': torch_guid,
        'BA48180C-934C-484C-B502-44C1A855A37C': torch_guid.upper(),
    }

    for project_name in PROJECT_NAMES:
        print(project_name)
        for dirpath, dirnames, filenames in os.walk(project_name):
            for filename in filenames:
                ext = filename.rsplit('.')[-1]
                if ext in ('xml', 'cs', 'sln', 'csproj', 'shproj'):
                    path = os.path.join(dirpath, filename)
                    if '\\obj\\' in path or '\\bin\\' in path:
                        continue
                    print(f'  {filename}')
                    replace_text_in_file(replacements, path)

    os.rename('PluginTemplate.sln', f'{plugin_name}.sln')

    if os.path.isfile('PluginTemplate.sln.DotSettings.user'):
        os.rename('PluginTemplate.sln.DotSettings.user', f'{plugin_name}.sln.DotSettings.user')

    print('Done.')


if __name__ == '__main__':
    main()