import os
import re
import xml.etree.ElementTree as ET
from xml.dom import minidom

def find_xaml_files(root_dir):
    """Trouve tous les fichiers .xaml dans le répertoire et ses sous-répertoires"""
    xaml_files = []
    for root, _, files in os.walk(root_dir):
        for file in files:
            if file.endswith('.xaml'):
                xaml_files.append(os.path.join(root, file))
    return xaml_files

def extract_automation_ids(xaml_file):
    """Extrait tous les AutomationId d'un fichier XAML"""
    automation_ids = set()
    
    try:
        tree = ET.parse(xaml_file)
        root = tree.getroot()
        
        # Namespace par défaut pour MAUI
        namespaces = {
            'x': 'http://schemas.microsoft.com/winfx/2006/xaml',
            '': 'http://schemas.microsoft.com/winfx/2006/xaml/presentation'
        }
        
        # Recherche de tous les éléments avec un AutomationId
        for element in root.findall('.//*[@AutomationId]', namespaces):
            automation_id = element.get('AutomationId')
            if automation_id:
                automation_ids.add(automation_id)
                
        # Recherche alternative pour différentes syntaxes
        for element in root.findall('.//*[@x:Name]', namespaces):
            x_name = element.get('{http://schemas.microsoft.com/winfx/2006/xaml}Name')
            if x_name and x_name.startswith(('automation_', 'auto_', 'test_')):
                automation_ids.add(x_name)
                
    except ET.ParseError as e:
        print(f"Erreur d'analyse du fichier {xaml_file}: {e}")
    
    return automation_ids

def generate_ids_xml(automation_ids, output_file):
    """Génère le fichier ids.xml"""
    # Crée la structure XML
    root = ET.Element('resources')
    
    # Trie les IDs pour une meilleure lisibilité
    for aid in sorted(automation_ids):
        item = ET.SubElement(root, 'item')
        item.set('name', aid)
        item.set('type', 'id')
    
    # Formate le XML pour une belle sortie
    rough_string = ET.tostring(root, 'utf-8')
    reparsed = minidom.parseString(rough_string)
    pretty_xml = reparsed.toprettyxml(indent='    ')
    
    # Enlève les lignes vides supplémentaires
    pretty_xml = '\n'.join(line for line in pretty_xml.split('\n') if line.strip())
    
    # Écrit dans le fichier
    with open(output_file, 'w', encoding='utf-8') as f:
        f.write(pretty_xml)

def main():
    # Configuration
    project_root = input("Entrez le chemin du projet MAUI : ").strip() or '.'
    output_file = os.path.join(project_root, 'Platforms', 'Android', 'Resources', 'values', 'ids.xml')
    
    # Trouve tous les fichiers XAML
    xaml_files = find_xaml_files(project_root)
    if not xaml_files:
        print("Aucun fichier .xaml trouvé.")
        return
    
    # Extrait tous les AutomationId
    all_automation_ids = set()
    for xaml_file in xaml_files:
        automation_ids = extract_automation_ids(xaml_file)
        if automation_ids:
            print(f"Trouvé dans {xaml_file}: {', '.join(automation_ids)}")
            all_automation_ids.update(automation_ids)
    
    if not all_automation_ids:
        print("Aucun AutomationId trouvé dans les fichiers XAML.")
        return
    
    # Crée le répertoire de sortie si nécessaire
    os.makedirs(os.path.dirname(output_file), exist_ok=True)
    
    # Génère le fichier XML
    generate_ids_xml(all_automation_ids, output_file)
    print(f"\nFichier généré avec succès : {output_file}")
    print(f"Nombre total d'AutomationId trouvés : {len(all_automation_ids)}")

if __name__ == '__main__':
    main()