# Packaging FSharp

NuGet.exe sometimes behaves strange when packaging FSharp applications or libraries. This page describes some of the common errors and workarounds.

## Parameter path cannot have zero length

When running ```NuGet.exe pack```, the following error occurs:

    Attempting to build package from 'Project.fsproj'.
    The value "" of the "Project" attribute in element <Import> is invalid. Parameter "path" cannot have zero length.

This error occurs when NuGet can not find the FSharp tools path, regardless whether the tools are installed or not. A workaround is to add a ```<PropertyGroup>``` element in the _.fsproj_ file:

    <PropertyGroup>
      <VisualStudioVersion Condition=" '$(VisualStudioVersion)' == '' ">12.0</VisualStudioVersion>
    </PropertyGroup>

Note this element should be added right before:

    <PropertyGroup>
      <MinimumVisualStudioVersion Condition="'$(MinimumVisualStudioVersion)' == ''">11</MinimumVisualStudioVersion>
    </PropertyGroup>

Using this approach, the F# project system is led to use the correct path to find all required tools.