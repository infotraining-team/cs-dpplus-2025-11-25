using System;
using System.Collections.Generic;

// 1. Component
// The base abstraction for both Files and Directories.
public abstract class FileSystemItem
{
    protected string Name;

    public FileSystemItem(string name)
    {
        Name = name;
    }

    public abstract void Display(int depth);
}

// 2. Leaf
// Represents end objects. A File cannot have children.
public class File : FileSystemItem
{
    public File(string name) : base(name) { }

    public override void Display(int depth)
    {
        // Display with indentation based on depth
        Console.WriteLine(new string('-', depth) + " File: " + Name);
    }
}

// 3. Composite
// Represents complex components that can have children.
public class Directory : FileSystemItem
{
    // The core of the pattern: A list of the Component type
    private List<FileSystemItem> _children = new List<FileSystemItem>();

    public Directory(string name) : base(name) { }

    public void Add(FileSystemItem item)
    {
        _children.Add(item);
    }

    public void Remove(FileSystemItem item)
    {
        _children.Remove(item);
    }

    public override void Display(int depth)
    {
        // 1. Display self
        Console.WriteLine(new string('-', depth) + " Directory: " + Name);

        // 2. Delegate work to children recursively
        foreach (var child in _children)
        {
            child.Display(depth + 2);
        }
    }
}

// 4. Client Code
public class Program
{
    public static void Main()
    {
        // Create leaves
        var file1 = new File("resume.pdf");
        var file2 = new File("budget.xlsx");
        var file3 = new File("logo.png");

        // Create composite (subdirectory)
        var subDir = new Directory("Images");
        subDir.Add(file3);

        // Create root composite
        var rootDir = new Directory("My Documents");
        rootDir.Add(file1);
        rootDir.Add(file2);
        rootDir.Add(subDir); // Adding a directory to a directory

        // The client treats the root directory just like any other item.
        // It doesn't need to know the complex structure underneath.
        Console.WriteLine("--- File System Scan ---");
        rootDir.Display(1);
    }
}