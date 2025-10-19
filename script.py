import os
import shutil
import subprocess
import re
from prompt_toolkit import PromptSession
from prompt_toolkit.key_binding import KeyBindings
from prompt_toolkit.formatted_text import HTML
from prompt_toolkit.styles import Style
from prompt_toolkit.completion import PathCompleter

def is_text_file(file_path):
    # Common text file extensions for .NET projects
    text_extensions = ('.cs', '.csproj', '.sln', '.json', '.xml', '.config', '.txt', '.md', '.yaml', '.yml', '.props', '.targets')
    return file_path.lower().endswith(text_extensions)

def smart_replace(content, old_prefix, new_prefix):
    # Handle PascalCase, camelCase, lowerCase, and upperCase with simple string replace
    # This allows replacing prefixes/suffixes/infixes in compound words
    old_pascal = old_prefix
    new_pascal = new_prefix
    old_camel = old_pascal[0].lower() + old_pascal[1:] if old_pascal else ''
    new_camel = new_pascal[0].lower() + new_pascal[1:] if new_pascal else ''
    old_lower = old_pascal.lower()
    new_lower = new_pascal.lower()
    old_upper = old_pascal.upper()
    new_upper = new_pascal.upper()
    
    replacements_made = 0
    new_content = content
    for old, new in [(old_pascal, new_pascal), (old_camel, new_camel), (old_lower, new_lower), (old_upper, new_upper)]:
        if old:
            count = new_content.count(old)
            replacements_made += count
            new_content = new_content.replace(old, new)
    
    return new_content, replacements_made

def replace_in_files_and_rename(template_dir, new_dir, old_prefix, new_prefix, build_after):
    # Copy template directory
    shutil.copytree(template_dir, new_dir)
    print(f"Copied template from '{template_dir}' to '{new_dir}'.")

    total_replacements = 0
    files_checked = 0

    # First, rename files and directories (topdown=False for dirs after files)
    for root, dirs, files in os.walk(new_dir, topdown=False):
        # Rename files
        for name in files:
            old_file_path = os.path.join(root, name)
            new_name, _ = smart_replace(name, old_prefix, new_prefix)
            new_file_path = os.path.join(root, new_name)
            if new_name != name:
                os.rename(old_file_path, new_file_path)
                print(f"Renamed file: '{old_file_path}' -> '{new_file_path}'")
        
        # Rename directories
        for name in dirs:
            old_dir_path = os.path.join(root, name)
            new_name, _ = smart_replace(name, old_prefix, new_prefix)
            new_dir_path = os.path.join(root, new_name)
            if new_name != name:
                os.rename(old_dir_path, new_dir_path)
                print(f"Renamed directory: '{old_dir_path}' -> '{new_dir_path}'")

    # Now, replace content in files after renaming
    for root, dirs, files in os.walk(new_dir):
        for name in files:
            file_path = os.path.join(root, name)
            files_checked += 1
            if is_text_file(file_path):
                with open(file_path, 'r', encoding='utf-8') as f:
                    content = f.read()
                new_content, reps = smart_replace(content, old_prefix, new_prefix)
                total_replacements += reps
                if reps > 0:
                    with open(file_path, 'w', encoding='utf-8') as f:
                        f.write(new_content)
                    print(f"Updated content in '{file_path}' ({reps} replacements)")

    print(f"\nSummary: Checked {files_checked} files, made {total_replacements} replacements.")
    if total_replacements == 0:
        print("Warning: No replacements were made. Check if the old prefix is correct.")

    # Restore and build if requested
    if build_after:
        try:
            print("\nRunning 'dotnet restore'...")
            restore_result = subprocess.run(['dotnet', 'restore'], cwd=new_dir, capture_output=True, text=True)
            if restore_result.returncode == 0:
                print("'dotnet restore' succeeded.")
            else:
                print(f"'dotnet restore' failed:\n{restore_result.stderr}")
                return

            print("Running 'dotnet build'...")
            build_result = subprocess.run(['dotnet', 'build'], cwd=new_dir, capture_output=True, text=True)
            if build_result.returncode == 0:
                print("'dotnet build' succeeded! The project should compile correctly.")
            else:
                print(f"'dotnet build' failed:\n{build_result.stderr}")
        except FileNotFoundError:
            print("Error: 'dotnet' command not found. Ensure .NET SDK is installed and in your PATH.")

def select_option(prompt, options):
    bindings = KeyBindings()
    selected_index = [0]  # Store selected index in a list to modify in closure

    @bindings.add('up')
    def _(event):
        selected_index[0] = (selected_index[0] - 1) % len(options)

    @bindings.add('down')
    def _(event):
        selected_index[0] = (selected_index[0] + 1) % len(options)

    @bindings.add('enter')
    def _(event):
        event.app.exit(result=options[selected_index[0]])

    style = Style([
        ('selected', 'bg:ansiblue fg:ansiwhite bold'),
        ('unselected', ''),
    ])

    session = PromptSession(multiline=False, key_bindings=bindings)
    while True:
        print("\n" + prompt)
        for i, option in enumerate(options):
            marker = '>' if i == selected_index[0] else ' '
            style_class = 'selected' if i == selected_index[0] else 'unselected'
            print(HTML(f'<{style_class}>{marker} {option}</{style_class}>'))
        result = session.prompt("")
        if result in options:
            return result

def main():
    print("Enhanced Universal CQRS Project Cloner Script with Interactive CLI")
    print("This script intelligently copies a template CQRS project, renames files/directories first, then smartly replaces entity references in code and names.")
    print("It handles different cases (Pascal, camel, lower, upper) with substring matching to handle prefixes/suffixes/infixes in compound names.")
    print("Warning: Simple substring replacement may cause unintended changes if the old entity name is part of other words. Choose unique entity names.")
    print("Use arrow keys to navigate and Enter to select. Use PascalCase for entity names (e.g., 'Product').\n")

    # Path completer for folder selection
    path_completer = PathCompleter(expanduser=True, only_directories=True)

    # Select template directory
    template_session = PromptSession(
        "Enter or paste the path to the main/template project folder: ",
        completer=path_completer
    )
    template_dir = template_session.prompt().strip()
    while not os.path.exists(template_dir):
        print(f"Error: Directory '{template_dir}' does not exist.")
        template_dir = template_session.prompt("Try again: ").strip()

    # Select new project directory
    new_dir_session = PromptSession(
        "Enter the path for the new project folder (will be created): ",
        completer=path_completer
    )
    new_dir = new_dir_session.prompt().strip()
    while os.path.exists(new_dir):
        print(f"Error: Directory '{new_dir}' already exists.")
        new_dir = new_dir_session.prompt("Try a different path: ").strip()

    # Input entity names
    old_prefix_session = PromptSession("Enter the old entity name (e.g., 'Product'): ")
    old_prefix = old_prefix_session.prompt().strip()
    while not old_prefix:
        print("Error: Entity name cannot be empty.")
        old_prefix = old_prefix_session.prompt("Try again: ").strip()

    new_prefix_session = PromptSession("Enter the new entity name (e.g., 'Order'): ")
    new_prefix = new_prefix_session.prompt().strip()
    while not new_prefix:
        print("Error: Entity name cannot be empty.")
        new_prefix = new_prefix_session.prompt("Try again: ").strip()

    # Confirm build option with buttons
    build = select_option("Build the project after cloning to verify compilation?", ["Yes", "No"]) == "Yes"

    # Confirm all inputs
    print("\nPlease confirm the following settings:")
    print(f"Template Folder: {template_dir}")
    print(f"New Project Folder: {new_dir}")
    print(f"Old Entity Name: {old_prefix}")
    print(f"New Entity Name: {new_prefix}")
    print(f"Build After Cloning: {'Yes' if build else 'No'}")
    confirm = select_option("Proceed with these settings?", ["Confirm", "Cancel"])
    if confirm != "Confirm":
        print("Operation cancelled.")
        return

    try:
        replace_in_files_and_rename(template_dir, new_dir, old_prefix, new_prefix, build)
        print("\nOperation completed successfully!")
    except Exception as e:
        print(f"An error occurred: {str(e)}")
        if os.path.exists(new_dir):
            shutil.rmtree(new_dir)
            print(f"Cleaned up incomplete new directory '{new_dir}'.")

if __name__ == "__main__":
    main()
