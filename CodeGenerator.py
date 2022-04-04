""" Generates code based on templates

Tested on Python 3.9, should work on any recent 3.x

C# template:

/* TEMPLATE_NAME
...
TEMPLATE_NAME */

XML or XAML template:

<!-- TEMPLATE_NAME
...
TEMPLATE_NAME */

Works with copying the lines inside the template before the template
(including any empty lines), then replacing the template names
specified with actual values.

It is an easier solution, then trying to metaprogram all those lines
using C# attributes. It would not be possible in XML or XAML anyway.

"""

import os
import re
import glob

DRY_RUN = False

RX_PUBLIC_NAME = re.compile(r'^([A-Z][a-z_0-9]+)+$')

SEP = os.path.sep
IGNORE = ('.git', '.idea', 'packages', 'bin', 'obj', 'Bin64', 'Torch')


def is_valid_dir(dirpath):
    dir_path_closed = dirpath + SEP
    for fragment in IGNORE:
        if f'{SEP}{fragment}{SEP}' in dirpath:
            return False

    if (glob.glob(os.path.join(dirpath, '*.dll')) or
            glob.glob(os.path.join(dirpath, '*.exe'))):
        return False

    return True


def substitute(path, rx, subs, encoding='utf8'):
    with open(path, 'rt', encoding=encoding) as f:
        text = f.read()

    original = text

    def handler(m):
        t = m.group(1)
        for k, v in subs.items():
            t = t.replace(k, v)
        return t + m.group(0)

    text, n = rx.subn(handler, text)
    if not n:
        return

    if text == original:
        return

    print(path)

    if DRY_RUN:
        return

    with open(path, 'wt', encoding=encoding) as f:
        f.write(text)


def generate_code(**rules_by_ext):

    if not os.path.exists('README.md'):
        raise IOError('This script must be run from the working copy folder')

    def iter_paths():
        for dirpath, dirnames, filenames in os.walk('.'):
            if not is_valid_dir(dirpath):
                continue
            for filename in filenames:
                fileext = filename.rsplit('.')[-1]
                for ext, rules in rules_by_ext.items():
                    if fileext != ext:
                        continue
                    path = os.path.join(dirpath, filename)
                    yield filename, path, rules

    for filename, path, rules in iter_paths():
        for rx, subs in rules:
            substitute(path, rx, subs)


def generate_bool_option(name, command, label, tooltip):
    if not RX_PUBLIC_NAME.match(name):
        raise ValueError(f'Invalid name: {name}')

    subs = {
        'OptionName': name[:1].upper() + name[1:],
        'optionName': name[:1].lower() + name[1:],
        'Option label': label,
        'Option tooltip': tooltip,
        'option_name': command,
    }

    topic = 'BOOL_OPTION'
    rx_cs_comment = re.compile(r'//%s (.*?\n)' % topic)
    rx_cs_multiline_comment = re.compile(r'/\*%s\s*(.*?)%s\*/' % (topic, topic), re.DOTALL)
    rx_xml_comment = re.compile(r'<!--%s\s*(.*?)%s-->' % (topic, topic), re.DOTALL)
    rx_xml_attribute = re.compile(r'{%s}(.*?){/%s}' % (topic, topic))

    generate_code(
        cs=[(rx_cs_comment, subs),
            (rx_cs_multiline_comment, subs)],
        xml=[(rx_xml_comment, subs),
             (rx_xml_attribute, subs)],
        xaml=[(rx_xml_comment, subs),
              (rx_xml_attribute, subs)],
    )


def main():
    pass
    # generate_bool_option('FixEndShoot', 'endshoot', 'Fix crash on grinding active turrets', 'Adds a missing call to EndShoot on server side, fixing subsequent issues on client side')


if __name__ == '__main__':
    main()